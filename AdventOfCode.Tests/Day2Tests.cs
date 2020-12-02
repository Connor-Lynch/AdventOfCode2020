using AdventOfCode.Day2;
using AdventOfCode.Day2.Entities;
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
    public class Day2Tests
    {
        private Mock<IFileReader> _fileReaderMock;
        private Day2Solution _solution;
        private List<string> _mockData;

        [TestInitialize]
        public void InIt()
        {
            _mockData = new List<string>() {"1-3 a: abcde", "1-3 b: cdefg", "2-9 c: ccccccccc"};
            var autoMocker = new AutoMoqer();

            _fileReaderMock = autoMocker.GetMock<IFileReader>();
            _fileReaderMock.Setup(f => f.ReadFileToStringArray(It.IsAny<string>())).Returns(_mockData);

            _solution = new Day2Solution(_fileReaderMock.Object);
        }

        [TestMethod]
        public void PasswordsShouldBeMappedFromInputFile()
        {
            var expectedPasswords = new List<Password>()
            {
                new Password()
                {
                    MinTimes = 1,
                    MaxTimes = 3,
                    RequiredLetter = 'a',
                    Text = "abcde"
                },
                new Password()
                {
                    MinTimes = 1,
                    MaxTimes = 3,
                    RequiredLetter = 'b',
                    Text = "cdefg"
                },
                new Password()
                {
                    MinTimes = 2,
                    MaxTimes = 9,
                    RequiredLetter = 'c',
                    Text = "ccccccccc"
                },
            };
            var passwords =_solution._passwords;

            Assert.IsInstanceOfType(passwords, typeof(List<Password>));
            Assert.AreEqual(passwords.Count, expectedPasswords.Count);

            Assert.AreEqual(passwords[0].MinTimes, expectedPasswords[0].MinTimes);
            Assert.AreEqual(passwords[0].MaxTimes, expectedPasswords[0].MaxTimes);
            Assert.AreEqual(passwords[0].RequiredLetter, expectedPasswords[0].RequiredLetter);
            Assert.AreEqual(passwords[0].Text, expectedPasswords[0].Text);
        }

        [TestMethod]
        public void GetValidPasswordsShouldReturnAllValidPasswordsForPart1()
        {
            var validPasswords = _solution.GetValidPasswords(1);

            Assert.AreEqual(validPasswords.Count, 2);
            Assert.AreEqual(validPasswords[0].Text, "abcde");
            Assert.AreEqual(validPasswords[1].Text, "ccccccccc");
        }

        [TestMethod]
        public void GetValidPasswordsShouldReturnAllValidPasswordsForPart2()
        {
            var validPasswords = _solution.GetValidPasswords(2);

            Assert.AreEqual(validPasswords.Count, 1);
            Assert.AreEqual(validPasswords[0].Text, "abcde");
        }
    }
}
