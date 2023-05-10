using EmailToSAPInvoice.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static EmailToSAPInvoice.Models.Imbox;
using System.Text.Json;
using System.Windows.Input;
using System.IO;

namespace EmailToSAPInvoice.ViewModels
{
    internal class MainProviderWindowViewModel : ViewModelBase
    {
        public string ButtonBack => "Atras";
        public string LabelName => "Nombre";
        public string LabelMethod => "Metodo";
        public string LabelPort => "Puerto";
        public string LabelURL => "URL";
        public string Opcion1 => "POP3";
        public string Opcion2 => "IMAP";
        public string Tittle1 => "Registrar Proveedor";
        public string ButtonSave => "Guardar"; public string LabelTittle => "Lista de Registrados";
        public List<string> Method { get; set; } = new List<string>();
        public List<Metodo> Metodos { get; set; } = new List<Metodo>();
        private string _nombre;
        private string _nombremetodo;
        private string _url;
        private int _puerto;
        public string rutaData { get; set; } = "../../../Data.json";
        public string Nombre
        {
            get => _nombre;
            set => SetProperty(ref _nombre, value);
        }
        public string NombreMetodo
        {
            get => _nombremetodo;
            set => SetProperty(ref _nombremetodo, value);
        }
        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }
        public int Puerto
        {
            get => _puerto;
            set => SetProperty(ref _puerto, value);
        }
        public ICommand SaveCommand => new RelayCommand(PostProveedores);
        public ICommand BackCommand => new RelayCommand(Back);

        public event EventHandler CloseWindow;
        public MainProviderWindowViewModel()
        {

        }
        private void Back()
        {
            CloseWindow?.Invoke(this, EventArgs.Empty);
        }

        private void PostProveedores()
        {
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Url) || Puerto == 0 || Puerto < 0 || string.IsNullOrEmpty(NombreMetodo))
            {
                return;
            }
            try
            {
                string jsonText = File.ReadAllText(rutaData);
                var proveedores = JsonSerializer.Deserialize<Imbox>(jsonText);
                var nuevoProveedor = new Proveedor
                {
                    Nombre = Nombre,
                    Metodos = new List<Metodo>
                    {
                        new Metodo
                        {
                            Nombre = NombreMetodo,
                            Url = Url,
                            Puerto = Puerto
                        }
                    },
                    Clientes = new List<Cliente>
                    { }
                };
                proveedores.Proveedores.Add(nuevoProveedor);
                string nuevoJsonText = JsonSerializer.Serialize(proveedores);
                File.WriteAllText(rutaData, nuevoJsonText);
                CloseWindow?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                throw new JsonException("Error al deserializar el archivo JSON. " + ex.Message, ex);

            }
        }
    }
}
