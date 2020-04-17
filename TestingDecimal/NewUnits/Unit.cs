using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingDecimal.NewUnits {


    enum UnitsOfMeasurement {
        Length,
        Area,
        Temperature
    }

    interface IUnit {
        Double? Magnitude { get; set; }
        Double MaxValue { get; set; }
        Double MinValue { get; set; }

        String Name { get; set; }
        String Symbol { get; set; }

        UnitsOfMeasurement UnitOfMeasurement { get; set; }

        Double Sloap { get; set; }
        Double Intercept { get; set; }

        Func<Double> ConvertToBaseUnit { get; set; }
        Func<Double, Double> ConvertFromBaseUnit { get; set; }
    }

    class Unit : IUnit, IEquatable<Unit> {

        public Unit() {
            Magnitude = 0;
            MaxValue = Double.MaxValue;
            MinValue = Double.MinValue;

            Sloap = 1;
            Intercept = 0;

            // y = m * x + b
            // x = (y - b) / m

            //ConvertFromBaseUnit = (baseUnitValue) => { return (baseUnitValue - Intercept) / Sloap; };
            //ConvertToBaseUnit = () => { return Magnitude.Value * Sloap + Intercept; };

            ConvertToBaseUnit = Linear;
            ConvertFromBaseUnit = InverseLinear;
        }

        public virtual double? Magnitude { get; set; }
        public virtual double MaxValue { get; set; }
        public virtual double MinValue { get; set; }

        public virtual Func<double, double> ConvertFromBaseUnit { get; set; }
        public virtual Func<double> ConvertToBaseUnit { get; set; }

        public double Sloap { get; set; }
        public double Intercept { get; set; }

        public string Name { get; set; }
        public string Symbol { get; set; }

        public UnitsOfMeasurement UnitOfMeasurement { get; set; }

        public bool Equals(Unit other) {

            if (this.Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase) && this.UnitOfMeasurement == other.UnitOfMeasurement) {
                return true;

            }

            return false;
        }

        public Double Linear() {
            return Magnitude.Value * Sloap + Intercept;
        }

        public Double InverseLinear(Double value) {
            return (value - Intercept) / Sloap;
        }
    }

    class UnitValue {

        protected double _value;

        public UnitValue(Double value) {
            Value = value;
        }

        public Double Value {
            get {
                return _value;
            }
            set {

                try {
                    _value = value;
                    this.State = ValueState.Valid;

                } catch (OverflowException) {
                    State = ValueState.Overflow;
                }
            }
        }

        public Boolean IsValid {
            get {
                return this.State == ValueState.Valid;
            }
        }

        public ValueState State { get; protected set; }

        public enum ValueState {
            Overflow,
            Valid,
            Underflow
        }
    }

    class UnitCollection {

        public UnitCollection() {
            Units = new List<Unit>();
            _populateUnits();
        }

        public IEnumerable<IUnit> Units { get; set; }


        private void _populateUnits() {
            var units = new List<Unit>();

            units.Add(new Unit() { Name = "Meter", Symbol = "m", UnitOfMeasurement = UnitsOfMeasurement.Length, Sloap = 1, Intercept = 0 });
            units.Add(new Unit() { Name = "Inch", Symbol = "in", UnitOfMeasurement = UnitsOfMeasurement.Length, Sloap = 0.0254, Intercept = 0 });
            units.Add(new Unit() { Name = "Foot", Symbol = "ft", UnitOfMeasurement = UnitsOfMeasurement.Length, Sloap = 0.3048, Intercept = 0 });
            units.Add(new Unit() { Name = "Yard", Symbol = "yd", UnitOfMeasurement = UnitsOfMeasurement.Length, Sloap = 0.9144, Intercept = 0 });

            this.Units = units;
        }
    }

    class UnitCollection<T> where T: IUnit, new() {

        public UnitCollection() {
            Units = new List<T>();
        }

        public IEnumerable<T> Units { get; set; }

        private void _populateUnits() {
            var units = new List<T>();

            units.Add(new T() { Name = "Meter", Symbol = "m", UnitOfMeasurement = UnitsOfMeasurement.Length, Sloap = 1, Intercept = 0 });
            units.Add(new T() { Name = "Inch", Symbol = "in", UnitOfMeasurement = UnitsOfMeasurement.Length, Sloap = 0.0254, Intercept = 0 });
            units.Add(new T() { Name = "Foot", Symbol = "ft", UnitOfMeasurement = UnitsOfMeasurement.Length, Sloap = 0.3048, Intercept = 0 });
            units.Add(new T() { Name = "Yard", Symbol = "yd", UnitOfMeasurement = UnitsOfMeasurement.Length, Sloap = 0.9144, Intercept = 0 });

            this.Units = units;
        }
    }

    class UnitManager {

        private UnitCollection _unitCollection = new UnitCollection();

        public UnitManager() {
        }

        public IEnumerable<IUnit> Units {
            get {
                return _unitCollection.Units;
            }
        }

        public void UpdateUnits(IUnit sourceUnit) {
            double baseUnitValue;
            IEnumerable<IUnit> unitsOfSameMeasurement = Units.Where(p => p.UnitOfMeasurement == sourceUnit.UnitOfMeasurement);

            try {
                baseUnitValue = sourceUnit.ConvertToBaseUnit();

            } catch {

                // if converting to base unit is not possible, no conversion is possible, so invalidate all units

                foreach (var unit in unitsOfSameMeasurement) {
                    unit.Magnitude = null;
                }

                return;
            }


            foreach (var unit in unitsOfSameMeasurement) {

                if (unit != sourceUnit) {

                    try {
                        unit.Magnitude = unit.ConvertFromBaseUnit(baseUnitValue);

                    } catch {
                        unit.Magnitude = null;
                    }
                }
            }
        }
    }



    class ScientificNotation {

        public ScientificNotation(double d = 0) {
            ToScientific(d);
        }

        public double Coefficient { get; set; }
        public int Exponent { get; set; }

        public void ToScientific(double d) {

            const string positiveExponent = "E+";
            const string negativeExponent = "E-";

            // sci will be something like "1.458E+001"
            string sci = d.ToString("E18");

            if (sci.IndexOf(positiveExponent, StringComparison.InvariantCultureIgnoreCase) >= 0) {
                Coefficient = Convert.ToDouble(sci.Substring(0, sci.IndexOf(positiveExponent, StringComparison.InvariantCultureIgnoreCase)));
                Exponent = Convert.ToInt32(sci.Substring(sci.IndexOf(positiveExponent, StringComparison.InvariantCultureIgnoreCase) + 2));

            } else if (sci.IndexOf(negativeExponent, StringComparison.InvariantCultureIgnoreCase) >= 0) {
                Coefficient = Convert.ToDouble(sci.Substring(0, sci.IndexOf(negativeExponent, StringComparison.InvariantCultureIgnoreCase)));
                Exponent = -Convert.ToInt32(sci.Substring(sci.IndexOf(negativeExponent, StringComparison.InvariantCultureIgnoreCase) + 2));
            }
        }

        public double ToDouble() {
            return Coefficient * Math.Pow(10, Exponent);
        }
    }


    //=================================================================
    // EXTENSIONS

    static class UnitExtensions {

        public static String Plural(this IUnit unit) {

            // hand exception
            if (unit.Name.Equals("Foot", StringComparison.InvariantCultureIgnoreCase)) { return "Feet"; }
            if (unit.Name.Equals("Inch", StringComparison.InvariantCultureIgnoreCase)) { return "Inches"; }

            return unit.Name + "s";
        }

        public static Double RoundedValue(this IUnit unit, int decimals) {
            return Math.Round(unit.Magnitude.Value, decimals);
        }

        public static ScientificNotation ToScientificNotation(this IUnit unit, int roundToDecimals = 0) {
            ScientificNotation result = null;

            if (unit.Magnitude.HasValue) {

                result = new ScientificNotation(unit.Magnitude.Value);

                if (roundToDecimals > 0) {
                    result.Coefficient = Math.Round(result.Coefficient, roundToDecimals);
                }
            }

            return result;
        }

        public static int DecimalPlaces(this IUnit unit) {

            string number = unit.Magnitude.ToString();

            var decimalPointPos = number.IndexOf('.');

            if (decimalPointPos >= 0) {
                // -1 because: do not count decimal point
                return number.Substring(decimalPointPos).Length - 1;
            }

            return 0;
        }

        public static int PlacesBeforeDecimalPoint(this IUnit unit) {

            string number = unit.Magnitude.ToString();

            var decimalPointPos = number.IndexOf('.');

            if (decimalPointPos >= 0) {

                var x = number.Substring(0, decimalPointPos);

                // a 0 before the decimal point doesn't count as a place (0.001m has zero places before decimal point)
                if (x == "0") {
                    return 0;
                }

                return x.Length;
            }

            return number.Length;
        }
    }
}
