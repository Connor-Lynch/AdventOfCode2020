using AdventOfCode.Day4;
using AdventOfCode.Day4.Entities;
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
    public class Day4Tests
    {
        private Mock<IFileReader> _fileReaderMock;
        private Day4Solution _solution;
        private List<string> _mockData;

        [TestInitialize]
        public void InIt()
        {
            _mockData = new List<string>() {
                "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd byr:1937 iyr:2017 cid:147 hgt:183cm",
                "iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884 hcl:#cfa07d byr:1929",
                "hcl:#ae17e1 iyr:2013 eyr:2024 ecl:brn pid:760753108 byr:1931 hgt:179cm",
                "hcl:#cfa07d eyr:2025 pid:166559648 iyr:2011 ecl:brn hgt:59in",
            };
            var autoMocker = new AutoMoqer();

            _fileReaderMock = autoMocker.GetMock<IFileReader>();
            _fileReaderMock.Setup(f => f.ReadFileToStringArray(It.IsAny<string>())).Returns(_mockData);

            _solution = new Day4Solution(_fileReaderMock.Object);
        }

        [TestMethod]
        public void SolutionShouldMapInputPassports()
        {
            var passports = _solution._passports;

            Assert.IsInstanceOfType(passports, typeof(List<Passport>));
            Assert.AreEqual(passports[0].EyeColor, "gry");
            Assert.AreEqual(passports[0].PassportID, "860033327");
            Assert.AreEqual(passports[0].ExpirationYear,"2020");
            Assert.AreEqual(passports[0].HairColor, "#fffffd");
            Assert.AreEqual(passports[0].BirthYear, "1937");
            Assert.AreEqual(passports[0].IssueYear, "2017");
            Assert.AreEqual(passports[0].CountryID, "147");
            Assert.AreEqual(passports[0].Height, "183cm");
            Assert.AreEqual(passports[1].Height, null);
        }

        [TestMethod]
        public void ShouldReturnAllValidPassports()
        {
            var validPassports = _solution.GetPasspoortsWithRequiredFields();

            Assert.AreEqual(validPassports.Count, 2);
            Assert.IsNotNull(validPassports.Where(p => p.PassportID == "860033327").FirstOrDefault());
            Assert.IsNotNull(validPassports.Where(p => p.PassportID == "760753108").FirstOrDefault());
        }

        [TestMethod]
        public void ShouldNotFindValidPasswordsWithInvalidData()
        {
            var validInput = new List<string>() {
                "eyr:1972 cid:100 hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926",
                "iyr:2019 hcl:#602927 eyr:1967 hgt:170cm ecl:grn pid:012533040 byr:1946",
                "hcl:dab227 iyr:2012 ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277",
                "hgt:59cm ecl:zzz eyr:2038 hcl:74454a iyr:2023 pid:3556412378 byr:2007",
            };
            _fileReaderMock.Setup(f => f.ReadFileToStringArray(It.IsAny<string>())).Returns(validInput);
            _solution = new Day4Solution(_fileReaderMock.Object);

            var validPassports = _solution.GetPasspoortsWithRequiredFields();
            var validData = _solution.GetPassportsWithValidData(validPassports);

            Assert.AreEqual(validData.Count, 0);
        }

        [TestMethod]
        public void ShouldFindValidPasswordsWithValidData()
        {
            var validInput = new List<string>() {
                "pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980 hcl:#623a2f",
                "eyr:2029 ecl:blu cid:129 byr:1989 iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm",
                "hcl:#888785 hgt:164cm byr:2001 iyr:2015 cid:88 pid:545766238 ecl:hzl eyr:2022",
                "iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719",
            };
            _fileReaderMock.Setup(f => f.ReadFileToStringArray(It.IsAny<string>())).Returns(validInput);
            _solution = new Day4Solution(_fileReaderMock.Object);

            var validPassports = _solution.GetPasspoortsWithRequiredFields();
            var validData = _solution.GetPassportsWithValidData(validPassports);

            Assert.AreEqual(validData.Count, 4);
        }

        [TestMethod]
        public void ValidateYearsShouldReturnTrueWithValidYear()
        {
            var min = _solution.ValidateYear(1920, 2002, "1920");
            var middle = _solution.ValidateYear(1920, 2002, "1980");
            var max = _solution.ValidateYear(1920, 2002, "2002");

            Assert.IsTrue(min);
            Assert.IsTrue(middle);
            Assert.IsTrue(max);
        }

        [TestMethod]
        public void ValidateYearsShouldReturnFalseWithInvalidYear()
        {
            var low = _solution.ValidateYear(1920, 2002, "1919");
            var high = _solution.ValidateYear(1920, 2002, "2003");
            var sml = _solution.ValidateYear(1920, 2002, "198");
            var lrg = _solution.ValidateYear(1920, 2002, "19800");
            var invalid = _solution.ValidateYear(1920, 2002, "test");


            Assert.IsFalse(low);
            Assert.IsFalse(high);
            Assert.IsFalse(sml);
            Assert.IsFalse(lrg);
            Assert.IsFalse(invalid);
        }

        [TestMethod]
        public void ValidateHeightShouldReturnTrueWithValidHeight()
        {
            var minIn = _solution.ValidateHeight("59in");
            var middleIn = _solution.ValidateHeight("60in");
            var maxIn = _solution.ValidateHeight("76in");

            var minCm = _solution.ValidateHeight("150cm");
            var middleCm = _solution.ValidateHeight("175cm");
            var maxCm = _solution.ValidateHeight("193cm");

            Assert.IsTrue(minIn);
            Assert.IsTrue(middleIn);
            Assert.IsTrue(maxIn);

            Assert.IsTrue(minCm);
            Assert.IsTrue(middleCm);
            Assert.IsTrue(maxCm);
        }

        [TestMethod]
        public void ValidateHeightShouldReturnFalseWithInvalidHeight()
        {
            var lowIn = _solution.ValidateHeight("58in");
            var heighIn = _solution.ValidateHeight("77in");
            var invalidIn = _solution.ValidateHeight("sixin");

            var lowCm = _solution.ValidateHeight("149cm");
            var heighCm = _solution.ValidateHeight("194cm");
            var invalidCm = _solution.ValidateHeight("sixcm");

            var invalidUnit = _solution.ValidateHeight("123");

            Assert.IsFalse(lowIn);
            Assert.IsFalse(heighIn);
            Assert.IsFalse(invalidIn);

            Assert.IsFalse(lowCm);
            Assert.IsFalse(heighCm);
            Assert.IsFalse(invalidCm);

            Assert.IsFalse(invalidUnit);
        }

        [TestMethod]
        public void ValidateHairColorShouldReturnTrueWithValidColor()
        {
            var valid = _solution.ValidateHairColor("#123abc");
            var intMin = _solution.ValidateHairColor("#000000");
            var intMax = _solution.ValidateHairColor("#999999");
            var charMin = _solution.ValidateHairColor("#AAAAAA");
            var charMax = _solution.ValidateHairColor("#FFFFFF");

            Assert.IsTrue(valid);
            Assert.IsTrue(intMin);
            Assert.IsTrue(intMax);
            Assert.IsTrue(charMin);
            Assert.IsTrue(charMax);
        }

        [TestMethod]
        public void ValidateHairColorShouldReturnFalseWithInalidColor()
        {
            var invalid1 = _solution.ValidateHairColor("#123abz");
            var invalid2 = _solution.ValidateHairColor("123abc");

            Assert.IsFalse(invalid1);
            Assert.IsFalse(invalid2);
        }

        [TestMethod]
        public void ValidateEyeColorShouldReturnTrueWithValidColor()
        {
            var amb = _solution.ValidateEyeColor("amb");
            var blu = _solution.ValidateEyeColor("blu");
            var brn = _solution.ValidateEyeColor("brn");
            var gry = _solution.ValidateEyeColor("gry");
            var grn = _solution.ValidateEyeColor("grn");
            var hzl = _solution.ValidateEyeColor("hzl");
            var oth =_solution.ValidateEyeColor("oth");

            Assert.IsTrue(amb);
            Assert.IsTrue(blu);
            Assert.IsTrue(brn);
            Assert.IsTrue(gry);
            Assert.IsTrue(grn);
            Assert.IsTrue(hzl);
            Assert.IsTrue(oth);
        }

        [TestMethod]
        public void ValidateEyeColorShouldReturnFalseWithInvalidColor()
        {
            var invalid = _solution.ValidateEyeColor("blk");

            Assert.IsFalse(invalid);
        }

        [TestMethod]
        public void ValidatePassportIdShouldReturnTrueForValidPassport()
        {
            var valid = _solution.ValidatePassportId("000000001");

            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void ValidatePassportIdShouldReturnFalseForInalidPassport()
        {
            var invalid = _solution.ValidatePassportId("0123456789");

            Assert.IsFalse(invalid);
        }
    }
}
