using AdventOfCode.Day10;
using AdventOfCode.Day10.Entities;
using AdventOfCode.Interfaces;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Tests
{
    [TestClass]
    public class Day10Tests
    {
        private Mock<IFileReader> _fileReaderMock;
        private Day10Solution _solution;
        private List<int> _mockData;


        [TestInitialize]
        public void InIt()
        {
            _mockData = new List<int>() {
                28,33,18,42,31,14,46,20,48,47,24,23,49,45,19,38,39,11,1,32,25,35,8,17,7,9,4,2,34,10,3
            };
            var autoMocker = new AutoMoqer();

            _fileReaderMock = autoMocker.GetMock<IFileReader>();
            _fileReaderMock.Setup(f => f.ReadFileToIntArray(It.IsAny<string>())).Returns(_mockData);

            _solution = new Day10Solution(_fileReaderMock.Object);
        }

        [TestMethod]
        public void ShouldMapAdaptersFromInput()
        {
            var adapters = _solution._adapters;

            Assert.IsInstanceOfType(adapters, typeof(List<Adapter>));
            Assert.AreEqual(adapters.Count, 33);
            Assert.AreEqual(adapters[1].Joltage, 1);
            Assert.AreEqual(adapters[1].Difference, 0);

            Assert.AreEqual(adapters[31].Joltage, 49);
            Assert.AreEqual(adapters[31].Difference, 0);
        }

        [TestMethod]
        public void ShouldSetAllAdapterDifferences()
        {
            _solution.SetAdapterDifferences();

            Assert.AreEqual(_solution._adapters[1].Difference, 1);
            Assert.AreEqual(_solution._adapters[30].Difference, 1);
        }

        [TestMethod]
        public void ShouldGetProductOfDifferences()
        {
            var product = _solution.GetProductOfDifferences();

            Assert.AreEqual(product, 220);
        }

        [TestMethod]
        public void ShouldGetAllPossibleAdapters()
        {
            var possible = _solution.CountAllAdapterCombinations();

            Assert.AreEqual(possible, 19208);
        }
    }
}
