using AdventOfCode.Day13.Entities;
using AdventOfCode.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day13
{
    public class Day13Solution : SolutionBase, ISolution
    {
        private IFileReader _fileReader;
        public int _arrivalTime;
        public List<Bus> _buses;

        public Day13Solution(IFileReader fileReader)
        {
            _fileReader = fileReader;
            InitBusesFromInput();
            ResetSolution();
        }

        public void Solve()
        {
            StartTime();
            var part1Answer = GetProductOfEarliestBusAndWithTime();
            SetAnswer(part1Answer);

            StartTime();
            var part2Answer = GetEarliestSubsequentDepartures();
            SetAnswer(part2Answer);
        }

        public int GetProductOfEarliestBusAndWithTime()
        {
            var earliestBus = GetTheEarliestBusAvailable();
            var waitTime = earliestBus.EarliestAvailableTimeStamp - _arrivalTime;

            return earliestBus.Id * waitTime;
        }

        public long GetEarliestSubsequentDepartures()
        {
            _buses = _buses.OrderBy(b => b.Position).ToList();
            return FindEarliestSubsequestDepartureTime();
        }

        public Bus GetTheEarliestBusAvailable()
        {
            SetArrivalTimesForBuses();

            _buses = _buses.OrderBy(b => b.EarliestAvailableTimeStamp).ToList();
            return _buses.First();
        }

        public void SetArrivalTimesForBuses() 
        {
            foreach(var bus in _buses)
            {
                var baseFactor = _arrivalTime / bus.Id;
                var clossestInterval = baseFactor * bus.Id;

                while(clossestInterval < _arrivalTime)
                {
                    clossestInterval += bus.Id;
                }

                bus.EarliestAvailableTimeStamp = clossestInterval;
            }
        }

        public long FindEarliestSubsequestDepartureTime()
        {
            var increment = _buses[0].Id;
            var busIndex = 1;
            long runningTime;
            for (runningTime = _buses[0].Id; busIndex < _buses.Count; runningTime += increment)
            {
                if ((runningTime + _buses[busIndex].Position) % _buses[busIndex].Id == 0)
                {
                    increment *= _buses[busIndex].Id;
                    busIndex++;
                }
            }

            return runningTime - increment;
        }

        private void InitBusesFromInput()
        {
            var rawInput = _fileReader.ReadFileToStringArray("Day13/data.json");
            MapBusesFromInput(rawInput);
        }

        private void MapBusesFromInput(List<string> rawInput)
        {
            _arrivalTime = Int32.Parse(rawInput[0]);
            _buses = new List<Bus>();

            var input = rawInput[1].Split(',');

            for (var index = 0; index < input.Length; index ++)
            {
                if (input[index] != "x")
                {
                    _buses.Add(new Bus()
                    {
                        Id = Int32.Parse(input[index]),
                        Position = index,
                        EarliestAvailableTimeStamp = 0
                    });
                }
            }
        }
    }
}
