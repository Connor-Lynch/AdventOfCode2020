using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day4.Interfaces
{
    public interface IPassportValidator
    {
        public bool ValidateYear(int minYear, int maxYear, string value);
        public bool ValidateHeight(string value);
        public bool ValidateHairColor(string value);
        public bool ValidateEyeColor(string value);
        public bool ValidatePassportId(string value);
    }
}
