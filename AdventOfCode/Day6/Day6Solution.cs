using AdventOfCode.Day6.Entities;
using AdventOfCode.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day6
{
    public class Day6Solution : SolutionBase, ISolution
    {
        private IFileReader _fileReader;
        public List<CustomsForm> _customsForms;

        public Day6Solution(IFileReader fileReader)
        {
            _fileReader = fileReader;
            InitCustomsForms();
            ResetSolution();
        }

        public void Solve()
        {
            StartTime();
            var part1Answer = SumAffirmativeAnswers();
            SetAnswer(part1Answer);

            StartTime();
            var part2Answer = SumSharedAffirmativeAnswers();
            SetAnswer(part2Answer);
        }

        public int SumAffirmativeAnswers()
        {
            TallyAffirmativeAnswersOnForms();
            return _customsForms.Sum(f => f.TotalAffirmativeAnswers);
        }

        public int SumSharedAffirmativeAnswers()
        {
            TallySharedAffirmativeAnswersOnForms();
            return _customsForms.Sum(f => f.TotalSharedAffirmativeAnswers);
        }

        public void TallyAffirmativeAnswersOnForms()
        {
            foreach(var form in _customsForms)
            {
                form.TotalAffirmativeAnswers = form.RawAnswers.Distinct().Count();
            }
        }

        public void TallySharedAffirmativeAnswersOnForms()
        {
            foreach (var form in _customsForms)
            {
                foreach(var character in form.RawAnswers.Distinct())
                {
                    var count = form.RawAnswers.Count(c => c == character);
                    if(count == form.IndividualsAnswers.Count())
                    {
                        form.TotalSharedAffirmativeAnswers++;
                    }
                }
            }
        }

        private void InitCustomsForms()
        {
            var rawInput = _fileReader.ReadFileToStringArray("Day6/data.json");
            MapDataIntoCustomsForms(rawInput);
        }

        private void MapDataIntoCustomsForms(List<string> rawInput)
        {
            _customsForms = new List<CustomsForm>();

            foreach (var form in rawInput)
            {
                var individualAnswers = form.Split(',');
                _customsForms.Add(
                    new CustomsForm()
                    {
                        RawAnswers = form.Replace(",", ""),
                        IndividualsAnswers = new List<string>(individualAnswers)
                    }
                ); ;
            }
        }
    }
}
