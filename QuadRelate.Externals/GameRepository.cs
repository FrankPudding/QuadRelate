using System.Collections.Generic;
using System.Text;
using QuadRelate.Contracts;
using QuadRelate.Types;

namespace QuadRelate.Externals
{
    public class GameRepository : IGameRepository
    {
        private readonly IFileReaderWriter _fileReaderWriter;
        private readonly ISerializer _serializer;

        public GameRepository(IFileReaderWriter fileReaderWriter, ISerializer serializer)
        {
            _fileReaderWriter = fileReaderWriter;
            _serializer = serializer;
        }

        public IEnumerable<GameResult> LoadGames(string fileName)
        {
            var previousGamesJson = _fileReaderWriter.ReadAllLines(fileName);

            return _serializer.Deserialize<GameResult>(previousGamesJson);
        }

        public void SaveGame(GameResult gameResult, string fileName)
        {
            var text = _serializer.Serialize(gameResult);
            _fileReaderWriter.WriteAllLines(fileName, new[] { text }, append:true);
        }
    }
}
