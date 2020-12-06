using AdventOfCode.Day5.Entities;
using AdventOfCode.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day5
{
    public class Day5Solution : SolutionBase, ISolution
    {
        private IFileReader _fileReader;
        public List<BoardingPass> _boardingPasses;

        public Day5Solution(IFileReader fileReader)
        {
            _fileReader = fileReader;
            InitBoardingPasses();
            ResetSolution();
        }

        public void Solve()
        {
            StartTime();
            SortBoardingPasses();
            var part1Solution = _boardingPasses.Last().SeatID;
            SetAnswer(part1Solution);

            StartTime();
            var part2Solution = GetOpenSeat();
            SetAnswer(part2Solution);
        }

        public void SortBoardingPasses()
        {
            PopulateBoardingPassData();
            _boardingPasses = _boardingPasses.OrderBy(p => p.SeatID).ToList();
        }

        public int GetOpenSeat()
        {
            var currentIndex = 0;
            foreach(var pass in _boardingPasses)
            {
                if (currentIndex != _boardingPasses.Last().SeatID - 1)
                {
                    var nextSeatId = _boardingPasses[currentIndex + 1].SeatID;
                    if (pass.SeatID + 1 == nextSeatId - 1)
                    {
                        return pass.SeatID + 1;
                    }
                }
                currentIndex++;
            }
            return 0;
        }

        public void PopulateBoardingPassData()
        {
            foreach(var boardingPass in _boardingPasses)
            {
                boardingPass.Row = GetBoardingPassRow(boardingPass.BianaryValue);
                boardingPass.Column = GetBoardingPassColumn(boardingPass.BianaryValue);
                boardingPass.SeatID = GetBoardingPassSeatId(boardingPass.Row, boardingPass.Column);
            }
        }

        private int GetBoardingPassSeatId(int row, int column)
        {
            return (row * 8) + column;
        }

        private int GetBoardingPassRow(string bianaryValue)
        {
            var rowData = bianaryValue.Take(7).ToList();
            return GetValueFromBinaryData(0, 127, rowData);
        }

        private int GetBoardingPassColumn(string bianaryValue)
        {
            var columnData = bianaryValue.Substring(bianaryValue.Length - 3).ToList();
            return GetValueFromBinaryData(0, 7, columnData);
        }

        private int GetValueFromBinaryData(int min, int max, List<char> binaryData)
        {
            foreach (var data in binaryData)
            {
                var middlePoint = (min + max) / 2D;
                switch (data)
                {
                    case 'L':
                    case 'F':
                        {
                            max = (int)Math.Floor(middlePoint);
                            break;
                        }
                    case 'R':
                    case 'B':
                        {
                            min = (int)Math.Ceiling(middlePoint);
                            break;
                        }
                }
            }

            return binaryData.Last() == 'F' || binaryData.Last() == 'L' ? min : max;
        }

        private void InitBoardingPasses()
        {
            var rawBoardingPasses = _fileReader.ReadFileToStringArray("Day5/data.json");
            MapDatatToBoardingPasses(rawBoardingPasses);
        }

        private void MapDatatToBoardingPasses(List<string> rawInput)
        {
            _boardingPasses = new List<BoardingPass>();

            foreach(var boardingPass in rawInput)
            {
                _boardingPasses.Add(
                    new BoardingPass()
                    {
                        BianaryValue = boardingPass
                    }
                );
            }
        }
    }
}
