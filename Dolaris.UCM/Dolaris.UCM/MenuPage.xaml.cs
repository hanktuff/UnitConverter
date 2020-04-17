using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Dolaris.UnitConverter;

namespace Dolaris.UCM {
    public partial class MenuPage : ContentPage {

        // all about Disclosure Indicator
        // http://www.wintellect.com/devcenter/krome/lighting-up-native-platform-features-in-xamarin-forms-part-1
        // http://www.wintellect.com/devcenter/krome/lighting-up-native-platform-features-in-xamarin-forms-part-2


        private CarouselPage _unitDetailsCarouselPage = new CarouselPage();


        public MenuPage() {

            InitializeComponent();

            foreach (var item in _getUnitTypeItems().ToList()) {

                var cell = new TextCell();

                cell.Text = item.UnitTypeFriendlyName;
                cell.BindingContext = item;
                cell.SetValue(CellExtensions.AccessoryProperty, AccessoryType.DisclosureIndicator);
                cell.Tapped += Cell_Tapped;

                tablesectionTypesOfUnits.Add(cell);

                _unitDetailsCarouselPage.Children.Add(new UnitDetailsPage(item));
            }

            _unitDetailsCarouselPage.CurrentPageChanged += _unitDetailsCarouselPage_CurrentPageChanged;

            // DEBUG
            tablesectionDEBUG.Add(new CustomCell());
            tablesectionDEBUG.Add(new CustomCell());
            tablesectionDEBUG.Add(new CustomCell());
            //////////
        }


        protected override void OnAppearing() {
            Title = "UnitCandy";
            base.OnAppearing();
        }

        protected override void OnDisappearing() {
            Title = "Home";
            base.OnDisappearing();
        }

        private void Cell_Tapped(object sender, EventArgs e) {

            if (sender is Cell) {

                var selectedUnitTypeItem = ((Cell)sender).BindingContext as UnitTypeItem;

                var startpage = _unitDetailsCarouselPage.Children.Where(p => p is UnitDetailsPage)
                                    .FirstOrDefault(p => ((UnitDetailsPage)p).UnitTypeItem == selectedUnitTypeItem);

                if (startpage != null) {

                    _unitDetailsCarouselPage.CurrentPage = startpage;
                    _unitDetailsCarouselPage.Title = startpage.Title;
                    Navigation.PushAsync(_unitDetailsCarouselPage);
                }
            }
        }

        private void _unitDetailsCarouselPage_CurrentPageChanged(object sender, EventArgs e) {

            var carouselpage = sender as CarouselPage;

            if (carouselpage != null) {
                carouselpage.Title = carouselpage.CurrentPage.Title;
            }
        }

        private IEnumerable<UnitTypeItem> _getUnitTypeItems() {

            return App.UnitsManager.Units
                        .Select(p => p.Type).Distinct()
                            .Select(p => new UnitTypeItem(p));
        }

        private void textcellAboutUnitCandy_Tapped(object sender, EventArgs e) {
            Navigation.PushAsync(new AboutPage());
        }
    }

    public class CustomCell : ViewCell {

        public CustomCell() {

            this.Height = 60;
            this.SetValue(CellExtensions.AccessoryProperty, AccessoryType.DisclosureIndicator);

            var container = new StackLayout();
            container.Margin = new Thickness(left: 0, top: 5, right: 0, bottom: 20);

            var inner = new StackLayout();

            inner.Children.Add(new Label() { Text = "Label1" });
            //inner.Children.Add(new Entry() { Text = "Entcell", WidthRequest = 60 });

            container.Children.Add(inner);

            View = container;
        }

        protected override void OnTapped() {
            base.OnTapped();
        }
    }
}
