using System;
using QuadRelate.Contracts;
using QuadRelate.Types;

namespace QuadRelate.Models
{
    public class BoardHasher : IBoardHasher
    {
        public int[] GetBoardHash(Board board)
        {
            var hash = new int[Board.Width];
            for (var column = 0; column < Board.Width; column++)
            {
                hash[column] = GetColumnHash(board, column);
            }

            return hash;
        }

        public int GetColumnHash(Board board, int column)
        {
            var hashTotal = 0;
            for (var row = 0; row < Board.Height; row++)
            {
                var hash = GetCellHash(board[column, row]);
                hashTotal += hash * (int)Math.Pow(3, row);
            }

            return hashTotal;
        }

        public int GetCellHash(Counter counter)
        {
            if (counter == Counter.Yellow) return 1;
            if (counter == Counter.Red) return 2;
            return 0;
        }

        public bool IsSubset(int subset, int full)
        {
            var offset = 3;
            while (subset > 0)
            {
                var s = subset % offset;
                var f = full % offset;

                if (s != f)
                    return false;

                subset -= s;
                full -= f;

                offset *= 3;
            }

            return true;
        }

        public bool IsSubset(Board subset, Board full)
        {
            var subsetHash = GetBoardHash(subset);
            var fullHash = GetBoardHash(full);
            for (var column = 0; column < Board.Width; column++)
            {
                if (!IsSubset(subsetHash[column], fullHash[column]))
                    return false;
            }

            return true;
        }
    }
}
