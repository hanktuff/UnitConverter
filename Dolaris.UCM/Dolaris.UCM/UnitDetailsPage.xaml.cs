using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Dolaris.UnitConverter;

namespace Dolaris.UCM {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UnitDetailsPage : ContentPage {

        public UnitDetailsPage(UnitTypeItem unitTypeItem) {

            InitializeComponent();

            UnitTypeItem = unitTypeItem;

            Title = unitTypeItem.UnitTypeFriendlyName;

            var units = _getSortedUnits(unitTypeItem.UnitType);
            //var units = App.UnitsManager.GetUnits(unitTypeItem.UnitType).ToList();

            foreach (IUnit unit in units) {

                var cell = new EntryCell() {
                    Label = unit.Name,
                    Placeholder = _withSuperscriptChars(unit.Symbol),
                    HorizontalTextAlignment = TextAlignment.End,
                    BindingContext = unit,
                    Keyboard = Keyboard.Plain
                };

                cell.Completed += Cell_Completed;
                cell.Tapped += Cell_Tapped;

                tablesectionUnits.Add(cell);
            }
        }

        // TODO: when cell is tapped, I want to start editing
        private void Cell_Tapped(object sender, EventArgs e) {
            //throw new NotImplementedException();
            //((EntryCell)sender)
        }

        private void Cell_Completed(object sender, EventArgs e) {

            if (sender is EntryCell) {

                var sourceEntryCell = (EntryCell)sender;
                var sourceUnit = (IUnit)sourceEntryCell.BindingContext;

                double number;
                bool isNumber = double.TryParse(sourceEntryCell.Text, out number);

                if (isNumber) {

                    sourceUnit.Magnitude = number;
                    int roundToDecimals = sourceUnit.GetDecimalPlaces() + 4;

                    string errorString;
                    sourceUnit.GetFormattedString(out errorString);
                    _setEntryCellText(sourceEntryCell, sourceUnit);

                    if (string.IsNullOrWhiteSpace(errorString)) {

                        App.UnitsManager.UpdateUnits(sourceUnit);

                        foreach (IUnit unit in App.UnitsManager.GetUnits(sourceUnit.Type)) {

                            var placesBeforeDecimalPoint = unit.GetPlacesBeforeDecimalPoint();
                            if (placesBeforeDecimalPoint > 4) {
                                roundToDecimals = placesBeforeDecimalPoint;
                            }

                            var scientific = unit.ToScientificNotation(roundToDecimals);
                            unit.Magnitude = scientific.ToDouble();

                            EntryCell cell = tablesectionUnits.Where(p => p.BindingContext is IUnit)
                                                .FirstOrDefault(p => ((IUnit)p.BindingContext).ID == unit.ID) as EntryCell;

                            if (cell != null) {
                                _setEntryCellText(cell, unit);
                            }
                        }
                    }

                } else {
                    sourceEntryCell.Text = string.Empty;
                    sourceEntryCell.Placeholder = "Please enter a number.";
                }
            }
        }

        public UnitTypeItem UnitTypeItem { get; }


        private string _withSuperscriptChars(string s) {

            // substitute m2, m3, ... with nicely looking superscript character
            //   more info: http://www.fileformat.info/info/unicode/char/b2/index.htm

            if (s.EndsWith("2")) {
                s = s.Trim('2') + "\u00B2";

            } else if (s.EndsWith("3")) {
                s = s.Trim('3') + "\u00B3";
            }

            return s;
        }

        private void _setEntryCellText(EntryCell cell, IUnit unit) {

            string errorString;
            string formattedString = unit.GetFormattedString(out errorString);

            if (string.IsNullOrWhiteSpace(errorString)) {

                cell.Text = formattedString;
                cell.Placeholder = unit.Symbol;

            } else {

                cell.Text = string.Empty;
                cell.Placeholder = errorString;
            }
        }

        private List<IUnit> _getSortedUnits(UnitType unitType) {
            var sortedUnits = new List<IUnit>();

            if (unitType == UnitType.Temperature) {

                sortedUnits.Add(App.UnitsManager.GetUnit(UnitID.Fahrenheit));
                sortedUnits.Add(App.UnitsManager.GetUnit(UnitID.Celsius));
                sortedUnits.Add(App.UnitsManager.GetUnit(UnitID.Kelvin));

            } else if (unitType == UnitType.Length) {

                sortedUnits.Add(App.UnitsManager.GetUnit(UnitID.Meter));
                sortedUnits.Add(App.UnitsManager.GetUnit(UnitID.Millimeter));
                sortedUnits.Add(App.UnitsManager.GetUnit(UnitID.Inch));
                sortedUnits.Add(App.UnitsManager.GetUnit(UnitID.Foot));
                sortedUnits.Add(App.UnitsManager.GetUnit(UnitID.Yard));
                sortedUnits.Add(App.UnitsManager.GetUnit(UnitID.Kilometer));
                sortedUnits.Add(App.UnitsManager.GetUnit(UnitID.Mile));
                sortedUnits.Add(App.UnitsManager.GetUnit(UnitID.NauticalMile));
                sortedUnits.Add(App.UnitsManager.GetUnit(UnitID.Lightyear));

            } else {
                sortedUnits = App.UnitsManager.GetUnits(unitType).ToList();
            }

            return sortedUnits;
        }
    }
}
