using EmailToSAPInvoice.Models;
using EmailToSAPInvoice.Service.Table;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http; 
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
                    //solo por prueba para insertar factura
                    var currencyRateJson = new
                    {
                        Currency = "USD",
                        Rate = "6.96",
                        RateDate = "20230518"
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
                    //hasta aqui quitar
                switch (config.TypeService)
                    {
                        case 1:
                            await GetCardCodeByTaxId("191040025");
                            //await ProcessInvoicesPrueba(timeCancelation.Token, listInvoiceXML);
                            break;
                        case 2:
                            //await ProcessInvoices(timeCancelation.Token, listInvoiceXML);
                            break;
                        case 3:
                            //await ProcessInvoices(timeCancelation.Token, listInvoiceXML);
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
        private async Task ProcessInvoicesPrueba(CancellationToken cancellationToken, List<FacturaBase> listInvoiceXML)
        {
            foreach (var factura in listInvoiceXML)
            {
                try
                {
                    bool proveedorX = true;
                    if (config.Provider)
                    {
                        if (!proveedorX)
                        {
                            // crea proveedor
                        }
                        await ProcessInvoice(factura, cancellationToken);
                    }
                    else if (proveedorX)
                    {
                        await ProcessInvoice(factura, cancellationToken);
                    }
                    else
                    {
                        // añade a la base que no existe proveedor
                    }
                }
                catch { 
                }
            }  
        }
        private async Task ProcessInvoice(FacturaBase factura, CancellationToken cancellationToken)
        {
            try
            {
                var invoiceJson = new
                {
                    CardCode = "R000003",//factura.cabecera.nitEmisor.ToString(),
                    //DocDate = factura.cabecera.fechaEmision.ToString("yyyy-MM-dd"), //Descomentar considerar el tipo de cambio en la fecha para insertar factura
                    DocumentLines = factura.detalle?.Select(d => d == null ? null : new
                    {
                        ItemCode = d.codigoProducto.ToString(),
                        Quantity = d.cantidad.ToString(),
                        TaxCode = "IVA",
                        UnitPrice = d.precioUnitario.ToString(CultureInfo.InvariantCulture)
                    }).ToList()
                };
                string jsonContent = JsonConvert.SerializeObject(invoiceJson);
                Console.Write("esto serializo: " + jsonContent + "\n");
                HttpContent contentInvoice = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var insertResponse = await client.PostAsync(config.Url + "Invoices", contentInvoice, cancellationToken);
                if (insertResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Se insertó factura: {insertResponse.StatusCode}");
                    databaseHandler.UpdateStatus(factura.identifier, Datas.StatusProcessed, $"Se insertó factura: {insertResponse.StatusCode}");
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
        private async Task GetCardCodeByTaxId(string taxId)
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
                }
                else
                {
                    Console.WriteLine($"No se encontró un socio comercial con el NIT {taxId}");
                }
            }
            else
            {
                Console.WriteLine($"Error al recuperar el socio comercial: {response.StatusCode}");
            }
        }

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
