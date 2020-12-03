using AdventOfCode.Day3.Entities;
using AdventOfCode.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day3
{
    public class Day3Solution : ISolution
    {
        private IFileReader _fileReader;
        public Map _map;

        public int _rightSteps;
        public int _downSteps;

        public Day3Solution(IFileReader fileReader)
        {
            _fileReader = fileReader;
            InitMap();
        }

        public void Solve()
        {
            _rightSteps = 3;
            _downSteps = 1;

            TraverseMap();
            var part1Answer = _map.Directions.TreesEncountered;
            Console.WriteLine($"Part 1: {part1Answer}");

            var part2Answer = EvaluatePart2(part1Answer);
            Console.WriteLine($"Part 2: {part2Answer}");
        }

        public int EvaluatePart2(int currentTrees)
        {
            ResetDirections();

            var treesOnEachPath = new List<int>();
            treesOnEachPath.Add(currentTrees);

            var paths = new List<List<int>>();
            paths.Add(new List<int>() { 1, 1 });
            paths.Add(new List<int>() { 5, 1 });
            paths.Add(new List<int>() { 7, 1 });
            paths.Add(new List<int>() { 1, 2 });
            
            foreach(var path in paths)
            {
                _rightSteps = path[0];
                _downSteps = path[1];
                TraverseMap();

                treesOnEachPath.Add(_map.Directions.TreesEncountered);

                ResetDirections();
            }

            return treesOnEachPath.Aggregate(1, (a, b) => a * b);
        }

        public void TraverseMap()
        {
            while(_map.Directions.CurrentRow != _map.Directions.FinalRow)
            {
                TakeAStep();
            }
        }

        private void TakeAStep()
        {
            SetNewPosition();
            CheckSurroundings();
            _map.Directions.RowsTraveled++;
        }

        private void SetNewPosition()
        {
            var newRow = _map.Directions.CurrentRow + _downSteps;
            var newCol = _map.Directions.CurrentCol + _rightSteps;

            if (newCol > _map.RowDistance)
            {
                newCol = newCol - _map.RowDistance - 1;
            }

            _map.Directions.CurrentRow = newRow;
            _map.Directions.CurrentCol = newCol;
        }

        private void CheckSurroundings()
        {
            var objectInCurrentPosition = _map.Data[_map.Directions.CurrentRow][_map.Directions.CurrentCol];

            if (objectInCurrentPosition == '#')
            {
                _map.Directions.TreesEncountered++;
            }
        }

        private void ResetDirections()
        {
            _map.Directions = new Directions()
            {
                StartingRow = 0,
                StartingCol = 0,
                FinalRow = _map.Data.Count - 1,
                CurrentRow = 0,
                CurrentCol = 0,
                RowsTraveled = 0,
                TreesEncountered = 0
            };
        }

        private void InitMap()
        {
            var rawInput = _fileReader.ReadFileToStringArray("Day3/data.json");
            MapDataToMapFromInput(rawInput);
        }

        private void MapDataToMapFromInput(List<string> rawInput)
        {
            _map = new Map()
            {
                Data = rawInput,
                RowDistance = rawInput[0].Length - 1,
                Directions = new Directions()
            };
            ResetDirections();
        }
    }
}
