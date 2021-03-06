﻿using System.Collections.Generic;

namespace QuadRelate.Contracts
{
    public interface IFileReaderWriter
    {
        IEnumerable<string> ReadAllLines(string filepath);

        void WriteAllLines(string filepath, IEnumerable<string> lines, bool append = false);

        void WriteLine(string filepath, string line);
    }
}