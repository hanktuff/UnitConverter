using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dolaris.UnitConverter;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;

namespace Dolaris.UnitConverter.Web {
    public partial class Default : System.Web.UI.Page {


        #region TODO
        // ***********************************************************************************************
        //
        // TODO: add hit counter
        //
        //
        // --- Completed Tasks ---
        //
        // Done: copy to clipboard
        // Done: add Clear button
        // Done: add page for About Us or similar
        // Done: add Google Analytics
        // Done: add unit of measurement "speed"
        // Done: only update unit group of unit that has changed
        // Done: tooltips for units (plurals, symbol)
        // Done: add proper ID to unit classes
        // Done: fix problem only 5 digits accepted as input before decimal point
        // Done: update icon
        // Done: update title
        // Done: add unit of measurement "energy"
        // Done: add error page
        // Done: use AJAX when updating numbers
        // Done: on top add links to units
        //
        // ***********************************************************************************************
        #endregion


        private WebManager _webManager = new WebManager();

        private List<string> _background = new List<string>() { "bg-dark", "bg-info", "bg-color", "bg-grey", "bg-info", "bg-primary", "bg-success", "bg-warning" };


        public Default() {
        }

        protected override void OnInit(EventArgs e) {

            int backgroundIndex = 0;
            var units = WebManager.GetUnits();

            var unitsForAnyUnitDropdown = new List<IUnit>();





            //var inner = template.InnerHtml;

            //var contrl = this.ParseControl(inner);



            //template.Attributes.Remove("hidden");
            //HtmlGenericControl g1 = new HtmlGenericControl("tag1");
            //g1.Controls.Add(template);
            //HtmlGenericControl g2 = new HtmlGenericControl("tag2");
            //g2.Controls.Add(new Literal() { Text = "This is another title" });
            //g2.Controls.Add(template);


            //template.FindControl("TIME").Controls.Add(new Literal() { Text = System.DateTime.Now.ToString() });
            //PH1.InnerHtml += template.InnerText;

            //template.FindControl("Title").Controls.Add(new Literal() { Text = "This is the title" });
            //PH1.Controls.Add(g2);
            //PH1.InnerHtml += template.InnerText;

            //template.FindControl("Title").Controls.Add(new Literal() { Text = "This is another title" });
            //PH1.Controls.Add(template);

            //template.FindControl("TIME").Controls.Add(new Literal() { Text = "sdfs sdf" });
            //PH1.Controls.Add(template);

            //System.Threading.Thread.Sleep(1000);
            //template.FindControl("TIME").Controls.Add(new Literal() { Text = System.DateTime.Now.ToString() });
            //PH1.Controls.Add(new Literal() { Text = System.DateTime.Now.ToString() });


            //PH1.Controls.Add(new Literal() { Text = System.DateTime.Now.ToString() });




            foreach (WebUnitGroup webUnitGroup in _webManager.WebUnitGroups) {

                if (webUnitGroup.Enabled == false) { continue; }

                UnitGroupControl unitGroupControl = (UnitGroupControl)Page.LoadControl("~/UnitGroupControl.ascx");
                unitGroupControl.Initialize(title: webUnitGroup.GroupName, description: webUnitGroup.Description);

                // set a different background for each unit group

                //unitGroupControl.Background = _background[backgroundIndex];

                backgroundIndex++;
                if (backgroundIndex > _background.Count - 1) {
                    backgroundIndex = 0;
                }

                // add all enabled units to the unit group control

                foreach (WebUnit webUnit in _webManager.WebUnits) {

                    if (webUnit.Enabled) {

                        var unit = units.First(p => p.ID == webUnit.UnitID);

                        if (unit.Type == webUnitGroup.GroupType) {

                            UnitControl unitControl = (UnitControl)Page.LoadControl("~/UnitControl.ascx");
                            unitControl.Initialize(name: unit.Name, symbol: unit.Symbol);
                            unitGroupControl.AddUnitControl(unitControl);

                            //unitsForAnyUnitDropdown.Add(unit);
                        }
                    }
                }

                //WebGroupsPlaceholder.Controls.Add(unitGroupControl);
                PH2.Controls.Add(unitGroupControl);
            }

            // add units to "Any Units" dropdown

            //foreach (var unit in unitsForAnyUnitDropdown.OrderBy(p => p.Name)) {
            //    AnyUnitLiteral.Text +=
            //        string.Format("<li value=\"{0}\"><a href=\"javascript: findUnitDropdownSelectionChanged('{0}');\">{1}</a></li>", unit.ID.ToString(), unit.Name);
            //}
        }

        protected void Page_Load(object sender, EventArgs e) {

            if (Page.IsPostBack) {

                // code that handles a postback

            } else {

                // code for first time the page is being called

                try {
                    // for now don't write to the log file anymore
                    //_writeLog(Page.Request);

                } catch (Exception ex) {
                    Debug.WriteLine(ex.Message ?? "(no message)", category: ex.GetType().Name);
                }
            }
        }

        private void _getControlList<T>(ControlCollection controlCollection, List<T> resultCollection) where T : Control {

            foreach (Control control in controlCollection) {
                //if (control.GetType() == typeof(T))
                if (control is T) {
                    resultCollection.Add((T)control);
                }

                if (control.HasControls()) {
                    _getControlList(control.Controls, resultCollection);
                }
            }
        }

        private void _writeLog(HttpRequest request, string logfilename = "logfile.txt") {

            // Find country, region, city, etc. from IP address:
            // http://stackoverflow.com/questions/4327629/get-user-location-by-ip-address

            const string no_data = "(no data)";
            string logfile = Server.MapPath("./" + logfilename);

            string logString = string.Empty;

            string UTCDateTime = DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss");
            string UTCDate = DateTime.UtcNow.ToString("yyyy/MM/dd");
            string UTCTime = DateTime.UtcNow.ToString("HH:mm:ss");
            string userHostAddress = string.Empty;
            string userHostName = string.Empty;
            string browserName = string.Empty;
            string browserPlatform = string.Empty;
            string browserVersion = string.Empty;
            string browserFirstUserLanguage = string.Empty;

            if (Request != null) {

                userHostAddress = request.UserHostAddress ?? no_data;
                userHostName = request.UserHostName ?? no_data;

                if (request.Browser != null) {

                    browserName = request.Browser.Browser ?? no_data;
                    browserVersion = request.Browser.Version ?? no_data;
                    browserPlatform = request.Browser.Platform ?? no_data;
                }

                if (request.UserLanguages != null) {
                    if (request.UserLanguages.Length > 0) {
                        browserFirstUserLanguage = request.UserLanguages[0] ?? no_data;
                    }
                }

            } else {
                logString = "Error: The HttpRequest object is null!";
            }


            //logString += string.Format(
            //                    "UTC={0}; HostAddress={1}; HostName={2}; Browser={3}; Version={4}; Platform={5}; Language:{6};",
            //                        UTCDateTime,
            //                        userHostAddress,
            //                        userHostName,
            //                        browserName,
            //                        browserVersion,
            //                        browserPlatform,
            //                        browserFirstUserLanguage);

            logString += string.Format(
                                "UTC-Date={0}; UTC-Time={1} HostAddress={2}; Browser={3}; Version={4}; Platform={5}; Language:{6}; ",
                                    UTCDate,
                                    UTCTime,
                                    userHostAddress,
                                    browserName,
                                    browserVersion,
                                    browserPlatform,
                                    browserFirstUserLanguage);


            File.AppendAllText(logfile, logString + Environment.NewLine);
        }
    }
}
