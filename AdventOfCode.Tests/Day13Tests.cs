using AdventOfCode.Day13;
using AdventOfCode.Day13.Entities;
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
    public class Day13Tests
    {
        private Mock<IFileReader> _fileReaderMock;
        private Day13Solution _solution;
        private List<string> _mockData;

        [TestInitialize]
        public void InIt()
        {
            _mockData = new List<string>() {
                "939",
                "7,13,x,x,59,x,31,19"
            };
            var autoMocker = new AutoMoqer();

            _fileReaderMock = autoMocker.GetMock<IFileReader>();
            _fileReaderMock.Setup(f => f.ReadFileToStringArray(It.IsAny<string>())).Returns(_mockData);

            _solution = new Day13Solution(_fileReaderMock.Object);
        }

        [TestMethod]
        public void ShouldMapBusesFromInput()
        {
            var buses = _solution._buses;

            Assert.IsInstanceOfType(buses, typeof(List<Bus>));
            Assert.AreEqual(_solution._arrivalTime, 939);
            Assert.AreEqual(buses.Count, 5);
            Assert.AreEqual(buses[0].Position, 0);
            Assert.AreEqual(buses[4].Position, 7);
        }

        [TestMethod]
        public void ShouldSetBusesClosestInterval()
        {
            _solution.SetArrivalTimesForBuses();
            var buses = _solution._buses;

            Assert.AreEqual(buses[0].EarliestAvailableTimeStamp, 945);
            Assert.AreEqual(buses[1].EarliestAvailableTimeStamp, 949);
            Assert.AreEqual(buses[2].EarliestAvailableTimeStamp, 944);
            Assert.AreEqual(buses[3].EarliestAvailableTimeStamp, 961);
            Assert.AreEqual(buses[4].EarliestAvailableTimeStamp, 950);
        }

        [TestMethod]
        public void ShouldGetTheEarliestAvailableBus()
        {
            var bus = _solution.GetTheEarliestBusAvailable();

            Assert.AreEqual(bus.Id, 59);
            Assert.AreEqual(bus.EarliestAvailableTimeStamp, 944);
        }

        [TestMethod]
        public void ShouldGetTheProductOfTheBusIdAndWaitTime()
        {
            var product = _solution.GetProductOfEarliestBusAndWithTime();

            Assert.AreEqual(product, 295);
        }

        [TestMethod]
        public void ShouldGetEarliestSubsequentDepartures()
        {
            var answer = _solution.GetEarliestSubsequentDepartures();

            Assert.AreEqual(answer, 1068781);
        }
    }
}
