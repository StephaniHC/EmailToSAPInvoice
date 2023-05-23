using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailToSAPInvoice.ViewModels
{ 
    public class MainConfigurationWindowViewModel : ViewModelBase
    {
        public string Greeting => "Configuración de Documentos";
        public string LabelPerfil => "Perfil";
        public string LabelIva => "% IVA";
        public string LabelIue => "% IUE";
        public string LabelExento => "(2)% Exento";
        public string LabelDocumento => "Documento";
        public string LabelCuentaIva => "Cuenta IVA";
        public string LabelCuentaIue => "Cuenta IUE";
        public string LabelCuentaExento => "Cuenta Exento";
        public string LabelTipoDoc => "Tipo Doc SAP";
        public string LabelIt => "% IT";
        public string LabelRcIva => "% RC-IVA";
        public string LabelTasa => "(2)% Tasa";
        public string LabelTipoCalculo => "(1) Tipo de Calculo";
        public string LabelCuentaIt => "Cuenta IT";
        public string LabelCuentaRcIva => "Cuenta RC-IVA";
        public string _perfil;
        public string _iva;
        public string _iue;
        public string _exento;
        public string _documento;
        public string _cuentaiva;
        public string _cuentaiue;
        public string _cuentaexento;
        public string _tipodoc;
        public string _it;
        public string _rciva;
        public string _tasa;
        public string _tipocalculo;
        public string _cuentait;
        public string _cuentarciva;

        public string Perfil
        {
            get => _perfil;
            set => SetProperty(ref _perfil, value);
        } 
        public string Iva
        {
            get => _iva; 
            set => SetProperty(ref _iva, value);
        }
        public string Iue
        {
            get => _iue;
            set => SetProperty(ref _iue, value);
        }
        public string Exento
        {
            get => _exento;
            set => SetProperty(ref _exento, value);
        } 
        public string Documento
        {
            get => _documento;
            set => SetProperty(ref _documento, value);
        }
        public string CuentaIva
        {
            get => _cuentaiva;
            set => SetProperty(ref _cuentaiva, value);
        }
        public string CuentaIue
        {
            get => _cuentaiue;
            set => SetProperty(ref _cuentaiue, value);
        }
        public string CuentaExento
        {
            get => _cuentaexento;
            set => SetProperty(ref _cuentaexento, value);
        }
        public string TipoDoc
        {
            get => _tipodoc;
            set => SetProperty(ref _tipodoc, value);
        }
        public string It
        {
            get => _it;
            set => SetProperty(ref _it, value);
        }
        public string RcIva
        {
            get => _rciva;
            set => SetProperty(ref _rciva, value);
        } 
        public string Tasa
        {
            get => _tasa;
            set => SetProperty(ref _tasa, value);
        }
        public string TipoCalculo
        {
            get => _tipocalculo;
            set => SetProperty(ref _tipocalculo, value);
        }
        public string CuentaIt
        {
            get => _cuentait;
            set => SetProperty(ref _cuentait, value);
        }
        public string CuentaRcIva
        {
            get => _cuentarciva;
            set => SetProperty(ref _cuentarciva, value);
        }

        public List<string> ComboBox1Items { get; } = new List<string> { "Opción 1", "Opción 2", "Opción 3" };
        public List<string> ComboBox2Items { get; } = new List<string> { "Opción 4", "Opción 5", "Opción 6" };
        public List<string> ComboBox3Items { get; } = new List<string> { "Opción 7", "Opción 8", "Opción 9" };
        public List<string> ComboBox4Items { get; } = new List<string> { "Opción 10", "Opción 11", "Opción 12" };

        // Aquí puedes agregar más propiedades como cadenas de texto para los TextBoxes y los Títulos.
    }

}
