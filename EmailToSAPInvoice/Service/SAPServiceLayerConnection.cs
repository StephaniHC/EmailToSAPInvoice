﻿using EmailToSAPInvoice.Models;
using EmailToSAPInvoice.Service.Table;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http; 
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

        public async Task ConnectToSAP(List<FacturaCompraVenta.facturaElectronicaCompraVenta> listInvoiceXML,
                                       List<FacturaServicioBasico.facturaElectronicaServicioBasico> listServiceBasicXML, 
                                       List<FacturaServicioTuristicoHospedaje.facturaElectronicaServicioTuristicoHospedaje> listServiceTouristXML)
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
                    // Insertar factura
                    foreach (var factura in listInvoiceXML)
                    { 
                        var invoiceJson = new
                        {
                            CardCode = factura.cabecera.nitEmisor,
                            DocDate = (factura.cabecera.fechaEmision).ToString("yyyy-MM-dd"),
                        //DocDate = DateTime.Parse(factura.cabecera.fechaEmision).ToString("yyyy-MM-dd"),
                        DocumentLines = factura.detalle?.Select(d => d == null ? null : new
                            {
                                ItemCode = d.codigoProducto,
                                Quantity = d.cantidad,
                                TaxCode = "IVA",
                                UnitPrice = d.precioUnitario
                            }).ToList()
                        };

                        string jsonContent = JsonConvert.SerializeObject(invoiceJson);
                        HttpContent contentInvoice = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                        var insertResponse = await client.PostAsync(config.Url + "Invoices", contentInvoice);
                        if (insertResponse.IsSuccessStatusCode)
                        {
                            databaseHandler.UpdateStatus(factura.identifier, Datas.StatusProcessed, " ");
                        }
                        else
                        {
                            databaseHandler.UpdateStatus(factura.identifier, Datas.StatusError, $"{insertResponse.StatusCode}");
                            Console.WriteLine($"Error al insertar factura: {insertResponse.StatusCode}");
                        }
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
