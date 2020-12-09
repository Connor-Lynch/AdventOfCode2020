using AdventOfCode.Day9.Entities;
using AdventOfCode.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day9
{
    public class Day9Solution : SolutionBase, ISolution
    {
        private IFileReader _fileReader;
        public List<CypherValue> _cypher;

        public Day9Solution(IFileReader fileReader)
        {
            _fileReader = fileReader;
            InitCypher();
            ResetSolution();
        }

        public void Solve()
        {
            StartTime();
            var invalidNumber = GetFirstInvalidCypherValue(25);
            SetAnswer(invalidNumber);

            StartTime();
            var encryptionWeakness = GetCypherEncryptionWeakness(invalidNumber);
            SetAnswer(encryptionWeakness);
        }

        public long GetFirstInvalidCypherValue(int preamble)
        {
            ValidateCypher(preamble);
            return _cypher.Skip(preamble).Where(c => c.Valid == false).Select(c => c.Number).First();
        }

        public long GetCypherEncryptionWeakness(long sum)
        {
            for (int index = 0; index < _cypher.Count; index++)
            {
                if (TryGetContiguousList(index, sum, out List<long> contiguousList))
                {
                    contiguousList.Sort();
                    return contiguousList.First() + contiguousList.Last();
                }
            }
            return 0;
        }

        private void ValidateCypher(int preamble)
        {
            for(int index = preamble; index < _cypher.Count; index ++)
            {
                var currentValue = _cypher[index];

                var currentPool = _cypher.GetRange(index - preamble, preamble).Select(c => c.Number);
                var combos = CombinationExtention.GetCombinations(currentPool, 2);
                var validPairs = combos.Where(c => c.Sum() == currentValue.Number);

                if (validPairs.Count() > 0)
                {
                    currentValue.Valid = true;
                }
            }
        }

        private bool TryGetContiguousList(int index, long sum, out List<long> contiguousList)
        {
            contiguousList = new List<long>();

            while (contiguousList.Sum() <= sum)
            {
                if (contiguousList.Sum() == sum)
                {
                    return true;
                }
                contiguousList.Add(_cypher[index].Number);
                index++;
            }
            return false;
        }

        private void InitCypher()
        {
            var rawInput = _fileReader.ReadFileToStringArray("Day9/data.json");
            MapInputToCypher(rawInput);
        }

        private void MapInputToCypher(List<string> rawInput)
        {
            _cypher = new List<CypherValue>();

            foreach (var input in rawInput)
            {
                _cypher.Add(new CypherValue()
                {
                    Number = Int64.Parse(input)
                });
            }
        }
    }
}
