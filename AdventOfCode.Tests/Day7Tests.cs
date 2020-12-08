using AdventOfCode.Day7;
using AdventOfCode.Day7.Entities;
using AdventOfCode.Interfaces;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Tests
{
    [TestClass]
    public class Day7Tests
    {
        private Mock<IFileReader> _fileReaderMock;
        private Day7Solution _solution;
        private List<string> _mockData;

        [TestInitialize]
        public void InIt()
        {
            _mockData = new List<string>() {
                "light red bags contain 1 bright white bag, 2 muted yellow bags.",
                "dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
                "bright white bags contain 1 shiny gold bag.",
                "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
                "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
                "dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
                "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
                "faded blue bags contain no other bags.",
                "dotted black bags contain no other bags."
            };
            var autoMocker = new AutoMoqer();

            _fileReaderMock = autoMocker.GetMock<IFileReader>();
            _fileReaderMock.Setup(f => f.ReadFileToStringArray(It.IsAny<string>())).Returns(_mockData);

            _solution = new Day7Solution(_fileReaderMock.Object);
        }

        [TestMethod]
        public void ShouldMapLuggageRulesFromInput()
        {
            var luggageRules = _solution._luggageRules;

            Assert.IsInstanceOfType(luggageRules, typeof(List<LuggageRule>));
            Assert.AreEqual(luggageRules.Count, 9);

            Assert.AreEqual(luggageRules[0].OuterColor, "light red");
            Assert.AreEqual(luggageRules[0].InnerBags.Count, 2);
            Assert.AreEqual(luggageRules[0].InnerBags[0].Color, "bright white");
            Assert.AreEqual(luggageRules[0].InnerBags[0].Count, 1);
            Assert.AreEqual(luggageRules[0].InnerBags[1].Color, "muted yellow");
            Assert.AreEqual(luggageRules[0].InnerBags[1].Count, 2);

            Assert.AreEqual(luggageRules[8].OuterColor, "dotted black");
            Assert.AreEqual(luggageRules[8].InnerBags.Count, 0);
        }

        [TestMethod]
        public void ShouldReturnAllRulesThatContainBagColor()
        {
            var count = _solution.GetRulesThatContainBag("shiny gold");

            Assert.AreEqual(count, 4);
        }

        [TestMethod]
        public void ShouldGetTheTotalNumberOfBagsContainedInBag()
        {
            var mockData = new List<string>() {
                "shiny gold bags contain 2 dark red bags.",
                "dark red bags contain 2 dark orange bags.",
                "dark orange bags contain 2 dark yellow bags.",
                "dark yellow bags contain 2 dark green bags.",
                "dark green bags contain 2 dark blue bags.",
                "dark blue bags contain 2 dark violet bags.",
                "dark violet bags contain no other bags.",
            };
            var autoMocker = new AutoMoqer();

            _fileReaderMock.Setup(f => f.ReadFileToStringArray(It.IsAny<string>())).Returns(mockData);
            _solution = new Day7Solution(_fileReaderMock.Object);

            var number = _solution.GetNumberOfBagsInsideBag("shiny gold") - 1;

            Assert.AreEqual(number, 126);
        }
    }
}
