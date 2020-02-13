using QuadRelate.Contracts;
using QuadRelate.Models;
using QuadRelate.Types;

namespace QuadRelate.Players.Vince
{
    public class CpuPlayerLefty : IPlayer
    {
        public string Name => "Lefty";

        private readonly IPlayerInitializer _playerInitializer;

        public CpuPlayerLefty(IPlayerInitializer playerInitializer)
        {
            _playerInitializer = playerInitializer;
        }
        
        public int NextMove(Board board, Counter colour)
        {
            return board.AvailableColumns()[0];
        }

        public void GameOver(GameResult result)
        {
            // Ignore.
        }
    }
}