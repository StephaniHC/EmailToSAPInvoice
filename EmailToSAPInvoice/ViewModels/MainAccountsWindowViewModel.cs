using EmailToSAPInvoice.Service.Table;
using EmailToSAPInvoice.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Windows.Input;
using static EmailToSAPInvoice.ViewModels.MainConfigurationCuentaWindowViewModel;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace EmailToSAPInvoice.ViewModels
{
    public class MainAccountsWindowViewModel : ViewModelBase
    {
        public string Greeting => "Configuración de Cuenta";
        public string LabelDoc => "Tipo de Documento";
        public string LabelAccount => "Cuenta"; 
        public string LabelDebit => "Debit";
        public string LabelCredit => "Credit";
        public string LabelParther => "Card Code";
        public string Opcion1 => "SI";
        public string Opcion2 => "NO";
        public string Button => "Añadir Cuenta";
        public string ButtonBack => "Atras"; 
        private List<string> _docType;
        private string _selectedDocType;
         
        public decimal _debit;
        public decimal _credit;

        private string _selectedCuentaType; 
        public ICommand SaveCommand => new RelayCommand(PostCuenta);
        public ICommand BackCommand => new RelayCommand(Back);
        public event EventHandler CloseWindow;
        private DatabaseHandler databaseHandler = new DatabaseHandler();
        private bool _parther;
        public bool Parther
        {
            get => _parther;
            set => SetProperty(ref _parther, value);
        }
        public string SelectedcuentaType
        {
            get { return _selectedCuentaType; }
            set
            {
                _selectedCuentaType = value;
                this.RaisePropertyChanged(nameof(_selectedCuentaType));
            }
        } 
        public List<string> DocumentoType
        {
            get { return _docType; }
            set
            {
                _docType = value;
                this.RaisePropertyChanged(nameof(DocumentoType));
            }
        }
        public string SelectedDocumento
        {
            get { return _selectedDocType; }
            set
            {
                _selectedDocType = value;
                this.RaisePropertyChanged(nameof(_selectedDocType));
            }
        }
        public decimal Credit
        {
            get { return _credit; }
            set
            {
                if (_credit != value)
                {
                    _credit = value;
                    OnPropertyChanged(nameof(Credit));
                }
            }
        }
        public decimal Debit
        {
            get { return _debit; }
            set
            {
                if (_debit != value)
                {
                    _debit = value;
                    OnPropertyChanged(nameof(Debit));
                }
            }
        }
        private ObservableCollection<CuentaResult> _resultCuentas;
        public ObservableCollection<CuentaResult> ResultCuentas
        {
            get { return _resultCuentas; }
            set
            {
                _resultCuentas = value;
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
        public MainAccountsWindowViewModel()
        {
            DocumentoType = new List<string>()
            {
                Accounts.TipoDocCompra,
                Accounts.TipoDocServicioBasico,
                Accounts.TipoDocServicioTuristico
            };
            databaseHandler = new DatabaseHandler();
            ResultCuentas = new ObservableCollection<CuentaResult>();
            GetData(); 
            nameToCode = new Dictionary<string, string>();
            nameToFormatCode = new Dictionary<string, string>(); 
            U_config = new List<List<string>>();
            Code = new List<string>();
            NameConfig = new ObservableCollection<string>();  
            _ = GetConfigurationCuentasAsync();
        }
        private void PostCuenta()
        {
            try
            {
                var selectedCuenta = nameToCode[SelectedcuentaType]; 
                databaseHandler.InsertAccount(SelectedDocumento,selectedCuenta, Credit, Debit, Parther);
                CloseWindow?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Back()
        {
            CloseWindow?.Invoke(this, EventArgs.Empty);
        }
        public class CuentaResult
        {
            public string TipoDoc { get; set; }
            public string Account { get; set; }
            public decimal Credit { get; set; }
            public decimal Debit { get; set; }
            public bool BusinessPartners { get; set; } 
        }
        private void GetData()
        {
            var cuenta = databaseHandler.GetAllAccounts();
            foreach (Accounts cuentas in cuenta)
            {
                var resultado = new CuentaResult
                {
                    TipoDoc = cuentas.TipoDoc.ToString(),
                    Account = cuentas.Account.ToString(),
                    Credit = cuentas.Credit,
                    Debit = cuentas.Debit,
                    BusinessPartners = cuentas.BusinessPartners,
                };
                ResultCuentas.Add(resultado);
            }
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
    }
}
