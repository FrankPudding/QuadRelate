using System.Collections.Generic;
using Microsoft.Azure.ServiceBus;
using QuadRelate.Contracts;
using QuadRelate.Types;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace QuadRelate.Cloud
{
    public class CloudRepository : IResultsRepository
    {
        private readonly IBoardHasher _boardHasher;

        private const string _serviceBusConnectionString =
            "<>";
        private const string _storageConnectionString =
            "<>";

        private const string _queuePath = "results";
        private QueueClient _queueClient;

        public CloudRepository(IBoardHasher boardHasher)
        {
            _boardHasher = boardHasher;
        }

        public async Task SaveGameAsync(Board board, Counter winner)
        {
            if (_queueClient == null)
                _queueClient = new QueueClient(_serviceBusConnectionString, _queuePath);

            var text = _boardHasher.GetHash(winner) + _boardHasher.GetHash(board);
            var bytes = Encoding.UTF8.GetBytes(text);
            var message = new Message(bytes);

            await _queueClient.SendAsync(message).ConfigureAwait(false);
        }

        public async Task<IEnumerable<BoardResult>> GetPreviousResultsAsync()
        {
            var storageAccount = CloudStorageAccount.Parse(_storageConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference("Result");

            var entities = new List<ResultEntity>();
            TableContinuationToken token = null;
            do
            {
                var queryResult = await table.ExecuteQuerySegmentedAsync(new TableQuery<ResultEntity>(), token);
                entities.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;
            } while (token != null);

            var results = new List<BoardResult>();
            foreach (var e in entities)
            {
                if (e.Winner != " ")
                    results.Add(new BoardResult { Board = e.Board, Winner = e.Winner });
            }

            return results;
        }

        public async Task CloseAsync()
        {
            await _queueClient.CloseAsync().ConfigureAwait(false);

            _queueClient = null;
        }
    }
}