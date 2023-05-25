
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using DynamicData;
using EmailToSAPInvoice.Service;
using EmailToSAPInvoice.Service.Table;
using EmailToSAPInvoice.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using static EmailToSAPInvoice.ViewModels.MainConfigurationPerfilWindowViewModel;
using static System.Net.Mime.MediaTypeNames;

namespace EmailToSAPInvoice.ViewModels
{ 
    public class MainConfigurationCuentaWindowViewModel : ViewModelBase
    {
        public string Greeting => "Configuración de Documentos";
        public string LabelPerfil => "Perfil";
        public string LabelIva => "% IVA";
        public string LabelIue => "% IUE";
        public string LabelExento => "% Exento";
        public string LabelDocumento => "Documento";
        public string LabelCuentaIva => "Cuenta IVA";
        public string LabelCuentaIue => "Cuenta IUE";
        public string LabelCuentaExento => "Cuenta Exento";
        public string LabelTipoDoc => "Tipo de Documento SAP";
        public string LabelIt => "% IT";
        public string LabelRcIva => "% RC-IVA";
        public string LabelTasa => "% Tasa";
        public string LabelTipoCalculo => "Tipo de Calculo";
        public string LabelCuentaIt => "Cuenta IT";
        public string LabelCuentaRcIva => "Cuenta RC-IVA";
        public string Button => "Añadir Cuenta";
        public string ButtonBack => "Atras";

        public string _uTipDoc;
        public decimal _uEXENTOpercent; 
        public decimal _uIVApercent; 
        public decimal _uITpercent; 
        public decimal _uIUEpercent; 
        public decimal _uRCIVApercent; 
        public decimal _uTASA;
        private List<string> _uCodPerfil;
        private List<string> _uIdTipoDoc;
        private List<string> _uTipoCalc; 

        private string _selectedCodPerfil; 
        private string _selectedTipoDoc;
        private string _selectedTipoCalc;
        private string _selectedIVAcuenta;
        private string _selectedITcuenta;
        private string _selectedIUEcuenta;
        private string _selectedRCIVAcuenta;
        private string _selectedCTAexento;
        public List<string> U_IdTipoDoc
        {
            get { return _uIdTipoDoc; }
            set
            {
                _uIdTipoDoc = value;
                this.RaisePropertyChanged(nameof(U_IdTipoDoc));
            }
        }
        public string SelectedTipoDoc
        {
            get { return _selectedTipoDoc; }
            set
            {
                _selectedTipoDoc = value;
                this.RaisePropertyChanged(nameof(_selectedTipoDoc));
            }
        }
        private List<string> U_CodPerfil
        {
            get { return _uCodPerfil; }
            set
            {
                if (_uCodPerfil != value)
                {
                    _uCodPerfil = value;
                    OnPropertyChanged(nameof(U_CodPerfil));
                }
            }
        }
        public string SelectedCodPerfil
        {
            get { return _selectedCodPerfil; }
            set
            {
                _selectedCodPerfil = value;
                this.RaisePropertyChanged(nameof(_selectedCodPerfil));
            }
        }
        public string U_TipDoc
        {
            get { return _uTipDoc; }
            set
            {
                if (_uTipDoc != value)
                {
                    _uTipDoc = value;
                    OnPropertyChanged(nameof(U_TipDoc));
                }
            }
        }
        public decimal U_EXENTOpercent
        {
            get { return _uEXENTOpercent; }
            set
            {
                if (_uEXENTOpercent != value)
                {
                    _uEXENTOpercent = value;
                    OnPropertyChanged(nameof(U_EXENTOpercent));
                }
            }
        }
        public List<string> U_TipoCalc
        {
            get { return _uTipoCalc; }
            set
            {
                _uTipoCalc = value;
                this.RaisePropertyChanged(nameof(U_TipoCalc));
            }
        }
        public string SelectedTipoCalc
        {
            get { return _selectedTipoCalc; }
            set
            {
                _selectedTipoCalc = value;
                this.RaisePropertyChanged(nameof(_selectedTipoCalc));
            }
        }
        public decimal U_IVApercent
        {
            get { return _uIVApercent; }
            set
            {
                _uIVApercent = value;
                this.RaisePropertyChanged(nameof(U_IdTipoDoc));
            }
        }  
        public string SelectedIVAcuenta
        {
            get { return _selectedIVAcuenta; }
            set
            {
                _selectedIVAcuenta = value;
                this.RaisePropertyChanged(nameof(_selectedIVAcuenta));
            }
        }
        public decimal U_ITpercent
        {
            get { return _uITpercent; }
            set
            {
                if (_uITpercent != value)
                {
                    _uITpercent = value;
                    OnPropertyChanged(nameof(U_ITpercent));
                }
            }
        } 
        public string SelectedITcuenta
        {
            get { return _selectedITcuenta; }
            set
            {
                _selectedITcuenta = value;
                this.RaisePropertyChanged(nameof(_selectedITcuenta));
            }
        }
        public decimal U_IUEpercent
        {
            get { return _uIUEpercent; }
            set
            {
                if (_uIUEpercent != value)
                {
                    _uIUEpercent = value;
                    OnPropertyChanged(nameof(U_IUEpercent));
                }
            }
        } 
        public string SelectedIUEcuenta
        {
            get { return _selectedIUEcuenta; }
            set
            {
                _selectedIUEcuenta = value;
                this.RaisePropertyChanged(nameof(_selectedIUEcuenta));
            }
        }
        public decimal U_RCIVApercent
        {
            get { return _uRCIVApercent; }
            set
            {
                if (_uRCIVApercent != value)
                {
                    _uRCIVApercent = value;
                    OnPropertyChanged(nameof(U_RCIVApercent));
                }
            }
        } 
        public string SelectedRCIVAcuenta
        {
            get { return _selectedRCIVAcuenta; }
            set
            {
                _selectedRCIVAcuenta = value;
                this.RaisePropertyChanged(nameof(_selectedRCIVAcuenta));
            }
        } 
        public string SelectedCTAexento
        {
            get { return _selectedCTAexento; }
            set
            {
                _selectedCTAexento = value;
                this.RaisePropertyChanged(nameof(_selectedCTAexento));
            }
        }
        public decimal U_TASA
        {
            get { return _uTASA; }
            set
            {
                if (_uTASA != value)
                {
                    _uTASA = value;
                    OnPropertyChanged(nameof(U_TASA));
                }
            }
        }
        public ICommand GoToConfigPerfilWindow { get; set; } 
        public ICommand SaveCommandCuenta => new RelayCommand(PostCuenta);
        public ICommand BackCommand => new RelayCommand(Back);
        public event EventHandler CloseWindow; 
        private DatabaseHandler databaseHandler = new DatabaseHandler();

