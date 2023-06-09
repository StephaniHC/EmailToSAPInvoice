﻿using Avalonia.X11;
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
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static EmailToSAPInvoice.Models.Imbox;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
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

        /*public async Task ConnectToSAP(List<FacturaBase> listInvoiceXML)
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
                        RateDate = "20230522"
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
        }*/
        public async Task<CancellationToken> ConnectToSAP()
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
                        RateDate = "20230522"
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
                    return timeCancelation.Token;
                }
                else
                {
                    Console.WriteLine($"Error al conectar a SAP: {response.StatusCode}");
                    return new CancellationToken(true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al conectar a SAP: {ex.Message}", ex);
                return new CancellationToken(true);
            }
        }
        public async Task PostInvoiceToSAP(List<FacturaBase> listInvoiceXML)
        {
            Console.WriteLine("ingreso al post");
            try
            {
                CancellationToken cancellationToken = await ConnectToSAP(); // Llama a ConnectToSAP y obtiene el token de cancelación
                Console.WriteLine("Salio por aqui");
                switch (config.TypeService)
                {
                    case 1:
                        await NormalFunction(cancellationToken, listInvoiceXML);
                        break;
                    case 2:
                        await ServiceFunction(cancellationToken, listInvoiceXML);
                        break;
                    case 3:
                        await AccountingEntriesFunction(cancellationToken, listInvoiceXML);
                        break;
                    default:
                        throw new Exception($"TypeService desconocido: {config.TypeService}");
                }
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"La operación de reprocesamiento de facturas fue cancelada porque excedió el límite de tiempo: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Excepción al reprocesar facturas: {e.Message}");
            }
        }

        private async Task NormalFunction(CancellationToken cancellationToken, List<FacturaBase> listInvoiceXML)
        {
            foreach (var factura in listInvoiceXML)
            {
                try
                {
                    (bool isBusinessPartnerExists, string cardCode, string statusCardCode) = await GetCardCodeByTaxId(factura.cabecera.nitEmisor.ToString());
                    if (!isBusinessPartnerExists && config.Provider)
                    {
                        (cardCode, string statusBusinessParther) = await CreateBusinessPartner(factura.cabecera.nitEmisor.ToString(), factura.cabecera.razonSocialEmisor, "S", factura.cabecera.nitEmisor.ToString(), cancellationToken);
                        if (string.IsNullOrEmpty(cardCode))
                        {
                            databaseHandler.UpdateStatus(factura.identifier, Datas.StatusError, statusBusinessParther);
                            continue;
                        }
                    }
                    else if (!isBusinessPartnerExists && !config.Provider)
                    {
                        databaseHandler.UpdateStatus(factura.identifier, Datas.StatusError, statusCardCode);
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
                    (bool isBusinessPartnerExists, string cardCode, string statusCardCode) = await GetCardCodeByTaxId(factura.cabecera.nitEmisor.ToString());
                    if (!isBusinessPartnerExists && config.Provider)
                    {
                        (cardCode, string statusBusinessParther) = await CreateBusinessPartner(factura.cabecera.nitEmisor.ToString(), factura.cabecera.razonSocialEmisor, "S", factura.cabecera.nitEmisor.ToString(), cancellationToken);
                        if (string.IsNullOrEmpty(cardCode))
                        {
                            databaseHandler.UpdateStatus(factura.identifier, Datas.StatusError, statusBusinessParther);
                            continue;
                        }
                    }
                    else if (!isBusinessPartnerExists && !config.Provider)
                    {
                        databaseHandler.UpdateStatus(factura.identifier, Datas.StatusError, statusCardCode);
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
            try
            {
                foreach (var factura in listInvoiceXML)
                {
                    switch (factura)
                    {
                        case FacturaCompraVenta facturaCompraVenta:
                            await FunctionFacturaCompra(cancellationToken, facturaCompraVenta);
                            break;
                        case FacturaServicioBasico facturaServicioBasico:
                            await FunctionFacturaServicioBasico(cancellationToken, facturaServicioBasico);
                            break;
                        case FacturaServicioTuristicoHospedaje facturaServicioTuristicoHospedaje:
                            await FunctionFacturaServicioTuristico(cancellationToken, facturaServicioTuristicoHospedaje);
                            break;
                        default:
                            throw new Exception($"Tipo de factura desconocido: {factura.GetType().Name}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en AccountingEntriesFunction: {ex.Message}");
            }
        }
        private async Task FunctionFacturaCompra(CancellationToken cancellationToken, FacturaCompraVenta facturaCompraVenta)
        {
            try
            {
                int cant = databaseHandler.CantAccounts("Compra Venta");
                List<List<object>> accounts = databaseHandler.GetAccounts("Compra Venta");
                List<object> allInvoiceLines = new List<object>(); // Almacena todas las invoiceLines

                foreach (var account in accounts)
                {
                    var invoiceLines = facturaCompraVenta.detalle?.Select(d => new
                    {
                        AccountCode = (string)account[0],
                        Credit = d.subTotal * (decimal)account[1],
                        Debit = d.subTotal * (decimal)account[2]
                    }).ToList();

                    allInvoiceLines.AddRange(invoiceLines);
                }
                var invoiceJson = new
                {
                    JournalEntryLines = allInvoiceLines
                };
                string jsonContent = JsonConvert.SerializeObject(invoiceJson);
                Console.Write("esto serializo: " + jsonContent + "\n");
                HttpContent contentInvoice = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var insertResponse = await client.PostAsync(config.Url + "JournalEntries", contentInvoice, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en FunctionFacturaCompra: {ex.Message}");
            }
        }

        private Task FunctionFacturaServicioTuristico(CancellationToken cancellationToken, FacturaServicioTuristicoHospedaje facturaServicioTuristicoHospedaje)
        {
            throw new NotImplementedException();
        }

        private Task FunctionFacturaServicioBasico(CancellationToken cancellationToken, FacturaServicioBasico facturaServicioBasico)
        {
            throw new NotImplementedException();
        } 

        private async Task AccountingEntriesFunction1(CancellationToken cancellationToken, List<FacturaBase> listInvoiceXML)
        {
            foreach (var factura in listInvoiceXML)
            {
                try
                {
                    (bool isBusinessPartnerExists, string cardCode, string statusCardCode) = await GetCardCodeByTaxId(factura.cabecera.nitEmisor.ToString());
                    if (!isBusinessPartnerExists && config.Provider)
                    {
                        (cardCode, string statusBusinessParther) = await CreateBusinessPartner(factura.cabecera.nitEmisor.ToString(), factura.cabecera.razonSocialEmisor, "S", factura.cabecera.nitEmisor.ToString(), cancellationToken);
                        if (string.IsNullOrEmpty(cardCode))
                        {
                            databaseHandler.UpdateStatus(factura.identifier, Datas.StatusError, (cardCode, statusBusinessParther));
                            continue;
                        }
                    }
                    else if (!isBusinessPartnerExists && !config.Provider)
                    {
                        databaseHandler.UpdateStatus(factura.identifier, Datas.StatusError, statusCardCode);
                        continue;
                    }

                    if (isBusinessPartnerExists || config.Provider)
                    {
                        var invoiceLines = factura.detalle?.Select(d => d == null ? null : new
                        {
                            AccountCode = config.SAPAccountCodeCredito,
                            Credit = d.subTotal,
                            Debit = 0m  // El "m" hace que sea decimal 
                        }).ToList();
                        var totalFactura = factura.detalle?.Sum(d => d.subTotal) ?? 0;
                        invoiceLines.Add(new
                        {
                            AccountCode = config.SAPAccountCodeDebito,
                            Credit = 0m,
                            Debit = totalFactura
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
                            databaseHandler.UpdateStatus(factura.identifier, Datas.StatusError, detailedErrorMessage);
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
                    DocumentLines = factura.detalle?.SelectMany(d => new[]
                    {
                        new
                        {
                            ItemCode = d.codigoProducto.ToString(),
                            Quantity = ((XmlNode[])d.cantidad)[0]?.InnerText,
                            TaxCode = "IVA",
                            UnitPrice = d.precioUnitario.ToString(CultureInfo.InvariantCulture),
                            PriceAfterVAT = d.subTotal.ToString(CultureInfo.InvariantCulture)
                        }
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
                    Console.WriteLine(detailedErrorMessage);
                    databaseHandler.UpdateStatus(factura.identifier, Datas.StatusError, detailedErrorMessage);
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
                    DocumentLines = factura.detalle?.SelectMany(d => new[]
                    {
                        new
                        {
                            ItemDescription = d.descripcion,
                            TaxCode = "IVA",
                            UnitPrice = d.precioUnitario.ToString(CultureInfo.InvariantCulture),
                            PriceAfterVAT = d.subTotal.ToString(CultureInfo.InvariantCulture),
                            AccountCode = config.SAPAccountCode
                        }
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
                    Console.WriteLine(detailedErrorMessage);
                    databaseHandler.UpdateStatus(factura.identifier, Datas.StatusError, detailedErrorMessage);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Excepción al intentar insertar la factura: {e.Message}");
            }
        }
        private async Task<(bool, string, string)> GetCardCodeByTaxId(string taxId)
        {
            string statusMessage;
            try
            {
                var response = await client.GetAsync(config.Url + "BusinessPartners?$filter=FederalTaxID eq '" + taxId + "'");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var businessPartners = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                    if (businessPartners.value.Count > 0)
                    {
                        var cardCode = businessPartners.value[0].CardCode;
                        statusMessage = $"CardCode para el NIT {taxId} es {cardCode}";
                        Console.WriteLine(statusMessage);
                        return (true, cardCode, statusMessage);
                    }
                    else
                    {
                        statusMessage = $"No se encontró un socio comercial con el NIT {taxId}";
                        Console.WriteLine(statusMessage);
                        return (false, string.Empty, statusMessage);
                    }
                }
                else
                {
                    statusMessage = $"Error al recuperar el socio comercial: {response.StatusCode}";
                    Console.WriteLine(statusMessage);
                    return (false, string.Empty, statusMessage);

                }
            }
            catch (Exception ex)
            {
                statusMessage = $"Error inesperado: {ex.Message}";
                Console.WriteLine(statusMessage);
                return (false, string.Empty, statusMessage);
            }
        }
        public async Task<(string, string)> CreateBusinessPartner(string cardCode, string cardName, string cardType, string FederalTaxID, CancellationToken cancellationToken)
        {
            string statusMessage;
            try
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
                    statusMessage = $"Socio comercial {createdCardCode} creado exitosamente"; 
                    return (createdCardCode, statusMessage);
                }
                else
                {
                    statusMessage = $"Error al crear socio comercial: {response.StatusCode}";
                    Console.WriteLine(statusMessage);
                    return (string.Empty, statusMessage);
                }
            }
            catch (Exception ex)
            {
                statusMessage = $"Error inesperado: {ex.Message}";
                Console.WriteLine(statusMessage);
                return (string.Empty, statusMessage);
            }
        }
        public async Task<List<List<string>>> GetConfiguration()
        {
            List<List<string>> accountList = new List<List<string>>();

            try
            {
                CancellationToken cancellationToken = await ConnectToSAP();
                var response = await client.GetAsync(config.Url + "ChartOfAccounts", cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var chartOfAccountsData = JsonConvert.DeserializeObject<ChartOfAccountsData>(content);

                    foreach (var account in chartOfAccountsData.Value)
                    {
                        List<string> accountData = new List<string>
                {
                    account.Code,
                    account.FormatCode,
                    account.Name
                };

                        accountList.Add(accountData);
                    }
                }
                else
                {
                    Console.WriteLine($"Error al obtener la configuracion: {response.StatusCode}");
                }
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"La operación de obtencion de configuraciones fue cancelada porque excedió el límite de tiempo: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Excepción al obtener la configuracion: {e.Message}");
            }
            Console.WriteLine("Antes de salir: " + accountList);
            return accountList;
        }
         
        public class ChartOfAccountsData
        {
            public List<Account> Value { get; set; }
        }

        public class Account
        {
            public string Code { get; set; }
            public string FormatCode { get; set; }
            public string Name { get; set; }
        }
         
        public void Dispose()
        {
            handler?.Dispose();
            client?.Dispose();
        }
    }
}
