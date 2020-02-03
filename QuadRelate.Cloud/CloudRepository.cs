using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using QuadRelate.Contracts;
using QuadRelate.Types;

namespace QuadRelate.Cloud
{
    public class CloudRepository : IAsyncRepository
    {
        private const string _connectionString =
            "Endpoint=sb://quadrelate.servicebus.windows.net/;SharedAccessKeyName=SendSharedAccessKey;SharedAccessKey=+6519fgpG+m37fAQsOuCqp+fAUN5+opT+5Z9rLu71LY=";

        private QueueClient _queueClient;

        public async Task SaveGameAsync(GameResult gameResult)
        {
            var queuePath = gameResult.Winner == Counter.Empty ? "Draw" : gameResult.Winner.ToString();
            
            if (_queueClient == null)
                _queueClient = new QueueClient(_connectionString, queuePath);

            var bytes = ConvertToBytes(gameResult.Moves);
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

        private static byte[] ConvertToBytes(IList<int> moves)
        {
            var bytes = new byte[moves.Count];
            for (var i = 0; i < moves.Count; i++)
            {
                bytes[i] = (byte)moves[i];
            }

            return bytes;
        }
    }
}