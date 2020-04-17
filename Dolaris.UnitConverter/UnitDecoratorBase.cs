using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolaris.UnitConverter {

    public abstract class UnitDecoratorBase : IUnit {

        protected IUnit _unit;


        public UnitDecoratorBase(IUnit unit) {
            _unit = unit;
        }


        public UnitID ID {
            get {
                return _unit.ID;
            }
            set {
                _unit.ID = value;
            }
        }


        public double? Magnitude {
            get {
                return _unit.Magnitude;
            }

            set {
                _unit.Magnitude = value;
            }
        }

        public double MaxValue {
            get {
                return _unit.MaxValue;
            }

            set {
                _unit.MaxValue = value;
            }
        }

        public double MinValue {
            get {
                return _unit.MinValue;
            }

            set {
                _unit.MinValue = value;
            }
        }


        public string Name {
            get {
                return _unit.Name;
            }

            set {
                _unit.Name = value;
            }
        }

        public string Symbol {
            get {
                return _unit.Symbol;
            }

            set {
                _unit.Symbol = value;
            }
        }


        public UnitType Type {
            get {
                return _unit.Type;
            }

            set {
                _unit.Type = value;
            }
        }


        public double Sloap {
            get {
                return _unit.Sloap;
            }

            set {
                _unit.Sloap = value;
            }
        }

        public double Intercept {
            get {
                return _unit.Intercept;
            }

            set {
                _unit.Intercept = value;
            }
        }


        public Func<double> ConvertToBaseUnit {
            get {
                return _unit.ConvertToBaseUnit;
            }

            set {
                _unit.ConvertToBaseUnit = value;
            }
        }

        public Func<double, double> ConvertFromBaseUnit {
            get {
                return _unit.ConvertFromBaseUnit;
            }

            set {
                _unit.ConvertFromBaseUnit = value;
            }
        }
    }
}
