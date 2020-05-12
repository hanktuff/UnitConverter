using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolaris.UnitConverter {

    /// <summary>
    /// Represents the collection of all units of measurement.
    /// </summary>
    public class UnitCollection {

        /// <summary>
        /// Instantiates an object of type UnitCollection.
        /// </summary>
        /// <param name="units"></param>
        public UnitCollection(IEnumerable<IUnit> units) {
            Units = units;
        }

        public static UnitCollection CreateInstance(IEnumerable<IUnit> units = null) {

            if (units == null) {
                units = UnitCollection.CreateUnits();
            }

            return new UnitCollection(units);
        }


        /// <summary>
        /// Gets or sets a collection of units of measurement.
        /// </summary>
        public IEnumerable<IUnit> Units { get; set; }


        /// <summary>
        /// Returns a collection of units of measurement.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IUnit> CreateUnits() {

            var listOfUnits = new List<IUnit>();

            listOfUnits.Add(new Unit() { Name = "Meter", Symbol = "m", ID = UnitID.Meter, Type = UnitType.Length, Sloap = 1, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Centimeter", Symbol = "cm", ID = UnitID.Centimeter, Type = UnitType.Length, Sloap = 0.01, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Millimeter", Symbol = "mm", ID = UnitID.Millimeter, Type = UnitType.Length, Sloap = 0.001, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Kilometer", Symbol = "km", ID = UnitID.Kilometer, Type = UnitType.Length, Sloap = 1000, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Inch", Symbol = "in", ID = UnitID.Inch, Type = UnitType.Length, Sloap = 0.0254, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Foot", Symbol = "ft", ID = UnitID.Foot, Type = UnitType.Length, Sloap = 0.3048, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Yard", Symbol = "yd", ID = UnitID.Yard, Type = UnitType.Length, Sloap = 0.9144, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Mile", Symbol = "mi", ID = UnitID.Mile, Type = UnitType.Length, Sloap = 1609.344, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Nautical Mile", Symbol = "nmi", ID = UnitID.NauticalMile, Type = UnitType.Length, Sloap = 1852, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Lightyear", Symbol = "ly", ID = UnitID.Lightyear, Type = UnitType.Length, Sloap = 9460730472580800, Intercept = 0 });

            listOfUnits.Add(new Unit() { Name = "Square Meter", Symbol = "m2", ID = UnitID.SquareMeter, Type = UnitType.Area, Sloap = 1, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Square Foot", Symbol = "sqft", ID = UnitID.SquareFoot, Type = UnitType.Area, Sloap = 0.09290304, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Acre", Symbol = "ac", ID = UnitID.Acre, Type = UnitType.Area, Sloap = 4047, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Square Kilometer", Symbol = "km2", ID = UnitID.SquareKilometer, Type = UnitType.Area, Sloap = 1000000, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Hectare", Symbol = "ha", ID = UnitID.Hectare, Type = UnitType.Area, Sloap = 10000, Intercept = 0 });

            listOfUnits.Add(new Unit() { Name = "Celsius", Symbol = "°C", ID = UnitID.Celsius, Type = UnitType.Temperature, Sloap = 1, Intercept = 273.15, MinValue = -273.15 });
            listOfUnits.Add(new Unit() { Name = "Kelvin", Symbol = "K", ID = UnitID.Kelvin, Type = UnitType.Temperature, Sloap = 1, Intercept = 0, MinValue = 0 });
            listOfUnits.Add(new Unit() { Name = "Fahrenheit", Symbol = "°F", ID = UnitID.Fahrenheit, Type = UnitType.Temperature, Sloap = (float)5 / (float)9, Intercept = 255.372, MinValue = -459.66 });

            listOfUnits.Add(new Unit() { Name = "Kilogram", Symbol = "kg", ID = UnitID.Kilogram, Type = UnitType.Mass, Sloap = 1, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Gram", Symbol = "g", ID = UnitID.Gram, Type = UnitType.Mass, Sloap = 0.001, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Stone", Symbol = "st", ID = UnitID.Stone, Type = UnitType.Mass, Sloap = 6.35029318, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Pound", Symbol = "lb", ID = UnitID.Pound, Type = UnitType.Mass, Sloap = 0.453592, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Ounce", Symbol = "oz", ID = UnitID.Ounce, Type = UnitType.Mass, Sloap = 0.0283495, Intercept = 0 });

            listOfUnits.Add(new Unit() { Name = "Cubic Meter", Symbol = "m3", ID = UnitID.CubicMeter, Type = UnitType.Volume, Sloap = 1, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Liter", Symbol = "ltr", ID = UnitID.Liter, Type = UnitType.Volume, Sloap = 0.001, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Milliliter", Symbol = "ml", ID = UnitID.Milliliter, Type = UnitType.Volume, Sloap = 0.000001, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Cubic Centimeter", Symbol = "cc", ID = UnitID.CC, Type = UnitType.Volume, Sloap = 0.000001, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Gallon", Symbol = "gal", ID = UnitID.Gallon, Type = UnitType.Volume, Sloap = 0.00378541, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Quart", Symbol = "qt", ID = UnitID.Quart, Type = UnitType.Volume, Sloap = 0.000946353, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Pint", Symbol = "pt", ID = UnitID.Pint, Type = UnitType.Volume, Sloap = 0.000473176, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Cubic Foot", Symbol = "cu ft", ID = UnitID.CubicFoot, Type = UnitType.Volume, Sloap = 0.0283168, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Cubic Inch", Symbol = "cu in", ID = UnitID.CubicInch, Type = UnitType.Volume, Sloap = 0.0000016387, Intercept = 0 });

            listOfUnits.Add(new Unit() { Name = "Kilometer per hour", Symbol = @"km\h", ID = UnitID.KilometerPerHour, Type = UnitType.Speed, Sloap = 1, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Miles per hour", Symbol = "mph", ID = UnitID.MilePerHour, Type = UnitType.Speed, Sloap = 1.60934, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Meter per second", Symbol = @"m\s", ID = UnitID.MeterPerSecond, Type = UnitType.Speed, Sloap = 3.6, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Knot", Symbol = "kn", ID = UnitID.Knot, Type = UnitType.Speed, Sloap = 1.852, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Mach", Symbol = "Ma", ID = UnitID.Mach, Type = UnitType.Speed, Sloap = 1225, Intercept = 0 });

            listOfUnits.Add(new Unit() { Name = "Joule", Symbol = "J", ID = UnitID.Joule, Type = UnitType.Energy, Sloap = 1, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Kilojoule", Symbol = "kJ", ID = UnitID.Kilojoule, Type = UnitType.Energy, Sloap = 1000, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Watt Hour", Symbol = "Wh", ID = UnitID.Watthour, Type = UnitType.Energy, Sloap = 3600, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Kilowatt Hour", Symbol = "kWh", ID = UnitID.Kilowatthour, Type = UnitType.Energy, Sloap = 3600000, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Calorie", Symbol = "cal", ID = UnitID.Calorie, Type = UnitType.Energy, Sloap = 4.2, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Kilocalorie", Symbol = "kcal", ID = UnitID.Kilocalorie, Type = UnitType.Energy, Sloap = 4189.7, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Foot-Pound", Symbol = "ft lb", ID = UnitID.FootPound, Type = UnitType.Energy, Sloap = 1.3558179483314004, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Mega Electronvolt", Symbol = "MeV", ID = UnitID.Megaelectronvolt, Type = UnitType.Energy, Sloap = (double)1.6E-13, Intercept = 0 });

            listOfUnits.Add(new Unit() { Name = "Second", Symbol = "sec", ID = UnitID.Second, Type = UnitType.Time, Sloap = 1, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Minute", Symbol = "min", ID = UnitID.Minute, Type = UnitType.Time, Sloap = 60, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Hour", Symbol = "h", ID = UnitID.Hour, Type = UnitType.Time, Sloap = 60 * 60, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Day", Symbol = "day", ID = UnitID.Day, Type = UnitType.Time, Sloap = 24 * 60 * 60, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Year", Symbol = "year", ID = UnitID.Year, Type = UnitType.Time, Sloap = 365 * 24 * 60 * 60, Intercept = 0 });

            listOfUnits.Add(new Unit() { Name = "Watt", Symbol = "W", ID = UnitID.Watt, Type = UnitType.Power, Sloap = 1, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Kilowatt", Symbol = "kW", ID = UnitID.Kilowatt, Type = UnitType.Power, Sloap = 1000, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Megawatt", Symbol = "MW", ID = UnitID.Megawatt, Type = UnitType.Power, Sloap = 1000000, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Horsepower", Symbol = "hp", ID = UnitID.Horsepower, Type = UnitType.Power, Sloap = 735.5, Intercept = 0 });


            // Fuel Economy is not a liniear conversion
            listOfUnits.Add(new Unit() { Name = "Miles per gallon", Symbol = "mpg", ID = UnitID.MilesPerGallon, Type = UnitType.FuelEconomy });

            var literPer100Km = new Unit() { Name = "Liter per 100 Kilometer", Symbol = "L/100km", ID = UnitID.LitersPer100Kilometers, Type = UnitType.FuelEconomy };

            literPer100Km.ConvertToBaseUnit = () => { return 235.215 / literPer100Km.Magnitude.Value; };
            literPer100Km.ConvertFromBaseUnit = (double input) => { return 235.215 / input; };

            listOfUnits.Add(literPer100Km);


            listOfUnits.Add(new Unit() { Name = "Pascal", Symbol = "Pa", ID = UnitID.Pascal, Type = UnitType.Pressure, Sloap = 1, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Bar", Symbol = "bar", ID = UnitID.Bar, Type = UnitType.Pressure, Sloap = 100000, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Torr", Symbol = "Torr", ID = UnitID.Torr, Type = UnitType.Pressure, Sloap = 133.322, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Atmosphere", Symbol = "atm", ID = UnitID.Atmosphere, Type = UnitType.Pressure, Sloap = 101325, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Pound-Force per Square Inch", Symbol = "psi", ID = UnitID.PoundForcePerSquareInch, Type = UnitType.Pressure, Sloap = 6894.76, Intercept = 0 });

            listOfUnits.Add(new Unit() { Name = "Hertz", Symbol = "Hz", ID = UnitID.Hertz, Type = UnitType.Frequency, Sloap = 1, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Kilohertz", Symbol = "kHz", ID = UnitID.Kilohertz, Type = UnitType.Frequency, Sloap = 1000, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Megahertz", Symbol = "MHz", ID = UnitID.Megahertz, Type = UnitType.Frequency, Sloap = 1000000, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Gigahertz", Symbol = "GHz", ID = UnitID.Gigahertz, Type = UnitType.Frequency, Sloap = 1000000000, Intercept = 0 });

            listOfUnits.Add(new Unit() { Name = "Meter per Second squared", Symbol = "m/s2", ID = UnitID.MeterPerSecondSquared, Type = UnitType.Acceleration, Sloap = 1, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Foot per Second squared", Symbol = "ft/s2", ID = UnitID.FootPerSecondSquared, Type = UnitType.Acceleration, Sloap = 0.3047, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "G-Force", Symbol = "g", ID = UnitID.GForce, Type = UnitType.Acceleration, Sloap = 9.81, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Galileo", Symbol = "Gal", ID = UnitID.Galileo, Type = UnitType.Acceleration, Sloap = 0.01, Intercept = 0 });

            listOfUnits.Add(new Unit() { Name = "Kilogram per cubic meter", Symbol = "kg/m3", ID = UnitID.KilogramPerCubicMeter, Type = UnitType.Density, Sloap = 1, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Gram per cubic centimeter", Symbol = "g/cm3", ID = UnitID.GramPerCubicCentimeter, Type = UnitType.Density, Sloap = 1000, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Pounds per cubic foot", Symbol = "lb/ft3", ID = UnitID.PoundPerCubicFoot, Type = UnitType.Density, Sloap = 16.01846, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Pounds per cubic inch", Symbol = "lb/in3", ID = UnitID.PoundPerCubicInch, Type = UnitType.Density, Sloap = 27679.91, Intercept = 0 });

            listOfUnits.Add(new Unit() { Name = "Degree", Symbol = "°", ID = UnitID.Degree, Type = UnitType.Angle, Sloap = 1, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Radian", Symbol = "rad", ID = UnitID.Radian, Type = UnitType.Angle, Sloap = 360d / (2d * Math.PI), Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Grad", Symbol = "gon", ID = UnitID.Grad, Type = UnitType.Angle, Sloap = 360d / 400d, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Minute of arc", Symbol = "'", ID = UnitID.MinuteOfArc, Type = UnitType.Angle, Sloap = 1d / 60d, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Second of Arc", Symbol = "''", ID = UnitID.SecondOfArc, Type = UnitType.Angle, Sloap = 1d / 60d / 60d, Intercept = 0 });

            listOfUnits.Add(new Unit() { Name = "Bit", Symbol = "bit", ID = UnitID.Bit, Type = UnitType.DigitalStorage, Sloap = 1, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Kilobit", Symbol = "kbit", ID = UnitID.Kilobit, Type = UnitType.DigitalStorage, Sloap = 1024, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Megabit", Symbol = "Mbit", ID = UnitID.Megabit, Type = UnitType.DigitalStorage, Sloap = 1024 * 1024, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Byte", Symbol = "B", ID = UnitID.Byte, Type = UnitType.DigitalStorage, Sloap = 8, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Kilobyte", Symbol = "kB", ID = UnitID.Kilobyte, Type = UnitType.DigitalStorage, Sloap = 1024 * 8, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Megabyte", Symbol = "MB", ID = UnitID.Megabyte, Type = UnitType.DigitalStorage, Sloap = 1024 * 1024 * 8, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Gigabyte", Symbol = "GB", ID = UnitID.Gigabyte, Type = UnitType.DigitalStorage, Sloap = 8589934592, Intercept = 0 });
            listOfUnits.Add(new Unit() { Name = "Terabyte", Symbol = "TB", ID = UnitID.Terabyte, Type = UnitType.DigitalStorage, Sloap = 8796093022208, Intercept = 0 });

            return listOfUnits;
        }
    }
}
