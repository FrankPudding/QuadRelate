using QuadRelate.Contracts;
using QuadRelate.Models;
using QuadRelate.Types;

namespace QuadRelate.Players.Vince
{
    public class CpuPlayerCentre : IPlayer
    {
        private readonly IRandomizer _randomizer;

        public CpuPlayerCentre(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        public string Name => "The Centaur";

        public int NextMove(Board board, Counter colour)
        {
            var centreMoves = MovesHelper.GetMovesClosestToCentre(board.AvailableColumns());
            return centreMoves[_randomizer.Next(centreMoves.Count)];
        }

        public void GameOver(GameResult result)
        {
        }
    }
}