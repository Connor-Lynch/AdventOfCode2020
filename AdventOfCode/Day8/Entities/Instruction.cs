using AdventOfCode.Day8.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day8.Entities
{
    public class Instruction
    {
        public Opperation Opperation { get; set; }
        public int Offset { get; set; }
        public int TimesCalled { get; set; }
    }
}
