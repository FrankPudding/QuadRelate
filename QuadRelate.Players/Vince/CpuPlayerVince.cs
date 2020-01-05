using System.Collections.Generic;
using System.Linq;
using QuadRelate.Contracts;
using QuadRelate.Models;
using QuadRelate.Players.Vince.Helpers;
using QuadRelate.Types;

namespace QuadRelate.Players.Vince
{
    public class CpuPlayerVince : IPlayer
    {
        private readonly IRandomizer _randomizer;
        private Counter _currentColour;

        public string Name => "Invincible";

        public CpuPlayerVince(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        public int NextMove(Board board, Counter colour)
        {
            _currentColour = colour;

            if (MovesHelper.TryGetBasicMove(board, colour, out var move))
                return move;

            var scores = GetScores(board, colour);

            var bestScores = scores.Where(x => x.Value == scores.Values.Max());
            var bestMoves = bestScores.Select(x => x.Key).ToList();

            var centreMoves = MovesHelper.GetMovesClosestToCentre(bestMoves);
            move = centreMoves[_randomizer.Next(centreMoves.Count)];
            Log("{string.Join('.', scores)}: Chose {move}");
            return move;
        }

        public void GameOver(GameResult result)
        {
            if (result.Winner == _currentColour.Invert())
            {
                Log(string.Join('.', result.Moves));
            }
        }

        private static IDictionary<int, int> GetScores(Board board, Counter colour)
        {
            var scores = new Dictionary<int, int>();
            foreach (var myMove in board.AvailableColumns())
            {
                var opponentTotal = 0;
                var clone = board.Clone();
                clone.PlaceCounter(myMove, colour);
                var myScore = ScoreEvaluator.GetScore(clone, colour);
                foreach (var opponentMove in clone.AvailableColumns())
                {
                    var innerClone = clone.Clone();
                    innerClone.PlaceCounter(opponentMove, colour.ReverseCounter());
                    opponentTotal += ScoreEvaluator.GetScore(innerClone, colour.ReverseCounter());
                }

                scores.Add(myMove, myScore - (opponentTotal / clone.AvailableColumns().Count));
            }

            return scores;
        }

        private static void Log(string message)
        {
            var _ = message;
            //Debug.WriteLine(message);
        }
    } 
}