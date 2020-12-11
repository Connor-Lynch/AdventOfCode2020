using AdventOfCode.Day11;
using AdventOfCode.Day11.Entites;
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
    public class Day11Tests
    {
        private Mock<IFileReader> _fileReaderMock;
        private Day11Solution _solution;
        private List<string> _mockData;

        [TestInitialize]
        public void InIt()
        {
            _mockData = new List<string>() {
                "L.LL.LL.LL",
                "LLLLLLL.LL",
                "L.L.L..L..",
                "LLLL.LL.LL",
                "L.LL.LL.LL",
                "L.LLLLL.LL",
                "..L.L.....",
                "LLLLLLLLLL",
                "L.LLLLLL.L",
                "L.LLLLL.LL"
                //"#.##.##.##",
                //"#######.##",
                //"#.#.#..#..",
                //"####.##.##",
                //"#.##.##.##",
                //"#.#####.##",
                //"..#.#.....",
                //"##########",
                //"#.######.#",
                //"#.#####.##"
            };
            var autoMocker = new AutoMoqer();

            _fileReaderMock = autoMocker.GetMock<IFileReader>();
            _fileReaderMock.Setup(f => f.ReadFileToStringArray(It.IsAny<string>())).Returns(_mockData);

            _solution = new Day11Solution(_fileReaderMock.Object);
        }

        [TestMethod]
        public void ShouldMapSeatingFromInput()
        {
            var seating = _solution._seating;

            Assert.IsInstanceOfType(seating, typeof(Seating));
            Assert.AreEqual(seating.Layout.Count, 10);
            Assert.AreEqual(seating.NumberOfSeats, 71);
            Assert.AreEqual(seating.Open, 71);
            Assert.AreEqual(seating.Taken, 0);
        }

        [TestMethod]
        public void ShouldCountSurroundingSeatsCorrectly()
        {
            _solution._seating.Layout = new List<string>()
            {
                "###",
                "#L#",
                "###"
            };

            var allOccupied = _solution.CheckSeatSurroundings(1, 1, false);

            _solution._seating.Layout = new List<string>()
            {
                "LLL",
                "LLL",
                "LLL"
            };
            var allOpen = _solution.CheckSeatSurroundings(1, 1, false);

            _solution._seating.Layout = new List<string>()
            {
                "#L#",
                "LLL",
                "#L#"
            };
            var allCorners = _solution.CheckSeatSurroundings(1, 1, false);

            _solution._seating.Layout = new List<string>()
            {
                "L#L",
                "#L#",
                "L#L"
            };
            var allAdj = _solution.CheckSeatSurroundings(1, 1, false);

            Assert.AreEqual(allOccupied, 8);
            Assert.AreEqual(allOpen, 0);
            Assert.AreEqual(allCorners, 4);
            Assert.AreEqual(allAdj, 4);
        }

        [TestMethod]
        public void ShouldCountSurroundingSeatsCorrectlyInSight()
        {
            _solution._seating.Layout = new List<string>()
            {
                "#.#.#",
                "L...L",
                "#.L.#",
                "L...L",
                "#L#L#",
            };

            var allOccupied = _solution.CheckSeatSurroundings(2, 2, true);

            _solution._seating.Layout = new List<string>()
            {
                "LLLLL",
                "LLLLL",
                "LLLLL",
                "LLLLL",
                "LLLLL",
            };
            var allOpen = _solution.CheckSeatSurroundings(2, 2, true);

            _solution._seating.Layout = new List<string>()
            {
                "#LLL#",
                "L.L.L",
                "LLLLL",
                "L.L.L",
                "#LLL#",
            };
            var allCorners = _solution.CheckSeatSurroundings(2, 2, true);

            _solution._seating.Layout = new List<string>()
            {
                "LL#LL",
                "LL.LL",
                "#.L.#",
                "LL.LL",
                "LL#LL",
            };
            var allAdj = _solution.CheckSeatSurroundings(2, 2, true);

            Assert.AreEqual(allOccupied, 8);
            Assert.AreEqual(allOpen, 0);
            Assert.AreEqual(allCorners, 4);
            Assert.AreEqual(allAdj, 4);
        }

        [TestMethod]
        public void ShouldSetAllSeatsToOccupiedOnFirstItteration()
        {
            _solution.ComputSeatingCycle(4, false);
            var firstOccupiedCount = _solution._seating.Taken;
            _solution.ComputSeatingCycle(4, false);
            var seccondOccupiedCount = _solution._seating.Taken;

            Assert.AreEqual(firstOccupiedCount, 71);
            Assert.AreEqual(seccondOccupiedCount, 20);
        }

        [TestMethod]
        public void ShoulrStableizeSeating()
        {
            var seatingCount = _solution.StabilizeSeating(4, false);

            Assert.AreEqual(seatingCount, 37);
        }

        [TestMethod]
        public void ShouldSetAllSeatsToOccupiedOnFirstItterationInSight()
        {
            _solution.ComputSeatingCycle(5, true);
            var firstOccupiedCount = _solution._seating.Taken;
            _solution.ComputSeatingCycle(5, true);
            var seccondOccupiedCount = _solution._seating.Taken;

            Assert.AreEqual(firstOccupiedCount, 71);
            Assert.AreEqual(seccondOccupiedCount, 7);
        }

        [TestMethod]
        public void ShoulrStableizeSeatingInSight()
        {
            var seatingCount = _solution.StabilizeSeating(5, true);

            Assert.AreEqual(seatingCount, 26);
        }
    }
}
