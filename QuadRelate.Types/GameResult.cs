using System.Collections.Generic;

namespace QuadRelate.Types
{
    public class GameResult
    {
        public GameResult(Counter winner,  IList<int> moves)
        {
            Winner = winner;
            Moves = moves;
        }

        public Counter Winner { get; }
        public IList<int> Moves { get;  }
    }
}