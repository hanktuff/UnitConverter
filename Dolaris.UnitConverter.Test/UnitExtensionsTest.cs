using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dolaris.UnitConverter;

namespace Dolaris.UnitConverter.Test {

    [TestClass]
    public class UnitExtensionsTest {

        [TestMethod]
        public void GetPlacesBeforeDecimalPointTest() {

            IUnit unit = GetDummyUnit();

            unit.Magnitude = 123.45678;
            Assert.AreEqual(expected: 3, actual: unit.GetPlacesBeforeDecimalPoint());

            unit.Magnitude = -123.45678;
            Assert.AreEqual(expected: 3, actual: unit.GetPlacesBeforeDecimalPoint());

            unit.Magnitude = 0.45678;
            Assert.AreEqual(expected: 0, actual: unit.GetPlacesBeforeDecimalPoint());

            unit.Magnitude = -0.45678;
            Assert.AreEqual(expected: 0, actual: unit.GetPlacesBeforeDecimalPoint());

            unit.Magnitude = 1000000000;
            Assert.AreEqual(expected: 10, actual: unit.GetPlacesBeforeDecimalPoint());

            unit.Magnitude = 1E+100;
            Assert.AreEqual(expected: 101, actual: unit.GetPlacesBeforeDecimalPoint());

            unit.Magnitude = 1.234E+100;
            Assert.AreEqual(expected: 101, actual: unit.GetPlacesBeforeDecimalPoint());

            unit.Magnitude = 123.45678E-4;
            Assert.AreEqual(expected: 0, actual: unit.GetPlacesBeforeDecimalPoint());
        }

        [TestMethod]
        public void GetFormattedStringTest() {

            IUnit unit = GetDummyUnit();
            string errorString = null;

            unit.Magnitude = 123;
            Assert.AreEqual(expected: "123", actual: unit.GetFormattedString(out errorString));

            unit.Magnitude = -1;
            unit.GetFormattedString(out errorString);
            Assert.IsFalse(string.IsNullOrEmpty(errorString));

            unit.Magnitude = double.NaN;
            unit.GetFormattedString(out errorString);
            Assert.IsFalse(string.IsNullOrEmpty(errorString));

            unit.Magnitude = 5;
            Assert.AreEqual(expected: "101", actual: unit.GetFormattedString(out errorString, toBase: 2));
            Assert.IsTrue(string.IsNullOrEmpty(errorString));

            unit.Magnitude = 13.12345;
            Assert.AreEqual(expected: "1101", actual: unit.GetFormattedString(out errorString, toBase: 2));

            unit.Magnitude = double.NaN;
            Assert.AreEqual(expected: "", actual: unit.GetFormattedString(out errorString, toBase: 2));

            unit.Magnitude = 58;
            Assert.AreEqual(expected: "72", actual: unit.GetFormattedString(out errorString, toBase: 8));

            unit.Magnitude = 253;
            Assert.AreEqual(expected: "fd", actual: unit.GetFormattedString(out errorString, toBase: 16));
        }



        // return a Unit object to play with
        private IUnit GetDummyUnit() {

            return UnitsManager.CreateInstance().GetUnit(UnitID.Meter);
        }
    }
}
