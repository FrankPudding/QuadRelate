using System;
using QuadRelate.Contracts;
using QuadRelate.Players.Rory;
using QuadRelate.Players.Vince;

namespace QuadRelate.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IPlayerInitializer _playerInitializer;

        public PlayerFactory(IPlayerInitializer playerInitializer)
        {
            _playerInitializer = playerInitializer;
        }

        public IPlayer CreatePlayer(string playerType)
        {
            switch (playerType)
            {
                case nameof(HumanPlayer): return new HumanPlayer();
                case nameof(CpuPlayerRandom): return new CpuPlayerRandom(_playerInitializer);
                case nameof(CpuPlayerVince): return new CpuPlayerVince(_playerInitializer);
                case nameof(CpuPlayerCellEvaluator): return new CpuPlayerCellEvaluator(_playerInitializer);
                case nameof(CpuPlayerRowDominator): return new CpuPlayerRowDominator(_playerInitializer);
                case nameof(CpuPlayerCentre): return new CpuPlayerCentre(_playerInitializer);
                case nameof(CpuPlayerLefty): return new CpuPlayerLefty(_playerInitializer);
                case nameof(CpuPlayer01): return new CpuPlayer01(_playerInitializer);
                case nameof(CpuPlayer02): return new CpuPlayer02(_playerInitializer);
                case nameof(CPUPlayer03): return new CPUPlayer03(_playerInitializer);
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerType), "That player does not exist");
            }
        }
    }
}
