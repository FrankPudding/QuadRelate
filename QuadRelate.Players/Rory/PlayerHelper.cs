using QuadRelate.Models;
using QuadRelate.Types;
using System.Collections.Generic;

namespace QuadRelate.Players.Rory
{
    internal static class PlayerHelper
    {
        public static Dictionary<int, double> ExpectedScores(this Board board, Counter colour)
        {
            var expectedScores = new Dictionary<int, double>();
            var opponentColour = colour.ReverseCounter();
            var availableColumns = board.AvailableColumns();

            foreach (var move in availableColumns)
            {
                var boardClone = board.Clone();
                boardClone.PlaceCounter(move, colour);

                if (boardClone.IsWinAvailable(opponentColour) || boardClone.IsUnblockableMoveAvailable(opponentColour))
                {
                    expectedScores.Add(move, 0);
                    continue;
                }

                expectedScores.Add(move, 0.5);
            }

            return expectedScores;
        }

        private static bool IsWinAvailable(this Board board, Counter colour)
        {
            foreach (var move in board.AvailableColumns())
            {
                var boardClone = board.Clone();
                boardClone.PlaceCounter(move, colour);

                if (boardClone.DoesWinnerExist())
                    return true;
            }

            return false;
        }

        private static List<int> WinningMoves(this Board board, Counter colour)
        {
            var winningMoves = new List<int>();

            foreach (var move in board.AvailableColumns())
            {
                var boardClone = board.Clone();
                boardClone.PlaceCounter(move, colour);

                if (boardClone.DoesWinnerExist())
                    winningMoves.Add(move);
            }

            return winningMoves;
        }

        private static bool IsUnblockableMoveAvailable(this Board board, Counter colour)
        {
            foreach (var move in board.AvailableColumns())
            {
                var boardClone = board.Clone();
                boardClone.PlaceCounter(move, colour);

                if (boardClone.WinningMoves(colour).Count > 1)
                    return true;
            }

            return false;
        }
    }
}
