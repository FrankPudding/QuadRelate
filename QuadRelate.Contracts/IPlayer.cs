using QuadRelate.Types;

namespace QuadRelate.Contracts
{
    public interface IPlayer
    {
        string Name { get; }

        int NextMove(Board board, Counter colour);
    }
}
