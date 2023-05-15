using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailToSAPInvoice.Service.Table
{
    public class Datas
    {
        public const string StatusPending = "Pendiente";
        public const string StatusProcessed = "Procesado";
        public const string StatusError = "Error";

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Date { get; set; }
        [MaxLength(250)]
        public string Subject { get; set; }
        [MaxLength(250)]
        public string Attached { get; set; }
        public string Status { get; set; } = StatusPending;

        public string Observation { get; set; }  
    }
}
