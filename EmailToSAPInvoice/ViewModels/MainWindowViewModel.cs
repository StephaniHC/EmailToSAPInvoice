using System.Linq;
using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MailKit.Net.Imap;
using MailKit; 
using System.Xml;
using System.Data;
using EmailToSAPInvoice.Service.Table;
using EmailToSAPInvoice.Service;
using System.Windows.Input;
using EmailToSAPInvoice.Models;
using MimeKit;
using MailKit.Net.Pop3;
using ReactiveUI;
using EmailToSAPInvoice.Views;
using System.Xml.Serialization;
using DynamicData;
using MailKit.Search;

namespace EmailToSAPInvoice.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Monitor de Correos";
        public string Result { get; set; } = "read emails only xml";
        public string ButtonAddEmail => "Añadir Correos";
        public string ButtonAddJournalEntries => "Configuracion Cuentas";
        public string ButtonRead => "Actualizar";
        public string LabelTittle => "Lista de Correos Registrados";
        public List<string> ResultEmails { get; set; } = new List<string>();
        public List<List<string>> dataReadEmail { get; set; } = new List<List<string>>();
        public IReadOnlyList<IReadOnlyList<string>> DataPop => dataReadEmail.AsReadOnly();
        public ObservableCollection<Datas> DatasEmailList { get; set; } = new ObservableCollection<Datas>();
        public ICommand GoToSecondWindow { get; set; }
        public ICommand DownloadXmlAttachmentsCommand => new RelayCommand(SetAttachments);
        public ICommand GoToConfigWindow { get; set; }
        public string rutaData { get; set; }
        public string rutaDownload { get; set; }
        public Route Rutas { get; set; }
        private DatabaseHandler databaseHandler = new DatabaseHandler();
        private ObservableCollection<EmailResult> _resultE;
        public ObservableCollection<EmailResult> ResultE
        {
            get { return _resultE; }
            set
            {
                _resultE = value;
            }
        }

        private SAPServiceLayerConnection sapService;

        private List<FacturaBase> listInvoice;
        public MainWindowViewModel()
        {
            GoToSecondWindow = ReactiveCommand.Create(() =>
            {
                var userEmailWindow = new MainUserEmailWindow();
                userEmailWindow.Show();
            });
            GetRutas();
            GetListaRegistrados();
            GetDatosEmail();
            Rutas = new Route();
            databaseHandler = new DatabaseHandler();
            ResultE = new ObservableCollection<EmailResult>();
            GetData();
            listInvoice = new List<FacturaBase>();
            GoToConfigWindow = ReactiveCommand.Create(() =>
            {
                var configurationCuentaWindow = new MainConfigurationCuentaWindow();
                configurationCuentaWindow.Show();
            });
        }

        private void GetRutas()
        {
            var directory = "Ruta.json";
            if (File.Exists(directory))
            {
                string json = File.ReadAllText(directory);
                Route ruta = JsonSerializer.Deserialize<Route>(json);
                rutaData = ruta.Read;
                rutaDownload = ruta.Download;
            }
            else
            {
                rutaData = "../../../Data.json";
                rutaDownload = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;
            }
        }
        private void GetListaRegistrados()
        {
            string json = File.ReadAllText(rutaData);
            Imbox inbox = JsonSerializer.Deserialize<Imbox>(json);
            foreach (var proveedor in inbox.Proveedores)
                foreach (var clientes in proveedor.Clientes)
                    ResultEmails.Add(clientes.Email);
        }
        public class EmailResult
        {
            public string Date { get; set; }
            public string Subject { get; set; }
            public string Attached { get; set; }
            public string Status { get; set; }
        }

        private void GetData()
        {
            var datas = databaseHandler.GetAllDatasEmail();
            foreach (Datas datasc in datas)
            {
                DateTime fechaHora = DateTime.Parse(datasc.Date);
                string fechaFormateada = fechaHora.ToString("M/d/yyyy HH:mm:ss");
                var resultado = new EmailResult
                {
                    Date = fechaFormateada,
                    Subject = datasc.Subject,
                    Attached = datasc.Attached,
                    Status = datasc.Status
                };
                ResultE.Add(resultado);
            }
        }
        private void GetDataUpdate()
        {
            var datas = databaseHandler.GetAllDatasEmail();
            ResultE.Clear();
            foreach (Datas datasc in datas)
            {
                DateTime fechaHora = DateTime.Parse(datasc.Date);
                string fechaFormateada = fechaHora.ToString("M/d/yyyy HH:mm:ss");
                var resultado = new EmailResult
                {
                    Date = fechaFormateada,
                    Subject = datasc.Subject,
                    Attached = datasc.Attached,
                    Status = datasc.Status
                };
                ResultE.Add(resultado);
            }
        }
        private void GetDatosEmail()
        {
            string json = File.ReadAllText(rutaData);
            Imbox inbox = JsonSerializer.Deserialize<Imbox>(json);
            foreach (var proveedor in inbox.Proveedores)
            {
                foreach (var clientes in proveedor.Clientes)
                {
                    foreach (var email in ResultEmails)
                    {
                        if (clientes.Email == email)
                        {
                            foreach (var metodo in proveedor.Metodos)
                            {
                                dataReadEmail.Add(new List<string> { clientes.Email, clientes.Password, metodo.Url, (metodo.Puerto).ToString(), metodo.Nombre });
                                break;
                            }
                        }
                    }
                }
            }
        }
        public void SetAttachments()
        {
            var downDirectory = Path.Combine(rutaDownload + "/descargaXML");
            if (!Directory.Exists(downDirectory))
            {
                Directory.CreateDirectory(downDirectory);
            }
            if (dataReadEmail.Count != 0)
            {
                foreach (var dataEmail in dataReadEmail)
                {
                    SetAttachmentsEmail(dataEmail[0], dataEmail[1], dataEmail[2], int.Parse(dataEmail[3]), dataEmail[4], downDirectory);
                }
            }
        }
        //aumentar un try y catch en caso que se corte la ejecucuion por internet
        private void SetAttachmentsEmail(string email, string password, string url, int puerto, string metodo, string downDirectory)
        {
            if (metodo.ToLower() == "pop3")
            {
                using (var client = new Pop3Client())
                {
                    client.Connect(url, puerto, true);
                    client.Authenticate(email, password);
                    int messageCount = client.GetMessageCount();
                    for (int i = messageCount - 1; i >= 0; i--)
                    {
                        var message = client.GetMessage(i);
                        if (message.Attachments.OfType<MimePart>().Any(x => x.FileName.EndsWith(".xml")))
                        {
                            foreach (var attachment in message.Attachments.OfType<MimePart>().Where(x => x.FileName.EndsWith(".xml")))
                            {
                                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(attachment.FileName);
                                var extension = Path.GetExtension(attachment.FileName);
                                int count = 0;
                                var fileName = Path.Combine(downDirectory, $"{fileNameWithoutExtension}{extension}");
                                while (File.Exists(fileName))
                                {
                                    count++;
                                    fileName = Path.Combine(downDirectory, $"{fileNameWithoutExtension}({count}){extension}");
                                }
                                var stream = File.Create(fileName);
                                attachment.Content.DecodeTo(stream);
                                stream.Dispose();
                                var finalFileName = count > 0 ? $"{fileNameWithoutExtension}({count}){extension}" : $"{fileNameWithoutExtension}{extension}";
                                databaseHandler.InsertData(message.Date.ToString(), message.Subject, finalFileName);
                            }
                        }
                    }
                    client.Disconnect(true);
                }

                GetDataUpdate();
            }
            else if (metodo.ToLower() == "imap")
            {
                using (var client = new ImapClient())
                {
                    client.Connect(url, puerto, true);
                    client.Authenticate(email, password); 
                    // Intentar obtener la carpeta 'Processed'.
                    var personalNamespaces = client.GetFolder(client.PersonalNamespaces[0]);
                    IMailFolder processedFolder = null;
                    try
                    {
                        processedFolder = personalNamespaces.GetSubfolder("Processed");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"No se pudo obtener la carpeta 'Processed'. Error: {ex.Message}");
                    } 
                    // Crear una carpeta 'Processed' si no existe.
                    if (processedFolder == null)
                    {
                        try
                        {
                            processedFolder = personalNamespaces.Create("Processed", true);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"No se pudo crear la carpeta 'Processed'. Error: {ex.Message}");
                        }
                    }

                    client.Inbox.Open(FolderAccess.ReadWrite);
                    for (int i = client.Inbox.Count - 1; i >= 0; i--)
                    {
                        var message = client.Inbox.GetMessage(i);
                        if (message.Attachments.OfType<MimePart>().Any(x => x.FileName.EndsWith(".xml")))
                        {
                            foreach (var attachment in message.Attachments.OfType<MimePart>().Where(x => x.FileName.EndsWith(".xml")))
                            {
                                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(attachment.FileName);
                                var extension = Path.GetExtension(attachment.FileName);
                                int count = 0;
                                var fileName = Path.Combine(downDirectory, $"{fileNameWithoutExtension}{extension}");
                                while (File.Exists(fileName))
                                {
                                    count++;
                                    fileName = Path.Combine(downDirectory, $"{fileNameWithoutExtension}({count}){extension}");
                                }
                                using (var stream = File.Create(fileName))
                                {
                                    attachment.Content.DecodeTo(stream);
                                }
                                var finalFileName = count > 0 ? $"{fileNameWithoutExtension}({count}){extension}" : $"{fileNameWithoutExtension}{extension}";
                                databaseHandler.InsertData(message.Date.ToString(), message.Subject, finalFileName);
                            }
                            // Mover el mensaje a la carpeta de procesados solo si contiene un archivo XML adjunto
                            client.Inbox.MoveTo(i, processedFolder);
                        }
                    }
                    client.Disconnect(true);
                } 
                GetDataUpdate();

                /* ORIGINAL
                using (var client = new ImapClient())
                {
                    client.Connect(url, puerto, true);
                    client.Authenticate(email, password);
                    client.Inbox.Open(FolderAccess.ReadOnly);
                    for (int i = client.Inbox.Count - 1; i >= 0; i--)
                    {
                        var message = client.Inbox.GetMessage(i);
                        if (message.Attachments.OfType<MimePart>().Any(x => x.FileName.EndsWith(".xml")))
                        {
                            foreach (var attachment in message.Attachments.OfType<MimePart>().Where(x => x.FileName.EndsWith(".xml")))
                            {
                                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(attachment.FileName);
                                var extension = Path.GetExtension(attachment.FileName);
                                int count = 0;
                                var fileName = Path.Combine(downDirectory, $"{fileNameWithoutExtension}{extension}");
                                while (File.Exists(fileName))
                                {
                                    count++;
                                    fileName = Path.Combine(downDirectory, $"{fileNameWithoutExtension}({count}){extension}");
                                }
                                using (var stream = File.Create(fileName))
                                {
                                    attachment.Content.DecodeTo(stream);
                                }
                                var finalFileName = count > 0 ? $"{fileNameWithoutExtension}({count}){extension}" : $"{fileNameWithoutExtension}{extension}";
                                databaseHandler.InsertData(message.Date.ToString(), message.Subject, finalFileName);
                            }
                        }
                    }
                    client.Disconnect(true);
                }*/
            }
            else
            {
                Console.WriteLine("Método desconocido \n");
                return;
            }
        }
        private List<string> CargarMensajesLeidos()
        {
            var filePath = "mensajes_leidos.txt";
            if (File.Exists(filePath))
            {
                return File.ReadAllLines(filePath).ToList();
            }
            else
            {
                return new List<string>();
            }
        }
        private void GuardarMensajesLeidos(List<string> mensajesLeidos)
        {
            var filePath = "mensajes_leidos.txt";
            File.WriteAllLines(filePath, mensajesLeidos);
        }

        public void GetDataInvoice()
        {
            sapService = new SAPServiceLayerConnection();
            var datas = databaseHandler.GetPendingEmails();
            string xmlFolderPath = Path.Combine(rutaDownload, "descargaXML");
            foreach (Datas email in datas)
            {
                string attachmentName = email.Attached;
                string filePath = Path.Combine(xmlFolderPath, attachmentName);
                if (File.Exists(filePath))
                {
                    var invoice = ReadFile(email.Id,filePath);
                    if (invoice != null)
                    {
                        var invoiceData = (FacturaBase)invoice;
                        invoiceData.identifier = email.Id;
                        listInvoice.Add(invoiceData);
                        Console.Write("\n guardo a la lista: " + listInvoice + "\n ");
                    } 
                }
            }
            sapService.ConnectToSAP(listInvoice).GetAwaiter().GetResult();
        }
        public FacturaBase ReadFile(int emailId, string archivo)
        {
            try
            {
                using (var reader = XmlReader.Create(archivo))
                {
                    reader.MoveToContent();
                    switch (reader.Name)
                    {
                        case "facturaElectronicaServicioTuristicoHospedaje":
                            var serializerHospedaje = new XmlSerializer(typeof(FacturaServicioTuristicoHospedaje));
                            return (FacturaServicioTuristicoHospedaje)serializerHospedaje.Deserialize(reader);
                        case "facturaElectronicaServicioBasico":
                            var serializerBasico = new XmlSerializer(typeof(FacturaServicioBasico));
                            return (FacturaServicioBasico)serializerBasico.Deserialize(reader);
                        case "facturaElectronicaCompraVenta":
                            var serializerCompraVenta = new XmlSerializer(typeof(FacturaCompraVenta));
                            return (FacturaCompraVenta)serializerCompraVenta.Deserialize(reader);
                        default: 
                            string errorMessage = $"No se reconoce el tipo de factura: {reader.Name}";  
                            databaseHandler.UpdateStatus(emailId, Datas.StatusError, errorMessage); 
                            return null;
                    }
                }
            }
            catch (Exception e)
            {
                databaseHandler.UpdateStatus(emailId, Datas.StatusError, e.Message); 
                return null;
            }
        }
    }
}