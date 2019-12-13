using QuadRelate.Contracts;
using System;

namespace QuadRelate.Externals
{
    public class MessageWriterConsole : IMessageWriter
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void Write(string message)
        {
            Console.Write(message);
        }
    }
}
