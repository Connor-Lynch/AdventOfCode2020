using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day3.Entities
{
    public class Directions
    {
        public int StartingRow { get; set; }
        public int StartingCol { get; set; }
        public int FinalRow { get; set; }
        public int CurrentRow { get; set; }
        public int CurrentCol { get; set; }
        public int RowsTraveled { get; set; }
        public int TreesEncountered { get; set; }
    }
}
