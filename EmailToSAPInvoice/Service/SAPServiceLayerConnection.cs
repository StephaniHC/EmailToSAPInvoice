using DynamicData;
using EmailToSAPInvoice.Models;
using EmailToSAPInvoice.Service.Table;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http; 
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using static EmailToSAPInvoice.Models.Imbox;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace EmailToSAPInvoice.Service
{
    public class SAPServiceLayerConnection
    {
        private HttpClientHandler handler;
        private HttpClient client;
        private SapConfiguration config;
        private DatabaseHandler databaseHandler = new DatabaseHandler();
        public SAPServiceLayerConnection()
        {
            LoadConfiguration();
            if (!config.Certificate)
            {
                handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
            }
            else
            {
                handler = new HttpClientHandler();
            }

            client = new HttpClient(handler);
        }

        private void LoadConfiguration()
        {
            config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build()
                .GetSection("SapConfiguration")
                .Get<SapConfiguration>();
        }

        public async Task ConnectToSAP(List<FacturaBase> listInvoiceXML)
        {
            if (client == null)
            {
                throw new ObjectDisposedException(nameof(client), "El HttpClient ha sido eliminado");
            }
            var timeCancelation = new CancellationTokenSource(TimeSpan.FromMinutes(30));
            try
            {
                var loginInfo = new
                {
                    UserName = config.UserName,
                    Password = config.Password,
                    CompanyDB = config.CompanyDB
                };
                var content = new StringContent(JsonConvert.SerializeObject(loginInfo), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(config.Url + "Login", content, timeCancelation.Token);
                if (response.IsSuccessStatusCode)
                {
                    var session = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                    client.DefaultRequestHeaders.Add("B1S-CaseInsensitive", "true");
                    client.DefaultRequestHeaders.Add("Cookie", $"B1SESSION={session.SessionId}; ROUTEID={session.RouteId};");
                    Console.Write("se conecto" + session);
                    //solo por prueba para insertar factura-----------------
                    var currencyRateJson = new
                    {
                        Currency = "USD",
                        Rate = "6.96",
                        RateDate = "20230519"
                    };
                    string jsonContent1 = JsonConvert.SerializeObject(currencyRateJson);
                    HttpContent contentCurrencyRate = new StringContent(jsonContent1, Encoding.UTF8, "application/json");
                    var response1 = await client.PostAsync(config.Url + "SBOBobService_SetCurrencyRate", contentCurrencyRate, timeCancelation.Token);
                    if (!response1.IsSuccessStatusCode)
                    {
                        var errorResponse = await response1.Content.ReadAsStringAsync();
                        var errorJson = JObject.Parse(errorResponse);
                        var detailedErrorMessage = errorJson["error"]["message"]["value"].ToString();
                        Console.WriteLine($"Error al actualizar la tasa de cambio: {detailedErrorMessage}");
                    }
                    else
                    {
                        Console.WriteLine("Tasa de cambio actualizada correctamente");
                    }
                    //hasta aqui quitar------------------------------
                switch (config.TypeService)
                    {
                        case 1:
                            //await GetCardCodeByTaxId("191040025");
                            await NormalFunction(timeCancelation.Token, listInvoiceXML);
                            break;
                        case 2:
                            await ServiceFunction(timeCancelation.Token, listInvoiceXML);
                            break;
                        case 3:
                            await AccountingEntriesFunction(timeCancelation.Token, listInvoiceXML);
                            break;
                        default:
                            throw new Exception($"TypeService desconocido: {config.TypeService}");
                    }
                }
                else
                {
                    Console.WriteLine($"Error al conectar a SAP: {response.StatusCode}");
                }
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"La operación fue cancelada porque excedió el límite de tiempo: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Excepción al conectar a SAP: {e.Message}");
            }
        }
        private async Task NormalFunction(CancellationToken cancellationToken, List<FacturaBase> listInvoiceXML)
        {
            foreach (var factura in listInvoiceXML)
            {
                try
                {
                    (bool isBusinessPartnerExists, string cardCode) = await GetCardCodeByTaxId(factura.cabecera.nitEmisor.ToString());
                    if (!isBusinessPartnerExists && config.Provider)
                    {
                        cardCode = await CreateBusinessPartner(factura.cabecera.nitEmisor.ToString(), factura.cabecera.razonSocialEmisor, "S", factura.cabecera.nitEmisor.ToString(), cancellationToken);
                        if (string.IsNullOrEmpty(cardCode))
                        { 
                            databaseHandler.UpdateStatus(factura.identifier, Datas.StatusError, "Error al crear Proveedor en SAP");
                            continue;
                        }
                    }
                    else if (!isBusinessPartnerExists && !config.Provider)
                    {
                        databaseHandler.UpdateStatus(factura.identifier, Datas.StatusError, "No existe Proveedor registrado en SAP");
                        continue;
                    }

                    if (isBusinessPartnerExists || config.Provider)
                    {
                        if (factura is FacturaCompraVenta)
                        {
                            await PostProcessInvoiceItem(factura, cardCode, cancellationToken);
                        }
                        else
                        { 
                            await PostProcessInvoiceService(factura, cardCode, cancellationToken);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Se ha producido un error: {ex.Message}");
                }
            }
        }

        private async Task ServiceFunction(CancellationToken cancellationToken, List<FacturaBase> listInvoiceXML)
        {
            foreach (var factura in listInvoiceXML)
            {
                try
                {
                    (bool isBusinessPartnerExists, string cardCode) = await GetCardCodeByTaxId(factura.cabecera.nitEmisor.ToString());
                    if (!isBusinessPartnerExists && config.Provider)
                    {
                        cardCode = await CreateBusinessPartner(factura.cabecera.nitEmisor.ToString(), factura.cabecera.razonSocialEmisor, "S", factura.cabecera.nitEmisor.ToString(), cancellationToken);
                        if (string.IsNullOrEmpty(cardCode))
                        {
                            databaseHandler.UpdateStatus(factura.identifier, Datas.StatusError, "Error al crear Proveedor en SAP");
                            continue;
                        }
                    }
                    else if (!isBusinessPartnerExists && !config.Provider)
                    {
                        databaseHandler.UpdateStatus(factura.identifier, Datas.StatusError, "No existe Proveedor registrado en SAP");
                        continue;
                    }

                    if (isBusinessPartnerExists || config.Provider)
                    {
                        await PostProcessInvoiceService(factura, cardCode, cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Se ha producido un error: {ex.Message}");
                }
            }
        }
        private async Task AccountingEntriesFunction(CancellationToken cancellationToken, List<FacturaBase> listInvoiceXML)
        {
            foreach (var factura in listInvoiceXML)
            {
                try
                {
                    (bool isBusinessPartnerExists, string cardCode) = await GetCardCodeByTaxId(factura.cabecera.nitEmisor.ToString());
                    if (!isBusinessPartnerExists && config.Provider)
                    {
                        cardCode = await CreateBusinessPartner(factura.cabecera.nitEmisor.ToString(), factura.cabecera.razonSocialEmisor, "S", factura.cabecera.nitEmisor.ToString(), cancellationToken);
                        if (string.IsNullOrEmpty(cardCode))
                        {
                            databaseHandler.UpdateStatus(factura.identifier, Datas.StatusError, "Error al crear Proveedor en SAP");
                            continue;
                        }
                    }
                    else if (!isBusinessPartnerExists && !config.Provider)
                    {
                        databaseHandler.UpdateStatus(factura.identifier, Datas.StatusError, "No existe Proveedor registrado en SAP");
                        continue;
                    }

                    if (isBusinessPartnerExists || config.Provider)
                    {
                        var invoiceLines = factura.detalle?.Select(d => d == null ? null : new
                        {
                            AccountCode = config.SAPAccountCodeCredito,
                            Credit = 100,
                            Debit = 0
                            //ShortName = cardCode
                        }).ToList(); 

                        invoiceLines.Add(new
                        {
                            AccountCode = config.SAPAccountCodeDebito,
                            Credit = 0,
                            Debit = 100
                           // ShortName = cardCode
                        });

                        var invoiceJson = new
                        {
                            JournalEntryLines = invoiceLines
                        };

                        string jsonContent = JsonConvert.SerializeObject(invoiceJson);
                        Console.Write("esto serializo: " + jsonContent + "\n");
                        HttpContent contentInvoice = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                        var insertResponse = await client.PostAsync(config.Url + "JournalEntries", contentInvoice, cancellationToken);
                        if (insertResponse.IsSuccessStatusCode)
                        {
                            Console.WriteLine($"Se insertó asiento: {insertResponse.StatusCode}");
                            databaseHandler.UpdateStatus(factura.identifier, Datas.StatusProcessed, $"Se insertó el Asiento: {insertResponse.StatusCode}");
                        }
                        else
                        {
                            var errorResponse = await insertResponse.Content.ReadAsStringAsync();
                            var errorJson = JObject.Parse(errorResponse);
                            var detailedErrorMessage = errorJson["error"]["message"]["value"].ToString();
                            databaseHandler.UpdateStatus(factura.identifier, Datas.StatusPending, detailedErrorMessage);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Se ha producido un error: {ex.Message}");
                }
            }
        }
        private async Task PostProcessInvoiceItem(FacturaBase factura, string cardCode, CancellationToken cancellationToken)
        {
            try
            {
                var invoiceJson = new
                {
                    CardCode = cardCode,
                    //DocDate = factura.cabecera.fechaEmision.ToString("yyyy-MM-dd"), //Descomentar considerar el tipo de cambio en la fecha para insertar factura
                    DocType = "I",
                    DocumentLines = factura.detalle?.Select(d => d == null ? null : new
                    {
                        ItemCode = d.codigoProducto.ToString(),
                        Quantity = ((XmlNode[])d.cantidad)[0]?.InnerText,
                        TaxCode = "IVA",
                        UnitPrice = d.precioUnitario.ToString(CultureInfo.InvariantCulture),
                        PriceAfterVAT = d.subTotal.ToString(CultureInfo.InvariantCulture),
                        WarehouseCode = config.SAPWarehouseCode
                    }).ToList()
                };
                string jsonContent = JsonConvert.SerializeObject(invoiceJson);
                Console.Write("esto serializo: " + jsonContent + "\n");
                HttpContent contentInvoice = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var insertResponse = await client.PostAsync(config.Url + "PurchaseInvoices", contentInvoice, cancellationToken);
                if (insertResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Se insertó factura: {insertResponse.StatusCode}");
                    databaseHandler.UpdateStatus(factura.identifier, Datas.StatusProcessed, $"Se insertó factura Item: {insertResponse.StatusCode}");
                }
                else
                { 
                    var errorResponse = await insertResponse.Content.ReadAsStringAsync();
                    var errorJson = JObject.Parse(errorResponse);
                    var detailedErrorMessage = errorJson["error"]["message"]["value"].ToString();
                    databaseHandler.UpdateStatus(factura.identifier, Datas.StatusPending, detailedErrorMessage);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Excepción al intentar insertar la factura: {e.Message}");
            }
        }
        private async Task PostProcessInvoiceService(FacturaBase factura, string cardCode, CancellationToken cancellationToken)
        {
            try
            {
                var invoiceJson = new
                {
                    CardCode = cardCode,
                    //DocDate = factura.cabecera.fechaEmision.ToString("yyyy-MM-dd"), //Descomentar considerar el tipo de cambio en la fecha para insertar factura
                    DocType = "S",
                    DocumentLines = factura.detalle?.Select(d => d == null ? null : new
                    {
                        ItemDescription = d.descripcion,
                        TaxCode = "IVA",
                        UnitPrice = d.precioUnitario.ToString(CultureInfo.InvariantCulture),
                        PriceAfterVAT = d.subTotal.ToString(CultureInfo.InvariantCulture), 
                        AccountCode = config.SAPAccountCode
                    }).ToList()
                };
                string jsonContent = JsonConvert.SerializeObject(invoiceJson);
                Console.Write("esto serializo: " + jsonContent + "\n");
                HttpContent contentInvoice = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var insertResponse = await client.PostAsync(config.Url + "PurchaseInvoices", contentInvoice, cancellationToken);
                if (insertResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Se insertó factura: {insertResponse.StatusCode}");
                    databaseHandler.UpdateStatus(factura.identifier, Datas.StatusProcessed, $"Se insertó factura Service: {insertResponse.StatusCode}");
                }
                else
                {
                    var errorResponse = await insertResponse.Content.ReadAsStringAsync();
                    var errorJson = JObject.Parse(errorResponse);
                    var detailedErrorMessage = errorJson["error"]["message"]["value"].ToString();
                    databaseHandler.UpdateStatus(factura.identifier, Datas.StatusPending, detailedErrorMessage);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Excepción al intentar insertar la factura: {e.Message}");
            }
        }
        private async Task<(bool, string)> GetCardCodeByTaxId(string taxId)
        {
            var response = await client.GetAsync(config.Url + "BusinessPartners?$filter=FederalTaxID eq '" + taxId + "'");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var businessPartners = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                if (businessPartners.value.Count > 0)
                {
                    var cardCode = businessPartners.value[0].CardCode;
                    Console.WriteLine($"CardCode para el NIT {taxId} es {cardCode}");
                    return (true, cardCode);
                }
                else
                {
                    Console.WriteLine($"No se encontró un socio comercial con el NIT {taxId}");
                    return (false, string.Empty);
                }
            }
            else
            {
                Console.WriteLine($"Error al recuperar el socio comercial: {response.StatusCode}");
                return (false, string.Empty);
            }
        }
        public async Task<string> CreateBusinessPartner(string cardCode, string cardName, string cardType, string FederalTaxID, CancellationToken cancellationToken)
        {
             var businessPartner = new
             {
                CardCode = cardCode,
                CardName = cardName,
                CardType = cardType,
                FederalTaxID = FederalTaxID
             };
             var json = JsonConvert.SerializeObject(businessPartner);
             var data = new StringContent(json, Encoding.UTF8, "application/json");
             var response = await client.PostAsync(config.Url + "BusinessPartners", data, cancellationToken);
             if (response.IsSuccessStatusCode)
             {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<dynamic>(responseJson);
                    var createdCardCode = responseObject?.CardCode?.Value as string;
                    Console.WriteLine($"Socio comercial {createdCardCode} creado exitosamente");
                    return createdCardCode;
             }
             else
             {
                    Console.WriteLine($"Error al crear socio comercial: {response.StatusCode}");
                    return string.Empty;
             }
        }

        //pruebas
        private async Task PruebaProvider()
        {
            var carCode = "R000003";
            var getResponse = await client.GetAsync(config.Url + "BusinessPartners" + $"('{carCode}')");
            Console.Write("Este dato: " + getResponse);
            if (getResponse.IsSuccessStatusCode)
            {
                var responseContent = await getResponse.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(responseContent))
                {
                    Console.WriteLine("La respuesta contiene datos.");
                }
                else
                {
                    Console.WriteLine("La respuesta no contiene datos.");
                }
            }
            else
            {
                Console.WriteLine($"La solicitud falló con el estado: {getResponse.StatusCode}");
            }
        }

        public void Dispose()
        {
            handler?.Dispose();
            client?.Dispose();
        }
    }
} 
