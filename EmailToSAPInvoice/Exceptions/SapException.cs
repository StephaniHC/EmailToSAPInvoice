using System;
using System.Collections.Generic;
using System.Text;

namespace EmailToSAPInvoice.Exceptions
{ 
    public class SapException : Exception
    {
        public int code;
        public string message;
        public string trace;
        public string data;

        public SapException(int code, string message)
        {
            this.code = code;
            this.message = message;
        }

        public SapException(string message)
        {
            this.message = message;
            this.code = 0;
        }
    }
}