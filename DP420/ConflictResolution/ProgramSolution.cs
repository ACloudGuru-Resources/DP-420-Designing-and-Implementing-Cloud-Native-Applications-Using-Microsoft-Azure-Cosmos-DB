using System.Net;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;

namespace ConflictResolution
{

class ProgramToo
    {
    static async Task MainToo(string[] args)
        {   
            string myConnectionString = "[ConnectionString]";

            //Declare a CosmosClient using the connection string
            CosmosClient aClient = new (myConnectionString);

//A slightly more verbose approach but that is somewhat easier to build in a step-wise manner

            //Get a pre-deployed database from the Cosmos DB account called "LabDB";
            Database myDatabase = aClient.GetDatabase("LabDB");

            //Declare a ContainerProperties object with the minimum properties:
            //  "LabItems" for the container name and "/labPK" for the partition key path;
            ContainerProperties containerProps = new ContainerProperties("LabItems","/labPK");

            //Add a ConflictResolutionPolicy to containerProps with the two properties appropriate to the lab scenario;
            containerProps.ConflictResolutionPolicy.Mode = Mode.LastWriterWins;
            containerProps.ConflictResolutionPolicy.ResolutionPath = "/myAltTimeStamp";

            //Create the new container, taking containerProps as a single parameter
            Container container = await myDatabase.CreateContainerIfNotExistsAsync(containerProps);

//A less verbose and, arguably, more elegant way to accomplish the same thing:

        // Container container = await aClient.GetDatabase("LabDB")
        //     .CreateContainerIfNotExistsAsync(new ContainerProperties("LabItems", "/labPK")
        //      {
        //         ConflictResolutionPolicy = new ConflictResolutionPolicy()
        //         {
        //             Mode = ConflictResolutionMode.LastWriterWins,
        //             ResolutionPath = "/myAltTimeStamp"

        //         }
        //     }
        //     );

    }
  }
}       
