using EmailToSAPInvoice.Connection; 
using EmailToSAPInvoice.Responses;
using SAPB1;
using System; 
using System.Linq;

namespace EmailToSAPInvoice.ViewModels
{ 
    public class UploadDataToSAP
    {
        private ServiceLayerConnection serviceLayerConnection;
        public UploadDataToSAP(ServiceLayerConnection serviceLayerConnection)
        {
            this.serviceLayerConnection = serviceLayerConnection;
        }
        public Response ServiceLayer( )
        { 
            Response response = new Response();
            ServiceLayer serviceLayerContext = null;
            try
            {
                serviceLayerContext = serviceLayerConnection.Init(serviceLayerContext);
                Document document = serviceLayerContext.Invoices.First();
            }
            catch (Exception exception)
            {
                ServiceLayerConnection.HandleServiceLayerException(exception, serviceLayerContext);
                response.code = -1;
                response.message = exception.InnerException?.Message ?? exception.Message;
                response.data = exception.Message;
                System.Diagnostics.Debug.WriteLine(exception);
            }
            finally
            {
                serviceLayerConnection.End(serviceLayerContext);
            }
            return response;
        }
    }
}
