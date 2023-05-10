using Microsoft.OData.Client;
using SAPB1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace EmailToSAPInvoice.Connection
{ 
    class Authentication
    {
        public static void Login(SAPB1.ServiceLayer serviceLayerContext, string companyDB, string userName, string password, SAPbobsCOM.BoSuppLangs language)
        {
            B1Session session = serviceLayerContext.Login(companyDB, userName, password, language.ToString()).GetValue();
            Console.WriteLine("[Authentication] SessionId={0}, Version={1}, Timeout={2}", session.SessionId, session.Version, session.SessionTimeout);
        }
        public static void Logout(SAPB1.ServiceLayer serviceLayerContext)
        {
            OperationResponse operationResponse = serviceLayerContext.Logout().Execute();
            Console.WriteLine("[Authentication] Logout code = {0}", operationResponse.StatusCode);
        }
    }
}
