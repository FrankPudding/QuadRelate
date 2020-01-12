using System.Collections.Generic;
using Newtonsoft.Json;
using QuadRelate.Contracts;

namespace QuadRelate.Models
{
    public class Serializer : ISerializer
    {
        public string Serialize(object obj, bool format = false)
        {
            return JsonConvert.SerializeObject(obj, format ? Formatting.Indented : Formatting.None);
        }

        public T Deserialize<T>(string text)
        {
            return JsonConvert.DeserializeObject<T>(text);
        }

        public IEnumerable<T> Deserialize<T>(IEnumerable<string> list)
        {
            foreach (var item in list)
            {
                yield return Deserialize<T>(item);
            }
        }
    }
}