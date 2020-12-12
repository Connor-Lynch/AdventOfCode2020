using AdventOfCode.Day12;
using AdventOfCode.Day12.Entities;
using AdventOfCode.Day12.Enums;
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
    public class Day12Tests
    {
        private Mock<IFileReader> _fileReaderMock;
        private Day12Solution _solution;
        private List<string> _mockData;

        [TestInitialize]
        public void InIt()
        {
            _mockData = new List<string>() {
                "F10",
                "N3",
                "F7",
                "R90",
                "F11",
            };
            var autoMocker = new AutoMoqer();

            _fileReaderMock = autoMocker.GetMock<IFileReader>();
            _fileReaderMock.Setup(f => f.ReadFileToStringArray(It.IsAny<string>())).Returns(_mockData);

            _solution = new Day12Solution(_fileReaderMock.Object);
        }

        [TestMethod]
        public void ShouldMapShipFromInput()
        {
            var ship = _solution._ship;

            Assert.IsInstanceOfType(ship, typeof(Ship));
            Assert.AreEqual(ship.North, 0);
            Assert.AreEqual(ship.South, 0);
            Assert.AreEqual(ship.East, 0);
            Assert.AreEqual(ship.West, 0);
            Assert.AreEqual(ship.Heading, Heading.East);
            Assert.AreEqual(ship.Directions.Count, 5);

            Assert.AreEqual(ship.Directions[0].Heading, Heading.Forward);
            Assert.AreEqual(ship.Directions[0].Units, 10);
            Assert.AreEqual(ship.Directions[1].Heading, Heading.North);
            Assert.AreEqual(ship.Directions[1].Units, 3);

            Assert.AreEqual(ship.WaypointX.Heading, Heading.East);
            Assert.AreEqual(ship.WaypointX.Units, 10);

            Assert.AreEqual(ship.WaypointY.Heading, Heading.North);
            Assert.AreEqual(ship.WaypointY.Units, 1);
        }

        [TestMethod]
        public void ShouldChangeShipHeadingLeftFromNorth()
        {
            ResetShip(Heading.North);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Left, Units = 90});
            var newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.West);

            ResetShip(Heading.North);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Left, Units = 180 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.South);

            ResetShip(Heading.North);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Left, Units = 270 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.East);

            ResetShip(Heading.North);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Left, Units = 0 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.North);

            ResetShip(Heading.North);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Left, Units = 360 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.North);
        }

        [TestMethod]
        public void ShouldChangeShipHeadingLeftFromEast()
        {
            ResetShip(Heading.East);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Left, Units = 90 });
            var newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.North);

            ResetShip(Heading.East);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Left, Units = 180 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.West);

            ResetShip(Heading.East);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Left, Units = 270 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.South);

            ResetShip(Heading.East);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Left, Units = 0 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.East);

            ResetShip(Heading.East);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Left, Units = 360 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.East);
        }

        [TestMethod]
        public void ShouldChangeShipHeadingLeftFromSouth()
        {
            ResetShip(Heading.South);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Left, Units = 90 });
            var newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.East);

            ResetShip(Heading.South);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Left, Units = 180 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.North);

            ResetShip(Heading.South);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Left, Units = 270 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.West);

            ResetShip(Heading.South);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Left, Units = 0 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.South);

            ResetShip(Heading.South);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Left, Units = 360 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.South);
        }

        [TestMethod]
        public void ShouldChangeShipHeadingLeftFromWest()
        {
            ResetShip(Heading.West);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Left, Units = 90 });
            var newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.South);

            ResetShip(Heading.West);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Left, Units = 180 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.East);

            ResetShip(Heading.West);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Left, Units = 270 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.North);

            ResetShip(Heading.West);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Left, Units = 0 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.West);

            ResetShip(Heading.West);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Left, Units = 360 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.West);
        }

        [TestMethod]
        public void ShouldChangeShipHeadingRightNorth()
        {
            ResetShip(Heading.North);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Right, Units = 90 });
            var newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.East);

            ResetShip(Heading.North);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Right, Units = 180 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.South);

            ResetShip(Heading.North);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Right, Units = 270 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.West);

            ResetShip(Heading.North);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Right, Units = 0 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.North);

            ResetShip(Heading.North);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Right, Units = 360 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.North);
        }

        [TestMethod]
        public void ShouldChangeShipHeadingRightEast()
        {
            ResetShip(Heading.East);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Right, Units = 90 });
            var newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.South);

            ResetShip(Heading.East);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Right, Units = 180 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.West);

            ResetShip(Heading.East);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Right, Units = 270 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.North);

            ResetShip(Heading.East);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Right, Units = 0 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.East);

            ResetShip(Heading.East);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Right, Units = 360 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.East);
        }

        [TestMethod]
        public void ShouldChangeShipHeadingRightSouth()
        {
            ResetShip(Heading.South);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Right, Units = 90 });
            var newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.West);

            ResetShip(Heading.South);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Right, Units = 180 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.North);

            ResetShip(Heading.South);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Right, Units = 270 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.East);

            ResetShip(Heading.South);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Right, Units = 0 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.South);

            ResetShip(Heading.South);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Right, Units = 360 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.South);
        }

        [TestMethod]
        public void ShouldChangeShipHeadingRightWest()
        {
            ResetShip(Heading.West);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Right, Units = 90 });
            var newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.North);

            ResetShip(Heading.West);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Right, Units = 180 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.East);

            ResetShip(Heading.West);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Right, Units = 270 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.South);

            ResetShip(Heading.West);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Right, Units = 0 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.West);

            ResetShip(Heading.West);
            _solution.ChangeDirection(new Direction() { Heading = Heading.Right, Units = 360 });
            newHeading = _solution._ship.Heading;

            Assert.AreEqual(newHeading, Heading.West);
        }

        [TestMethod]
        public void ShouldMoveTheShipInGivenDirection()
        {
            ResetShip(Heading.North);
            _solution.MoveInDirection(new Direction() { Heading = Heading.North, Units = 10 });

            Assert.AreEqual(_solution._ship.North, 10);

            ResetShip(Heading.North);
            _solution.MoveInDirection(new Direction() { Heading = Heading.South, Units = 10 });

            Assert.AreEqual(_solution._ship.South, 10);

            ResetShip(Heading.North);
            _solution.MoveInDirection(new Direction() { Heading = Heading.East, Units = 10 });

            Assert.AreEqual(_solution._ship.East, 10);

            ResetShip(Heading.North);
            _solution.MoveInDirection(new Direction() { Heading = Heading.West, Units = 10 });

            Assert.AreEqual(_solution._ship.West, 10);

            ResetShip(Heading.North);
            _solution.MoveInDirection(new Direction() { Heading = Heading.Forward, Units = 10 });

            Assert.AreEqual(_solution._ship.North, 10);
        }

        [TestMethod]
        public void ShouldChangeShipHeadingRightNorthEast()
        {
            ResetShip(Heading.East, Heading.North);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Right, Units = 90 });
            var newX = _solution._ship.WaypointX;
            var newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 0);
            Assert.AreEqual(newY.Heading, Heading.South);
            Assert.AreEqual(newY.Units, 1);

            ResetShip(Heading.East, Heading.North);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Right, Units = 180 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.West);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.South);
            Assert.AreEqual(newY.Units, 0);

            ResetShip(Heading.East, Heading.North);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Right, Units = 270 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.West);
            Assert.AreEqual(newX.Units, 0);
            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 1);

            ResetShip(Heading.East, Heading.North);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Right, Units = 360 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 0);

            ResetShip(Heading.East, Heading.North);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Right, Units = 0 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 0);
        }

        [TestMethod]
        public void ShouldChangeShipHeadingRightSouthEast()
        {
            ResetShip(Heading.East, Heading.South);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Right, Units = 90 });
            var newX = _solution._ship.WaypointX;
            var newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.West);
            Assert.AreEqual(newX.Units, 0);
            Assert.AreEqual(newY.Heading, Heading.South);
            Assert.AreEqual(newY.Units, 1);

            ResetShip(Heading.East, Heading.South);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Right, Units = 180 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.West);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 0);

            ResetShip(Heading.East, Heading.South);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Right, Units = 270 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 0);
            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 1);

            ResetShip(Heading.East, Heading.South);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Right, Units = 360 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.South);
            Assert.AreEqual(newY.Units, 0);

            ResetShip(Heading.East, Heading.South);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Right, Units = 0 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.South);
            Assert.AreEqual(newY.Units, 0);
        }

        [TestMethod]
        public void ShouldChangeShipHeadingRightSouthWest()
        {
            ResetShip(Heading.West, Heading.South);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Right, Units = 90 });
            var newX = _solution._ship.WaypointX;
            var newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.West);
            Assert.AreEqual(newX.Units, 0);
            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 1);

            ResetShip(Heading.West, Heading.South);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Right, Units = 180 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 0);

            ResetShip(Heading.West, Heading.South);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Right, Units = 270 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 0);
            Assert.AreEqual(newY.Heading, Heading.South);
            Assert.AreEqual(newY.Units, 1);

            ResetShip(Heading.West, Heading.South);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Right, Units = 360 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.West);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.South);
            Assert.AreEqual(newY.Units, 0);

            ResetShip(Heading.West, Heading.South);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Right, Units = 0 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.West);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.South);
            Assert.AreEqual(newY.Units, 0);
        }

        [TestMethod]
        public void ShouldChangeShipHeadingRightNorthWest()
        {
            ResetShip(Heading.West, Heading.North);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Right, Units = 90 });
            var newX = _solution._ship.WaypointX;
            var newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 0);
            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 1);

            ResetShip(Heading.West, Heading.North);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Right, Units = 180 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.South);
            Assert.AreEqual(newY.Units, 0);

            ResetShip(Heading.West, Heading.North);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Right, Units = 270 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.West);
            Assert.AreEqual(newX.Units, 0);
            Assert.AreEqual(newY.Heading, Heading.South);
            Assert.AreEqual(newY.Units, 1);

            ResetShip(Heading.West, Heading.North);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Right, Units = 360 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.West);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 0);

            ResetShip(Heading.West, Heading.North);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Right, Units = 0 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.West);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 0);
        }

        [TestMethod]
        public void ShouldChangeShipHeadingLeftNorthEast()
        {
            ResetShip(Heading.East, Heading.North);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Left, Units = 90 });
            var newX = _solution._ship.WaypointX;
            var newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.West);
            Assert.AreEqual(newX.Units, 0);
            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 1);

            ResetShip(Heading.East, Heading.North);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Left, Units = 180 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.West);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.South);
            Assert.AreEqual(newY.Units, 0);

            ResetShip(Heading.East, Heading.North);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Left, Units = 270 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 0);
            Assert.AreEqual(newY.Heading, Heading.South);
            Assert.AreEqual(newY.Units, 1);

            ResetShip(Heading.East, Heading.North);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Left, Units = 360 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 0);

            ResetShip(Heading.East, Heading.North);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Left, Units = 0 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 0);
        }

        [TestMethod]
        public void ShouldChangeShipHeadingLeftSouthEast()
        {
            ResetShip(Heading.East, Heading.South);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Left, Units = 90 });
            var newX = _solution._ship.WaypointX;
            var newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 0);
            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 1);

            ResetShip(Heading.East, Heading.South);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Left, Units = 180 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.West);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 0);

            ResetShip(Heading.East, Heading.South);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Left, Units = 270 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.West);
            Assert.AreEqual(newX.Units, 0);
            Assert.AreEqual(newY.Heading, Heading.South);
            Assert.AreEqual(newY.Units, 1);

            ResetShip(Heading.East, Heading.South);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Left, Units = 360 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.South);
            Assert.AreEqual(newY.Units, 0);

            ResetShip(Heading.East, Heading.South);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Left, Units = 0 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.South);
            Assert.AreEqual(newY.Units, 0);
        }

        [TestMethod]
        public void ShouldChangeShipHeadingLeftSouthWest()
        {
            ResetShip(Heading.West, Heading.South);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Left, Units = 90 });
            var newX = _solution._ship.WaypointX;
            var newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 0);
            Assert.AreEqual(newY.Heading, Heading.South);
            Assert.AreEqual(newY.Units, 1);

            ResetShip(Heading.West, Heading.South);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Left, Units = 180 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 0);

            ResetShip(Heading.West, Heading.South);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Left, Units = 270 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.West);
            Assert.AreEqual(newX.Units, 0);
            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 1);

            ResetShip(Heading.West, Heading.South);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Left, Units = 360 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.West);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.South);
            Assert.AreEqual(newY.Units, 0);

            ResetShip(Heading.West, Heading.South);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Left, Units = 0 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.West);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.South);
            Assert.AreEqual(newY.Units, 0);
        }

        [TestMethod]
        public void ShouldChangeShipHeadingLeftNorthWest()
        {
            ResetShip(Heading.West, Heading.North);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Left, Units = 90 });
            var newX = _solution._ship.WaypointX;
            var newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.West);
            Assert.AreEqual(newX.Units, 0);
            Assert.AreEqual(newY.Heading, Heading.South);
            Assert.AreEqual(newY.Units, 1);

            ResetShip(Heading.West, Heading.North);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Left, Units = 180 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.South);
            Assert.AreEqual(newY.Units, 0);

            ResetShip(Heading.West, Heading.North);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Left, Units = 270 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 0);
            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 1);

            ResetShip(Heading.West, Heading.North);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Left, Units = 360 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.West);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 0);

            ResetShip(Heading.West, Heading.North);
            _solution.ChangeWaypoint(new Direction() { Heading = Heading.Left, Units = 0 });
            newX = _solution._ship.WaypointX;
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newX.Heading, Heading.West);
            Assert.AreEqual(newX.Units, 1);
            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 0);
        }

        [TestMethod]
        public void ShouldSetTheXWaypoint()
        {
            ResetShip(Heading.East, Heading.North);
            _solution.SetWaypointX(new Direction() { Heading = Heading.East, Units = 1 });
            var newX = _solution._ship.WaypointX;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 2);

            ResetShip(Heading.East, Heading.North);
            _solution.SetWaypointX(new Direction() { Heading = Heading.West, Units = 1 });
            newX = _solution._ship.WaypointX;

            Assert.AreEqual(newX.Heading, Heading.East);
            Assert.AreEqual(newX.Units, 0);

            ResetShip(Heading.East, Heading.North);
            _solution.SetWaypointX(new Direction() { Heading = Heading.West, Units = 2 });
            newX = _solution._ship.WaypointX;

            Assert.AreEqual(newX.Heading, Heading.West);
            Assert.AreEqual(newX.Units, 1);
        }

        [TestMethod]
        public void ShouldSetTheYWaypoint()
        {
            ResetShip(Heading.East, Heading.North);
            _solution.SetWaypointY(new Direction() { Heading = Heading.North, Units = 1 });
            var newY = _solution._ship.WaypointY;

            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 1);

            ResetShip(Heading.East, Heading.North);
            _solution._ship.WaypointY.Units = 1;
            _solution.SetWaypointY(new Direction() { Heading = Heading.South, Units = 1 });
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newY.Heading, Heading.North);
            Assert.AreEqual(newY.Units, 0);

            ResetShip(Heading.East, Heading.North);
            _solution._ship.WaypointY.Units = 1;
            _solution.SetWaypointY(new Direction() { Heading = Heading.South, Units = 2 });
            newY = _solution._ship.WaypointY;

            Assert.AreEqual(newY.Heading, Heading.South);
            Assert.AreEqual(newY.Units, 1);
        }

        [TestMethod]
        public void ShouldTraverseAllDirectionInShipDirections()
        {
            _solution.TraverseDirections();

            Assert.AreEqual(_solution._ship.North, 3);
            Assert.AreEqual(_solution._ship.East, 17);
            Assert.AreEqual(_solution._ship.West, 0);
            Assert.AreEqual(_solution._ship.South, 11);
            Assert.AreEqual(_solution._ship.Heading, Heading.South);
        }

        [TestMethod]
        public void ShouldGetManhattanDistanceForShip()
        {
            var manhattanDistance = _solution.GetManhattanDistance();

            Assert.AreEqual(manhattanDistance, 25);
        }

        [TestMethod]
        public void ShouldGetManhattanDistanceForShipWithWaypoints()
        {
            var manhattanDistance = _solution.GetManhattanDistanceWithWaypoints();

            Assert.AreEqual(manhattanDistance, 286);
        }

        private void ResetShip(Heading heading)
        {
            _solution._ship.Heading = heading;
            _solution._ship.North = 0;
            _solution._ship.South = 0;
            _solution._ship.West = 0;
            _solution._ship.East = 0;
        }

        private void ResetShip(Heading headingX, Heading headingY)
        {
            _solution._ship.WaypointX.Heading = headingX;
            _solution._ship.WaypointX.Units = 1;
            _solution._ship.WaypointY.Heading = headingY;
            _solution._ship.WaypointY.Units = 0;
            _solution._ship.North = 0;
            _solution._ship.South = 0;
            _solution._ship.West = 0;
            _solution._ship.East = 0;
        }
    }
}
