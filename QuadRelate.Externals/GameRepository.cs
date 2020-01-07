using System.Collections.Generic;
using System.Text;
using QuadRelate.Contracts;
using QuadRelate.Types;

namespace QuadRelate.Externals
{
    public class GameRepository : IGameRepository
    {
        private readonly IFileReaderWriter _fileReaderWriter;

        public GameRepository(IFileReaderWriter fileReaderWriter)
        {
            _fileReaderWriter = fileReaderWriter;
        }

        public IEnumerable<GameResult> LoadGames(string fileName)
        {


            throw new System.NotImplementedException();
        }

        public void SaveGame(GameResult gameResult, string fileName)
        {
            var sb = new StringBuilder();

            sb.Append(gameResult.Winner);
            _fileReaderWriter.WriteLine(fileName, sb.ToString());
        }
    }
}
