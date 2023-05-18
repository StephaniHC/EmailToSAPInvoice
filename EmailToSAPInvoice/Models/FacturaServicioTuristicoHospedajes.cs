using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailToSAPInvoice.Models
{
    public class FacturaServicioTuristicoHospedajes
    { 
        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class facturaElectronicaServicioTuristicoHospedaje
        {
            public object identifier { get; set; }

            private facturaElectronicaServicioTuristicoHospedajeCabecera cabeceraField;

            private facturaElectronicaServicioTuristicoHospedajeDetalle detalleField;

            private Signature signatureField;

            /// <remarks/>
            public facturaElectronicaServicioTuristicoHospedajeCabecera cabecera
            {
                get
                {
                    return this.cabeceraField;
                }
                set
                {
                    this.cabeceraField = value;
                }
            }

            /// <remarks/>
            public facturaElectronicaServicioTuristicoHospedajeDetalle detalle
            {
                get
                {
                    return this.detalleField;
                }
                set
                {
                    this.detalleField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
            public Signature Signature
            {
                get
                {
                    return this.signatureField;
                }
                set
                {
                    this.signatureField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class facturaElectronicaServicioTuristicoHospedajeCabecera
        {
            private ulong nitEmisorField;

            private string razonSocialEmisorField;

            private string municipioField;

            private uint telefonoField;

            private byte numeroFacturaField;

            private string cufField;

            private string cufdField;

            private byte codigoSucursalField;

            private string direccionField;

            private object codigoPuntoVentaField;

            private System.DateTime fechaEmisionField;

            private string nombreRazonSocialField;

            private byte codigoTipoDocumentoIdentidadField;

            private ulong numeroDocumentoField;

            private object complementoField;

            private string codigoClienteField;

            private string razonSocialOperadorTurismoField;

            private byte cantidadHuespedesField;

            private byte cantidadHabitacionesField;

            private byte cantidadMayoresField;

            private byte cantidadMenoresField;

            private System.DateTime fechaIngresoHospedajeField;

            private byte codigoMetodoPagoField;

            private ulong numeroTarjetaField;

            private decimal montoTotalField;

            private byte codigoMonedaField;

            private decimal tipoCambioField;

            private decimal montoTotalMonedaField;

            private string leyendaField;

            private string usuarioField;

            private byte codigoDocumentoSectorField;

            /// <remarks/>
            public ulong nitEmisor
            {
                get
                {
                    return this.nitEmisorField;
                }
                set
                {
                    this.nitEmisorField = value;
                }
            }

            /// <remarks/>
            public string razonSocialEmisor
            {
                get
                {
                    return this.razonSocialEmisorField;
                }
                set
                {
                    this.razonSocialEmisorField = value;
                }
            }

            /// <remarks/>
            public string municipio
            {
                get
                {
                    return this.municipioField;
                }
                set
                {
                    this.municipioField = value;
                }
            }

            /// <remarks/>
            public uint telefono
            {
                get
                {
                    return this.telefonoField;
                }
                set
                {
                    this.telefonoField = value;
                }
            }

            /// <remarks/>
            public byte numeroFactura
            {
                get
                {
                    return this.numeroFacturaField;
                }
                set
                {
                    this.numeroFacturaField = value;
                }
            }

            /// <remarks/>
            public string cuf
            {
                get
                {
                    return this.cufField;
                }
                set
                {
                    this.cufField = value;
                }
            }

            /// <remarks/>
            public string cufd
            {
                get
                {
                    return this.cufdField;
                }
                set
                {
                    this.cufdField = value;
                }
            }

            /// <remarks/>
            public byte codigoSucursal
            {
                get
                {
                    return this.codigoSucursalField;
                }
                set
                {
                    this.codigoSucursalField = value;
                }
            }

            /// <remarks/>
            public string direccion
            {
                get
                {
                    return this.direccionField;
                }
                set
                {
                    this.direccionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object codigoPuntoVenta
            {
                get
                {
                    return this.codigoPuntoVentaField;
                }
                set
                {
                    this.codigoPuntoVentaField = value;
                }
            }

            /// <remarks/>
            public System.DateTime fechaEmision
            {
                get
                {
                    return this.fechaEmisionField;
                }
                set
                {
                    this.fechaEmisionField = value;
                }
            }

            /// <remarks/>
            public string nombreRazonSocial
            {
                get
                {
                    return this.nombreRazonSocialField;
                }
                set
                {
                    this.nombreRazonSocialField = value;
                }
            }

            /// <remarks/>
            public byte codigoTipoDocumentoIdentidad
            {
                get
                {
                    return this.codigoTipoDocumentoIdentidadField;
                }
                set
                {
                    this.codigoTipoDocumentoIdentidadField = value;
                }
            }

            /// <remarks/>
            public ulong numeroDocumento
            {
                get
                {
                    return this.numeroDocumentoField;
                }
                set
                {
                    this.numeroDocumentoField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object complemento
            {
                get
                {
                    return this.complementoField;
                }
                set
                {
                    this.complementoField = value;
                }
            }

            /// <remarks/>
            public string codigoCliente
            {
                get
                {
                    return this.codigoClienteField;
                }
                set
                {
                    this.codigoClienteField = value;
                }
            }

            /// <remarks/>
            public string razonSocialOperadorTurismo
            {
                get
                {
                    return this.razonSocialOperadorTurismoField;
                }
                set
                {
                    this.razonSocialOperadorTurismoField = value;
                }
            }

            /// <remarks/>
            public byte cantidadHuespedes
            {
                get
                {
                    return this.cantidadHuespedesField;
                }
                set
                {
                    this.cantidadHuespedesField = value;
                }
            }

            /// <remarks/>
            public byte cantidadHabitaciones
            {
                get
                {
                    return this.cantidadHabitacionesField;
                }
                set
                {
                    this.cantidadHabitacionesField = value;
                }
            }

            /// <remarks/>
            public byte cantidadMayores
            {
                get
                {
                    return this.cantidadMayoresField;
                }
                set
                {
                    this.cantidadMayoresField = value;
                }
            }

            /// <remarks/>
            public byte cantidadMenores
            {
                get
                {
                    return this.cantidadMenoresField;
                }
                set
                {
                    this.cantidadMenoresField = value;
                }
            }

            /// <remarks/>
            public System.DateTime fechaIngresoHospedaje
            {
                get
                {
                    return this.fechaIngresoHospedajeField;
                }
                set
                {
                    this.fechaIngresoHospedajeField = value;
                }
            }

            /// <remarks/>
            public byte codigoMetodoPago
            {
                get
                {
                    return this.codigoMetodoPagoField;
                }
                set
                {
                    this.codigoMetodoPagoField = value;
                }
            }

            /// <remarks/>
            public ulong numeroTarjeta
            {
                get
                {
                    return this.numeroTarjetaField;
                }
                set
                {
                    this.numeroTarjetaField = value;
                }
            }

            /// <remarks/>
            public decimal montoTotal
            {
                get
                {
                    return this.montoTotalField;
                }
                set
                {
                    this.montoTotalField = value;
                }
            }

            /// <remarks/>
            public byte codigoMoneda
            {
                get
                {
                    return this.codigoMonedaField;
                }
                set
                {
                    this.codigoMonedaField = value;
                }
            }

            /// <remarks/>
            public decimal tipoCambio
            {
                get
                {
                    return this.tipoCambioField;
                }
                set
                {
                    this.tipoCambioField = value;
                }
            }

            /// <remarks/>
            public decimal montoTotalMoneda
            {
                get
                {
                    return this.montoTotalMonedaField;
                }
                set
                {
                    this.montoTotalMonedaField = value;
                }
            }

            /// <remarks/>
            public string leyenda
            {
                get
                {
                    return this.leyendaField;
                }
                set
                {
                    this.leyendaField = value;
                }
            }

            /// <remarks/>
            public string usuario
            {
                get
                {
                    return this.usuarioField;
                }
                set
                {
                    this.usuarioField = value;
                }
            }

            /// <remarks/>
            public byte codigoDocumentoSector
            {
                get
                {
                    return this.codigoDocumentoSectorField;
                }
                set
                {
                    this.codigoDocumentoSectorField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class facturaElectronicaServicioTuristicoHospedajeDetalle
        {
            private ushort actividadEconomicaField;

            private uint codigoProductoSinField;

            private byte codigoProductoField;

            private string descripcionField;

            private byte codigoTipoHabitacionField;

            private byte cantidadField;

            private byte unidadMedidaField;

            private decimal precioUnitarioField;

            private object montoDescuentoField;

            private decimal subTotalField;

            private string detalleHuespedesField;

            /// <remarks/>
            public ushort actividadEconomica
            {
                get
                {
                    return this.actividadEconomicaField;
                }
                set
                {
                    this.actividadEconomicaField = value;
                }
            }

            /// <remarks/>
            public uint codigoProductoSin
            {
                get
                {
                    return this.codigoProductoSinField;
                }
                set
                {
                    this.codigoProductoSinField = value;
                }
            }

            /// <remarks/>
            public byte codigoProducto
            {
                get
                {
                    return this.codigoProductoField;
                }
                set
                {
                    this.codigoProductoField = value;
                }
            }

            /// <remarks/>
            public string descripcion
            {
                get
                {
                    return this.descripcionField;
                }
                set
                {
                    this.descripcionField = value;
                }
            }

            /// <remarks/>
            public byte codigoTipoHabitacion
            {
                get
                {
                    return this.codigoTipoHabitacionField;
                }
                set
                {
                    this.codigoTipoHabitacionField = value;
                }
            }

            /// <remarks/>
            public byte cantidad
            {
                get
                {
                    return this.cantidadField;
                }
                set
                {
                    this.cantidadField = value;
                }
            }

            /// <remarks/>
            public byte unidadMedida
            {
                get
                {
                    return this.unidadMedidaField;
                }
                set
                {
                    this.unidadMedidaField = value;
                }
            }

            /// <remarks/>
            public decimal precioUnitario
            {
                get
                {
                    return this.precioUnitarioField;
                }
                set
                {
                    this.precioUnitarioField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object montoDescuento
            {
                get
                {
                    return this.montoDescuentoField;
                }
                set
                {
                    this.montoDescuentoField = value;
                }
            }

            /// <remarks/>
            public decimal subTotal
            {
                get
                {
                    return this.subTotalField;
                }
                set
                {
                    this.subTotalField = value;
                }
            }

            /// <remarks/>
            public string detalleHuespedes
            {
                get
                {
                    return this.detalleHuespedesField;
                }
                set
                {
                    this.detalleHuespedesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
        public partial class Signature
        {
            private SignatureSignedInfo signedInfoField;

            private object signatureValueField;

            private SignatureKeyInfo keyInfoField;

            /// <remarks/>
            public SignatureSignedInfo SignedInfo
            {
                get
                {
                    return this.signedInfoField;
                }
                set
                {
                    this.signedInfoField = value;
                }
            }

            /// <remarks/>
            public object SignatureValue
            {
                get
                {
                    return this.signatureValueField;
                }
                set
                {
                    this.signatureValueField = value;
                }
            }

            /// <remarks/>
            public SignatureKeyInfo KeyInfo
            {
                get
                {
                    return this.keyInfoField;
                }
                set
                {
                    this.keyInfoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public partial class SignatureSignedInfo
        {
            private SignatureSignedInfoCanonicalizationMethod canonicalizationMethodField;

            private SignatureSignedInfoSignatureMethod signatureMethodField;

            private SignatureSignedInfoReference referenceField;

            /// <remarks/>
            public SignatureSignedInfoCanonicalizationMethod CanonicalizationMethod
            {
                get
                {
                    return this.canonicalizationMethodField;
                }
                set
                {
                    this.canonicalizationMethodField = value;
                }
            }

            /// <remarks/>
            public SignatureSignedInfoSignatureMethod SignatureMethod
            {
                get
                {
                    return this.signatureMethodField;
                }
                set
                {
                    this.signatureMethodField = value;
                }
            }

            /// <remarks/>
            public SignatureSignedInfoReference Reference
            {
                get
                {
                    return this.referenceField;
                }
                set
                {
                    this.referenceField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public partial class SignatureSignedInfoCanonicalizationMethod
        {
            private string algorithmField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Algorithm
            {
                get
                {
                    return this.algorithmField;
                }
                set
                {
                    this.algorithmField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public partial class SignatureSignedInfoSignatureMethod
        {
            private string algorithmField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Algorithm
            {
                get
                {
                    return this.algorithmField;
                }
                set
                {
                    this.algorithmField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public partial class SignatureSignedInfoReference
        {
            private SignatureSignedInfoReferenceTransform[] transformsField;

            private SignatureSignedInfoReferenceDigestMethod digestMethodField;

            private object digestValueField;

            private string uRIField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Transform", IsNullable = false)]
            public SignatureSignedInfoReferenceTransform[] Transforms
            {
                get
                {
                    return this.transformsField;
                }
                set
                {
                    this.transformsField = value;
                }
            }

            /// <remarks/>
            public SignatureSignedInfoReferenceDigestMethod DigestMethod
            {
                get
                {
                    return this.digestMethodField;
                }
                set
                {
                    this.digestMethodField = value;
                }
            }

            /// <remarks/>
            public object DigestValue
            {
                get
                {
                    return this.digestValueField;
                }
                set
                {
                    this.digestValueField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string URI
            {
                get
                {
                    return this.uRIField;
                }
                set
                {
                    this.uRIField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public partial class SignatureSignedInfoReferenceTransform
        {
            private string algorithmField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Algorithm
            {
                get
                {
                    return this.algorithmField;
                }
                set
                {
                    this.algorithmField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public partial class SignatureSignedInfoReferenceDigestMethod
        {
            private string algorithmField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Algorithm
            {
                get
                {
                    return this.algorithmField;
                }
                set
                {
                    this.algorithmField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public partial class SignatureKeyInfo
        {
            private SignatureKeyInfoX509Data x509DataField;

            /// <remarks/>
            public SignatureKeyInfoX509Data X509Data
            {
                get
                {
                    return this.x509DataField;
                }
                set
                {
                    this.x509DataField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public partial class SignatureKeyInfoX509Data
        {
            private object x509CertificateField;

            private object x509SubjectNameField;

            private SignatureKeyInfoX509DataX509IssuerSerial x509IssuerSerialField;

            /// <remarks/>
            public object X509Certificate
            {
                get
                {
                    return this.x509CertificateField;
                }
                set
                {
                    this.x509CertificateField = value;
                }
            }

            /// <remarks/>
            public object X509SubjectName
            {
                get
                {
                    return this.x509SubjectNameField;
                }
                set
                {
                    this.x509SubjectNameField = value;
                }
            }

            /// <remarks/>
            public SignatureKeyInfoX509DataX509IssuerSerial X509IssuerSerial
            {
                get
                {
                    return this.x509IssuerSerialField;
                }
                set
                {
                    this.x509IssuerSerialField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public partial class SignatureKeyInfoX509DataX509IssuerSerial
        {
            private byte x509IssuerNameField;

            private byte x509SerialNumberField;

            /// <remarks/>
            public byte X509IssuerName
            {
                get
                {
                    return this.x509IssuerNameField;
                }
                set
                {
                    this.x509IssuerNameField = value;
                }
            }

            /// <remarks/>
            public byte X509SerialNumber
            {
                get
                {
                    return this.x509SerialNumberField;
                }
                set
                {
                    this.x509SerialNumberField = value;
                }
            }
        } 
    }
}
