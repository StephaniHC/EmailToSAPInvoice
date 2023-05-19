using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EmailToSAPInvoice.Models
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class FacturaBase
    { 
        public object identifier { get; set; }

        [XmlElement("cabecera")]
        public Cabecera cabecera { get; set; }

        [XmlElement("detalle")]
        public Detalle[] detalle { get; set; }

        [XmlElement("signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature signature { get; set; }
        public class Cabecera
        { 
            public Int64 nitEmisor { get; set; }
            public string razonSocialEmisor { get; set; }
            public string municipio { get; set; }
            public object telefono { get; set; }
            public uint numeroFactura { get; set; }
            public string cuf { get; set; }
            public string cufd { get; set; }
            public uint codigoSucursal { get; set; }
            public string direccion { get; set; }
            public object codigoPuntoVenta { get; set; }
            public System.DateTime fechaEmision { get; set; }
            public string nombreRazonSocial { get; set; }
            public byte codigoTipoDocumentoIdentidad { get; set; }
            public ulong numeroDocumento { get; set; }
            public object complemento { get; set; }
            public object codigoCliente { get; set; }
            public object codigoMetodoPago { get; set; }
            public object numeroTarjeta { get; set; }
            public decimal montoTotal { get; set; }
            public byte codigoMoneda { get; set; }
            public decimal tipoCambio { get; set; }
            public decimal montoTotalMoneda { get; set; }
            public string leyenda { get; set; }
            public string usuario { get; set; }
            public byte codigoDocumentoSector { get; set; } 
        }
        public class Detalle
        {
            public uint actividadEconomica { get; set; }
            public uint codigoProductoSin { get; set; }
            public string codigoProducto { get; set; }
            public string descripcion { get; set; }
            public object cantidad { get; set; }
            public byte unidadMedida { get; set; }
            public decimal precioUnitario { get; set; }
            public object montoDescuento { get; set; }
            public decimal subTotal { get; set; }  
        }
        public class Signature
        {
            public SignedInfo signedInfo { get; set; }
            public string signatureValue { get; set; }
            public KeyInfo keyInfo { get; set; }

            public class SignedInfo
            {
                public CanonicalizationMethod canonicalizationMethod { get; set; }
                public SignatureMethod signatureMethod { get; set; }
                public Reference reference { get; set; }

                public class CanonicalizationMethod
                {
                    public string Algorithm { get; set; }
                }

                public class SignatureMethod
                {
                    public string Algorithm { get; set; }
                }

                public class Reference
                {
                    public Transforms transforms { get; set; }
                    public DigestMethod digestMethod { get; set; }
                    public string DigestValue { get; set; }

                    public class Transforms
                    { 
                        public Transform[] transform { get; set; }

                        public class Transform
                        { 
                            public string algorithm { get; set; }
                        }
                    }

                    public class DigestMethod
                    { 
                        public string algorithm { get; set; }
                    }
                }
            }

            public class KeyInfo
            {
                public X509Data x509Data { get; set; }

                public class X509Data
                {
                    public string x509Certificate { get; set; }
                    public string x509SubjectName { get; set; }
                    public X509IssuerSerial x509IssuerSerial { get; set; }

                    public class X509IssuerSerial
                    {
                        public string x509IssuerName { get; set; }
                        public string x509SerialNumber { get; set; }
                    }
                }
            }
        }
    }
    [System.Xml.Serialization.XmlRootAttribute("facturaElectronicaServicioTuristicoHospedaje", Namespace = "", IsNullable = false)]
    public class FacturaServicioTuristicoHospedaje : FacturaBase
    {
        public new class Cabecera : FacturaBase.Cabecera
        {
            public string razonSocialOperadorTurismo { get; set; }
            public byte cantidadHuespedes { get; set; }
            public byte cantidadHabitaciones { get; set; }
            public byte cantidadMayores { get; set; }
            public byte cantidadMenores { get; set; }
            public System.DateTime fechaIngresoHospedaje { get; set; }
        }

        public new class Detalle : FacturaBase.Detalle
        {
            public byte codigoTipoHabitacion { get; set; }
            public string detalleHuespedes { get; set; }
        } 
    }

    [System.Xml.Serialization.XmlRootAttribute("facturaElectronicaServicioBasico", Namespace = "", IsNullable = false)]
    public class FacturaServicioBasico : FacturaBase
    {
        public new class Cabecera : FacturaBase.Cabecera
        {
            public string mes { get; set; }
            public ushort gestion { get; set; }
            public string ciudad { get; set; }
            public string zona { get; set; }
            public string numeroMedidor { get; set; }
            public string domicilioCliente { get; set; }
            public decimal montoTotalSujetoIva { get; set; }
            public ushort consumoPeriodo { get; set; }
            public object beneficiarioLey1886 { get; set; }
            public object montoDescuentoLey1886 { get; set; }
            public object montoDescuentoTarifaDignidad { get; set; }
            public decimal tasaAseoField { get; set; }
            public decimal tasaAlumbrado { get; set; }
            public object ajusteNoSujetoIva { get; set; }
            public object otrosPagosNoSujetoIva { get; set; }
        }
    }

    [System.Xml.Serialization.XmlRootAttribute("facturaElectronicaCompraVenta", Namespace = "", IsNullable = false)]
    public class FacturaCompraVenta : FacturaBase
   {
        public new class Cabecera : FacturaBase.Cabecera
        {
            public decimal montoTotalSujetoIva { get; set; }
        }

        public new class Detalle : FacturaBase.Detalle
        {
            public object numeroSerie { get; set; }
            public object numeroImei { get; set; }
        }
    }
}