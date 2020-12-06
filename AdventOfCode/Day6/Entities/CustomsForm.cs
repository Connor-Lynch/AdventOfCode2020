using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day6.Entities
{
    public class CustomsForm
    {
        public string RawAnswers { get; set; }
        public List<string> IndividualsAnswers { get; set; }
        public int TotalAffirmativeAnswers { get; set; }
        public int TotalSharedAffirmativeAnswers { get; set; }
    }
}
