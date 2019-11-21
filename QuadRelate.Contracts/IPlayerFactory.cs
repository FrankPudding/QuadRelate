namespace QuadRelate.Contracts
{
    public interface IPlayerFactory
    {
        IPlayer CreatePlayer(string typeName);
    }
}
