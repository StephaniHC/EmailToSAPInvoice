using EmailToSAPInvoice.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using static EmailToSAPInvoice.Models.Imbox;
using System.Text.Json;
using System.Windows.Input;
using EmailToSAPInvoice.Views;
using System.IO; 


namespace EmailToSAPInvoice.ViewModels
{
    internal class MainUserEmailWindowViewModel : ViewModelBase
    {
        public string ButtonSave => "Guardar";
        public string ButtonBack => "Atras";
        public string LabelEmail { get; set; } = "Correo";
        public string LabelPassword { get; set; } = "Contraseña";
        public string LabelProvider { get; set; } = "Proveedor";
        public string Tittle0 => "Registrar Correo";
        public string ButtonAddProvider => "Añadir Proveedor";
        public List<string> ListProveedores { get; set; } = new List<string>();
        public ICommand GoToFloatingWindow { get; }
        private string _email;
        private string _password;
        private string _proveedor;
        public string rutaData { get; set; } = "../../../Data.json";
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        public string Proveedor
        {
            get => _proveedor;
            set => SetProperty(ref _proveedor, value);
        }
        public ICommand SaveCommand => new RelayCommand(PostCliente);
        public ICommand BackCommand => new RelayCommand(Back);

        public event EventHandler CloseWindow;
        public MainUserEmailWindowViewModel()
        {
            GoToFloatingWindow = ReactiveCommand.Create(() =>
            {
                var userEmailWindow = new MainUserEmailWindow();
                userEmailWindow.Show();
            });
            GetListaProveedores();
        }
        private void Back()
        {
            CloseWindow?.Invoke(this, EventArgs.Empty);
        }

        private void GetListaProveedores()
        {
            string json = File.ReadAllText(rutaData);
            Console.WriteLine(json);
            Imbox inbox = JsonSerializer.Deserialize<Imbox>(json);
            foreach (var proveedor in inbox.Proveedores)
                ListProveedores.Add(proveedor.Nombre);
        }

        private void PostCliente()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Proveedor))
            {
                return;
            }
            try
            {
                string jsonText = File.ReadAllText(rutaData);
                Imbox imbox = JsonSerializer.Deserialize<Imbox>(jsonText);
                foreach (var proveedor in imbox.Proveedores)
                {
                    if (proveedor.Nombre == Proveedor)
                    {
                        var nuevoCliente = new Cliente
                        {
                            Email = Email,
                            Password = Password
                        };
                        proveedor.Clientes.Add(nuevoCliente);
                        string nuevoJsonText = JsonSerializer.Serialize(imbox);
                        File.WriteAllText(rutaData, nuevoJsonText);
                        CloseWindow?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new JsonException("Error al deserializar el archivo JSON. " + ex.Message, ex);

            }
        }
    }
}
