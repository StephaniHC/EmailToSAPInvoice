using System;
using System.Collections.Generic;
using System.Text; 
using System.Net.Http;
using System.Threading.Tasks;

namespace EmailToSAPInvoice.Connections
{
     public class ServerLayerClient
    {
        private readonly HttpClient _httpClient;

        public ServerLayerClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetServerDataAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Lanza una excepción si la respuesta no es exitosa
            Console.Write("SE CONECTOO " + response);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
         
    }

}
