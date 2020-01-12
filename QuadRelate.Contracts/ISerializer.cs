using System.Collections.Generic;

namespace QuadRelate.Contracts
{
    public interface ISerializer
    {
        string Serialize(object obj, bool format = false);

        T Deserialize<T>(string text);

        IEnumerable<T> Deserialize<T>(IEnumerable<string> list);
    }
}