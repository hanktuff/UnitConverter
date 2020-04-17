using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Dolaris.UCM {

    class ViewExtensions {

        public static BindableProperty HyperlinkProperty =
                        BindableProperty.CreateAttached("Hyperlink", typeof(string), typeof(View), defaultValue: string.Empty);

        public static string GetHyperlink(BindableObject view) {
            return (string)view.GetValue(HyperlinkProperty);
        }

        public static void SetMyHyperlink(BindableObject view, string value) {
            view.SetValue(HyperlinkProperty, value);
        }
    }
}
