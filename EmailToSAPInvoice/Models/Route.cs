using System;
using System.Collections.Generic;
using System.Text;

namespace EmailToSAPInvoice.Models
{
    public class Route
    {
        public string Read { get; set; } = default!;
        public string Base { get; set; } = default!;
        public string Download { get; set; } = default!;
    }
}
