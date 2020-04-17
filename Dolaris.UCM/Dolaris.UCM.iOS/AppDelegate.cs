using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TextCell), typeof(Dolaris.UCM.iOS.TextCellWithAccessoryRenderer))]
//[assembly: ExportRenderer(typeof(Cell), typeof(Dolaris.UCM.iOS.CellWithAccessoryRenderer))]
[assembly: ExportRenderer(typeof(EntryCell), typeof(Dolaris.UCM.iOS.EntryCellWithKeyboardNumbersFirst))]

namespace Dolaris.UCM.iOS {
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options) {

            //UINavigationBar.Appearance.BarTintColor = UIColor.LightGray;

            //UITableView.Appearance.TintColor = UIColor.Green;
            //UITableView.Appearance.SectionIndexBackgroundColor = UIColor.Black;
            //UITableViewCell.Appearance.BackgroundColor = UIColor.Purple;
            

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new Dolaris.UCM.App());

            return base.FinishedLaunching(app, options);
        }
    }

    //public class Xxasd : EntryCellRenderer {
    //    public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv) {

    //        var cell = base.GetCell(item, reusableCell, tv);

            

    //        return base.GetCell(item, reusableCell, tv);
    //    }
    //}

    public class TextCellWithAccessoryRenderer : TextCellRenderer {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv) {

            var cell = base.GetCell(item, reusableCell, tv);

            var accessory = (AccessoryType)item.GetValue(CellExtensions.AccessoryProperty);

            switch (accessory) {
                case AccessoryType.None:
                    cell.Accessory = UITableViewCellAccessory.None;
                    break;
                case AccessoryType.DisclosureIndicator:
                    cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
                    break;
                case AccessoryType.DetailDisclosureButton:
                    cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
                    break;
                case AccessoryType.Checkmark:
                    cell.Accessory = UITableViewCellAccessory.Checkmark;
                    break;
                case AccessoryType.DetailButton:
                    cell.Accessory = UITableViewCellAccessory.DetailButton;
                    break;
                default:
                    cell.Accessory = UITableViewCellAccessory.None;
                    break;
            }

            return cell;
        }
    }

    //public class CellWithAccessoryRenderer: CellRenderer {
    //    public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv) {
    //        //return base.GetCell(item, reusableCell, tv);


    //        var cell = base.GetCell(item, reusableCell, tv);

    //        var accessory = (AccessoryType)item.GetValue(CellExtensions.AccessoryProperty);

    //        switch (accessory) {
    //            case AccessoryType.None:
    //                cell.Accessory = UITableViewCellAccessory.None;
    //                break;
    //            case AccessoryType.DisclosureIndicator:
    //                cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
    //                break;
    //            case AccessoryType.DetailDisclosureButton:
    //                cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
    //                break;
    //            case AccessoryType.Checkmark:
    //                cell.Accessory = UITableViewCellAccessory.Checkmark;
    //                break;
    //            case AccessoryType.DetailButton:
    //                cell.Accessory = UITableViewCellAccessory.DetailButton;
    //                break;
    //            default:
    //                cell.Accessory = UITableViewCellAccessory.None;
    //                break;
    //        }

    //        return cell;
    //    }
    //}

    public class EntryCellWithKeyboardNumbersFirst : EntryCellRenderer {

        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv) {

            var cell = base.GetCell(item, reusableCell, tv);

            UITextField textField = (UITextField)cell.ContentView.Subviews[0];
            textField.KeyboardType = UIKeyboardType.NumbersAndPunctuation;

            return cell;
        }
    }
}
