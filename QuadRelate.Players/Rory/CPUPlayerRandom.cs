using QuadRelate.Contracts;
using QuadRelate.Models;
using QuadRelate.Types;

namespace QuadRelate.Players.Rory
{
    public class CpuPlayerRandom : IPlayer
    {
        private readonly IRandomizer _randomizer;

        public CpuPlayerRandom(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        public string Name => "The Randomizer";

        public int NextMove(Board board, Counter colour)
        {
            var index = _randomizer.Next(board.AvailableColumns().Count);

            return board.AvailableColumns()[index];
        }
    }
}