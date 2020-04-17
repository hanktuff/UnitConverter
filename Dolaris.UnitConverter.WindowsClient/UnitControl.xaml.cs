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
    /// Interaction logic for UnitControl.xaml
    /// </summary>
    public partial class UnitControl : UserControl {

        public UnitControl(IUnit unit) {
            InitializeComponent();

            Unit = unit;
            State = UIState.NotEditing;
        }


        private IUnit _unit = null;
        public IUnit Unit {
            get {
                return _unit;
            }
            set {
                _unit = value;
                //value.ValueUpdated += value_ValueUpdated;
                _update();
            }
        }

        public enum UIState { Editing, NotEditing }

        private UIState _state;
        public UIState State {
            get {
                return _state;
            }
            set {
                _state = value;

                if (State == UIState.Editing) {
                    this.textblockUnitValue.Visibility = System.Windows.Visibility.Hidden;
                    this.textboxUnitValueEditing.Visibility = System.Windows.Visibility.Visible;

                } else if (State == UIState.NotEditing) {
                    this.textblockUnitValue.Visibility = System.Windows.Visibility.Visible;
                    this.textboxUnitValueEditing.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        public Double Value {
            get {
                return Unit.Magnitude.HasValue ? Unit.Magnitude.Value : -99;
            }
            set {
                Unit.Magnitude = value;
                _update();
            }
        }

        public event EventHandler ValueChanged;

        private void UnitValue_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            if (State == UIState.NotEditing) {
                State = UIState.Editing;
            }
        }

        private void textboxUnitValueEditing_LostFocus(object sender, RoutedEventArgs e) {
            if (State == UIState.Editing) {
                State = UIState.NotEditing;

                double newValue;
                var success = double.TryParse(this.textboxUnitValueEditing.Text, out newValue);

                if (success) {
                    Value = newValue;
                }

                if (ValueChanged != null) {
                    ValueChanged(this, EventArgs.Empty);
                }
            }
        }


        private void _update() {
            this.labelUnitName.Content = Unit.GetPlural() + ":";
            this.textblockUnitValue.Text = Unit.Magnitude.ToString();
            this.textboxUnitValueEditing.Text = Unit.Magnitude.ToString();
        }

        private void value_ValueUpdated(object sender, EventArgs e) {
            _update();
        }
    }
}
