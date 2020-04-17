using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dolaris.UnitConverter;
using System.Linq;
using Dolaris.UnitConverter;

namespace Dolaris.UnitConverter.Test {

    [TestClass]
    public class UnitsOfLengthTest {

        [TestMethod]
        public void ConversionCorrectness() {

            var manager = new UnitsManager(new UnitCollection(UnitCollection.CreateUnits()));

            var inches = manager.GetUnit(UnitID.Inch);
            Assert.IsNotNull(inches);

            var feet = manager.GetUnit(UnitID.Foot);
            Assert.IsNotNull(feet);

            inches.Magnitude = 32;
            manager.UpdateUnits(sourceUnit: inches);

            Assert.AreEqual<double>(expected: 2.6666666666666667, actual: feet.Magnitude.Value, message: "Conversion from inches to feet is incorrect!");
        }

        [TestMethod]
        public void ManagerTest() {

            var manager = new UnitsManager(new UnitCollection(UnitCollection.CreateUnits()));

            var meter = manager.GetUnit(UnitID.Meter); //manager.Units.First(p => p.Name.Equals("Meter", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsNotNull(meter, "The unit Meter was not found.");

            meter.Magnitude = 5;   // 5m

            manager.UpdateUnits(meter);
        }

        [TestMethod]
        public void RoundingTest() {

            var manager = new UnitsManager(new UnitCollection(UnitCollection.CreateUnits()));

            var meter = manager.GetUnit(UnitID.Meter);
            meter.Magnitude = 123456789;

            double d = 1000000;
            string dstr = d.ToString();
        }
    }
}
