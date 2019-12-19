using System;
using QuadRelate.Contracts;
using QuadRelate.Players.Rory;
using QuadRelate.Players.Vince;

namespace QuadRelate.Factories
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
                case nameof(HumanPlayer): return new HumanPlayer();
                case nameof(CpuPlayerRandom): return new CpuPlayerRandom(_randomizer);
                case nameof(CpuPlayerVince): return new CpuPlayerVince(_randomizer);
                case nameof(CpuPlayerLefty): return new CpuPlayerLefty();
                case nameof(CpuPlayer01): return new CpuPlayer01();
                case nameof(CpuPlayer02): return new CpuPlayer02();
                case nameof(CPUPlayer03): return new CPUPlayer03(_randomizer);
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerType), "That player does not exist");
            }
        }
    }
}
