using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OData;
using Microsoft.OData.Client; 

namespace SAPB1
{
    public partial class ServiceLayer
    {
        private string authCookie = "";
        private static bool TLSCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors ssl)
        {
            return true;
        }
        partial void OnContextCreated() => InitializeContext();
        private void InitializeContext()
        {
            this.ReceivingResponse += (sender, eventArgs) =>
            {
                string cookie = eventArgs.ResponseMessage.GetHeader("Set-Cookie");
                if (!string.IsNullOrEmpty(cookie))
                {
                    if (eventArgs.ResponseMessage.StatusCode == (int)HttpStatusCode.OK)
                    {
                        authCookie = cookie;
                    }
                }
            };
            this.BuildingRequest += (sender, eventArgs) =>
            {
                if (authCookie != null)
                {
                    eventArgs.Headers.Remove("cookie");
                    eventArgs.Headers.Add("cookie", authCookie);
                }
            };
            ServicePointManager.ServerCertificateValidationCallback = TLSCertificateValidate;

            // todo: implement FilterNullValues
            //this.Configurations.RequestPipeline.OnEntryStarting((arg) =>
            //{
            //	arg.Entry.Properties = FilterNullValues(arg.Entry);
            //});

        }
        public bool IsSessionStarted()
        {
            return authCookie != "";
        }
        public void ClearSession()
        {
            authCookie = "";
        }
    }
}
