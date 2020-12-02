using AdventOfCode.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

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

        public List<string> ReadFileToStringArray(string filePath)
        {
            List<string> result;

            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                result = JsonConvert.DeserializeObject<List<string>>(json);
            }

            return result;
        }
    }
}
