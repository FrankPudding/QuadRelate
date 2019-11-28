using QuadRelate.Contracts;
using QuadRelate.Models;
using QuadRelate.Types;

namespace QuadRelate.Players.Vince
{
    public class CpuPlayerLefty : IPlayer
    {
        public string Name => "Lefty";

        public int NextMove(Board board, Counter colour)
        {
            return board.AvailableColumns()[0];
        }
    }
}