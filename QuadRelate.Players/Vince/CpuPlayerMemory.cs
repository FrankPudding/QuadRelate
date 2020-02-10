using System.Collections.Generic;
using System.Linq;
using QuadRelate.Contracts;
using QuadRelate.Models;
using QuadRelate.Players.Vince.Helpers;
using QuadRelate.Types;

namespace QuadRelate.Players.Vince
{
    public class CpuPlayerMemory : IPlayer
    {
        private readonly IRandomizer _randomizer;
        private readonly IBoardHasher _boardHasher;
        private readonly IEnumerable<BoardResult> _previousResults;

        public string Name => "Mr Swallow";
        
        public CpuPlayerMemory(IRandomizer randomizer, IBoardHasher boardHasher,  IResultsRepository resultsRepository)
        {
            _randomizer = randomizer;
            _boardHasher = boardHasher;

            _previousResults = resultsRepository.GetPreviousResultsAsync().Result;
        }

        public int NextMove(Board board, Counter colour)
        {
            if (MovesHelper.TryGetBasicMove(board, colour, out var move))
                return move;

            var scores = new Dictionary<int, int>();

            var playerHash = _boardHasher.GetHash(colour).ToString();
            var opponentHash = _boardHasher.GetHash(colour.Invert()).ToString();
            foreach (var m in board.AvailableColumns())
            {
                var clone = board.Clone();
                board.PlaceCounter(m, colour);
                var boardHash = _boardHasher.GetHash(clone);

                var score = 0;
                foreach (var r in _previousResults)
                {
                    if (_boardHasher.IsSubset(boardHash, r.Board))
                    {
                        if (r.Winner == playerHash) score++;
                        if (r.Winner == opponentHash) score--;
                    }
                }

                scores.Add(m, score);
            }

            var bestScores = scores.Where(x => x.Value == scores.Values.Max());
            var bestMoves = bestScores.Select(x => x.Key).ToList();

            var index = _randomizer.Next(bestMoves.Count);
            return bestMoves[index];
        }

        public void GameOver(GameResult result)
        {
            // Not used.
        }
    }
}
