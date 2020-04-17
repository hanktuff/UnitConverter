using Dolaris.UnitConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Dolaris.UCM {
    public partial class App : Application {

        public static UnitsManager UnitsManager = new UnitsManager(new UnitCollection(UnitCollection.CreateUnits()));


        public App() {

            InitializeComponent();

            MainPage = new NavigationPage(new MenuPage());
        }

        private void Navpage_Pushed(object sender, NavigationEventArgs e) {
            //throw new NotImplementedException();    
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
