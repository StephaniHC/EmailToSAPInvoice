using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailToSAPInvoice.Service.Table
{
    public class Accounts
    {
        public const string TipoDocCompra = "Compra Venta";
        public const string TipoDocServicioTuristico = "Servicio Turistico Hospedaje";
        public const string TipoDocServicioBasico = "Servicio Basico";

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string TipoDoc { get; set; }
        public string Account { get; set; } 
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public bool BusinessPartners { get; set; } 
        public bool Status { get; set; }
    }
}