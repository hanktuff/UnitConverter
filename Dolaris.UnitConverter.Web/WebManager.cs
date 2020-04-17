using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dolaris.UnitConverter;

namespace Dolaris.UnitConverter.Web {

    public class WebManager {

        /// <summary>
        /// An instance of the Unit Manager (used to work with units of measurement).
        /// </summary>
        private UnitsManager _unitsManager = UnitsManager.CreateInstance();

        protected IList<WebUnitGroup> _webUnitGroups;
        protected IList<WebUnit> _webUnits;


        /// <summary>
        /// Creates a new instance of the WebManager class.
        /// </summary>
        public WebManager() {

            _webUnitGroups = CreateWebUnitGroups();
            _webUnits = CreateWebUnits();
        }


        /// <summary>
        /// Recalculates (updates) all values of all units based on the source unit.
        /// </summary>
        /// <param name="sourceUnit"></param>
        /// <returns></returns>
        public static IEnumerable<IUnit> Recalculate(IUnit sourceUnit) {

            using (UnitsManager unitsManager = UnitsManager.CreateInstance()) {

                unitsManager.UpdateUnits(sourceUnit);

                return unitsManager.Units;
            }
        }

        public static IUnit GetUnit(UnitID unitID) {

            using (var unitsManager = UnitsManager.CreateInstance()) {

                return unitsManager.Units.FirstOrDefault(p => p.ID == unitID);
            }
        }

        public static IList<IUnit> GetUnits(UnitType? unitType = null) {

            using (var unitsManager = UnitsManager.CreateInstance()) {

                if (unitType.HasValue) {
                    return unitsManager.Units.Where(p => p.Type == unitType).ToList();

                } else {
                    return unitsManager.Units.ToList();
                }
            }
        }

        public virtual IList<WebUnitGroup> WebUnitGroups {
            get {
                return _webUnitGroups;
            }
        }

        public virtual IList<WebUnit> WebUnits {
            get {
                return _webUnits;
            }
        }

        public virtual WebUnit GetWebUnit(UnitID unitID) {

            return WebUnits.FirstOrDefault(p => p.UnitID == unitID);
        }

        /// <summary>
        /// Returns True if the object is a number.
        /// (calls ToString() on the object and tries converting the string to a double)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Boolean IsNumber(Object obj) {
            bool result = false;

            try {
                double dbl;
                result = double.TryParse(obj.ToString(), out dbl);

            } catch {
                result = false;
            }

            return result;
        }


        protected IList<WebUnitGroup> CreateWebUnitGroups() {

            var webUnitGroups = new List<WebUnitGroup>();

            webUnitGroups.Add(new WebUnitGroup("Length", UnitType.Length) { Description = Properties.Resources.LengthDescription, Enabled = true });
            webUnitGroups.Add(new WebUnitGroup("Area", UnitType.Area) { Description = Properties.Resources.AreaDescription, Enabled = true });
            webUnitGroups.Add(new WebUnitGroup("Volume", UnitType.Volume) { Description = Properties.Resources.VolumeDescription, Enabled = true });
            webUnitGroups.Add(new WebUnitGroup("Temperature", UnitType.Temperature) { Description = Properties.Resources.TemperatureDescription, Enabled = true });
            webUnitGroups.Add(new WebUnitGroup("Mass", UnitType.Mass) { Description = Properties.Resources.MassDescription, Enabled = true });

            // these ones are in the dropdown
            webUnitGroups.Add(new WebUnitGroup("Speed", UnitType.Speed) { Description = Properties.Resources.SpeedDescription, Enabled = true, StartCollapsed = false });
            webUnitGroups.Add(new WebUnitGroup("Energy", UnitType.Energy) { Description = Properties.Resources.EnergyDescription, Enabled = true, StartCollapsed = false });
            webUnitGroups.Add(new WebUnitGroup("Power", UnitType.Power) { Description = Properties.Resources.PowerDescription, Enabled = true, StartCollapsed = false });
            webUnitGroups.Add(new WebUnitGroup("Pressure", UnitType.Pressure) { Description = Properties.Resources.PressureDescription, Enabled = true, StartCollapsed = false });
            webUnitGroups.Add(new WebUnitGroup("Time", UnitType.Time) { Description = Properties.Resources.TimeDescription, Enabled = true, StartCollapsed = false });
            webUnitGroups.Add(new WebUnitGroup("Fuel Economy", UnitType.FuelEconomy) { Description = Properties.Resources.FuelEconomyDescription, Enabled = true, StartCollapsed = false });
            webUnitGroups.Add(new WebUnitGroup("Frequency", UnitType.Frequency) { Description = Properties.Resources.FrequencyDescription, Enabled = true, StartCollapsed = false });
            webUnitGroups.Add(new WebUnitGroup("Acceleration", UnitType.Acceleration) { Description = Properties.Resources.AccelerationDescription, Enabled = true, StartCollapsed = false });
            webUnitGroups.Add(new WebUnitGroup("Density", UnitType.Density) { Description = Properties.Resources.DensityDescription, Enabled = true, StartCollapsed = false });
            webUnitGroups.Add(new WebUnitGroup("Angle", UnitType.Angle) { Description = Properties.Resources.AngleDescription, Enabled = true, StartCollapsed = false });
            webUnitGroups.Add(new WebUnitGroup("Digital Storage", UnitType.DigitalStorage) { Description = Properties.Resources.DigitalStorageDescription, Enabled = true, StartCollapsed = false });

            return webUnitGroups;
        }

