using AdventOfCode.Day11.Entites;
using AdventOfCode.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day11
{
    public class Day11Solution : SolutionBase, ISolution
    {
        private IFileReader _fileReader;
        public Seating _seating;

        public Day11Solution(IFileReader fileReader)
        {
            _fileReader = fileReader;
            InitSeatingFromInput();
            ResetSolution();
        }

        public void Solve()
        {
            StartTime();
            var part1Asnwer = StabilizeSeating(4, false);
            SetAnswer(part1Asnwer);

            InitSeatingFromInput();

            StartTime();
            var part2Asnwer = StabilizeSeating(5, true);
            SetAnswer(part2Asnwer);
        }

        public int StabilizeSeating(int toleratedOccupiedSeats, bool inSight)
        {
            var occupiedItterations = new List<int>();
            bool run = true;
            while (run)
            {
                ComputSeatingCycle(toleratedOccupiedSeats, inSight);
                occupiedItterations.Add(_seating.Taken);

                if(occupiedItterations.Count > 1)
                {
                    var lastTwo = occupiedItterations.TakeLast(2);
                    if (lastTwo.First() == lastTwo.Last())
                    {
                        run = false;
                    }
                }
            }

            return occupiedItterations.Last();
        }

        private void PrintTable()
        {
            foreach(var row in _seating.Layout)
            {
                Console.WriteLine(row);
            }
            Console.WriteLine();
        }

        public void ComputSeatingCycle(int toleratedOccupiedSeats, bool inSight)
        {
            var newLayout = _seating.Layout.Select(elt => (string)elt.Clone()).ToList();

            for (int indexRow = 0; indexRow < _seating.Layout.Count; indexRow ++)
            {
                for(int indexCol = 0; indexCol < _seating.Layout[indexRow].Length; indexCol ++)
                {
                    var currenctChar = _seating.Layout[indexRow][indexCol];
                    if (currenctChar != '.')
                    {
                        switch (currenctChar)
                        {
                            case '#':
                                {
                                    var occupiedSeats = CheckSeatSurroundings(indexRow, indexCol, inSight);

                                    if (occupiedSeats >= toleratedOccupiedSeats)
                                    {
                                        var newRow = newLayout[indexRow].ToCharArray();
                                        newRow[indexCol] = 'L';
                                        newLayout[indexRow] = new string(newRow);

                                        _seating.Taken--;
                                        _seating.Open++;
                                    }
                                    break;
                                }
                            case 'L':
                                {
                                    var occupiedSeats = CheckSeatSurroundings(indexRow, indexCol, inSight);

                                    if (occupiedSeats == 0)
                                    {
                                        var newRow = newLayout[indexRow].ToCharArray();
                                        newRow[indexCol] = '#';
                                        newLayout[indexRow] = new string(newRow);

                                        _seating.Taken++;
                                        _seating.Open--;
                                    }
                                    break;
                                }
                        }
                    }
                }
            }

            _seating.Layout = newLayout;
        }

        public int CheckSeatSurroundings(int indexRow, int indexCol, bool inSight)
        {
            int numberOfOccupiedAdjacesntSeats = 0;

            var test1 = CheckSeatsAbove(indexRow, indexCol, inSight) ? 1 : 0;
            var test2 = CheckSeatsAboveLeft(indexRow, indexCol, inSight) ? 1 : 0;
            var test3 = CheckSeatsAboveRight(indexRow, indexCol, inSight) ? 1 : 0;
            var test4 = CheckSeatsBelow(indexRow, indexCol, inSight) ? 1 : 0;
            var test5 = CheckSeatsBelowLeft(indexRow, indexCol, inSight) ? 1 : 0;
            var test6 = CheckSeatsBelowRight(indexRow, indexCol, inSight) ? 1 : 0;
            var test7 = CheckSeatsRight(indexRow, indexCol, inSight) ? 1 : 0;
            var test8 = CheckSeatsLeft(indexRow, indexCol, inSight) ? 1 : 0;

            numberOfOccupiedAdjacesntSeats += CheckSeatsAbove(indexRow, indexCol, inSight) ? 1 : 0;
            numberOfOccupiedAdjacesntSeats += CheckSeatsAboveLeft(indexRow, indexCol, inSight) ? 1 : 0;
            numberOfOccupiedAdjacesntSeats += CheckSeatsAboveRight(indexRow, indexCol, inSight) ? 1 : 0;
            numberOfOccupiedAdjacesntSeats += CheckSeatsBelow(indexRow, indexCol, inSight) ? 1 : 0;
            numberOfOccupiedAdjacesntSeats += CheckSeatsBelowLeft(indexRow, indexCol, inSight) ? 1 : 0;
            numberOfOccupiedAdjacesntSeats += CheckSeatsBelowRight(indexRow, indexCol, inSight) ? 1 : 0;
            numberOfOccupiedAdjacesntSeats += CheckSeatsRight(indexRow, indexCol, inSight) ? 1 : 0;
            numberOfOccupiedAdjacesntSeats += CheckSeatsLeft(indexRow, indexCol, inSight) ? 1 : 0;

            return numberOfOccupiedAdjacesntSeats;
        }

        private bool CheckSeatsAbove(int indexRow, int indexCol, bool inSight)
        {
            var occupiedSeat = false;
            if (indexRow > 0)
            {
                if (_seating.Layout[indexRow - 1][indexCol] == '#')
                {
                    return true;
                }
                if (inSight && _seating.Layout[indexRow - 1][indexCol] == 'L')
                {
                    return false;
                }
            }
            else
            {
                return occupiedSeat;
            }
            if (inSight)
            {
                return CheckSeatsAbove(indexRow - 1, indexCol, inSight);
            }
            return occupiedSeat;
        }

        private bool CheckSeatsAboveLeft(int indexRow, int indexCol, bool inSight)
        {
            var occupiedSeat = false;
            if (indexRow > 0 && indexCol > 0)
            {
                if(_seating.Layout[indexRow - 1][indexCol - 1] == '#')
                {
                    return true;
                }
                if (inSight && _seating.Layout[indexRow - 1][indexCol - 1] == 'L')
                {
                    return false;
                }
            }
            else
            {
                return occupiedSeat;
            }
            if (inSight)
            {
                return CheckSeatsAboveLeft(indexRow - 1, indexCol - 1, inSight);
            }
            return occupiedSeat;
        }

        private bool CheckSeatsAboveRight(int indexRow, int indexCol, bool inSight)
        {
            var occupiedSeat = false;
            if (indexRow > 0 && indexCol < _seating.Layout[indexRow].Length - 1)
            {
                if(_seating.Layout[indexRow - 1][indexCol + 1] == '#')
                {
                    return true;
                }
                if (inSight && _seating.Layout[indexRow - 1][indexCol + 1] == 'L')
                {
                    return false;
                }
            }
            else
            {
                return occupiedSeat;
            }
            if (inSight)
            {
                return CheckSeatsAboveRight(indexRow - 1, indexCol + 1, inSight);
            }
            return occupiedSeat;
        }

        private bool CheckSeatsBelow(int indexRow, int indexCol, bool inSight)
        {
            var occupiedSeat = false;
            if (indexRow < _seating.Layout.Count - 1)
            {
                if (_seating.Layout[indexRow + 1][indexCol] == '#')
                {
                    return true;
                }
                if (inSight && _seating.Layout[indexRow + 1][indexCol] == 'L')
                {
                    return false;
                }
            }
            else
            {
                return occupiedSeat;
            }
            if (inSight)
            {
                return CheckSeatsBelow(indexRow + 1, indexCol, inSight);
            }
            return occupiedSeat;
        }

        private bool CheckSeatsBelowLeft(int indexRow, int indexCol, bool inSight)
        {
            var occupiedSeat = false;
            if (indexRow < _seating.Layout.Count - 1 && indexCol > 0)
            {
                if (_seating.Layout[indexRow + 1][indexCol - 1] == '#')
                {
                    return true;
                }
                if (inSight && _seating.Layout[indexRow + 1][indexCol - 1] == 'L')
                {
                    return false;
                }
            }
            else
            {
                return occupiedSeat;
            }
            if (inSight)
            {
                return CheckSeatsBelowLeft(indexRow + 1, indexCol - 1, inSight);
            }
            return occupiedSeat;
        }

        private bool CheckSeatsBelowRight(int indexRow, int indexCol, bool inSight)
        {
            var occupiedSeat = false;
            if (indexRow < _seating.Layout.Count - 1 && indexCol < _seating.Layout[indexRow].Length - 1)
            {
                if (_seating.Layout[indexRow + 1][indexCol + 1] == '#')
                {
                    return true;
                }
                if (inSight && _seating.Layout[indexRow + 1][indexCol + 1] == 'L')
                {
                    return false;
                }
            }
            else
            {
                return occupiedSeat;
            }
            if (inSight)
            {
                return CheckSeatsBelowRight(indexRow + 1, indexCol + 1, inSight);
            }
            return occupiedSeat;
        }

        private bool CheckSeatsLeft(int indexRow, int indexCol, bool inSight)
        {
            var occupiedSeat = false;
            if (indexCol > 0)
            {
                if (_seating.Layout[indexRow][indexCol - 1] == '#')
                {
                    return true;
                }
                if (inSight && _seating.Layout[indexRow][indexCol - 1] == 'L')
                {
                    return false;
                }
            }
            else
            {
                return occupiedSeat;
            }
            if (inSight)
            {
                return CheckSeatsLeft(indexRow, indexCol - 1, inSight);
            }
            return occupiedSeat;
        }

        private bool CheckSeatsRight(int indexRow, int indexCol, bool inSight)
        {
            var occupiedSeat = false;
            if (indexCol < _seating.Layout[indexRow].Length - 1)
            {
                if (_seating.Layout[indexRow][indexCol + 1] == '#')
                {
                    return true;
                }
                if (inSight && _seating.Layout[indexRow][indexCol + 1] == 'L')
                {
                    return false;
                }
            }
            else
            {
                return occupiedSeat;
            }
            if (inSight)
            {
                return CheckSeatsRight(indexRow, indexCol + 1, inSight);
            }
            return occupiedSeat;
        }

        private void InitSeatingFromInput()
        {
            var rawInput = _fileReader.ReadFileToStringArray("Day11/data.json");
            MapSeatingFromInput(rawInput);
        }

        private void MapSeatingFromInput(List<string> rawInput)
        {
            var rawString = string.Join(",", rawInput);
            _seating = new Seating()
            {
                Layout = rawInput,
                NumberOfSeats = rawString.Count(c => c == 'L'),
                Open = rawString.Count(c => c == 'L'),
                Taken = 0
            };
        }
    }
}
