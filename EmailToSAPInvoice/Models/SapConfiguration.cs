using System;
using System.Collections.Generic;
using System.Text;

namespace EmailToSAPInvoice.Models
{
    public class SapConfiguration
    {
        public string SLDServer { get; set; }
        public string Server { get; set; }
        public string CompanyDB { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DbUserName { get; set; }
        public string DbPassword { get; set; }
        public string Url { get; set; }  
        public bool Certificate { get; set; }
        public int TypeService { get; set; }
        public bool Provider { get; set; }
        public string SAPWarehouseCode { get; set; }
        public string SAPAccountCode { get; set; } 
        public string SAPAccountCodeCredito { get; set; }
        public string SAPAccountCodeDebito { get; set; }
    }
} 