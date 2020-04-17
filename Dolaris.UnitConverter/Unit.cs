using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Dolaris.UnitConverter {

    [DebuggerDisplay("{Name}:  {Magnitude.Value} {Symbol}   ({Type})")]
    public class Unit : IUnit, IEquatable<Unit> {

        public Unit() {

            Magnitude = 0;
            MaxValue = double.MaxValue;
            MinValue = 0;

            Sloap = 1;
            Intercept = 0;

            ConvertFromBaseUnit = InverseLinear;
            ConvertToBaseUnit = Linear;
        }


        public UnitID ID { get; set; }

        public virtual double? Magnitude { get; set; }
        public virtual double MaxValue { get; set; }
        public virtual double MinValue { get; set; }

        public string Name { get; set; }
        public string Symbol { get; set; }

        public UnitType Type { get; set; }

        public double Sloap { get; set; }
        public double Intercept { get; set; }

        public virtual Func<double, double> ConvertFromBaseUnit { get; set; }
        public virtual Func<double> ConvertToBaseUnit { get; set; }

        public bool IsBaseUnit {
            get {
                if (Sloap == 1 && Intercept == 0) {
                    return true;

                } else {
                    return false;
                }
            }
        }


        /// <summary>
        /// Determins if this instance of Unit is equal to the other instance of Unit.
        /// Returns True if the instances are considered to be equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Unit other) {

            // to instances of Unit are equal if their IDs are equal

            return this.ID == other.ID;
        }


        public Double Linear() {
            return Magnitude.Value * Sloap + Intercept;
        }

        public Double InverseLinear(Double value) {
            return (value - Intercept) / Sloap;
        }
    }
}
