using QuadRelate.Contracts;
using System;
using QuadRelate.Players.Rory;
using QuadRelate.Players.Vince;

namespace QuadRelate.Models
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IRandomizer _randomizer;

        public PlayerFactory(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        public IPlayer CreatePlayer(string playerType)
        {
            switch (playerType)
            {
                case nameof(CpuPlayerRandom): return new CpuPlayerRandom();
                case nameof(CpuPlayerBasic): return new CpuPlayerBasic(_randomizer);
                case nameof(CpuPlayerLefty): return new CpuPlayerLefty();
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerType), "That player does not exist");
            }
        }
    }
}
