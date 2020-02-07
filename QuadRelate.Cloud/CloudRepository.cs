using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using QuadRelate.Contracts;
using QuadRelate.Types;

namespace QuadRelate.Cloud
{
    public class CloudRepository : IAsyncRepository
    {
        private readonly IBoardHasher _boardHasher;

        private const string _connectionString =
            "Endpoint=sb://quadrelate.servicebus.windows.net/;SharedAccessKeyName=SendSharedAccessKey;SharedAccessKey=17vuo4JO0brvVp284TY+6/csQxR1fM2lVPa14fpIKwM=";

        private QueueClient _queueClient;

        public CloudRepository(IBoardHasher boardHasher)
        {
            _boardHasher = boardHasher;
        }

        public async Task SaveGameAsync(Board board, Counter winner)
        {
            var queuePath = QueuePath(winner);
            
            if (_queueClient == null)
                _queueClient = new QueueClient(_connectionString, queuePath);

            var hash = _boardHasher.GetBoardHash(board);
            var bytes = ConvertToBytes(hash);
            var message = new Message(bytes);

            await _queueClient.SendAsync(message).ConfigureAwait(false);
        }

        public IEnumerable<GameResult> LoadGames()
        {
            return new GameResult[0];
        }

        public async Task CloseAsync()
        {
            await _queueClient.CloseAsync().ConfigureAwait(false);
            _queueClient = null;
        }

        private static byte[] ConvertToBytes(IReadOnlyList<int> hash)
        {
            var bytes = new List<byte>();
            foreach (var h in hash)
            {
                var b = ToBytes(h);
                bytes.AddRange(b);
            }

            return bytes.ToArray();
        }

        private static byte[] ToBytes(int i)
        {
            byte[] result = new byte[4];

            result[0] = (byte)(i >> 24);
            result[1] = (byte)(i >> 16);
            result[2] = (byte)(i >> 8);
            result[3] = (byte)(i /*>> 0*/);

            return result;
        }

        private static string QueuePath(Counter winner)
        {
            if (winner == Counter.Yellow)
                return "yellow-wins";
            if (winner == Counter.Red)
                return "red-wins";

            return "draws";
        }
    }
}