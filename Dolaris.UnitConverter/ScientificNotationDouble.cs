using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolaris.UnitConverter {

    public class ScientificNotationDouble {

        private double _originalValue = 0;


        public ScientificNotationDouble(double d = 0) {

            _originalValue = d;
            ToScientific(d);
        }

        public double Coefficient { get; set; }
        public int Exponent { get; set; }

        public double OriginalValue {
            get {
                return _originalValue;
            }
            set {
                _originalValue = value;
                ToScientific(value);
            }
        }

        public void ToScientific(double? d = null) {

            const string positiveExponent = "E+";
            const string negativeExponent = "E-";

            if (d == null) {
                d = _originalValue;
            }

            // sci will be something like "1.458E+001"
            string sci = d.Value.ToString("E18");

            if (sci.IndexOf(positiveExponent, StringComparison.CurrentCultureIgnoreCase) >= 0) {
                Coefficient = Convert.ToDouble(sci.Substring(0, sci.IndexOf(positiveExponent, StringComparison.CurrentCultureIgnoreCase)));
                Exponent = Convert.ToInt32(sci.Substring(sci.IndexOf(positiveExponent, StringComparison.CurrentCultureIgnoreCase) + 2));

            } else if (sci.IndexOf(negativeExponent, StringComparison.CurrentCultureIgnoreCase) >= 0) {
                Coefficient = Convert.ToDouble(sci.Substring(0, sci.IndexOf(negativeExponent, StringComparison.CurrentCultureIgnoreCase)));
                Exponent = -Convert.ToInt32(sci.Substring(sci.IndexOf(negativeExponent, StringComparison.CurrentCultureIgnoreCase) + 2));
            }
        }

        public double ToDouble() {
            return Coefficient * Math.Pow(10, Exponent);
        }

        public override string ToString() {
            
            if (Exponent < 0) {
                return string.Format("{0}e{1}", Coefficient, Exponent);
            }

            return string.Format("{0}e+{1}", Coefficient, Exponent);
        }
    }
}
