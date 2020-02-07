using QuadRelate.Types;

namespace QuadRelate.Contracts
{
    public interface IBoardHasher
    {
        int[] GetBoardHash(Board board);
        int GetColumnHash(Board board, int column);
        int GetCellHash(Counter counter);

        bool IsSubset(int hash1, int hash2);
        bool IsSubset(Board subset, Board full);
    }
}