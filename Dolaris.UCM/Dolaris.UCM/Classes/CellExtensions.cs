using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Dolaris.UCM {

    class CellExtensions {

        public static BindableProperty AccessoryProperty =
            BindableProperty.CreateAttached("Accessory", typeof(AccessoryType), typeof(Cell), AccessoryType.None);
    }
}
