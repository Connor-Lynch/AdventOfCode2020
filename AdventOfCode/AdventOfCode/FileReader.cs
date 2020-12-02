using AdventOfCode.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode
{
    public class FileReader : IFileReader
    {
        public List<int> ReadFileToIntArray(string filePath)
        {
            List<int> result;

            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                result = JsonConvert.DeserializeObject<List<int>>(json);
            }

            return result;
        }
    }
}
