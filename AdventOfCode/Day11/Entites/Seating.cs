using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day11.Entites
{
    public class Seating
    {
        public List<string> Layout { get; set; }
        public int NumberOfSeats { get; set; }
        public int Taken { get; set; }
        public int Open { get; set; }
    }
}
