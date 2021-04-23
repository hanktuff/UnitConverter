using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolaris.UnitConverter {
    public class UnitConverterUtility {

        public static Boolean TryParseUnits(string s, out IEnumerable<IUnit> units) {

            units = new List<IUnit>();

            if (string.IsNullOrWhiteSpace(s)) {
                return false;
            }

            string unitDesignation;
            var number = UnitConverterUtility.ParseDouble(s, out unitDesignation);

            if (number.HasValue == false) {
                return false;
            }

            units = UnitConverterUtility.GetMatchingUnits(unitDesignation);

            if (units == null || units.Count() == 0) {
                return false;
            }

            foreach (var unit in units) {
                unit.Magnitude = number.Value;
            }

            return true;
        }

        /// <summary>
        /// Parses a string and returns the double the string starts with.
        /// Returns Null if the string does not start with a double.
        /// For example: " -12.345E+006meters  " returns -12.345E+006.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Double? ParseDouble(string s, out string remainder) {

            remainder = string.Empty;

            if (string.IsNullOrWhiteSpace(s)) {
                return null;
            }

            double result = 0;
            bool doubleFound = false;
            int foundAt = 0;
            //string s2 = string.Copy(s);

            string s2 = string.Empty;
            foreach (char ch in s.ToCharArray()) {
                s2 += ch;
            }

            while (!string.IsNullOrEmpty(s2)) {

                doubleFound = double.TryParse(s2, out result);

                if (doubleFound) {
                    break;
                }

                // take away the last character and check again
                s2 = s2.Substring(0, s2.Length - 1);
                foundAt = s2.Length;
            }

            if (foundAt > 0) {
                remainder = s.Substring(foundAt);
            }

            return doubleFound ? result : (double?)null;
        }

        /// <summary>
        /// Returns a collection of units that match the string.
        /// For example: "Meter" returns the unit whose name is Meter.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static IEnumerable<IUnit> GetMatchingUnits(string s) {

            var units = UnitCollection.CreateUnits();
            s = s.Trim();

            // check if the unit name matches exactly

            var nameMatchesExactly = units.Where(p => p.ID.ToString().Equals(s, StringComparison.CurrentCultureIgnoreCase)
                                                   || p.Name.Equals(s, StringComparison.CurrentCultureIgnoreCase)
                                                   || p.GetPlural().Equals(s, StringComparison.CurrentCultureIgnoreCase));

            if (nameMatchesExactly.Any()) {
                return nameMatchesExactly;
            }

            // check if the unit symbol matches exactly

            var symbolMatchesExactly = units.Where(p => p.Symbol.TrimStart('°').Equals(s, StringComparison.CurrentCultureIgnoreCase));

            if (symbolMatchesExactly.Any()) {
                return symbolMatchesExactly;
            }

            // check if there are unit names that start with the string

            var namesStartWith = units.Where(p => p.Name.StartsWith(s, StringComparison.CurrentCultureIgnoreCase));

            if (namesStartWith.Any()) {
                return namesStartWith;
            }

            return null;
        }


        public static List<UnitString.Element> SplitIntoParts(string s) {

            var unitString = new UnitString();

            return unitString.Split(s).ToList();
        }
    }
}
