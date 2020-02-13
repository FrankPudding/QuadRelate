using QuadRelate.Contracts;

namespace QuadRelate.Models
{
    public class PlayerInitializer : IPlayerInitializer
    {
        public PlayerInitializer(IRandomizer randomizer, IGameRepository gameRepository)
        {
            Randomizer = randomizer;
            GameRepository = gameRepository;
        }

        public IRandomizer Randomizer { get; set; }
        public IGameRepository GameRepository { get; set; }
    }
}