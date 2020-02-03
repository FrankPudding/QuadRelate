using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace QuadRelate.FunctionApp
{
    public static class Wins
    {
        [FunctionName("YellowWin")]
        public static void Run([ServiceBusTrigger("Yellow", Connection = "yellow")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
