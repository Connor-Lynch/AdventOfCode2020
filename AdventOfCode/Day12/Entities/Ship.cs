using AdventOfCode.Day12.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day12.Entities
{
    public class Ship
    {
        public Heading Heading { get; set; }
        public Waypoint WaypointX { get; set; }
        public Waypoint WaypointY { get; set; }
        public List<Direction> Directions { get; set; }
        public int North { get; set; }
        public int South { get; set; }
        public int East { get; set; }
        public int West { get; set; }
    }
}
