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
using System.Net.Mail;
using System.Reactive;

namespace EmailToSAPInvoice.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Correos Adjuntos XML";
        public string Result { get; set; } = "read emails only xml";  
        public string ButtonAddEmail => "Añadir Correos"; 
        public string ButtonRead => "Actualizar";
        public string LabelTittle  => "Lista de Registrados";  
        public List<string> ResultEmails { get; set; } = new List<string>();
        public List<List<string>> dataReadEmail{ get; set; } = new List<List<string>>(); 
        public IReadOnlyList<IReadOnlyList<string>> DataPop => dataReadEmail.AsReadOnly();
        public ObservableCollection<Datas> DatasEmailList { get; set; } = new ObservableCollection<Datas>();
        public ICommand GoToSecondWindow { get; set; }
        public ICommand DownloadXmlAttachmentsCommand => new RelayCommand(SetAttachments); 
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
        private List<FacturaCompraVenta.facturaElectronicaCompraVenta> facturaList;
        private List<FacturaServicioBasico.facturaElectronicaServicioBasico> facturaServiceList;
        private List<FacturaServicioTuristicoHospedaje.facturaElectronicaServicioTuristicoHospedaje> facturaServiceTouristList;
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
            facturaList = new List<FacturaCompraVenta.facturaElectronicaCompraVenta>();
            facturaServiceList = new List<FacturaServicioBasico.facturaElectronicaServicioBasico>();
            facturaServiceTouristList = new List<FacturaServicioTuristicoHospedaje.facturaElectronicaServicioTuristicoHospedaje>();
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
                                dataReadEmail.Add(new List<string> { clientes.Email, clientes.Password, metodo.Url, (metodo.Puerto).ToString(), metodo.Nombre});
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
                                var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                                var fileName = Path.Combine(downDirectory, $"{attachment.FileName}_{timestamp}");
                                var stream = File.Create(fileName);
                                attachment.Content.DecodeTo(stream);
                                stream.Dispose();
                                databaseHandler.InsertData(message.Date.ToString(), message.Subject, $"{timestamp}_{attachment.FileName}");
                            }
                        }
                    }
                    client.Disconnect(true);
                }
            }
            else if (metodo.ToLower() == "imap")
            {
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
                                var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                                var fileName = Path.Combine(downDirectory, $"{timestamp}_{attachment.FileName}");
                                using (var stream = File.Create(fileName))
                                {
                                    attachment.Content.DecodeTo(stream);
                                }
                                databaseHandler.InsertData(message.Date.ToString(), message.Subject, $"{timestamp}_{attachment.FileName}");
                            }
                        }
                    }
                    client.Disconnect(true);
                }
            }
            else
            {
                Console.WriteLine("Método desconocido \n");
                return;
            }
        }   
        public object ReadFile(string archivo)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(archivo);
            string rootElementName = doc.DocumentElement.Name; 
            XmlSerializer serializer;
            if (rootElementName == "facturaElectronicaCompraVenta")
            { 
                serializer = new XmlSerializer(typeof(FacturaCompraVenta.facturaElectronicaCompraVenta)); 
            }
            else if (rootElementName == "facturaElectronicaServicioBasico")
            { 
                serializer = new XmlSerializer(typeof(FacturaServicioBasico.facturaElectronicaServicioBasico)); 
            }
            else if (rootElementName == "facturaElectronicaServicioTuristicoHospedaje")
            { 
                serializer = new XmlSerializer(typeof(FacturaServicioTuristicoHospedaje.facturaElectronicaServicioTuristicoHospedaje)); 
            }
            else
            { 
                return null;
            }
            try
            {
                using (StreamReader reader = new StreamReader(archivo))
                { 
                    return serializer.Deserialize(reader);
                }
            }
            catch (InvalidOperationException)
            {
                return null;
            }
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
                    var factura = ReadFile(filePath);
                    if (factura != null)
                    {
                        if (factura is FacturaCompraVenta.facturaElectronicaCompraVenta)
                        {
                            var facturaCompraVenta = (FacturaCompraVenta.facturaElectronicaCompraVenta)factura;
                            facturaCompraVenta.identifier = email.Id;
                            facturaList.Add(facturaCompraVenta);
                            Console.Write("\n guardo a la lista: " + facturaList + "\n ");

                        }
                        else if (factura is FacturaServicioBasico.facturaElectronicaServicioBasico)
                        {
                            var facturaServicioBasico = (FacturaServicioBasico.facturaElectronicaServicioBasico)factura;
                            facturaServicioBasico.identifier = email.Id;
                            facturaServiceList.Add(facturaServicioBasico);
                            Console.Write("\n guardo a la lista: " + facturaServiceList + "\n ");
                        }
                        else if (factura is FacturaServicioTuristicoHospedaje.facturaElectronicaServicioTuristicoHospedaje)
                        {
                            var facturaServicioTuristicoHospedaje = (FacturaServicioTuristicoHospedaje.facturaElectronicaServicioTuristicoHospedaje)factura;
                            facturaServicioTuristicoHospedaje.identifier = email.Id;
                            facturaServiceTouristList.Add(facturaServicioTuristicoHospedaje);
                            Console.Write("\n guardo a la lista: " + facturaServiceTouristList + "\n ");
                        }
                    }
                    else
                    {
                        databaseHandler.UpdateStatus(email.Id,Datas.StatusError,"No cumple con la estructura de Facturacion.XML");
                    }
                }
            }
            sapService.ConnectToSAP(facturaList, facturaServiceList, facturaServiceTouristList).GetAwaiter().GetResult();
        }  
    }
}