using QuadRelate.Contracts;
using QuadRelate.Models;
using QuadRelate.Players.Vince.Helpers;
using QuadRelate.Types;

namespace QuadRelate.Players.Vince
{
    public class CpuPlayerCentre : IPlayer
    {
        private readonly IPlayerInitializer _playerInitializer;

        public CpuPlayerCentre(IPlayerInitializer playerInitializer)
        {
            _playerInitializer = playerInitializer;
        }

        public string Name => "The Centaur";

        public int NextMove(Board board, Counter colour)
        {
            if (MovesHelper.TryGetBasicMove(board, colour, out var move))
                return move;
            
            var centreMoves = MovesHelper.GetMovesClosestToCentre(board.AvailableColumns());
            return centreMoves[_playerInitializer.Randomizer.Next(centreMoves.Count)];
        }

        public void GameOver(GameResult result)
        {
        }
    }
}