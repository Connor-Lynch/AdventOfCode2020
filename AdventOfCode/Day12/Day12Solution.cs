using AdventOfCode.Day12.Entities;
using AdventOfCode.Day12.Enums;
using AdventOfCode.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day12
{
    public class Day12Solution : SolutionBase, ISolution
    {
        private IFileReader _fileReader;
        public Ship _ship;

        public Day12Solution(IFileReader fileReader)
        {
            _fileReader = fileReader;
            InitShipFromInput();
            ResetSolution();
        }

        public void Solve()
        {
            StartTime();
            var part1Answer = GetManhattanDistance();
            SetAnswer(part1Answer);

            ResetShip();

            StartTime();
            var part2Answer = GetManhattanDistanceWithWaypoints();
            SetAnswer(part2Answer);
        }

        public int GetManhattanDistance()
        {
            TraverseDirections();
            var northSouth = Math.Abs(_ship.North - _ship.South);
            var eastWest = Math.Abs(_ship.East - _ship.West);

            return northSouth + eastWest;
        }

        public int GetManhattanDistanceWithWaypoints()
        {
            TraverseWaypoints();
            var northSouth = Math.Abs(_ship.North - _ship.South);
            var eastWest = Math.Abs(_ship.East - _ship.West);

            return northSouth + eastWest;
        }

        public void TraverseDirections()
        {
            foreach(var direction in _ship.Directions)
            {
                switch(direction.Heading) 
                {
                    case Heading.Left:
                    case Heading.Right:
                    {
                        ChangeDirection(direction);
                        break;
                    }
                    case Heading.North:
                    case Heading.South:
                    case Heading.East:
                    case Heading.West:
                    case Heading.Forward:
                    {
                        MoveInDirection(direction);
                        break;
                    }
                };
            }
        }

        public void TraverseWaypoints()
        {
            foreach (var direction in _ship.Directions)
            {
                switch (direction.Heading)
                {
                    case Heading.Left:
                    case Heading.Right:
                    {
                        ChangeWaypoint(direction);
                        break;
                    }
                    case Heading.North:
                    case Heading.South:
                    case Heading.East:
                    case Heading.West:
                    case Heading.Forward:
                    {
                        MoveWaypoint(direction);
                        break;
                    }
                };
            }
        }

        public void MoveInDirection(Direction direction)
        {
            switch(direction.Heading)
            {
                case Heading.North:
                {
                    _ship.North += direction.Units;
                    break;
                }
                case Heading.East:
                {
                    _ship.East += direction.Units;
                    break;
                }
                case Heading.South:
                {
                    _ship.South += direction.Units;
                    break;
                }
                case Heading.West:
                {
                    _ship.West += direction.Units;
                    break;
                }
                case Heading.Forward:
                {
                    var newDirection = new Direction() { 
                        Heading = _ship.Heading,
                        Units = direction.Units
                    };
                    MoveInDirection(newDirection);
                    break;
                }
            }
        }

        public void MoveWaypoint(Direction direction)
        {
            switch (direction.Heading)
            {
                case Heading.North:
                case Heading.South:
                {
                    SetWaypointY(direction);
                    break;
                }
                case Heading.East:
                case Heading.West:
                {
                    SetWaypointX(direction);
                    break;
                }
                case Heading.Forward:
                {
                    MoveShipToWaypoint(direction.Units);
                    break;
                }
            }
        }

        private void MoveShipToWaypoint(int unit)
        {
            switch(_ship.WaypointX.Heading)
            {
                case Heading.East:
                {
                    var total = unit * _ship.WaypointX.Units;
                    _ship.East += total;
                    break;
                }
                case Heading.West:
                {
                    var total = unit * _ship.WaypointX.Units;
                    _ship.West += total;
                    break;
                }
            }
            switch (_ship.WaypointY.Heading)
            {
                case Heading.North:
                {
                    var total = unit * _ship.WaypointY.Units;
                    _ship.North += total;
                    break;
                }
                case Heading.South:
                {
                    var total = unit * _ship.WaypointY.Units;
                    _ship.South += total;
                    break;
                }
            }
        }

        public void SetWaypointX(Direction direction)
        {
            if (direction.Heading == _ship.WaypointX.Heading)
            {
                _ship.WaypointX.Units += direction.Units;
            }
            else if (_ship.WaypointX.Units >= direction.Units)
            {
                _ship.WaypointX.Units += -direction.Units;
            }
            else
            {
                _ship.WaypointX.Heading = SetNewHeading(GetCurrentHeadingAsInt(_ship.WaypointX.Heading) + 180);
                _ship.WaypointX.Units = direction.Units - _ship.WaypointX.Units;
            }
        }

        public void SetWaypointY(Direction direction)
        {
            if (direction.Heading == _ship.WaypointY.Heading)
            {
                _ship.WaypointY.Units += direction.Units;
            }
            else if (_ship.WaypointY.Units >= direction.Units)
            {
                _ship.WaypointY.Units += -direction.Units;
            }
            else
            {
                _ship.WaypointY.Heading = SetNewHeading(GetCurrentHeadingAsInt(_ship.WaypointY.Heading) + 180);
                _ship.WaypointY.Units = direction.Units - _ship.WaypointY.Units;
            }
        }

        public void ChangeDirection(Direction direction)
        {
            var currentHeading = GetCurrentHeadingAsInt(_ship.Heading);
            switch (direction.Heading) 
            {
                case Heading.Left:
                {
                    var newHeading = SetNewHeading(currentHeading - direction.Units);
                    _ship.Heading = newHeading == Heading.NoChange ? _ship.Heading : newHeading;
                    break;
                }
                case Heading.Right:
                {
                    var newHeading = SetNewHeading(currentHeading + direction.Units);
                    _ship.Heading = newHeading == Heading.NoChange ? _ship.Heading : newHeading;
                    break;
                }
            }
        }

        public void ChangeWaypoint(Direction direction)
        {
            switch (direction.Heading)
            {
                case Heading.Left:
                {
                    // Does Not Have To Change Both Directions
                    var newXHeading = SetNewHeading(GetCurrentHeadingAsInt(_ship.WaypointX.Heading) - direction.Units);
                    var newYHeading = SetNewHeading(GetCurrentHeadingAsInt(_ship.WaypointY.Heading) - direction.Units);
                    if (newXHeading == Heading.East || newXHeading == Heading.West || newXHeading == Heading.NoChange)
                    {
                        _ship.WaypointX.Heading = newXHeading == Heading.NoChange ? _ship.WaypointX.Heading : newXHeading;
                        _ship.WaypointY.Heading = newYHeading == Heading.NoChange ? _ship.WaypointY.Heading : newYHeading;
                    } else
                    {
                        var newXUnit = _ship.WaypointY.Units;
                        var newYUnit = _ship.WaypointX.Units;
                        _ship.WaypointX.Heading = newYHeading;
                        _ship.WaypointX.Units = newXUnit;
                        _ship.WaypointY.Heading = newXHeading;
                        _ship.WaypointY.Units = newYUnit;
                    }
                    break;
                }
                case Heading.Right:
                {
                    var newXHeading = SetNewHeading(GetCurrentHeadingAsInt(_ship.WaypointX.Heading) + direction.Units);
                    var newYHeading = SetNewHeading(GetCurrentHeadingAsInt(_ship.WaypointY.Heading) + direction.Units);
                    if (newXHeading == Heading.East || newXHeading == Heading.West || newXHeading == Heading.NoChange)
                    {
                        _ship.WaypointX.Heading = newXHeading == Heading.NoChange ? _ship.WaypointX.Heading : newXHeading;
                        _ship.WaypointY.Heading = newYHeading == Heading.NoChange ? _ship.WaypointY.Heading : newYHeading;
                    }
                    else
                    {
                        var newXUnit = _ship.WaypointY.Units;
                        var newYUnit = _ship.WaypointX.Units;
                        _ship.WaypointX.Heading = newYHeading;
                        _ship.WaypointX.Units = newXUnit;
                        _ship.WaypointY.Heading = newXHeading;
                        _ship.WaypointY.Units = newYUnit;
                    }
                    break;
                }
            }
        }

        private int GetCurrentHeadingAsInt(Heading heading)
        {
            return heading switch
            {
                Heading.North => 0,
                Heading.East => 90,
                Heading.South => 180,
                Heading.West => 270
            };
        }

        private Heading SetNewHeading(int units)
        {
            return units switch
            {
                0 => Heading.North,
                360 => Heading.North,
                -270 => Heading.East,
                90 => Heading.East,
                450 => Heading.East,
                -180 => Heading.South,
                180 => Heading.South,
                540 => Heading.South,
                -90 => Heading.West,
                270 => Heading.West,
                _ => Heading.NoChange
            };
        }

        private void ResetShip()
        {
            _ship.Heading = Heading.East;
            _ship.WaypointX = new Waypoint()
            {
                Heading = Heading.East,
                Units = 10
            };
            _ship.WaypointY = new Waypoint()
            {
                Heading = Heading.North,
                Units = 1
            };
            _ship.North = 0;
            _ship.South = 0;
            _ship.East = 0;
            _ship.West = 0;
        }

        private void InitShipFromInput()
        {
            var rawInput = _fileReader.ReadFileToStringArray("Day12/data.json");
            MapShipFromInput(rawInput);
        }

        private void MapShipFromInput(List<string> rawInput)
        {
            _ship = new Ship()
            {
                Heading = Heading.East,
                WaypointX = new Waypoint()
                {
                    Heading = Heading.East,
                    Units = 10
                },
                WaypointY = new Waypoint()
                {
                    Heading = Heading.North,
                    Units = 1
                },
                North = 0,
                South = 0,
                East = 0,
                West = 0,
                Directions = new List<Direction>()
            };

            foreach(var direction in rawInput)
            {
                _ship.Directions.Add(new Direction()
                {
                    Heading = GetHeading(direction[0]),
                    Units = Int32.Parse(direction.Substring(1))
                });
            }
        }

        private Heading GetHeading(char heading)
        {
            return heading switch
            {
                'N' => Heading.North,
                'S' => Heading.South,
                'E' => Heading.East,
                'W' => Heading.West,
                'R' => Heading.Right,
                'L' => Heading.Left,
                'F' => Heading.Forward,
            };
        }
    }
}
