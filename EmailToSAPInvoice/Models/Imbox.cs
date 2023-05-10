using System;
using System.Collections.Generic;
using System.Text;

namespace EmailToSAPInvoice.Models
{
    public class Imbox
    {
        public List<Proveedor> Proveedores { get; set; } = default!;
        public class Proveedor
        {
            public string Nombre { get; set; } = default!;
            public List<Metodo> Metodos { get; set; } = default!;
            public List<Cliente> Clientes { get; set; } = default!;
        }
        public class Metodo
        {
            public string Nombre { get; set; } = default!;
            public string Url { get; set; } = default!;
            public int Puerto { get; set; } = default!;
        }

        public class Cliente
        {
            public string Email { get; set; } = default!;
            public string Password { get; set; } = default!;
        }
    }
}
