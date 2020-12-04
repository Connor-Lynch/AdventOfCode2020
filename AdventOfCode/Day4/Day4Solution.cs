using AdventOfCode.Day4.Entities;
using AdventOfCode.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day4
{
    public class Day4Solution : SolutionBase, ISolution
    {
        private IFileReader _fileReader;
        public List<Passport> _passports;

        public Day4Solution(IFileReader fileReader)
        {
            _fileReader = fileReader;
            InitPassports();
            ResetSolution();
        }

        public void Solve()
        {
            StartTime();
            var validPassports = GetPasspoortsWithRequiredFields();
            StopTime();
            SetAnswer(validPassports.Count);

            StartTime();
            var part2Answer = GetPassportsWithValidData(validPassports).Count;
            StopTime();
            SetAnswer(part2Answer);
        }

        public List<Passport> GetPasspoortsWithRequiredFields()
        {
            return _passports.Where(p => !string.IsNullOrWhiteSpace(p.BirthYear) &&
                !string.IsNullOrWhiteSpace(p.IssueYear) &&
                !string.IsNullOrWhiteSpace(p.ExpirationYear) &&
                !string.IsNullOrWhiteSpace(p.Height) &&
                !string.IsNullOrWhiteSpace(p.HairColor) &&
                !string.IsNullOrWhiteSpace(p.EyeColor) &&
                !string.IsNullOrWhiteSpace(p.PassportID)
            ).ToList();
        }

        public List<Passport> GetPassportsWithValidData(List<Passport> passports)
        {
            return passports.Where(p => ValidateYear(1920, 2002, p.BirthYear) &&
                ValidateYear(2010, 2020, p.IssueYear) &&
                ValidateYear(2020, 2030, p.ExpirationYear) &&
                ValidateHeight(p.Height) &&
                ValidateHairColor(p.HairColor) &&
                ValidateEyeColor(p.EyeColor) &&
                ValidatePassportId(p.PassportID)
            ).ToList();
        }

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
            switch(value)
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
            if(value.Length == 9 && TryGetInt(value, out int passportId))
            {
                return true;
            }
            return false;
        }

        private bool ValidateHeightRange(int min, int max, string value)
        {
            if (TryGetInt(value, out int height))
            {
                if(height >= min && height <= max)
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

        private void InitPassports()
        {
            var rawPassports = _fileReader.ReadFileToStringArray("Day4/data.json");
            MapDataFromRawInput(rawPassports);
        }

        private void MapDataFromRawInput(List<string> rawInput)
        {
            _passports = new List<Passport>();
            foreach(var entry in rawInput)
            {
                _passports.Add(GetPassportFromRawInput(entry));
            }
        }

        private Passport GetPassportFromRawInput(string rawInput)
        {
            var pairs = rawInput.Split().Select(p => p.Split(':')).ToDictionary(p => p[0], p => p[1]);

            return new Passport()
            {
                BirthYear = pairs.ContainsKey("byr")? pairs["byr"] : null,
                IssueYear = pairs.ContainsKey("iyr") ? pairs["iyr"] : null,
                ExpirationYear = pairs.ContainsKey("eyr") ? pairs["eyr"] : null,
                Height = pairs.ContainsKey("hgt") ? pairs["hgt"] : null,
                HairColor = pairs.ContainsKey("hcl") ? pairs["hcl"] : null,
                EyeColor = pairs.ContainsKey("ecl") ? pairs["ecl"] : null,
                PassportID = pairs.ContainsKey("pid") ? pairs["pid"] : null,
                CountryID = pairs.ContainsKey("cid") ? pairs["cid"] : null
            };
        }

    }
}
