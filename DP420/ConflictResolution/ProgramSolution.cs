using System.Net;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Bogus;

namespace ConflictResolution
{

class Program
    {
    static async Task Main(string[] args)
        {   string myConnectionString =
            "https://dp420.documents.azure.com:443/;notARealKeyfDR2ci9QgkdkvERTQ==";

CosmosClient aClient = new (myConnectionString);

Container container = await aClient.GetDatabase("DatabaseForLab")
    .CreateContainerIfNotExistsAsync(new ContainerProperties("LabItems", "/labPK")
     {
        ConflictResolutionPolicy = new ConflictResolutionPolicy()
        {
            Mode = ConflictResolutionMode.LastWriterWins,
            ResolutionPath = "/myAltTimeStamp"

        }
    }
    );

    }
  }
}       