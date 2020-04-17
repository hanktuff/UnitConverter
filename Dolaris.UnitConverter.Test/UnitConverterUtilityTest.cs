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
    }
}
