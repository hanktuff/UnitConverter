using Dolaris.UnitConverter;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Dolaris.UCM
{
    public class UnitTypeCell : ViewCell
    {
        public UnitTypeCell() {

            var labelUnitType = new Label();
            labelUnitType.SetBinding(Label.TextProperty, new Binding("."));


            var labelX = new Label();
            //labelX.FontFamily = "FontAwesome";
            labelX.Text = ">";
            //labelX.XAlign = TextAlignment.End;
            labelX.HorizontalTextAlignment = TextAlignment.End;
            labelX.HorizontalOptions = LayoutOptions.FillAndExpand;
            labelX.TextColor = Color.Gray;

            //UIKit.UITableViewCellAccessory.DisclosureIndicator

            var s = new StackLayout();

            s.Margin = new Thickness(20, 0, 0, 5);
            s.Children.Add(labelUnitType);
            s.Children.Add(labelX);
            s.Orientation = StackOrientation.Horizontal;

            View = s;
        }
    }
}
