using AdventOfCode.Day4.Interfaces;
using System;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day4
{
    public class PassportValidator : IPassportValidator
    {
        public bool ValidateYear(int minYear, int maxYear, string value)
        {
            if (TryGetInt(value, out int year))
            {
                if (value.Length == 4 && year >= minYear && year <= maxYear)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ValidateHeight(string value)
        {
            var unit = $"{value[value.Length - 2]}{value[value.Length - 1] }";
            switch (unit)
            {
                case "in":
                    {
                        var rawHeight = value.Replace("in", "");
                        return ValidateHeightRange(59, 76, rawHeight);
                    }
                case "cm":
                    {
                        var rawHeight = value.Replace("cm", "");
                        return ValidateHeightRange(150, 193, rawHeight);
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        public bool ValidateHairColor(string value)
        {
            if (value[0] == '#' && value.Length == 7)
            {
                var pattern = Regex.Match(value, @"^#([a-fA-F0-9]{6})$", RegexOptions.IgnoreCase);
                return pattern.Success;
            }
            return false;
        }

        public bool ValidateEyeColor(string value)
        {
            switch (value)
            {
                case "amb":
                case "blu":
                case "brn":
                case "gry":
                case "grn":
                case "hzl":
                case "oth":
                    return true;
                default:
                    return false;
            }
        }

        public bool ValidatePassportId(string value)
        {
            if (value.Length == 9 && TryGetInt(value, out int passportId))
            {
                return true;
            }
            return false;
        }

        private bool ValidateHeightRange(int min, int max, string value)
        {
            if (TryGetInt(value, out int height))
            {
                if (height >= min && height <= max)
                {
                    return true;
                }
            }
            return false;
        }

        private bool TryGetInt(string value, out int intValue)
        {
            intValue = 0;
            try
            {
                intValue = Int32.Parse(value);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
