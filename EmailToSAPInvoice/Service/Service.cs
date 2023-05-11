using System;
using System.Collections.Generic;
using System.Text;

namespace EmailToSAPInvoice.Service
{
    public class Service
    {
        private readonly HttpClient _client;

        public Service(HttpClient client)
        {
            _client = client;
        }

        public async Task DoSomethingWithServerData()
        {
            var data = await _client.GetAsync("/api/data");
            // hacer algo con los datos
        }
    }
}
