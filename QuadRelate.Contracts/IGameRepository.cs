using QuadRelate.Types;
using System.Collections.Generic;

namespace QuadRelate.Contracts
{
    public interface IGameRepository
    {
        void SaveGame(GameResult gameResult, string fileName);

        IEnumerable<GameResult> LoadGames(string fileName);
    }
}
