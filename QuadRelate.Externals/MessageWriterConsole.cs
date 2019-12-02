using QuadRelate.Contracts;
using System;

namespace QuadRelate.Externals
{
    public class MessageWriterConsole : IMessageWriter
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
