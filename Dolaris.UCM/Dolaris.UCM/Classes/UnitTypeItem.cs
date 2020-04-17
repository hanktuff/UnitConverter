using Dolaris.UnitConverter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xamarin.Forms;

namespace Dolaris.UCM {

    public class UnitTypeItem : IEquatable<UnitTypeItem> {

        public UnitTypeItem(UnitType unitType) {

            this.UnitType = unitType;

            Units = App.UnitsManager.GetUnits(unitType).ToList();
        }


        public UnitType UnitType { get; protected set; }

        public string UnitTypeFriendlyName {
            get {
                string result = UnitType.ToString();

                switch (UnitType) {
                    case UnitType.FuelEconomy:
                        result = "Fuel Economy";
                        break;
                    case UnitType.DigitalStorage:
                        result = "Digital Storage";
                        break;
                }

                return result;
            }
        }

        public List<IUnit> Units { get; protected set; }

        public Color Color {
            get {
                return Color.FromRgb(r: 100, g: 50, b: 150);
            }
        }

        public bool Equals(UnitTypeItem other) {
            return this.UnitType == other.UnitType;
        }
    }
}
