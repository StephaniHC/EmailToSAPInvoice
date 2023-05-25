
using Avalonia.Controls;
using Avalonia.Layout;
using EmailToSAPInvoice.Service;
using EmailToSAPInvoice.Service.Table;
using EmailToSAPInvoice.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Xml.Linq;

namespace EmailToSAPInvoice.ViewModels
{ 
    public class MainConfigurationCuentaWindowViewModel : ViewModelBase
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
        private List<string> _uIVAcuenta;
        private List<string> _uITcuenta;
        private List<string> _uIUEcuenta;
        private List<string> _uRCIVAcuenta;
        private List<string> _uCTAexento;


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
        public List<string> U_IVAcuenta
        {
            get { return _uIVAcuenta; }
            set
            {
                _uIVAcuenta = value;
                this.RaisePropertyChanged(nameof(U_IVAcuenta));
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
        public List<string> U_ITcuenta
        {
            get { return _uITcuenta; }
            set
            {
                if (_uITcuenta != value)
                {
                    _uITcuenta = value;
                    OnPropertyChanged(nameof(U_ITcuenta));
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
        public List<string> U_IUEcuenta
        {
            get { return _uIUEcuenta; }
            set
            {
                if (_uIUEcuenta != value)
                {
                    _uIUEcuenta = value;
                    OnPropertyChanged(nameof(U_IUEcuenta));
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
        public List<string> U_RCIVAcuenta
        {
            get { return _uRCIVAcuenta; }
            set
            {
                if (_uRCIVAcuenta != value)
                {
                    _uRCIVAcuenta = value;
                    OnPropertyChanged(nameof(U_RCIVAcuenta));
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
        public List<string> U_CTAexento
        {
            get { return _uCTAexento; }
            set
            {
                if (_uCTAexento != value)
                {
                    _uCTAexento = value;
                    OnPropertyChanged(nameof(U_CTAexento));
                }
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
            U_CodPerfil = new List<string>();
            GetPerfiles();
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
        private void PostCuenta() {
            try
            {
               // databaseHandler.InsertCuenta(SelectedCodPerfil, SelectedTipoDoc, );
                CloseWindow?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}
