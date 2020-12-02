using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Interfaces
{
    public interface IFileReader
    {
        public List<int> ReadFileToIntArray(string filePath);
        public List<string> ReadFileToStringArray(string filePath);
    }
}
