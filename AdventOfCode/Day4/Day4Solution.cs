using AdventOfCode.Day4.Entities;
using AdventOfCode.Day4.Interfaces;
using AdventOfCode.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day4
{
    public class Day4Solution : SolutionBase, ISolution
    {
        private IFileReader _fileReader;
        public IPassportValidator _passportValidator;
        public List<Passport> _passports;

        public Day4Solution(IFileReader fileReader)
        {
            _fileReader = fileReader;
            _passportValidator = new PassportValidator();
            InitPassports();
            ResetSolution();
        }

        public void Solve()
        {
            StartTime();
            var validPassports = GetPasspoortsWithRequiredFields();
            SetAnswer(validPassports.Count);

            StartTime();
            var part2Answer = GetPassportsWithValidData(validPassports).Count;
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
            return passports.Where(p => _passportValidator.ValidateYear(1920, 2002, p.BirthYear) &&
                _passportValidator.ValidateYear(2010, 2020, p.IssueYear) &&
                _passportValidator.ValidateYear(2020, 2030, p.ExpirationYear) &&
                _passportValidator.ValidateHeight(p.Height) &&
                _passportValidator.ValidateHairColor(p.HairColor) &&
                _passportValidator.ValidateEyeColor(p.EyeColor) &&
                _passportValidator.ValidatePassportId(p.PassportID)
            ).ToList();
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
