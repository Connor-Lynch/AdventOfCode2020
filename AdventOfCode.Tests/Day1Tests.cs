using AdventOfCode.Day1;
using AdventOfCode.Interfaces;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace AdventOfCode.Tests
{
    [TestClass]
    public class Day1Tests
    {
        private Mock<IFileReader> _fileReaderMock;
        private Day1Solution _solution;
        private List<int> _mockData;

        [TestInitialize]
        public void InIt()
        {
            _mockData = new List<int>() {1721,979,366,299,675,1456};
            var autoMocker = new AutoMoqer();

            _fileReaderMock = autoMocker.GetMock<IFileReader>();
            _fileReaderMock.Setup(f => f.ReadFileToIntArray(It.IsAny<string>())).Returns(_mockData);

            _solution = new Day1Solution(_fileReaderMock.Object);
        }

        [TestMethod]
        public void ShouldFindTheCorrectProductForTwoExpenses()
        {
            var product = _solution.GetProductOfExpenses(2);

            Assert.AreEqual(product, 514579);
        }

        [TestMethod]
        public void ShouldFindTheCorrectProductForThreeExpenses()
        {
            var product = _solution.GetProductOfExpenses(3);

            Assert.AreEqual(product, 241861950);
        }
    }
}
