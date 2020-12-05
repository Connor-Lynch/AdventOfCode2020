using AdventOfCode.Day1;
using AdventOfCode.Day2;
using AdventOfCode.Day3;
using AdventOfCode.Day4;
using AdventOfCode.Day5;
using AdventOfCode.Interfaces;

namespace AdventOfCode
{
    public class SolutionFactory : ISolutionFactory
    {
        private IFileReader _fileReader;
        
        public SolutionFactory()
        {
            _fileReader = new FileReader();
        }

        public ISolution GetSolution(string day)
        {
            return day switch
            {
                "1" => new Day1Solution(_fileReader),
                "2" => new Day2Solution(_fileReader),
                "3" => new Day3Solution(_fileReader),
                "4" => new Day4Solution(_fileReader),
                "5" => new Day5Solution(_fileReader),
                _ => new DefaultSolution(),
            };
        }
    }
}
