using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolaris.UnitConverter {

    public static class UnitExtensions {

        public static String GetPlural(this IUnit unit) {

            // the standard way of creating the plurar in English is to add an "s" at the end
            // but these ones are exceptions

            if (unit.ID == UnitID.Foot) { return "Feet"; }
            if (unit.ID == UnitID.SquareFoot) { return "Square Feet"; }
            if (unit.ID == UnitID.CubicFoot) { return "Cubic Feet"; }
            if (unit.ID == UnitID.Inch) { return "Inches"; }
            if (unit.ID == UnitID.CubicInch) { return "Cubic Inches"; }
            if (unit.ID == UnitID.KilometerPerHour) { return "Kilometers per hour"; }
            if (unit.ID == UnitID.MeterPerSecond) { return "Meters per second"; }
            if (unit.ID == UnitID.MilePerHour) { return "Miles per hour"; }
            if (unit.ID == UnitID.Stone) { return "Stone"; }
            if (unit.ID == UnitID.Celsius) { return "Degrees Celsius"; }
            if (unit.ID == UnitID.Fahrenheit) { return "Degrees Fahrenheit"; }
            if (unit.ID == UnitID.Kelvin) { return "Degrees Kelvin"; }
            if (unit.ID == UnitID.Mach) { return "Mach"; }
            if (unit.ID == UnitID.MeterPerSecondSquared) { return "Meters per second squared"; }
            if (unit.ID == UnitID.FootPerSecondSquared) { return "Feet per second squared"; }
            if (unit.ID == UnitID.KilogramPerCubicMeter) { return "Kilograms per cubic meter"; }
            if (unit.ID == UnitID.GramPerCubicCentimeter) { return "Grams per cubic centimeter"; }
            if (unit.ID == UnitID.PoundPerCubicFoot) { return "Pounds per cubic foot"; }
            if (unit.ID == UnitID.PoundPerCubicInch) { return "Pounds per cubic Inch"; }
            if (unit.ID == UnitID.MinuteOfArc) { return "Minutes of Arc"; }
            if (unit.ID == UnitID.SecondOfArc) { return "Seconds of Arc"; }

            return unit.Name + "s";
        }

        public static Double RoundedValue(this IUnit unit, int decimals) {

            return Math.Round(unit.Magnitude.Value, decimals.LimitTo(0, 15));
        }

        public static ScientificNotationDouble ToScientificNotation(this IUnit unit, int roundToDecimals = 0) {
            ScientificNotationDouble result = null;

            if (unit.Magnitude.HasValue) {

                result = new ScientificNotationDouble(unit.Magnitude.Value);

                if (roundToDecimals > 0) {
                    result.Coefficient = Math.Round(result.Coefficient, roundToDecimals.LimitTo(0, 15));
                }
            }

            return result;
        }

        public static String ToString(this IUnit unit, string defaultIfNoValue = "") {

            if (unit.Magnitude.HasValue) {
                return unit.Magnitude.ToString();

            } else {
                return defaultIfNoValue;
            }
        }

        /// <summary>
        /// Returns the number of places after the decimal point.
        /// For example: 123.45678 return 5.
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static int GetDecimalPlaces(this IUnit unit) {

            string number = unit.Magnitude.Value.ToString("F99").Trim('0');

            var decimalPointPos = number.IndexOf('.');

            if (decimalPointPos >= 0) {
                // -1 because: do not count decimal point
                return number.Substring(decimalPointPos).Length - 1;
            }

            return 0;
        }

        /// <summary>
        /// Returns the number of places before the decimal point.
        /// For example: 123.45678 returns 3.
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static int GetPlacesBeforeDecimalPoint(this IUnit unit) {

            // if there is no value, simply return 0

            if (unit.Magnitude.HasValue == false) {
                return 0;
            }

            //string number = unit.Magnitude.ToString();

            // the "F" paramenter creates a string that doesn't use exponential notation
            string number = ((double)unit.Magnitude.Value).ToString("F");

            //((double)(1E+308)).ToString("F");


            var decimalPointPos = number.IndexOf('.');

            if (decimalPointPos >= 0) {

                var beforeDecimalPoint = number.Substring(0, decimalPointPos).TrimStart('-');

                // a 0 before the decimal point doesn't count as a place (0.001m has zero places before decimal point)
                if (beforeDecimalPoint == "0") {
                    return 0;
                }

                return beforeDecimalPoint.Length;
            }

            return number.Length;
        }

        public static string GetFormattedString(this IUnit unit, out string errorString, int toBase = 10) {

            string result = string.Empty;
            errorString = string.Empty;

            if (unit.Magnitude.HasValue) {

                if (double.IsNaN(unit.Magnitude.Value)) {
                    errorString = "Please enter a number.";

                } else if (double.IsPositiveInfinity(unit.Magnitude.Value)) {
                    errorString = "The number is too large.";

                } else if (double.IsNegativeInfinity(unit.Magnitude.Value)) {
                    errorString = "The number is too small.";

                } else if (unit.Magnitude.Value > unit.MaxValue) {
                    errorString = string.Format("{0} is the largest number allowed.", unit.MaxValue);

                } else if (unit.Magnitude.Value < unit.MinValue) {
                    errorString = string.Format("{0} is the smallest number allowed.", unit.MinValue);
                }

                if (string.IsNullOrEmpty(errorString)) {

                    result = unit.Magnitude.Value.ToString("F" + unit.GetDecimalPlaces().ToString());

                    if (toBase != 10) {
                        result = Convert.ToString((int)unit.Magnitude.Value, toBase);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Limits an interger to stay between the lower limit and the upper limit.
        /// </summary>
        /// <param name="i">The integer</param>
        /// <param name="lowerLimit">Lower limit</param>
        /// <param name="upperLimit">Upper limit</param>
        /// <returns></returns>
        public static int LimitTo(this int i, int lowerLimit = int.MinValue, int upperLimit = int.MaxValue) {

            if (lowerLimit > upperLimit) {
                throw new Exception("The lower limit cannot be greater than the upper limit.");
            }

            if (i < lowerLimit) {
                return lowerLimit;

            } else if (i > upperLimit) {
                return upperLimit;
            }

            return i;
        }

        /// <summary>
        /// Returns a UnitID enum from a string.
        /// Returns Null if UnitID cannot be determined.
        /// </summary>
        /// <param name="unitIDString"></param>
        /// <returns></returns>
        public static UnitID? GetUnitID(this String unitIDString) {

            UnitID result;
            bool success = Enum.TryParse<UnitID>(unitIDString, out result);

            return success ? result : (UnitID?)null;
        }

        /// <summary>
        /// Returns a UnitType enum from a string.
        /// Returns Null if UnitType cannot be determined.
        /// </summary>
        /// <param name="unitTypeString"></param>
        /// <returns></returns>
        public static UnitType? GetUnitType(this String unitTypeString) {

            UnitType result;
            bool success = Enum.TryParse<UnitType>(unitTypeString, out result);

            return success ? result : (UnitType?)null;
        }
    }
}
