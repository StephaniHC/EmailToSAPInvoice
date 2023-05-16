using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailToSAPInvoice.Models
{
    public class FacturaServicioBasico
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class facturaElectronicaServicioBasico
        {
            public object identifier { get; set; }

            private facturaElectronicaServicioBasicoCabecera cabeceraField;

            private facturaElectronicaServicioBasicoDetalle detalleField;

            private Signature signatureField;

            /// <remarks/>
            public facturaElectronicaServicioBasicoCabecera cabecera
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
            public facturaElectronicaServicioBasicoDetalle detalle
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
        public partial class facturaElectronicaServicioBasicoCabecera
        {

            private uint nitEmisorField;

            private string razonSocialEmisorField;

            private string municipioField;

            private uint telefonoField;

            private uint numeroFacturaField;

            private string cufField;

            private string cufdField;

            private byte codigoSucursalField;

            private string direccionField;

            private object codigoPuntoVentaField;

            private string mesField;

            private ushort gestionField;

            private string ciudadField;

            private string zonaField;

            private string numeroMedidorField;

            private System.DateTime fechaEmisionField;

            private string nombreRazonSocialField;

            private string domicilioClienteField;

            private byte codigoTipoDocumentoIdentidadField;

            private uint numeroDocumentoField;

            private object complementoField;

            private string codigoClienteField;

            private byte codigoMetodoPagoField;

            private object numeroTarjetaField;

            private decimal montoTotalField;

            private decimal montoTotalSujetoIvaField;

            private ushort consumoPeriodoField;

            private object beneficiarioLey1886Field;

            private object montoDescuentoLey1886Field;

            private object montoDescuentoTarifaDignidadField;

            private decimal tasaAseoField;

            private decimal tasaAlumbradoField;

            private object ajusteNoSujetoIvaField;

            private object detalleAjusteNoSujetoIvaField;

            private object ajusteSujetoIvaField;

            private object detalleAjusteSujetoIvaField;

            private object otrosPagosNoSujetoIvaField;

            private object detalleOtrosPagosNoSujetoIvaField;

            private object otrasTasasField;
            private byte codigoMonedaField;

            private decimal tipoCambioField;

            private decimal montoTotalMonedaField;

            private object descuentoAdicionalField;

            private object codigoExcepcionField;

            private object cafcField;
            private string leyendaField;

            private string usuarioField;

            private byte codigoDocumentoSectorField;

            /// <remarks/>
            public uint nitEmisor
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
            public uint numeroFactura
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
            public string mes
            {
                get
                {
                    return this.mesField;
                }
                set
                {
                    this.mesField = value;
                }
            }

            /// <remarks/>
            public ushort gestion
            {
                get
                {
                    return this.gestionField;
                }
                set
                {
                    this.gestionField = value;
                }
            }

            /// <remarks/>
            public string ciudad
            {
                get
                {
                    return this.ciudadField;
                }
                set
                {
                    this.ciudadField = value;
                }
            }

            /// <remarks/>
            public string zona
            {
                get
                {
                    return this.zonaField;
                }
                set
                {
                    this.zonaField = value;
                }
            }

            /// <remarks/>
            public string numeroMedidor
            {
                get
                {
                    return this.numeroMedidorField;
                }
                set
                {
                    this.numeroMedidorField = value;
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
            public string domicilioCliente
            {
                get
                {
                    return this.domicilioClienteField;
                }
                set
                {
                    this.domicilioClienteField = value;
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
            public uint numeroDocumento
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
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object numeroTarjeta
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
            public decimal montoTotalSujetoIva
            {
                get
                {
                    return this.montoTotalSujetoIvaField;
                }
                set
                {
                    this.montoTotalSujetoIvaField = value;
                }
            }

            /// <remarks/>
            public ushort consumoPeriodo
            {
                get
                {
                    return this.consumoPeriodoField;
                }
                set
                {
                    this.consumoPeriodoField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object beneficiarioLey1886
            {
                get
                {
                    return this.beneficiarioLey1886Field;
                }
                set
                {
                    this.beneficiarioLey1886Field = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object montoDescuentoLey1886
            {
                get
                {
                    return this.montoDescuentoLey1886Field;
                }
                set
                {
                    this.montoDescuentoLey1886Field = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object montoDescuentoTarifaDignidad
            {
                get
                {
                    return this.montoDescuentoTarifaDignidadField;
                }
                set
                {
                    this.montoDescuentoTarifaDignidadField = value;
                }
            }

            /// <remarks/>
            public decimal tasaAseo
            {
                get
                {
                    return this.tasaAseoField;
                }
                set
                {
                    this.tasaAseoField = value;
                }
            }

            /// <remarks/>
            public decimal tasaAlumbrado
            {
                get
                {
                    return this.tasaAlumbradoField;
                }
                set
                {
                    this.tasaAlumbradoField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object ajusteNoSujetoIva
            {
                get
                {
                    return this.ajusteNoSujetoIvaField;
                }
                set
                {
                    this.ajusteNoSujetoIvaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object detalleAjusteNoSujetoIva
            {
                get
                {
                    return this.detalleAjusteNoSujetoIvaField;
                }
                set
                {
                    this.detalleAjusteNoSujetoIvaField = value;
                }
            }

            /// <remarks/>
            public object ajusteSujetoIva
            {
                get
                {
                    return this.ajusteSujetoIvaField;
                }
                set
                {
                    this.ajusteSujetoIvaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object detalleAjusteSujetoIva
            {
                get
                {
                    return this.detalleAjusteSujetoIvaField;
                }
                set
                {
                    this.detalleAjusteSujetoIvaField = value;
                }
            }
            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object otrosPagosNoSujetoIva
            {
                get
                {
                    return this.otrosPagosNoSujetoIvaField;
                }
                set
                {
                    this.otrosPagosNoSujetoIvaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object detalleOtrosPagosNoSujetoIva
            {
                get
                {
                    return this.detalleOtrosPagosNoSujetoIvaField;
                }
                set
                {
                    this.detalleOtrosPagosNoSujetoIvaField = value;
                }
            }

            /// <remarks/>
            public object otrasTasas
            {
                get
                {
                    return this.otrasTasasField;
                }
                set
                {
                    this.otrasTasasField = value;
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
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object descuentoAdicional
            {
                get
                {
                    return this.descuentoAdicionalField;
                }
                set
                {
                    this.descuentoAdicionalField = value;
                }
            }

            /// <remarks/>
            public object codigoExcepcion
            {
                get
                {
                    return this.codigoExcepcionField;
                }
                set
                {
                    this.codigoExcepcionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
            public object cafc
            {
                get
                {
                    return this.cafcField;
                }
                set
                {
                    this.cafcField = value;
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
        public partial class facturaElectronicaServicioBasicoDetalle
        {

            private uint actividadEconomicaField;

            private uint codigoProductoSinField;

            private string codigoProductoField;

            private string descripcionField;

            private byte cantidadField;

            private byte unidadMedidaField;

            private decimal precioUnitarioField;

            private object montoDescuentoField;

            private decimal subTotalField;

            /// <remarks/>
            public uint actividadEconomica
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
            public string codigoProducto
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

            private object x509IssuerNameField;

            private object x509SerialNumberField;

            /// <remarks/>
            public object X509IssuerName
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
            public object X509SerialNumber
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
