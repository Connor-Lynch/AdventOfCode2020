using AdventOfCode.Day9;
using AdventOfCode.Day9.Entities;
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
    public class Day9Tests
    {
        private Mock<IFileReader> _fileReaderMock;
        private Day9Solution _solution;
        private List<string> _mockData;


        [TestInitialize]
        public void InIt()
        {
            _mockData = new List<string>() {
                "35","20","15","25","47","40","62","55","65","95","102","117","150","182","127","219","299","277","309","576"
            };
            var autoMocker = new AutoMoqer();

            _fileReaderMock = autoMocker.GetMock<IFileReader>();
            _fileReaderMock.Setup(f => f.ReadFileToStringArray(It.IsAny<string>())).Returns(_mockData);

            _solution = new Day9Solution(_fileReaderMock.Object);
        }

        [TestMethod]
        public void ShouldMapCypherFromInput()
        {
            var cypher = _solution._cypher;

            Assert.IsInstanceOfType(cypher, typeof(List<CypherValue>));
            Assert.AreEqual(cypher.Count, 20);
        }

        [TestMethod]
        public void ShouldFindFirstInvalidCypherValue()
        {
            var value = _solution.GetFirstInvalidCypherValue(5);

            Assert.AreEqual(value, 127);
        }

        [TestMethod]
        public void ShouldFindTheEncryptionWeakness()
        {
            var weakness = _solution.GetCypherEncryptionWeakness(127);

            Assert.AreEqual(weakness, 62);
        }
    }
}
