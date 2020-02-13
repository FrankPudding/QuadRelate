using QuadRelate.Contracts;
using QuadRelate.Models;
using QuadRelate.Types;

namespace QuadRelate.Players.Rory
{
    public class CpuPlayerRandom : IPlayer
    {
        private readonly IPlayerInitializer _playerInitializer;

        public CpuPlayerRandom(IPlayerInitializer playerInitializer)
        {
            _playerInitializer = playerInitializer;
        }

        public string Name => "The Randomizer";

        public int NextMove(Board board, Counter colour)
        {
            var index = _playerInitializer.Randomizer.Next(board.AvailableColumns().Count);

            return board.AvailableColumns()[index];
        }

        public void GameOver(GameResult result)
        {
            // Ignore.
        }
    }
}