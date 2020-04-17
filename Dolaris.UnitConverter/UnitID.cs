using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolaris.UnitConverter {

    public enum UnitID {

        // length
        Meter,
        Centimeter,
        Millimeter,
        Kilometer,
        Inch,
        Foot,
        Yard,
        Mile,
        NauticalMile,
        Lightyear,

        // area
        SquareMeter,
        SquareFoot,
        Acre,
        SquareKilometer,
        Hectare,

        // temperature
        Celsius,
        Kelvin,
        Fahrenheit,

        // mass
        Kilogram,
        Gram,
        Stone,
        Pound,
        Ounce,

        // volume
        CubicMeter,
        Liter,
        Milliliter,
        Gallon,
        Quart,
        Pint,
        CubicFoot,
        CubicInch,

        // speed
        KilometerPerHour,
        MilePerHour,
        MeterPerSecond,
        Knot,
        Mach,

        // energy
        Joule,
        Kilojoule,
        Watthour,
        Kilowatthour,
        Calorie,
        Kilocalorie,
        Megaelectronvolt,
        FootPound,

        // time
        Second,
        Minute,
        Hour,
        Day,
        Year,

        // power
        Watt,
        Kilowatt,
        Megawatt,
        Horsepower,

        // fuel economy
        MilesPerGallon,
        LitersPer100Kilometers,

        // pressure
        Pascal,
        Bar,
        Torr,
        Atmosphere,
        PoundForcePerSquareInch,

        // frequency
        Hertz,
        Kilohertz,
        Megahertz,
        Gigahertz,

        // Acceleration
        MeterPerSecondSquared,
        FootPerSecondSquared,
        GForce,
        Galileo,

        // Density
        KilogramPerCubicMeter,
        GramPerCubicCentimeter,
        PoundPerCubicFoot,
        PoundPerCubicInch,

        // Angle
        Degree,
        Radian,
        Grad,
        MinuteOfArc,
        SecondOfArc,

        // Digital Storage
        Bit,
        Kilobit,
        Megabit,
        Byte,
        Kilobyte,
        Megabyte,
        Gigabyte,
        Terabyte
    }
}
