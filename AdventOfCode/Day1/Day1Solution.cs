using AdventOfCode.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day1
{
    public class Day1Solution : ISolution
    {
        private IFileReader _fileReader;
        private List<int> _expenses;

        public Day1Solution(IFileReader fileReader)
        {
            _fileReader = fileReader;
            _expenses = InitExpenses();
        }

        public void Solve()
        {
            var part1Answer = GetProductOfExpenses(2);
            Console.WriteLine($"Part 1: {part1Answer}");

            var part2Answer = GetProductOfExpenses(3);
            Console.WriteLine($"Part 2: {part2Answer}");
        }

        public int GetProductOfExpenses(int numberOfExpenses)
        {
            var combos = CombinationsExtention.GetCombinations(_expenses, numberOfExpenses);
            var foundExpenses = combos.Where(c => c.Sum() == 2020).FirstOrDefault();
            var answer = foundExpenses.Aggregate(1, (a, b) => a * b); ;

            return answer;
        }

        private List<int> InitExpenses()
        {
            return _fileReader.ReadFileToIntArray("Day1/data.json");
        }
    }
}
