using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day5.Entities
{
    public class BoardingPass
    {
        public string BianaryValue { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int SeatID { get; set; }
    }
}
