using System.Linq;
using System.Text;
using QuadRelate.Contracts;
using QuadRelate.Types;

namespace QuadRelate.Models
{
    public class BoardHasher : IBoardHasher
    {
        private const char _yellow = 'Y';
        private const char _red = 'R';
        private const char _empty = ' ';

        public string GetHash(Board board)
        {
            var hash = new StringBuilder();
            for (var row = 0; row < Board.Height; row++)
            {
                for (var column = 0; column < Board.Width; column++)
                {
                    hash.Append(GetHash(board[column, row]));
                }
            }

            return hash.ToString();
        }
        
        public char GetHash(Counter counter)
        {
            if (counter == Counter.Yellow) return _yellow;
            if (counter == Counter.Red) return _red;

            return _empty;
        }

        public bool IsSubset(Board subset, Board full)
        {
            return IsSubset(GetHash(subset), GetHash(full));
        }
        
        public bool IsSubset(string subset, string full)
        {
            return !subset.Where((t, i) => !IsSubset(t, full[i])).Any();
        }

        public bool IsSubset(char subset, char full)
        {
            if (subset == _empty) return true;
            if (full == _empty) return false;

            return subset == full;
        }
    }
}