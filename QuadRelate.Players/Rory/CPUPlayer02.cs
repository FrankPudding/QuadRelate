using QuadRelate.Contracts;
using QuadRelate.Models;
using QuadRelate.Types;
using System.Collections.Generic;
using System.Diagnostics;

namespace QuadRelate.Players.Rory
{
    public class CpuPlayer02 : IPlayer
    {
        private Counter _currentColour;
        private const int _middleColumn = 3;

        public string Name => "Not So Fast Swaggy";

        private readonly IPlayerInitializer _playerInitializer;

        public CpuPlayer02(IPlayerInitializer playerInitializer)
        {
            _playerInitializer = playerInitializer;
        }

        public int NextMove(Board board, Counter colour)
        {
            _currentColour = colour;
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

            // Play middle column if first move
            if (board[_middleColumn, 0] == Counter.Empty)
            {
                return _middleColumn;
            }

            var nonLosingMoves = new List<int>(availableColumns);

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

            // Play in highest column
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

        public void GameOver(GameResult result)
        {
            if (result.Winner == _currentColour.Invert())
            {
                Debug.WriteLine(string.Join('.', result.Moves));
            }
        }
    }
}
