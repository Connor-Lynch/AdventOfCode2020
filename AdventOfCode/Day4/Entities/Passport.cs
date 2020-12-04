using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day4.Entities
{
    public class Passport
    {
        public string BirthYear { get; set; }
        public string IssueYear { get; set; }
        public string ExpirationYear { get; set; }
        public string Height { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string PassportID { get; set; }
        public string CountryID { get; set; }
        public bool Valid { get; set; }
    }
}
