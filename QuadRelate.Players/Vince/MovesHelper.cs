using System.Collections.Generic;
using System.Linq;
using QuadRelate.Models;
using QuadRelate.Types;

namespace QuadRelate.Players.Vince
{
    internal static class MovesHelper
    {
        private const int _centreColumn = 3;

        public static bool TryGetOpeningMove(Board board, Counter colour, out int move)
        {
            move = _centreColumn;
            if (board[move, 0] == Counter.Empty)
                return true;

            if (board[move, 0] == colour && board[move, 1] == colour.Invert() && board[move, 2] == Counter.Empty)
                return true;

            return false;
        }

        public static IList<int> GetMovesClosestToCentre(ICollection<int> moves)
        {
            var list = new List<int>();
            for (var offset = 0; offset <= 3; offset++)
            {
                var move = _centreColumn - offset;
                if (moves.Contains(move))
                    list.Add(move);

                if (offset > 0)
                {
                    move = _centreColumn + offset;
                    if (moves.Contains(move))
                        list.Add(move);
                }

                if (list.Any())
                    return list;
            }

            return list;
        }
    }
}