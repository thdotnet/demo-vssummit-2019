using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ChangeFeedSubscriber
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task Run([CosmosDBTrigger(
            databaseName: "db",
            collectionName: "products",
            ConnectionStringSetting = "CosmosDbConnectionString",            
            LeaseCollectionName = "leases")]IReadOnlyList<Document> input,
            [SignalR(HubName = "notify")]IAsyncCollector<SignalRMessage> signalRMessages, ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                await signalRMessages.AddAsync(new SignalRMessage
                {
                    Target = "notify",
                    Arguments = new[] { input[0].Id }
                });

                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].Id);
            }
        }
    }
}
