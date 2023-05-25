using DynamicData;
using EmailToSAPInvoice.Models;
using EmailToSAPInvoice.Service;
using EmailToSAPInvoice.Service.Table;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static EmailToSAPInvoice.Models.Imbox;
using static EmailToSAPInvoice.ViewModels.MainWindowViewModel;
using static SQLite.SQLite3;

namespace EmailToSAPInvoice.ViewModels
{
    public class MainConfigurationPerfilWindowViewModel : ViewModelBase
    {
        public string Greeting => "Configuración de Perfiles";
        public string LabelPerfil => "Nombre de Perfil"; 
        public string LabelUTrabaja => "Perfil Trabaja";
        public string Button => "Añadir Perfil";
        public string ButtonBack => "Atras";
        private string _perfil;
        private List<string> _perfilTrabaja;
        private string _selectedPerfilTrabaja;
        public ICommand SaveCommandPerfil => new RelayCommand(PostPerfil);
        public ICommand BackCommand => new RelayCommand(Back);
        public event EventHandler CloseWindow; 
        private DatabaseHandler databaseHandler = new DatabaseHandler();
        public string Perfil
        {
            get { return _perfil; }
            set
            {
                if (_perfil != value)
                {
                    _perfil = value;
                    OnPropertyChanged(nameof(Perfil));
                }
            }
        }
        public List<string> PerfilTrabaja
        {
            get { return _perfilTrabaja; }
            set
            {
                _perfilTrabaja = value;
                this.RaisePropertyChanged(nameof(PerfilTrabaja));
            }
        }
        public string SelectedPerfilTrabaja
        {
            get { return _selectedPerfilTrabaja; }
            set
            {
                _selectedPerfilTrabaja = value;
                this.RaisePropertyChanged(nameof(_selectedPerfilTrabaja));
            }
        }
        private ObservableCollection<PerfilResult> _resultPerfil;
        public ObservableCollection<PerfilResult> ResultPerfil
        {
            get { return _resultPerfil; }
            set
            {
                _resultPerfil = value;
            }
        }
        public MainConfigurationPerfilWindowViewModel()
        {
            PerfilTrabaja = new List<string>()
            {
                Rperfil.UMonedaLocal,
                Rperfil.UMonedaSistema
            };
            databaseHandler = new DatabaseHandler();
            ResultPerfil = new ObservableCollection<PerfilResult>();
            GetData();
        }
        private void PostPerfil()
        {
            try
            {
                databaseHandler.InsertPerfil(Perfil, SelectedPerfilTrabaja); 
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
        public class PerfilResult
        { 
            public string U_CodPerfil { get; set; }
            public string U_NombrePerfil { get; set; }
            public string U_Trabaja { get; set; } 
        }
        private void GetData()
        {
            var perfil = databaseHandler.GetAllPerfil();
            foreach (Rperfil perfiles in perfil)
            { 
                var resultado = new PerfilResult
                {
                    U_CodPerfil = perfiles.U_CodPerfil.ToString(),
                    U_NombrePerfil = perfiles.U_NombrePerfil,
                    U_Trabaja = perfiles.U_Trabaja, 
                };
                ResultPerfil.Add(resultado);
            }
        }
    }
} 