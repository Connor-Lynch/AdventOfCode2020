using AdventOfCode.Day10.Entities;
using AdventOfCode.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day10
{
    public class Day10Solution : SolutionBase, ISolution
    {
        private IFileReader _fileReader;
        public List<Adapter> _adapters;

        public Day10Solution(IFileReader fileReader)
        {
            _fileReader = fileReader;
            InitAdaptersFromInput();
            ResetSolution();
        }

        public void Solve()
        {
            StartTime();
            var part1Answer = GetProductOfDifferences();
            SetAnswer(part1Answer);

            StartTime();
            var part2Answer = CountAllAdapterCombinations();
            SetAnswer(part2Answer);
        }

        public int GetProductOfDifferences()
        {
            SetAdapterDifferences();

            var differencesOfOne = _adapters.Skip(1).Where(a => a.Difference == 1).Count();
            var differencesOfThree = _adapters.Where(a => a.Difference == 3).Count();

            return differencesOfOne * differencesOfThree;
        }

        public void SetAdapterDifferences()
        {
            for(int index = 0; index < _adapters.Count; index ++)
            {
                var previousJoltage = index != 0 ? _adapters[index - 1].Joltage : 0;

                _adapters[index].Difference = _adapters[index].Joltage - previousJoltage;
            }
        }

        public long CountAllAdapterCombinations()
        {
            var adapterCounter = new List<long>();
            adapterCounter.Add(1);

            for (int index = 1; index < _adapters.Count; index++)
            {
                adapterCounter.Add(GetCombinationCountAtLocation(index, adapterCounter));
            }

            return adapterCounter.Last();
        }

        private long GetCombinationCountAtLocation(int index, List<long> adapterCounter)
        {
            long combinationsAtIndex = 0;

            if (_adapters[index].Joltage - _adapters[index - 1].Joltage <= 3)
            {
                combinationsAtIndex += adapterCounter[index - 1];
            }

            if (index > 1 && (_adapters[index].Joltage - _adapters[index - 2].Joltage <= 3))
            {
                combinationsAtIndex += adapterCounter[index - 2];
            }

            if (index > 2 && (_adapters[index].Joltage - _adapters[index - 3].Joltage <= 3))
            {
                combinationsAtIndex += adapterCounter[index - 3];
            }

            return combinationsAtIndex;
        }

        private void InitAdaptersFromInput()
        {
            var rawInput = _fileReader.ReadFileToIntArray("Day10/data.json");
            MapInputToAdapters(rawInput);
        }

        private void MapInputToAdapters(List<int> rawInput)
        {
            rawInput.Add(0);
            rawInput.Sort();
            rawInput.Add(rawInput.Last() + 3);
            _adapters = new List<Adapter>();

            foreach(var input in rawInput)
            {
                _adapters.Add(new Adapter() {
                    Joltage = input,
                    Difference = 0
                });
            }
        }
    }
}
