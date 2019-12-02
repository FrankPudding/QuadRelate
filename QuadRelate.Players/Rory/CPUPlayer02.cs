using QuadRelate.Contracts;
using QuadRelate.Models;
using QuadRelate.Types;
using System.Collections.Generic;

namespace QuadRelate.Players.Rory
{
    public class CPUPlayer02 : IPlayer
    {
        public string Name => "Not So Fast Swaggy";

        public int NextMove(Board board, Counter colour)
        {
            var availableColumns = board.AvailableColumns();

            // Play only move available
            if (availableColumns.Count == 1)
                return availableColumns[0];

            Board boardClone;

            // Play winning move
            foreach (var move in availableColumns)
            {
                boardClone = board.Clone();
                boardClone.PlaceCounter(move, colour);

                if (boardClone.IsGameOver())
                    return move;
            }

            // Play blocking move
            foreach (var move in availableColumns)
            {
                boardClone = board.Clone();
                boardClone.PlaceCounter(move, colour.ReverseCounter());

                if (boardClone.DoesWinnerExist())
                    return move;
            }

            // Do not allow two possible wins
            foreach (var move in availableColumns)
            {
                boardClone = board.Clone();
                boardClone.PlaceCounter(move, colour.ReverseCounter());

                if (boardClone.AvailableColumns().Contains(move + 3))
                    boardClone.PlaceCounter(move + 3, colour.ReverseCounter());
                else
                    continue;

                if (boardClone.DoesWinnerExist())
                    return move;
            }

            var nonLosingMoves = new List<int>();
            foreach (var move in availableColumns)
            {
                nonLosingMoves.Add(move);
            }

            // Don't play moves that allow the opponent to win
            foreach (var move in availableColumns)
            {
                boardClone = board.Clone();

                boardClone.PlaceCounter(move, colour);

                if (boardClone.AvailableColumns().Contains(move))
                {
                    boardClone.PlaceCounter(move, colour.ReverseCounter());

                    if (boardClone.DoesWinnerExist())
                        nonLosingMoves.Remove(move);
                }
            }

            // Don't play moves that allow the opponent two possible wins
            foreach (var move in availableColumns)
            {
                if (nonLosingMoves.Contains(move))
                {
                    boardClone = board.Clone();
                    boardClone.PlaceCounter(move, colour);

                    var postMoveAvailableColumns = boardClone.AvailableColumns();

                    foreach (var opponentMove in postMoveAvailableColumns)
                    {
                        var boardPostMove = boardClone.Clone();
                        boardPostMove.PlaceCounter(opponentMove, colour.ReverseCounter());

                        if (WinningMoves(boardPostMove, colour.ReverseCounter()).Count > 1)
                            nonLosingMoves.Remove(move);
                    }
                }
            }

            // Play highest column
            for (var row = Board.Height - 2; row >= 0; row--)
            {
                foreach (var move in nonLosingMoves)
                {
                    if (board[move, row] != Counter.Empty)
                        return move;
                }
            }

            // Failsafe that shouldn't be used
            return availableColumns[0];
        }

        private List<int> WinningMoves(Board board, Counter colour)
        {
            var winningMoves = new List<int>();

            foreach (var move in board.AvailableColumns().ToArray())
            {
                var boardClone = board.Clone();
                boardClone.PlaceCounter(move, colour);

                if (!boardClone.DoesWinnerExist())
                    winningMoves.Add(move);
            }

            return winningMoves;
        }
    }
}
