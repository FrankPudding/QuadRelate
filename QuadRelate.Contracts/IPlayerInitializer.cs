namespace QuadRelate.Contracts
{
    public interface IPlayerInitializer
    {
        IRandomizer Randomizer { get; set; }
        IGameRepository GameRepository { get; set; }
    }
}