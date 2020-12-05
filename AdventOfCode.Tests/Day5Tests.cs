using AdventOfCode.Day5;
using AdventOfCode.Day5.Entities;
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
    public class Day5Tests
    {
        private Mock<IFileReader> _fileReaderMock;
        private Day5Solution _solution;
        private List<string> _mockData;

        [TestInitialize]
        public void InIt()
        {
            _mockData = new List<string>() {
                "BFFFBBFRRR",
                "FFFBBBFRRR",
                "BBFFBBFRLL"
            };
            var autoMocker = new AutoMoqer();

            _fileReaderMock = autoMocker.GetMock<IFileReader>();
            _fileReaderMock.Setup(f => f.ReadFileToStringArray(It.IsAny<string>())).Returns(_mockData);

            _solution = new Day5Solution(_fileReaderMock.Object);
        }

        [TestMethod]
        public void ShouldMapBoardingPassesFromInput()
        {
            var boardingPasses = _solution._boardingPasses;

            Assert.IsInstanceOfType(boardingPasses, typeof(List<BoardingPass>));
            Assert.AreEqual(boardingPasses.Count, 3);
            Assert.AreEqual(boardingPasses[0].BianaryValue, "BFFFBBFRRR");
            Assert.AreEqual(boardingPasses[1].BianaryValue, "FFFBBBFRRR");
            Assert.AreEqual(boardingPasses[2].BianaryValue, "BBFFBBFRLL");
        }

        [TestMethod]
        public void ShouldCalculateBoardingPassRow()
        {
            _solution.PopulateBoardingPassData();
            var boardinPasses = _solution._boardingPasses;

            Assert.AreEqual(boardinPasses[0].Row, 70);
            Assert.AreEqual(boardinPasses[1].Row, 14);
            Assert.AreEqual(boardinPasses[2].Row, 102);
        }

        [TestMethod]
        public void ShouldCalculateBoardingPassColumn()
        {
            _solution.PopulateBoardingPassData();
            var boardinPasses = _solution._boardingPasses;

            Assert.AreEqual(boardinPasses[0].Column, 7);
            Assert.AreEqual(boardinPasses[1].Column, 7);
            Assert.AreEqual(boardinPasses[2].Column, 4);
        }

        [TestMethod]
        public void ShouldCalculateBoardingPassSeatID()
        {
            _solution.PopulateBoardingPassData();
            var boardinPasses = _solution._boardingPasses;

            Assert.AreEqual(boardinPasses[0].SeatID, 567);
            Assert.AreEqual(boardinPasses[1].SeatID, 119);
            Assert.AreEqual(boardinPasses[2].SeatID, 820);
        }

        [TestMethod]
        public void ShouldSortBoardingPasses()
        {
            _solution.SortBoardingPasses();
            var boardingPasses = _solution._boardingPasses;

            Assert.IsTrue(boardingPasses[0].SeatID < boardingPasses[2].SeatID);
            Assert.AreEqual(boardingPasses[2].SeatID, 820);
        }

        [TestMethod]
        public void ShouldFindValidOpenSeatId()
        {
            var mockBoardingPassData = new List<BoardingPass>() {
                new BoardingPass()
                {
                    SeatID = 1
                },
                new BoardingPass()
                {
                    SeatID = 2
                },
                new BoardingPass()
                {
                    SeatID = 5
                },
                new BoardingPass()
                {
                    SeatID = 7
                },
                new BoardingPass()
                {
                    SeatID = 8
                }
            };
            _solution._boardingPasses = mockBoardingPassData;

            var openSeatId = _solution.GetOpenSeat();

            Assert.AreEqual(openSeatId, 6);
        }
    }
}
