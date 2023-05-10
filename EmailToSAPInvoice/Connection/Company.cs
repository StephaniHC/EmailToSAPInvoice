using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace EmailToSAPInvoice.Connection
{ 
    public class Company
    {
        public string CompanyDB;
        public string UserName;
        public string Password;
        public SAPbobsCOM.BoSuppLangs Language;

        public Company() { }
    }
}
