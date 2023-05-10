using System;
using System.Collections.Generic;
using System.Text;

namespace EmailToSAPInvoice.Requests
{ 
    public class InvoiceRequest
    {
        public int Code { get; set; }

        public int DocEntry { get; set; }

        public string Response { get; set; }

        public string EmissionDate { get; set; }

        public string Url { get; set; }

        public string Cuf { get; set; }

        public int InvoiceNumber { get; set; }

        public int Invoice { get; set; }

        public int Advanced { get; set; }

        public string Legend { get; set; }

        public string ExternalId { get; set; }
    }
}
