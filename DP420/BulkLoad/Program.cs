using System.Net;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Bogus;

namespace BulkLoad
{

    class Program
    {
        static async Task Main(string[] args)
        {  
            string connString = "[Paste_the_copied_Primary_Connection_String_here]";

            CosmosClientOptions blOptions = new CosmosClientOptions() 
            { 
                AllowBulkExecution = true 
            };
            CosmosClient bulkClient = new (connString,blOptions);

            Microsoft.Azure.Cosmos.Database myDatabase = bulkClient.GetDatabase("LabDB");
            Container theContainer = myDatabase.GetContainer("LabContainer");

            var fruit = new[] {"apple", "peach", "lemon", "strawberry", "pear"};
            //get items from a source; we're using a fake data generator, here
            List<GenericItem> itemsToInsert = new Faker<GenericItem>()
                .RuleFor(i => i.id, f => Guid.NewGuid())
                //itemId is partition key
                .RuleFor(i => i.itemId, f => f.Random.Number(1, 10))
                .RuleFor(i => i.itemName, f=> f.PickRandom(fruit))
                .Generate(1000);

            List<Task> batchOfTasks = new List<Task>();

            foreach(GenericItem genItem in itemsToInsert)
            { 
                batchOfTasks.Add( 
                    theContainer.CreateItemAsync<GenericItem>(genItem,
                        new PartitionKey(genItem.itemId))
                );
            }
            await Task.WhenAll(batchOfTasks);
 
        }

        public class GenericItem
        {
            public Guid id {get; set;}
            public string? itemName {get; set;}
            public int itemId {get; set;} 
        } 
    }
}