        protected IList<WebUnit> CreateWebUnits() {

            var webUnits = new List<WebUnit>();


            // Length
            webUnits.Add(new WebUnit(UnitID.Meter) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Centimeter) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Millimeter) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Yard) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.Foot) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.Inch) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.Kilometer) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Mile) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.NauticalMile) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.Lightyear) { Enabled = true, IsMetric = true });

            // Area
            webUnits.Add(new WebUnit(UnitID.SquareMeter) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.SquareFoot) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.Acre) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.SquareKilometer) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Hectare) { Enabled = true, IsMetric = true });

            // Volume
            webUnits.Add(new WebUnit(UnitID.CubicMeter) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Liter) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Milliliter) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Gallon) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Quart) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Pint) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.CubicFoot) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.CubicInch) { Enabled = true, IsMetric = true });

            // Temperature
            webUnits.Add(new WebUnit(UnitID.Celsius) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.Fahrenheit) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.Kelvin) { Enabled = true, IsMetric = true });

            // Mass
            webUnits.Add(new WebUnit(UnitID.Kilogram) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Gram) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Stone) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Pound) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.Ounce) { Enabled = true, IsMetric = true });

            // Speed
            webUnits.Add(new WebUnit(UnitID.KilometerPerHour) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.MilePerHour) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.MeterPerSecond) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Knot) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.Mach) { Enabled = true, IsMetric = false });

            //Energy
            webUnits.Add(new WebUnit(UnitID.Joule) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Kilojoule) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Watthour) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Kilowatthour) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Calorie) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.Kilocalorie) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.FootPound) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.Megaelectronvolt) { Enabled = true, IsMetric = true });

            // Power
            webUnits.Add(new WebUnit(UnitID.Watt) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Kilowatt) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Megawatt) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Horsepower) { Enabled = true, IsMetric = false });

            // Pressure
            webUnits.Add(new WebUnit(UnitID.Pascal) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Bar) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Torr) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.Atmosphere) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.PoundForcePerSquareInch) { Enabled = true, IsMetric = false });

            // Time
            webUnits.Add(new WebUnit(UnitID.Second) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Minute) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Hour) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Day) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.Year) { Enabled = true, IsMetric = false });

            // Fuel Economy
            webUnits.Add(new WebUnit(UnitID.MilesPerGallon) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.LitersPer100Kilometers) { Enabled = true, IsMetric = true });

            // Frequency
            webUnits.Add(new WebUnit(UnitID.Hertz) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Kilohertz) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Megahertz) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Gigahertz) { Enabled = true, IsMetric = true });

            // Acceleration
            webUnits.Add(new WebUnit(UnitID.MeterPerSecondSquared) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.FootPerSecondSquared) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.GForce) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.Galileo) { Enabled = true, IsMetric = true });

            // Density
            webUnits.Add(new WebUnit(UnitID.KilogramPerCubicMeter) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.GramPerCubicCentimeter) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.PoundPerCubicFoot) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.PoundPerCubicInch) { Enabled = true, IsMetric = false });

            // Angle
            webUnits.Add(new WebUnit(UnitID.Degree) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Radian) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.Grad) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.MinuteOfArc) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(UnitID.SecondOfArc) { Enabled = true, IsMetric = true });

            // Digital Storage
            webUnits.Add(new WebUnit(UnitID.Bit) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.Kilobit) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.Megabit) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.Byte) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.Kilobyte) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.Megabyte) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.Gigabyte) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(UnitID.Terabyte) { Enabled = true, IsMetric = false });

            return webUnits;
        }
    }
}