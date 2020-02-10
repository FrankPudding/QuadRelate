using QuadRelate.Types;

namespace QuadRelate.Contracts
{
    public interface IBoardHasher
    {
        string GetHash(Board board);
        char GetHash(Counter counter);

        bool IsSubset(Board subset, Board full);
        bool IsSubset(string subset, string full);
        bool IsSubset(char subset, char full);
    }
}