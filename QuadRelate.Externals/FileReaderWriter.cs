using QuadRelate.Contracts;
using System.Collections.Generic;
using System.IO;

public class FileReaderWriter : IFileReaderWriter
{
    public IEnumerable<string> ReadAllLines(string filepath)
    {
        if (!File.Exists(filepath))
        {
            yield break;
        }

        using (var streamReader = new StreamReader(filepath))
        {
            var line = streamReader.ReadLine();

            while (line != null)
            {
                yield return line;
                line = streamReader.ReadLine();
            }
        }
    }

    public void WriteAllLines(string filepath, IEnumerable<string> lines, bool append = false)
    {
        using (var streamWriter = new StreamWriter(filepath, append))
        {
            foreach (var line in lines)
            {
                streamWriter.WriteLine(line);
            }
        }
    }

    public void WriteLine(string filepath, string line)
    {
        using (var streamWriter = new StreamWriter(filepath))
        {
            streamWriter.WriteLine(line);
        }
    }
}