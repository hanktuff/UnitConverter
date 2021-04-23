using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dolaris.UnitConverter;

namespace Dolaris.UnitConverter.Test {

    [TestClass]
    public class UnitConverterUtilityTest {

        [TestMethod]
        public void TryParseUnitsTest() {

            string str1 = " 3.14 m  ";

            IEnumerable<IUnit> units;
            var unitsFound = UnitConverterUtility.TryParseUnits(str1, out units);

            Assert.IsTrue(unitsFound);
            Assert.IsNotNull(units);
            Assert.AreEqual(expected: 3.14, actual: units.First().Magnitude.Value);
            Assert.AreEqual<UnitID>(expected: UnitID.Meter, actual: units.First().ID);
        }

        [TestMethod]
        public void ParseDoubleTest() {

            string remainder;

            Assert.AreEqual(expected: 45, actual: UnitConverterUtility.ParseDouble("45", out remainder));
            Assert.AreEqual(expected: 3.14, actual: UnitConverterUtility.ParseDouble("3.14", out remainder));
            Assert.AreEqual(expected: -123.45678E+004, actual: UnitConverterUtility.ParseDouble("  -123.45678E+004   ", out remainder));
            Assert.AreEqual(expected: 9.98, actual: UnitConverterUtility.ParseDouble(" 9.98  abcdef", out remainder));

            Assert.IsFalse(UnitConverterUtility.ParseDouble("qqq123", out remainder).HasValue);
            Assert.IsFalse(UnitConverterUtility.ParseDouble(string.Empty, out remainder).HasValue);
            Assert.IsFalse(UnitConverterUtility.ParseDouble(null, out remainder).HasValue);

            UnitConverterUtility.ParseDouble(" 1.2m  ", out remainder);
            Assert.AreEqual(expected: "m  ", actual: remainder);
        }

        [TestMethod]
        public void GetMatchingUnitsTest() {

            Assert.AreEqual<UnitID>(expected: UnitID.Meter, actual: UnitConverterUtility.GetMatchingUnits("Meter").First().ID);
            Assert.AreEqual<UnitID>(expected: UnitID.Meter, actual: UnitConverterUtility.GetMatchingUnits("  meTEr   ").First().ID);
            Assert.AreEqual<UnitID>(expected: UnitID.Meter, actual: UnitConverterUtility.GetMatchingUnits("m").First().ID);
            Assert.IsNull(UnitConverterUtility.GetMatchingUnits("thisisnotaunitname"));
            Assert.AreEqual<UnitID>(expected: UnitID.Meter, actual: UnitConverterUtility.GetMatchingUnits("met").First().ID);

            var startsWithMeter = UnitConverterUtility.GetMatchingUnits(" mete").ToList();
            if (startsWithMeter.Count() < 2) {
                throw new Exception("There should be at least 2 units starting with this string");
            }
        }



        [TestMethod]
        public void SplitUnitStringBasicTests() {

            var unitstr = new UnitString();

            var elements = new List<UnitString.Element>();

            elements = unitstr.Split(null)?.ToList();
            Assert.AreEqual(0, elements.Count());

            elements = unitstr.Split("")?.ToList();
            Assert.AreEqual(0, elements.Count());

            elements = unitstr.Split("    ")?.ToList();
            Assert.AreEqual(0, elements.Count());
        }

        [TestMethod]
        public void SplitUnitStringOnlyTextTest() {

            var unitstr = new UnitString();
            var elements = new List<UnitString.Element>();

            elements = unitstr.Split("a")?.ToList();
            Assert.AreEqual(1, elements.Count());
            Assert.IsFalse(elements.First().IsNumeric);

            elements = unitstr.Split("  abc     ")?.ToList();
            Assert.AreEqual(1, elements.Count());
            Assert.IsFalse(elements.First().IsNumeric);

            elements = unitstr.Split(" a   b  c  ")?.ToList();
            Assert.AreEqual(1, elements.Count());
            Assert.IsFalse(elements.First().IsNumeric);

            elements = unitstr.Split("abc   def")?.ToList();
            Assert.AreEqual(1, elements.Count());
            Assert.IsFalse(elements.First().IsNumeric);

            elements = unitstr.Split("-a.e+x")?.ToList();
            Assert.AreEqual(1, elements.Count());
            Assert.IsFalse(elements.First().IsNumeric);
        }

        [TestMethod]
        public void SplitUnitStringBasicNumbersTest() {

            var unitstr = new UnitString();
            var elements = new List<UnitString.Element>();

            elements = unitstr.Split("123")?.ToList();
            Assert.AreEqual(1, elements.Count());
            Assert.IsTrue(elements.First().IsNumeric);

            elements = unitstr.Split("-4.56")?.ToList();
            Assert.AreEqual(1, elements.Count());
            Assert.IsTrue(elements.First().IsNumeric);

            elements = unitstr.Split("2e+3")?.ToList();
            Assert.AreEqual(1, elements.Count());
            Assert.IsTrue(elements.First().IsNumeric);

            elements = unitstr.Split(".12345")?.ToList();
            Assert.AreEqual(1, elements.Count());

            elements = unitstr.Split("-.5e+2")?.ToList();
            Assert.AreEqual(1, elements.Count());
            Assert.IsTrue(elements.First().IsNumeric);
        }

        [TestMethod]
        public void SplitUnitStringComplexTest() {

            var unitstr = new UnitString();
            var elements = new List<UnitString.Element>();

            elements = unitstr.Split("123 45 6")?.ToList();
            Assert.AreEqual(5, elements.Count());
            Assert.AreEqual(2, elements.Count(p => p.IsNumeric == false));
            Assert.AreEqual(3, elements.Count(p => p.IsNumeric == true));

            elements = unitstr.Split("abcd-4.56")?.ToList();
            Assert.AreEqual(2, elements.Count());
            Assert.AreEqual(1, elements.Count(p => p.IsNumeric == false));
            Assert.AreEqual(1, elements.Count(p => p.IsNumeric == true));

            elements = unitstr.Split("2e+3qwerty")?.ToList();
            Assert.AreEqual(2, elements.Count());
            Assert.AreEqual(1, elements.Count(p => p.IsNumeric == false));
            Assert.AreEqual(1, elements.Count(p => p.IsNumeric == true));

            elements = unitstr.Split(".12345;'+{}(-*-0.5e+1")?.ToList();
            Assert.AreEqual(3, elements.Count());
            Assert.AreEqual(2, elements.Count(p => p.IsNumeric == true));
            Assert.AreEqual(1, elements.Count(p => p.IsNumeric == false));

            elements = unitstr.Split("a33bc5.4 qwerty-7e-2xyz")?.ToList();
            Assert.AreEqual(7, elements.Count());
            Assert.AreEqual(3, elements.Count(p => p.IsNumeric == true));
            Assert.AreEqual(4, elements.Count(p => p.IsNumeric == false));
        }
    }
}
