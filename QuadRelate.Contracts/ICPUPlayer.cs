using QuadRelate.Types;

namespace QuadRelate.Contracts
{
    public interface ICPUPlayer
    {
        int NextMove(Board board, Cell colour);
    }
}
