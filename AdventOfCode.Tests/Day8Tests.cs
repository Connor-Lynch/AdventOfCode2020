using AdventOfCode.Day8;
using AdventOfCode.Day8.Entities;
using AdventOfCode.Day8.Enums;
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
    public class Day8Tests
    {
        private Mock<IFileReader> _fileReaderMock;
        private Day8Solution _solution;
        private List<string> _mockData;

        [TestInitialize]
        public void InIt()
        {
            _mockData = new List<string>() {
                "nop +0",
                "acc +1",
                "jmp +4",
                "acc +3",
                "jmp -3",
                "acc -99",
                "acc +1",
                "jmp -4",
                "acc +6"
            };
            var autoMocker = new AutoMoqer();

            _fileReaderMock = autoMocker.GetMock<IFileReader>();
            _fileReaderMock.Setup(f => f.ReadFileToStringArray(It.IsAny<string>())).Returns(_mockData);

            _solution = new Day8Solution(_fileReaderMock.Object);
        }

        [TestMethod]
        public void ShouldMapProgramFromInput()
        {
            var program = _solution._program;

            Assert.IsInstanceOfType(program, typeof(HandheldProgram));
            Assert.AreEqual(program.Accumulator, 0);
            Assert.AreEqual(program.CurrentIndex, 0);
            Assert.AreEqual(program.Instructions.Count, 9);

            Assert.AreEqual(program.Instructions[0].Opperation, Opperation.nop);
            Assert.AreEqual(program.Instructions[0].Offset, 0);

            Assert.AreEqual(program.Instructions[1].Opperation, Opperation.acc);
            Assert.AreEqual(program.Instructions[1].Offset, 1);

            Assert.AreEqual(program.Instructions[2].Opperation, Opperation.jmp);
            Assert.AreEqual(program.Instructions[2].Offset, 4);

            Assert.AreEqual(program.Instructions[5].Opperation, Opperation.acc);
            Assert.AreEqual(program.Instructions[5].Offset, -99);
        }

        [TestMethod]
        public void ShouldRunProgramUntilLoopDetected()
        {
            _solution.RunProgramUntiLoopDetected();
            var finalAccumulator = _solution._program.Accumulator;

            Assert.AreEqual(finalAccumulator, 5);
        }

        [TestMethod]
        public void ShouldFixTheProgram()
        {
            _solution.FixProgram();

            Assert.AreEqual(_solution._program.Accumulator, 8);
        }
    }
}