        private ObservableCollection<CuentaResult> _resultCuenta;
        public ObservableCollection<CuentaResult> ResultCuenta
        {
            get { return _resultCuenta; }
            set
            {
                _resultCuenta = value;
            }
        }

        private SAPServiceLayerConnection sapService;

        List<List<string>> U_config;
        List<string> Code; 
        public string _selected;
        private ObservableCollection<string> _nameconfig;
        public ObservableCollection<string> NameConfig
        {
            get { return _nameconfig; }
            set
            {
                if (_nameconfig != value)
                {
                    _nameconfig = value;
                    OnPropertyChanged(nameof(NameConfig));
                }
            }
        }
        private Dictionary<string, string> nameToCode;
        private Dictionary<string, string> nameToFormatCode;
        public MainConfigurationCuentaWindowViewModel()
        {
            GoToConfigPerfilWindow = ReactiveCommand.Create(() =>
            {
                var configurationPerfilWindow = new MainConfigurationPerfilWindow();
                configurationPerfilWindow.Show();
            });
            U_IdTipoDoc = new List<string>()
            {
                Rcuenta.TipoDocCompra,
                Rcuenta.TipoDocServicioBasico,
                Rcuenta.TipoDocServicioTuristico
            };
            U_TipoCalc = new List<string>
            {
                Rcuenta.TipoCalculoUp,
                Rcuenta.TipoCalculoDown
            }; 
            nameToCode = new Dictionary<string, string>();
            nameToFormatCode = new Dictionary<string, string>();
            U_CodPerfil = new List<string>();
            ResultCuenta = new ObservableCollection<CuentaResult>(); 
            U_config = new List<List<string>>();
            Code = new List<string>();
            NameConfig = new ObservableCollection<string>() { null };
            GetPerfiles();
            GetData();
            _ = GetConfigurationCuentasAsync();
        } 

        private void GetPerfiles()
        {
            var perfiles = databaseHandler.GetAllPerfil();

            foreach (var perfil in perfiles)
            {
                U_CodPerfil.Add(perfil.U_NombrePerfil);
            }
        }

