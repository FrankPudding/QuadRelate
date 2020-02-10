using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using QuadRelate.Cloud;

namespace QuadRelate.FunctionApp
{
    public static class Result
    {
        [FunctionName("Result")]
        [return: Table("Result", Connection = "AzureWebJobsStorage")]
        public static async Task<ResultEntity> Run(
            [ServiceBusTrigger("results", Connection = "AzureWebJobsServiceBus")] string myQueueItem,
            [Table("Result", Connection = "AzureWebJobsStorage")] CloudTable results,
            ILogger log)
        {
            var winner = myQueueItem[0].ToString();
            var board = myQueueItem.Substring(1);

            //var findOperation = TableOperation.Retrieve<ResultEntity>(winner, board);
            //var findResult = await results.ExecuteAsync(findOperation);
            //if (findResult != null)
            //    return null;

            log.LogInformation($"Winner: {winner}");
            log.LogInformation($"Board: {board}");

            var entity = new ResultEntity
            {
                Winner = winner,
                Board = board,
                PartitionKey = winner,
                RowKey = board
            };

            return entity;
        }
    }
}