using System.Net;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace Triggers
{

class Program
    {
    static async Task Main(string[] args)
        {  
            string connString = "[ConnectionString]";

            CosmosClient myClient = new (connString);
            Database myDatabase = myClient.GetDatabase("LabDB");
            Container myContainer = myDatabase.GetContainer("LabContainer");

            ItemRequestOptions rOptions = new ItemRequestOptions()
            { 
                PreTriggers = new List<string> { "addOpsCounter" }
                PostTriggers = new List<string> { "incrementOpsCounter" }
            };

        GenericItem myItem = new()
            {itemName = "labSample", itemId = 10, id=Guid.NewGuid()};

            GenericItem createdItem = await myContainer.CreateItemAsync<GenericItem>(myItem, requestOptions: rOptions);
        }
 
        public class GenericItem
        {
            public Guid id {get; set;}
            public string? itemName {get; set;}
            public int itemId {get; set;} 
        } 
    }   
}