        private void Back()
        {
            CloseWindow?.Invoke(this, EventArgs.Empty);
        }

        public async Task GetConfigurationCuentasAsync()
        {
            try
            {
                sapService = new SAPServiceLayerConnection();
                U_config = await sapService.GetConfiguration(); 
                foreach (List<string> accountData in U_config)
                {
                    string code = accountData[0];
                    string name = accountData[1] + " - " + accountData[2]; 
                    Code.Add(code);
                    NameConfig.Add(name); 
                    nameToCode.Add(name, code);
                    nameToFormatCode[name] = accountData[1];
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Excepción al obtener la configuracion de cuentas: {e.Message}");
            }
        }

        private void PostCuenta()
        {
            try
            {
                string selectedIVAcuentaCode = null;
                string selectedITcuentaCode = null;
                string selectedIUEcuentaCode = null;
                string selectedRCIVAcuentaCode = null;
                string selectedCTAexentoCode = null;

                if (SelectedIVAcuenta != null)
                {
                    selectedIVAcuentaCode = nameToFormatCode[SelectedIVAcuenta];
                }
                if (SelectedITcuenta != null)
                {
                    selectedITcuentaCode = nameToFormatCode[SelectedITcuenta];
                }
                if (SelectedIUEcuenta != null)
                {
                    selectedIUEcuentaCode = nameToFormatCode[SelectedIUEcuenta];
                }
                if (SelectedRCIVAcuenta != null)
                {
                    selectedRCIVAcuentaCode = nameToFormatCode[SelectedRCIVAcuenta];
                }
                if (SelectedCTAexento != null)
                {
                    selectedCTAexentoCode = nameToFormatCode[SelectedCTAexento];
                }

                _ = databaseHandler.InsertCuenta(SelectedCodPerfil, _uTipDoc, _uEXENTOpercent, SelectedTipoDoc, SelectedTipoCalc, _uIVApercent,
                                        selectedIVAcuentaCode, _uITpercent, selectedITcuentaCode, _uIUEpercent, selectedIUEcuentaCode,
                                        _uRCIVApercent, selectedRCIVAcuentaCode, selectedCTAexentoCode, _uTASA);
                CloseWindow?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public class CuentaResult
        {
            public string U_IdDocumento { get; set; }
            public string U_CodPerfil { get; set; }
            public string U_TipDoc { get; set; }
            public string U_EXENTOpercent { get; set; }
            public string U_IdTipoDoc { get; set; }
            public string U_TipoCalc { get; set; }
            public string U_IVApercent { get; set; }
            public string U_IVAcuenta { get; set; }
            public string U_ITpercent { get; set; }
            public string U_ITcuenta { get; set; }
            public string U_IUEpercent { get; set; }
            public string U_IUEcuenta { get; set; }
            public string U_RCIVApercent { get; set; }
            public string U_RCIVAcuenta { get; set; }
            public string U_CTAexento { get; set; }
            public string U_TASA { get; set; }
        }
        private void GetData()
        {
            var cuenta = databaseHandler.GetAllCuenta();
            foreach (Rcuenta cuentas in cuenta)
            {
                var resultado = new CuentaResult
                {
                    U_IdDocumento = cuentas.U_IdDocumento.ToString(),
                    U_CodPerfil = cuentas.U_CodPerfil,
                    U_TipDoc = cuentas.U_TipDoc,
                    U_EXENTOpercent = cuentas.U_EXENTOpercent.ToString(),
                    U_IdTipoDoc = cuentas.U_IdTipoDoc,
                    U_TipoCalc = cuentas.U_TipoCalc,
                    U_IVApercent = cuentas.U_IVApercent.ToString(),
                    U_IVAcuenta = cuentas.U_IVAcuenta,
                    U_ITpercent = cuentas.U_ITpercent.ToString(),
                    U_ITcuenta = cuentas.U_ITcuenta,
                    U_IUEpercent = cuentas.U_IUEpercent.ToString(),
                    U_IUEcuenta = cuentas.U_IUEcuenta,
                    U_RCIVApercent = cuentas.U_RCIVApercent.ToString(),
                    U_RCIVAcuenta = cuentas.U_RCIVAcuenta,
                    U_CTAexento = cuentas.U_CTAexento,
                    U_TASA = cuentas.U_TASA.ToString()

                };
                ResultCuenta.Add(resultado);
            }
        }
    }
}
