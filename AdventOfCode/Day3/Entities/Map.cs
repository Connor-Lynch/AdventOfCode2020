using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day3.Entities
{
    public class Map
    {
        public List<string> Data { get; set; }
        public int RowDistance { get; set; }
        public Directions Directions { get; set; }
    }
}
