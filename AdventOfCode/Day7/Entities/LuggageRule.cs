using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day7.Entities
{
    public class LuggageRule
    {
        public string OuterColor { get; set; }
        public List<InnerBag> InnerBags { get; set; }
    }
}
