using QuadRelate.Contracts;
using QuadRelate.Helpers;
using QuadRelate.Types;

namespace QuadRelate.Players.Vince
{
    public class CpuPlayerBasic : IPlayer
    {
        private readonly IRandomizer _randomizer;

        public CpuPlayerBasic(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        public string Name => "Invincible";

        public int NextMove(Board board, Cell colour)
        {
            var available = board.AvailableColumns();

            // 1. If only one possible move - play it.
            if (available.Count == 1)
                return available[0];

            // 2. If there's a winning move - play it.
            foreach (var m in available)
            {
                var clone = board.Clone();
                clone.PlaceCounter(m, colour);
                if (clone.IsGameOver())
                    return m;
            }

            // 3. If there's a blocking move - play it.
            var opponent = Invert(colour);
            foreach (var m in available)
            {
                var clone = board.Clone();
                clone.PlaceCounter(m, opponent);
                if (clone.IsGameOver())
                    return m;
            }

            // 4. If we can win in two moves - play it.
            foreach (var m in available)
            {
                var clone = board.Clone();
                clone.PlaceCounter(m, colour);
                var nextLotOfAvailable = clone.AvailableColumns();
                foreach (var n in nextLotOfAvailable)
                {
                    clone.PlaceCounter(n, colour);
                    if (clone.IsGameOver())
                        return n;
                }
            }

            // 5. Fallback - play random move.
            return available[_randomizer.Next(available.Count)];
        }

        private static Cell Invert(Cell colour)
        {
            return (colour == Cell.Yellow) ? Cell.Red : Cell.Yellow;
        }
    } 
}