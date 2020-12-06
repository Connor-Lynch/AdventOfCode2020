using AdventOfCode.Day6;
using AdventOfCode.Day6.Entities;
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
    public class Day6Tests
    {
        private Mock<IFileReader> _fileReaderMock;
        private Day6Solution _solution;
        private List<string> _mockData;

        [TestInitialize]
        public void InIt()
        {
            _mockData = new List<string>() {
                "abc",
                "a,b,c",
                "ab,ac",
                "a,a,a,a",
                "b"
            };
            var autoMocker = new AutoMoqer();

            _fileReaderMock = autoMocker.GetMock<IFileReader>();
            _fileReaderMock.Setup(f => f.ReadFileToStringArray(It.IsAny<string>())).Returns(_mockData);

            _solution = new Day6Solution(_fileReaderMock.Object);
        }

        [TestMethod]
        public void ShouldMapCustomsFormsFromInput()
        {
            var forms = _solution._customsForms;

            Assert.IsInstanceOfType(forms, typeof(List<CustomsForm>));
            Assert.AreEqual(forms.Count, 5);

            Assert.AreEqual(forms[0].RawAnswers, "abc");
            Assert.AreEqual(forms[0].IndividualsAnswers.Count, 1);

            Assert.AreEqual(forms[1].RawAnswers, "abc");
            Assert.AreEqual(forms[1].IndividualsAnswers.Count, 3);

            Assert.AreEqual(forms[2].RawAnswers, "abac");
            Assert.AreEqual(forms[2].IndividualsAnswers.Count, 2);

            Assert.AreEqual(forms[3].RawAnswers, "aaaa");
            Assert.AreEqual(forms[3].IndividualsAnswers.Count, 4);

            Assert.AreEqual(forms[4].RawAnswers, "b");
            Assert.AreEqual(forms[4].IndividualsAnswers.Count, 1);
        }

        [TestMethod]
        public void ShouldSetTotalAffirmativeAnswersOnEachForm()
        {
            _solution.TallyAffirmativeAnswersOnForms();
            var forms = _solution._customsForms;

            Assert.AreEqual(forms[0].TotalAffirmativeAnswers, 3);
            Assert.AreEqual(forms[1].TotalAffirmativeAnswers, 3);
            Assert.AreEqual(forms[2].TotalAffirmativeAnswers, 3);
            Assert.AreEqual(forms[3].TotalAffirmativeAnswers, 1);
            Assert.AreEqual(forms[4].TotalAffirmativeAnswers, 1);
        }

        [TestMethod]
        public void ShouldGetSumOfAllAffirmativeAnswersOnForms()
        {
            var affirmativeAsnwers = _solution.SumAffirmativeAnswers();

            Assert.AreEqual(affirmativeAsnwers, 11);
        }

        [TestMethod]
        public void ShouldGetTotalNumberOfSharedAnswersOnForm()
        {
            _solution.TallySharedAffirmativeAnswersOnForms();
            var forms = _solution._customsForms;

            Assert.AreEqual(forms[0].TotalSharedAffirmativeAnswers, 3);
            Assert.AreEqual(forms[1].TotalSharedAffirmativeAnswers, 0);
            Assert.AreEqual(forms[2].TotalSharedAffirmativeAnswers, 1);
            Assert.AreEqual(forms[3].TotalSharedAffirmativeAnswers, 1);
            Assert.AreEqual(forms[4].TotalSharedAffirmativeAnswers, 1);
        }

        [TestMethod]
        public void ShouldGetSumOfAllSharedAffirmativeAnswersOnForms()
        {
            var sharedAffirmativeAsnwers = _solution.SumSharedAffirmativeAnswers();

            Assert.AreEqual(sharedAffirmativeAsnwers, 6);
        }
    }
}
