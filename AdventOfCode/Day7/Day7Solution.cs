using AdventOfCode.Day7.Entities;
using AdventOfCode.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day7
{
    public class Day7Solution : SolutionBase, ISolution
    {
        private IFileReader _fileReader;
        public List<LuggageRule> _luggageRules;

        public Day7Solution(IFileReader fileReader)
        {
            _fileReader = fileReader;
            InitLuggageRules();
            ResetSolution();
        }

        public void Solve()
        {
            StartTime();
            var part1Answer = GetRulesThatContainBag("shiny gold");
            SetAnswer(part1Answer);

            StartTime();
            var part2Answer = GetNumberOfBagsInsideBag("shiny gold") - 1;
            SetAnswer(part2Answer);
        }

        public int GetRulesThatContainBag(string bagColor)
        {
            return _luggageRules.Where(r => SearchForContainingBags(bagColor, r) == true).Count();
        }

        private bool SearchForContainingBags(string bagColor, LuggageRule currentRule)
        {
            foreach(var bag in currentRule.InnerBags)
            {
                if (bag.Color == bagColor)
                {
                    return true;
                }
                return SearchForContainingBags(bagColor, _luggageRules.Where(r => r.OuterColor == bag.Color).First());
            }

            return false;
        }

        public int GetNumberOfBagsInsideBag(string bagColor)
        {
            int numberOfBags = 1;
            var outerBags = _luggageRules.Where(r => r.OuterColor == bagColor).First();
            foreach(var innerBag in outerBags.InnerBags)
            {
                numberOfBags = numberOfBags + (innerBag.Count * GetNumberOfBagsInsideBag(innerBag.Color));
            }

            return numberOfBags;
        }

        private void InitLuggageRules()
        {
            var rawInput = _fileReader.ReadFileToStringArray("Day7/data.json");
            MapInputToLuggageRules(rawInput);
        }

        private void MapInputToLuggageRules(List<string> rawInput)
        {
            _luggageRules = new List<LuggageRule>();

            foreach(var rawRule in rawInput)
            {
                var splitRule = rawRule.Split("contain");

                var newRule = new LuggageRule()
                {
                    OuterColor = splitRule[0].Replace("bags", "").Trim(),
                    InnerBags = CreateInnerBagsFromRawRule(splitRule[1])
                };
                _luggageRules.Add(newRule);
            }
        }

        private List<InnerBag> CreateInnerBagsFromRawRule(string rawRule)
        {
            var newBags = new List<InnerBag>();            

            if (!rawRule.Contains(" no "))
            {
                var innerBags = rawRule.Replace("bags", "").Replace("bag", "").Split(',');
                foreach (var bag in innerBags)
                {
                    var count = bag.Trim().Split()[0];
                    var color = bag.Replace(count, "").Replace(".", "").Trim();
                    newBags.Add(
                        new InnerBag()
                        {
                            Color = color,
                            Count = Int32.Parse(count)
                        });

                }
            }

            return newBags;
        }
    }
}
