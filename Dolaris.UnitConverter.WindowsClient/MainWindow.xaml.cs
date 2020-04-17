using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dolaris.UnitConverter;

namespace Dolaris.UnitConverter.WindowsClient {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();

            _initializeUnitControls();
        }


        private void _initializeUnitControls() {

            // find all distinct unit group names (like "length", "area", ...) and create one group per tab

            //var unitGroupNames = _manager.Units.Select(p => p.UnitOfMeasurementInfo.Name).Distinct().ToList();
            var unitTypeNames = _manager.Units.Select(p => p.Type.ToString()).Distinct().ToList();


            foreach (var unitTypeName in unitTypeNames) {

                // create a stack panel and add all unit with the unit name to it

                var stackPanel = new StackPanel();

                foreach (var unit in _manager.Units.Where(p => p.Type.ToString() == unitTypeName)) {

                    var unitControl = new UnitControl(unit);
                    unitControl.ValueChanged += UnitControl_ValueChanged;

                    stackPanel.Children.Add(unitControl);
                    _controls.Add(unitControl);
                }

                // create a tab item containing the stack panel and add it to the tab control

                var tabItem = new TabItem();

                tabItem.Header = unitTypeName;
                tabItem.Content = stackPanel;

                tabControlUnits.Items.Add(tabItem);
            }
        }

        private void UnitControl_ValueChanged(object sender, EventArgs e) {
            var updatedControl = (UnitControl)sender;

            _manager.UpdateUnits(sourceUnit: updatedControl.Unit);
            

            foreach (UnitControl control in _controls) {
                if (control.Unit.Type == updatedControl.Unit.Type) {
                    control.Unit.Magnitude = _manager.GetUnit(control.Unit.Name.GetUnitID().Value).Magnitude;

                    var roundToDecimals = updatedControl.Unit.GetDecimalPlaces() + 4;
                    var scientificNotation = control.Unit.ToScientificNotation(roundToDecimals);

                    control.Value = scientificNotation.ToDouble();
                    control.Unit.Magnitude = scientificNotation.ToDouble();

                    //control.Unit.ConvertValueFrom(updatedControl.Unit);
                }
            }

            var debug = _controls.Select(p => string.Format("{0} {1}", p.Unit.Magnitude, p.Unit.Symbol)).ToList();
        }

        private UnitsManager _manager = new UnitsManager(new UnitCollection(UnitCollection.CreateUnits()));
        private List<UnitControl> _controls = new List<UnitControl>();
    }
}
