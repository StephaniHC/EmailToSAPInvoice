using EmailToSAPInvoice.Connection;
using EmailToSAPInvoice.Requests;
using EmailToSAPInvoice.Responses;
using SAPB1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EmailToSAPInvoice.ViewModels
{ 
    public class UploadDataToSAP
    {
        private log4net.ILog logger = null;
        private ServiceLayerConnection serviceLayerConnection;
        public UploadDataToSAP(ServiceLayerConnection serviceLayerConnection)
        {
            logger = log4net.LogManager.GetLogger(typeof(ServiceLayerConnection).ToString());
            this.serviceLayerConnection = serviceLayerConnection;
        }
        public Response ServiceLayer(InvoiceRequest invoiceRequest)
        {
            Console.Write("Ingreso a la funcion update Data SAP");
            Response response = new Response();
            ServiceLayer serviceLayerContext = null;
            try
            {
                logger.Error("------------ENTRO A CONSULTAR POR NOTA DE DEBITO------------");
                serviceLayerContext = serviceLayerConnection.Init(serviceLayerContext);
                logger.Error("------------CONECTO------------");
                logger.Error("INICIO : " + serviceLayerContext);
                Document document = serviceLayerContext.Invoices.First();
                logger.Error("ESTO ENCONTRO : " + document);
            }
            catch (Exception exception)
            {
                ServiceLayerConnection.HandleServiceLayerException(exception, serviceLayerContext);
                response.code = -1;
                response.message = exception.InnerException?.Message ?? exception.Message;
                response.data = exception.Message;
                System.Diagnostics.Debug.WriteLine(exception);
                logger.Error("SE FUE POR EL CATCH" + exception);
            }
            finally
            {
                serviceLayerConnection.End(serviceLayerContext);
                logger.Error("------------SE DESCONECTOOOOOOOOO------------");
            }
            return response;
        }
    }
}
