using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Dolaris.UCM {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage {
        public AboutPage() {
            InitializeComponent();

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += TapGesture_Tapped;

            labelLinkToUnitcandyWebsite.GestureRecognizers.Add(tapGesture);
            labelLinkToTwitter.GestureRecognizers.Add(tapGesture);
            labelLinkToUnitcandyEmail.GestureRecognizers.Add(tapGesture);
        }

        private void TapGesture_Tapped(object sender, EventArgs e) {

            if (sender is View) {

                String hyperlink = ((View)sender).GetValue(ViewExtensions.HyperlinkProperty) as String;

                if (!string.IsNullOrWhiteSpace(hyperlink)) {
                    Device.OpenUri(new Uri(hyperlink));
                }
            }
        }
    }
}
