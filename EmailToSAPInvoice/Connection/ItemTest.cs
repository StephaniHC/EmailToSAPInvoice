using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.OData;
using Microsoft.OData.Client;
// The generated classes are defined in this namespace.
using SAPB1;

namespace EmailToSAPInvoice.Connection
{ 
    public class ItemTest
    {
        public static void createItem(ServiceLayer serviceLayerContext)
        {
            //Create an Item and add it to the OData context.
            Item item = new Item();
            item.ItemCode = "yourItemCode"; // Specify the item code you want to create.
            serviceLayerContext.AddToItems(item);

            // Send the request
            DataServiceResponse responses = serviceLayerContext.SaveChanges();

            // Get the responses. For this case, there is only one response.
            foreach (OperationResponse response in responses)
            {
                var changeResponse = response as ChangeOperationResponse;
                var entityDescriptor = changeResponse.Descriptor as EntityDescriptor;

                // Get the entity created on the service and check if the entity is as expected.
                Item entity = entityDescriptor.Entity as Item;
                Console.WriteLine("Expected ItemCode={0}, Actual ItemCode={1}", item.ItemCode, entity.ItemCode);

                // Check if the response code is as expected.
                Console.WriteLine("Expected StatusCode={0}, Actual StatusCode={1}", (int)HttpStatusCode.Created, response.StatusCode);
            }
        }
    }
}
