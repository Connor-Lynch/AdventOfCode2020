using AdventOfCode.Day3;
using AdventOfCode.Day3.Entities;
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
    public class Day3Tests
    {
        private Mock<IFileReader> _fileReaderMock;
        private Day3Solution _solution;
        private List<string> _mockData;

        [TestInitialize]
        public void InIt()
        {
            _mockData = new List<string>() {
                "..##.......",
                "#...#...#..",
                ".#....#..#.",
                "..#.#...#.#",
                ".#...##..#.",
                "..#.##.....",
                ".#.#.#....#",
                ".#........#",
                "#.##...#...",
                "#...##....#",
                ".#..#...#.#"
            };
            var autoMocker = new AutoMoqer();

            _fileReaderMock = autoMocker.GetMock<IFileReader>();
            _fileReaderMock.Setup(f => f.ReadFileToStringArray(It.IsAny<string>())).Returns(_mockData);

            _solution = new Day3Solution(_fileReaderMock.Object);
        }
        
        [TestMethod]
        public void ShouldInitMap()
        {
            var map = _solution._map;

            Assert.IsInstanceOfType(map, typeof(Map));
            Assert.AreEqual(map.Data, _mockData);
            Assert.AreEqual(map.RowDistance, 10);
            Assert.AreEqual(map.Directions.FinalRow, 10);
        }

        [TestMethod]
        public void ShouldTravelTheMapAndCountTreesEncountered()
        {
            _solution._rightSteps = 3;
            _solution._downSteps = 1;
            _solution.TraverseMap();

            var map = _solution._map;

            Assert.AreEqual(map.Directions.TreesEncountered, 7);
        }

        [TestMethod]
        public void ShouldGetTheProductOfAllAdditionalTreesOnRoutes()
        {
            var product = _solution.EvaluatePart2(7);

            Assert.AreEqual(product, 336);
        }
    }
}
