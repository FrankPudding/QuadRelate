using QuadRelate.Contracts;
using QuadRelate.Helpers;
using QuadRelate.Types;

namespace QuadRelate.Players.Vince
{
    public class CpuPlayerLefty : IPlayer
    {
        public string Name => "Lefty";

        public int NextMove(Board board, Cell colour)
        {
            return board.AvailableColumns()[0];
        }
    }
}