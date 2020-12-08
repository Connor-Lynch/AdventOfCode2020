using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day8.Entities
{
    public class HandheldProgram
    {
        public int Accumulator { get; set; }
        public int CurrentIndex { get; set; }
        public List<Instruction> Instructions { get; set; }
    }
}
