using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailToSAPInvoice.Service.Table
{
    public class Rperfil
    {
        public const string UMonedaLocal = "Moneda Local(BS)";
        public const string UMonedaSistema = "Moneda Sistema(USD)"; 
        [PrimaryKey, AutoIncrement]
        public int U_CodPerfil { get; set; }
        public string U_NombrePerfil { get; set; }
        public string U_Trabaja { get; set; } 

    }
}
