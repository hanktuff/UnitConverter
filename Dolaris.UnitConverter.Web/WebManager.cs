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
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Meter)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Centimeter)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Millimeter)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Yard)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Foot)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Inch)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Kilometer)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Mile)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.NauticalMile)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Lightyear)) { Enabled = true, IsMetric = true });

            // Area
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.SquareMeter)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.SquareFoot)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Acre)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.SquareKilometer)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Hectare)) { Enabled = true, IsMetric = true });

            // Volume
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.CubicMeter)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Liter)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Milliliter)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.CC)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Gallon)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Quart)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Pint)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.CubicFoot)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.CubicInch)) { Enabled = true, IsMetric = true });

            // Temperature
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Celsius)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Fahrenheit)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Kelvin)) { Enabled = true, IsMetric = true });

            // Mass
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Kilogram)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Gram)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Stone)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Pound)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Ounce)) { Enabled = true, IsMetric = true });

            // Speed
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.KilometerPerHour)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.MilePerHour)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.MeterPerSecond)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Knot)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Mach)) { Enabled = true, IsMetric = false });

            //Energy
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Joule)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Kilojoule)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Watthour)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Kilowatthour)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Calorie)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Kilocalorie)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.FootPound)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Megaelectronvolt)) { Enabled = true, IsMetric = true });

            // Power
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Watt)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Kilowatt)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Megawatt)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Horsepower)) { Enabled = true, IsMetric = false });

            // Pressure
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Pascal)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Bar)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Torr)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Atmosphere)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.PoundForcePerSquareInch)) { Enabled = true, IsMetric = false });

            // Time
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Second)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Minute)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Hour)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Day)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Year)) { Enabled = true, IsMetric = false });

            // Fuel Economy
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.MilesPerGallon)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.LitersPer100Kilometers)) { Enabled = true, IsMetric = true });

            // Frequency
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Hertz)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Kilohertz)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Megahertz)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Gigahertz)) { Enabled = true, IsMetric = true });

            // Acceleration
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.MeterPerSecondSquared)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.FootPerSecondSquared)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.GForce)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Galileo)) { Enabled = true, IsMetric = true });

            // Density
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.KilogramPerCubicMeter)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.GramPerCubicCentimeter)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.PoundPerCubicFoot)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.PoundPerCubicInch)) { Enabled = true, IsMetric = false });

            // Angle
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Degree)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Radian)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Grad)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.MinuteOfArc)) { Enabled = true, IsMetric = true });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.SecondOfArc)) { Enabled = true, IsMetric = true });

            // Digital Storage
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Bit)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Kilobit)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Megabit)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Byte)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Kilobyte)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Megabyte)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Gigabyte)) { Enabled = true, IsMetric = false });
            webUnits.Add(new WebUnit(_unitsManager.GetUnit(UnitID.Terabyte)) { Enabled = true, IsMetric = false });

            return webUnits;
        }
    }
}