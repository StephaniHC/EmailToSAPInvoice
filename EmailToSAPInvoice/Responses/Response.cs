
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailToSAPInvoice.Responses
{ 
    public class Response
    {
        public int code { get; set; }
        public string message { get; set; }

        public string data { get; set; }

        public Response()
        {
            this.code = 1;
            this.message = "Response OK";
        }

        public Response(string message)
        {
            this.code = 1;
            this.message = message;
        }

        public Response(int code, string message)
        {
            this.code = code;
            this.message = message;
        }
    }
}
