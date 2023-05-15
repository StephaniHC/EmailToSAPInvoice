using EmailToSAPInvoice.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EmailToSAPInvoice.Service
{
    public class SAPServiceLayerConnection
    {
        private static HttpClient client;
        private SapConfiguration config;

        public SAPServiceLayerConnection()
        {
            bool x = true;
            HttpClientHandler handler;
            if (x)
                handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };

            else
                handler = new HttpClientHandler();

            client = new HttpClient(handler);

            LoadConfiguration();
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

        public async Task ConnectToSAP()
        {
            try
            {
                var loginInfo = new
                {
                    UserName = config.UserName,
                    Password = config.Password,
                    CompanyDB = config.CompanyDB
                };

                var content = new StringContent(JsonConvert.SerializeObject(loginInfo), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(config.Url + "Login", content);

                if (response.IsSuccessStatusCode)
                {
                    var session = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
                    client.DefaultRequestHeaders.Add("B1S-CaseInsensitive", "true");
                    client.DefaultRequestHeaders.Add("Cookie", $"B1SESSION={session.SessionId}; ROUTEID={session.RouteId};");
                    Console.Write("se conecto" + session);

                }
                else
                {
                    Console.WriteLine($"Error al conectar a SAP: {response.StatusCode}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Excepción al conectar a SAP: {e.Message}");
            }
        }
    }
}
