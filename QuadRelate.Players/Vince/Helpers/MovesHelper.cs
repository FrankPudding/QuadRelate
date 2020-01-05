using System.Collections.Generic;
using System.Linq;
using QuadRelate.Models;
using QuadRelate.Types;

namespace QuadRelate.Players.Vince.Helpers
{
    internal static class MovesHelper
    {
        private const int _centreColumn = 3;

        public static bool TryGetBasicMove(Board board, Counter colour, out int bestMove)
        {
            bestMove = _centreColumn;

            // 1. If only one possible move - play it.
            var availableMoves = board.AvailableColumns();
            if (availableMoves.Count == 1)
            {
                bestMove = availableMoves[0];
                return true;
            }

            // 2. Starting moves.
            if (TryGetOpeningMove(board, colour, out var move))
            {
                bestMove = move;
                return true;
            }

            // 3. If there's a winning move - play it.
            if (TryGetWinningMove(board, colour, out var winningMove))
            {
                bestMove = winningMove;
                return true;
            }

            // 4. If there's a blocking move - play it.
            if (TryGetWinningMove(board, colour.Invert(), out var opponentWinningMove))
            {
                bestMove = opponentWinningMove;
                return true;
            }

            return false;
        }

        private static bool TryGetWinningMove(Board board, Counter colour, out int bestMove)
        {
            bestMove = _centreColumn;
            foreach (var move in board.AvailableColumns())
            {
                var clone = board.Clone();
                clone.PlaceCounter(move, colour);
                if (clone.IsGameOver())
                {
                    bestMove = move;
                    return true;
                }
            }

            return false;
        }

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