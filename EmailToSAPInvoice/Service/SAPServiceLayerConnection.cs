using EmailToSAPInvoice.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailToSAPInvoice.Service
{
    public class SAPServiceLayerConnection
    {
        private HttpClientHandler handler;
        private HttpClient client;
        private SapConfiguration config;

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

        public async Task ConnectToSAP(List<FacturaElectronicaCompraVenta> listInvoiceXML)
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
                    foreach (var invoice in listInvoiceXML)
                    {
                        // Convertir la factura a un objeto anónimo con las propiedades que necesita la API de SAP
                        var sapInvoice = new
                        {
                            //CardCode = invoice.CardCode,
                            //DocDate = invoice.DocDate,
                            //DocDueDate = invoice.DocDueDate, 
                        };

                        // Serializar el objeto de factura a JSON
                        var invoiceContent = new StringContent(JsonConvert.SerializeObject(sapInvoice), Encoding.UTF8, "application/json");

                        // Hacer una solicitud POST a la API de facturas de SAP
                        var invoiceResponse = await client.PostAsync(config.Url + "Invoices", invoiceContent, timeCancelation.Token);

                        /*if (invoiceResponse.IsSuccessStatusCode)
                        {
                            Console.WriteLine($"Factura {invoice.DocEntry} insertada correctamente en SAP.");
                        }
                        else
                        {
                            Console.WriteLine($"Error al insertar la factura {invoice.DocEntry} en SAP: {invoiceResponse.StatusCode}");
                        }*/
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

        public void Dispose()
        {
            handler?.Dispose();
            client?.Dispose();
        }
    }
} 
