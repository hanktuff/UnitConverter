using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolaris.UnitConverter {

    public class UnitsManager : IDisposable {

        private bool disposed = false;
        private UnitCollection _unitCollection;


        public UnitsManager(UnitCollection unitCollection) {
            _unitCollection = unitCollection;
        }

        public static UnitsManager CreateInstance(UnitCollection unitCollection = null, IEnumerable<IUnit> units = null) {

            if (unitCollection == null) {
                unitCollection = UnitCollection.CreateInstance(units);
            }

            return new UnitsManager(unitCollection);
        }


        public IEnumerable<IUnit> Units {
            get {
                if (_unitCollection == null) {
                    return null;
                }

                return _unitCollection.Units;
            }
        }

        public IUnit GetUnit(UnitID id) {
            return Units.FirstOrDefault(p => p.ID == id);
        }

        public IEnumerable<IUnit> GetUnits(UnitType type) {
            return Units.Where(p => p.Type == type);
        }

        public virtual void UpdateUnits(IUnit sourceUnit) {

            double baseUnitMagnitude;
            IEnumerable<IUnit> unitsOfSameType = Units.Where(p => p.Type == sourceUnit.Type);

            try {
                baseUnitMagnitude = sourceUnit.ConvertToBaseUnit();

            } catch {

                // if converting to base unit is not possible, no conversion is possible, so invalidate all units

                foreach (var unit in unitsOfSameType) {
                    unit.Magnitude = null;
                }

                return;
            }


            foreach (var unit in unitsOfSameType) {

                if (unit != sourceUnit) {

                    try {
                        unit.Magnitude = unit.ConvertFromBaseUnit(baseUnitMagnitude);

                    } catch {
                        unit.Magnitude = null;
                    }
                }
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {

            if (disposed) {
                return;
            }

            if (disposing) {
                // free managed resources
            }

            disposed = true;
        }
    }
}
