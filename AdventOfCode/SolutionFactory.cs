using AdventOfCode.Day1;
using AdventOfCode.Day2;
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
                _ => new DefaultSolution(),
            };
        }
    }
}
