using EmailToSAPInvoice.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EmailToSAPInvoice.Service.Table
{
    public class Rcuenta
    {
        public const string TipoDocCompra = "Compra Venta";
        public const string TipoDocServicioTuristico = "Servicio Turistico Hospedaje";
        public const string TipoDocServicioBasico = "Servicio Basico";

        public const string TipoCalculoUp = "Grossing Up";
        public const string TipoCalculoDown = "Grossing Down";

        [PrimaryKey, AutoIncrement]
        public int U_IdDocumento { get; set; }
        public string U_CodPerfil { get; set; }
        public string U_TipDoc { get; set; }
        public decimal U_EXENTOpercent { get; set; }
        public string U_IdTipoDoc { get; set; }
        public string U_TipoCalc { get; set; }
        public decimal U_IVApercent { get; set; }
        public string U_IVAcuenta { get; set; }
        public decimal U_ITpercent { get; set; }
        public string U_ITcuenta { get; set; }
        public decimal U_IUEpercent { get; set; }
        public string U_IUEcuenta { get; set; }
        public decimal U_RCIVApercent { get; set; }
        public string U_RCIVAcuenta { get; set; }
        public string U_CTAexento { get; set; }
        public decimal U_TASA { get; set; }

    }
}
