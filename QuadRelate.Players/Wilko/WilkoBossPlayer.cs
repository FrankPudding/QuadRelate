using QuadRelate.Contracts;
using QuadRelate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuadRelate.Players.Wilko
{
    public class WilkoBossPlayer : IPlayer
    {
        private List<GameHistory> _oldGames = new List<GameHistory>();
        private bool firstMove;

        public string Name => nameof(WilkoBossPlayer);

        public void GameOver(GameResult result)
        {
            throw new NotImplementedException();
        }

        public int NextMove(Board board, Counter colour)
        {
            if (board.IsFirstMove())
            {
                firstMove = true;
                int move = Board.Width / 2 + 1;
                if (_oldGames.Count == 0)
                {
                    _oldGames.Add(new GameHistory(new List<int> { move }, colour));
                }
                return move;
            }
            else if (board.IsSecondMove())
            {
                firstMove = false;
            }

            var currentMoves = board.Moves.ToList();
            var currentGame = new GameHistory(currentMoves, colour);
            var oldGameMatches = _oldGames.Where(g => g.DoesGameContainPosition(currentGame.Moves)).ToList();
            if (oldGameMatches.Count == 0)
            {
                // This position doesn't exist
                // Create new position
                // Add it to _oldGames
                // return random move?
            }

            var winners = oldGameMatches.Where(g => g.Result == Outcome.Win).ToList();
            if (winners.Count > 0)
            {
                // Evaulate winners somehow ?  Score number of wins?
            }


            throw new NotImplementedException();
        }
    }

    public static class BoardExtensions
    {
        public static bool IsFirstMove(this Board board)
        {
            for (var x = 0; x < Board.Width; x++)
                if (board[x, 0] != Counter.Empty)
                    return false;
            return true;
        }

        public static bool IsSecondMove(this Board board)
        {
            int count = 0;
            for (var x = 0; x < Board.Width; x++)
            {
                if (board[x, 0] != Counter.Empty)
                    count++;
            }
            return count == 1;
        }
    }
}
