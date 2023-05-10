using EmailToSAPInvoice.Connection;
using Microsoft.Extensions.Configuration;
using Microsoft.OData.Client;
using SAPB1;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.OData; 
using SAPbobsCOM;

namespace EmailToSAPInvoice.Connection
{ 
    public class ServiceLayerConnection
    {
        public static Company company;
        private string serviceLayerUrl;
        private log4net.ILog logger = null;

        public ServiceLayerConnection(IConfiguration configuration)
        {
            logger = log4net.LogManager.GetLogger(typeof(ServiceLayerConnection).ToString());
            logger.Error("----------------------CONNECTION--------------------------------");
            company = new Company();
            company.CompanyDB = configuration.GetValue<string>("SapConfiguration:CompanyDB");
            logger.Error("ERROR CONNECTION CompanyDB:" + company.CompanyDB);
            company.UserName = configuration.GetValue<string>("SapConfiguration:UserName");
            logger.Error("ERROR CONNECTION UserName:" + company.UserName);
            company.Password = configuration.GetValue<string>("SapConfiguration:Password");
            logger.Error("ERROR CONNECTION Password:" + company.Password);
            company.Language = (SAPbobsCOM.BoSuppLangs)configuration.GetValue<int>("SapConfiguration:Language");
            logger.Error("ERROR CONNECTION Language:" + company.Language);

            serviceLayerUrl = configuration.GetValue<string>("SapConfiguration:Url");
        }
        public ServiceLayer Init(ServiceLayer serviceLayerContext)
        {
            serviceLayerContext = new ServiceLayer(new Uri(serviceLayerUrl));
            Authentication.Login(serviceLayerContext, company.CompanyDB, company.UserName, company.Password, company.Language);
            return serviceLayerContext;
        }
        public void End(ServiceLayer serviceLayerContext)
        {
            if (serviceLayerContext == null) return;
            Authentication.Logout(serviceLayerContext);
        }
        public static Exception HandleServiceLayerException(Exception exception, ServiceLayer serviceLayerContext)
        {
            // Client level Exception message
            Console.WriteLine("[ServiceLayerConnection] " + exception.Message);
            Console.WriteLine("[ServiceLayerConnection] " + exception.StackTrace);

            // The InnerException of Exception contains DataServiceQueryException
            //DataServiceQueryException dataServiceQueryException = exception.InnerException as DataServiceQueryException;
            //if (dataServiceQueryException != null)
            //{

            //}
            // The InnerException of DataServiceQueryException contains DataServiceClientException
            DataServiceClientException dataServiceClientException = exception.InnerException as DataServiceClientException;
            if (dataServiceClientException != null)
            {
                // The InnerException of DataServiceClientException	contains ODataErrorException.
                // You can get ODataErrorException from dataServiceClientException.InnerException
                // This object holds Exception as thrown from the service.
                //ODataErrorException odataErrorException = dataServiceClientException.InnerException as ODataErrorException;
                Console.WriteLine("[ServiceLayerConnection] " + dataServiceClientException.Message.ToString());
                if (dataServiceClientException.StatusCode == 401)
                {
                    if (serviceLayerContext != null)
                    {
                        serviceLayerContext.ClearSession();
                        Authentication.Login(serviceLayerContext, company.CompanyDB, company.UserName, company.Password, company.Language);
                    }
                }
            }
            return exception;
        }
        public static void SaveCreateChanges(ServiceLayer serviceLayerContext)
        {
            SaveChanges(serviceLayerContext, HttpStatusCode.Created);
        }
        public static void SaveUpdateChanges(ServiceLayer serviceLayerContext)
        {
            SaveChanges(serviceLayerContext, HttpStatusCode.NoContent);
        }
        public static void SaveDeleteChanges(ServiceLayer serviceLayerContext)
        {
            SaveChanges(serviceLayerContext, HttpStatusCode.NoContent);
        }
        private static void SaveChanges(ServiceLayer serviceLayerContext, HttpStatusCode statusCode)
        {
            DataServiceResponse responses = serviceLayerContext.SaveChanges();
            foreach (OperationResponse res in responses)
            {
                Console.WriteLine("Expected StatusCode={0}, Actual StatusCode={1}", (int)statusCode, res.StatusCode);
                if (res.StatusCode != (int)statusCode)
                {
                    throw new Exception(res.Error.Message);
                }
            }
        }
    }
}
