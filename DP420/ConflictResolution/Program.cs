using System.Net;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace ConflictResolution
{

class Program
    {
    static async Task Main(string[] args)
        {   
            //Copy the primary connection string from the Cosmos DB account in the portal and paste it in place of the placeholder, below
            string myConnectionString = "[ConnectionString]";

            //Declare a CosmosClient, called myClient, using the connection string


            //Declare a Database, called myDatabase, from the Cosmos DB account called "LabDB";


            //Declare a ContainerProperties object, called containerProps, with the minimum properties:
            //  "LabItems" for the container name and "/labPK" for the partition key path;


            //Assign a ConflictResolutionPolicy to containerProps with the two policy properties appropriate to the lab scenario;


            //Create the new container on myDatabase (LabDB), taking containerProps as a single parameter
            ContainerResponse response = await myDatabase.CreateContainerIfNotExistsAsync(containerProps);



        }
    }
}
