using EmailToSAPInvoice.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailToSAPInvoice.Service
{
    public class HttpClient
    {
        private readonly HttpClient _client;

        public HttpClient(ServerCredentials credentials)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(credentials.Url)
            };

            var byteArray = Encoding.ASCII.GetBytes($"{credentials.Username}:{credentials.Password}");
            _client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public async Task<string> GetAsync(string requestUri)
        {
            var response = await _client.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
