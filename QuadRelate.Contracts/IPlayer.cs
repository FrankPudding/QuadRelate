using QuadRelate.Types;

namespace QuadRelate.Contracts
{
    public interface IPlayer
    {
        int NextMove(Board board, Cell colour);
    }
}
