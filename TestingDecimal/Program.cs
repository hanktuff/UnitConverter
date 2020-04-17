using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingDecimal.NewUnits;

namespace TestingDecimal {

    // Testing the Decimal type

    class Program {
        static void Main(string[] args) {

            //Decimal d = 3.0000M;

            //Console.WriteLine(Decimal.Divide(Decimal.MaxValue, 19M));
            //Console.WriteLine(Decimal.Divide((decimal)277, (decimal)19).ToString("E3"));
            //Console.WriteLine("Rounding:   " + Decimal.Round((decimal)0.123456789, decimals: 5));

            ////decimal.TryParse("678E+002", out d);
            //Console.WriteLine("d=" + d);

            //var sci = new ScientificDecimal();
            //sci = 12345.67890987654321;
            //Console.WriteLine(sci.Value);
            //Console.WriteLine(sci.DecimalValue);
            //float f = (float)sci.Coefficient;
            //sci.Round(4);
            //Console.WriteLine(sci.Value);
            //Console.WriteLine(sci.DecimalValue);

            //Console.WriteLine("MaxValue: " + Decimal.MaxValue);
            //Console.WriteLine("MinValue: " + Decimal.MinValue);


            //Console.WriteLine("=================================================================");

            //var rxi = new Rounding(new ExtraInfo(new Unit()));            

            UnitManager mgr = new UnitManager();
            //UnitManager<Rounding> mgr = new UnitManager<Rounding>();


            double ddd;
            var su = double.TryParse("3.4m", out ddd);




            while (true) {

                Console.WriteLine("Enter value and unit (for example 3.4m):");
                string inputstr = Console.ReadLine();
                inputstr = inputstr.Trim();

                if (string.IsNullOrEmpty(inputstr)) { return; }

                double number;
                string symbol;
                bool success = TryParseUnit(inputstr, out number, out symbol);

                IUnit srcUnit = mgr.Units.FirstOrDefault(p => p.Symbol.Equals(symbol, StringComparison.InvariantCultureIgnoreCase));

                if (srcUnit == null) {
                    Console.WriteLine(string.Format("{0} was not recognized as a valid input. Press enter to quit.", inputstr));
                    Console.WriteLine();
                    Console.WriteLine();
                    continue;
                }

                srcUnit.Magnitude = number;

                Console.WriteLine("places before decimal point: " + srcUnit.PlacesBeforeDecimalPoint());
                Console.WriteLine("places after decimal point: " + srcUnit.DecimalPlaces());

                //IUnit foot = mgr.Units.FirstOrDefault(p => p.Name == "Foot");
                ////foot.Magnitude = 10;
                //foot.Magnitude = 10.123456789;
                //foot.Magnitude = 0.00123456789;

                //try {
                //    foot.Magnitude = Convert.ToDouble("20e+4");

                //} catch {
                //    Console.Read();
                //}

                //foot.Magnitude = 10000000000000000000000000000;
                //foot.Magnitude = 79228162514264337593543950335;

                //mgr.UpdateUnits(foot);
                mgr.UpdateUnits(srcUnit);

                Console.WriteLine();

                foreach (var unit in mgr.Units) {

                    if (unit.Magnitude.HasValue) {
                        Console.WriteLine("{0}:\t{1}{2}", unit.Plural(), unit.Magnitude, unit.Symbol);
                        //Console.WriteLine("{0}:\t{1}{2} (rounded)", unit.Plural(), unit.RoundedValue(2), unit.Symbol);

                        var roundToDecimalPlaces = srcUnit.DecimalPlaces() + 4;
                        if (roundToDecimalPlaces < 0) { roundToDecimalPlaces = 0; }
                        if (roundToDecimalPlaces > 15) { roundToDecimalPlaces = 15; }

                        var scientificNotation = unit.ToScientificNotation(roundToDecimalPlaces);
                        Console.WriteLine("{0}:\t{1}e{2} {3}", unit.Plural(), scientificNotation.Coefficient, scientificNotation.Exponent, unit.Symbol);

                        Console.WriteLine("{0}:\t{1}{2}", unit.Plural(), scientificNotation.ToDouble(), unit.Symbol);

                    } else {
                        Console.WriteLine("{0}:\t(no value)", unit.Name);
                    }

                    Console.WriteLine();
                } 
            }

            //Console.WriteLine("................................................");
            //var r = new Rounding(mgr.Units.First());

            //r.Value = 1234.56789M;
            //r.Round(2);
            //Console.WriteLine(r.Value);
            //Console.WriteLine(r.RoundedValue);

            Console.Read();
        }

        static bool TryParseUnit(string s, out double number, out string symbol) {
            bool result = false;

            number = -1;
            symbol = null;
            bool numberFound = false;
            int numberFoundAt = -1;

            for (int i = s.Length; i >= 0; i--) {

                string substring = s.Substring(0, i);

                numberFound = double.TryParse(substring, out number);

                if (numberFound) {
                    numberFoundAt = i;
                    break;
                }
            }

            if (numberFound) {
                symbol = s.Substring(numberFoundAt).Trim();

                if (string.IsNullOrEmpty(symbol) == false) {
                    result = true;
                }
            }

            return result;
        }
    }
}
