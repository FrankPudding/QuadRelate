namespace QuadRelate.Contracts
{
    public interface ICPUPlayerFactory
    {
        ICPUPlayer CreateCPUPlayer(string typeName);
    }
}
