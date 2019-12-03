using QuadRelate.Contracts;
using QuadRelate.Models;
using QuadRelate.Types;

namespace QuadRelate.Players.Rory
{
    public class CpuPlayer01 : IPlayer
    {
        public string Name => "Easy Swags";

        public int NextMove(Board board, Counter colour)
        {
            // Play only move available
            if (board.AvailableColumns().Count == 1)
                return board.AvailableColumns()[0];

            Board boardClone;

            // Play winning move
            foreach (var move in board.AvailableColumns())
            {
                boardClone = board.Clone();
                boardClone.PlaceCounter(move, colour);

                if (boardClone.IsGameOver())
                    return move;
            }

            // Play blocking move
            foreach (var move in board.AvailableColumns())
            {
                boardClone = board.Clone();
                boardClone.PlaceCounter(move, colour.ReverseCounter());

                if (boardClone.DoesWinnerExist())
                    return move;
            }

            // Do not allow two possible wins
            foreach (var move in board.AvailableColumns())
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

            // Play in highest column
            for (var row = Board.Height - 2; row >= 0; row--)
            {
                foreach (var move in board.AvailableColumns())
                {
                    if (board[move, row] != Counter.Empty)
                        return move;
                }
            }

            return board.AvailableColumns()[0];
        }

        public void GameOver(GameResult result)
        {
            // Ignore.
        }
    }
}
