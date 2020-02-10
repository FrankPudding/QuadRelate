using System;
using QuadRelate.Contracts;
using QuadRelate.Players.Rory;
using QuadRelate.Players.Vince;

namespace QuadRelate.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IRandomizer _randomizer;
        private readonly IGameRepository _gameRepository;
        private readonly IBoardHasher _boardHasher;
        private readonly IResultsRepository _resultsRepository;

        public PlayerFactory(IRandomizer randomizer, IGameRepository gameRepository, IBoardHasher boardHasher, IResultsRepository resultsRepository)
        {
            _randomizer = randomizer;
            _gameRepository = gameRepository;
            _boardHasher = boardHasher;
            _resultsRepository = resultsRepository;
        }

        public IPlayer CreatePlayer(string playerType)
        {
            switch (playerType)
            {
                case nameof(HumanPlayer): return new HumanPlayer();
                case nameof(CpuPlayerRandom): return new CpuPlayerRandom(_randomizer);
                case nameof(CpuPlayerVince): return new CpuPlayerVince(_randomizer);
                case nameof(CpuPlayerCellEvaluator): return new CpuPlayerCellEvaluator(_randomizer);
                case nameof(CpuPlayerRowDominator): return new CpuPlayerRowDominator(_randomizer);
                case nameof(CpuPlayerCentre): return new CpuPlayerCentre(_randomizer);
                case nameof(CpuPlayerLefty): return new CpuPlayerLefty();
                case nameof(CpuPlayerMemory): return new CpuPlayerMemory(_randomizer, _boardHasher, _resultsRepository);
                case nameof(CpuPlayer01): return new CpuPlayer01();
                case nameof(CpuPlayer02): return new CpuPlayer02();
                case nameof(CPUPlayer03): return new CPUPlayer03(_randomizer, _gameRepository);
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerType), "That player does not exist");
            }
        }
    }
}
