using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailToSAPInvoice.Service.Table
{
    public class JournalEntries
    {
        public string perfil { get; set; }
        public string iva { get; set; }
        public string iue { get; set; }
        public string exento { get; set; }
        public string documento { get; set; }
        public string cuentaIva { get; set; }
        public string cuentaIue { get; set; }
        public string cuentaExento { get; set; }
        public string tipoDocSap { get; set; }
        public string it { get; set; }
        public string rcIva { get; set; }
        public string tasa { get; set; }
        public string tipoCalculo { get; set; }
        public string cuentaIt { get; set; }
        public string cuentaRCIva { get; set; }
    }
}
