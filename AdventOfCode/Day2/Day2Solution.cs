using AdventOfCode.Day2.Entities;
using AdventOfCode.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day2
{
    public class Day2Solution : SolutionBase, ISolution
    {
        private IFileReader _fileReader;
        public List<Password> _passwords;

        public Day2Solution(IFileReader fileReader)
        {
            _fileReader = fileReader;
            InitPasswords();
            ResetSolution();
        }

        public void Solve()
        {
            StartTime();
            var part1Answer = GetValidPasswords(1).Count;
            StopTime();
            SetAnswer(part1Answer);

            ResetPasswordValidity();

            StartTime();
            var part2Answer = GetValidPasswords(2).Count;
            StopTime();
            SetAnswer(part2Answer);
        }

        public List<Password> GetValidPasswords(int part)
        {
            switch(part)
            {
                case 1:
                    ValidatePasswordsForPart1();
                    break;
                case 2:
                    ValidatePasswordsForPart2();
                    break;
            }

            return _passwords.Where(p => p.Valid == true).ToList();
        }

        private void ValidatePasswordsForPart1()
        {
            foreach(var password in _passwords)
            {
                var requiredCharCount = password.Text.Count(p => p == password.RequiredLetter);
                if(password.MaxTimes >= requiredCharCount && requiredCharCount >= password.MinTimes)
                {
                    password.Valid = true;
                }
            }
        }

        private void ValidatePasswordsForPart2()
        {
            foreach (var password in _passwords)
            {
                var charInSlot1 = password.Text[password.MinTimes - 1] == password.RequiredLetter;
                var charInSlot2 = password.Text[password.MaxTimes - 1] == password.RequiredLetter;

                if ((charInSlot1 && !charInSlot2) || (!charInSlot1 && charInSlot2))
                {
                    password.Valid = true;
                }
            }
        }

        private void ResetPasswordValidity()
        {
            foreach(var password in _passwords)
            {
                password.Valid = false;
            }
        }

        private void InitPasswords()
        {
            var rawPasswordInput = _fileReader.ReadFileToStringArray("Day2/data.json");
            MapPasswordsFromInput(rawPasswordInput);
        }

        private void MapPasswordsFromInput(List<string> rawPasswords)
        {
            _passwords = new List<Password>();

            foreach (var rawPassword in rawPasswords)
            {
                var passwordPieces = rawPassword.Replace(":", "").Split(' ');

                var passwordRange = passwordPieces[0].Split('-');

                var password = new Password()
                {
                    MinTimes = Int32.Parse(passwordRange[0]),
                    MaxTimes = Int32.Parse(passwordRange[1]),
                    RequiredLetter = passwordPieces[1][0],
                    Text = passwordPieces[2]
                };

                _passwords.Add(password);
            }
        }
    }
}
