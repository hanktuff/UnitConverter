using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dolaris.UnitConverter;

namespace Dolaris.UnitConverter.Web {
    public partial class UnitControl : System.Web.UI.UserControl {


        /// <summary>
        /// Name of attribute used to store the value of the unit name.
        /// </summary>
        private const string _attributeUnitName = "data-unitname";

        /// <summary>
        /// Name of attribute used to store the value of the unit group name.
        /// </summary>
        private const string _attributeUnitGroupName = "data-unitgroupname";


        private const string _attributeUnitSymbol = "data-unitsymbol";

        /// <summary>
        /// Name of attribute used to store the value of the helper.
        /// </summary>
        private const string _attributeUnitHelper = "data-helper";


        protected void Page_Load(object sender, EventArgs e) {
        }


        /// <summary>
        /// Initializes an UnitControl object using information from an IUnit.
        /// Initialize() is the preferred way of setting properties.
        /// </summary>
        /// <param name="unit"></param>
        public void Initialize(IUnit unit, bool startCollapsed = false) {

            UnitName = unit.ID.ToString();
            UnitGroupName = unit.Type.ToString();
            UnitNameTag = unit.Name + ":";
            UnitSymbol = unit.Symbol;
            Tooltip = unit.GetPlural();

            SetCollapseAttribute(unit.Type.ToString(), startCollapsed);

            // show or hide the helper links (+1, -1, ...)

            UnitHelperActionClean.Visible = UnitHelperActionMin.Visible = UnitHelperActionMax.Visible = false;

            if (unit.MinValue == 0) {
                UnitHelperActionClean.Visible = true;

            } else if (unit.MinValue < 0) {
                UnitHelperActionMin.Visible = true;
            }

            if (unit.MaxValue != double.MaxValue) {
                UnitHelperActionMax.Visible = true;
            }
        }

        /// <summary>
        /// Gets or sets the unit name.
        /// For example: "Meter" is the name of a unit.
        /// </summary>
        public String UnitName {
            get {
                string unitName = null;

                try {
                    unitName = UnitTextBox.Attributes[_attributeUnitName];

                } catch (Exception) {
                }

                return unitName;
            }
            set {
                UnitTextBox.Attributes.Add(_attributeUnitName, value);
                UnitHelper.Attributes.Add(_attributeUnitHelper, value);
            }
        }

        /// <summary>
        /// Gets or sets the unit group name.
        /// For example: "Length" is the name of a group of units.
        /// </summary>
        public String UnitGroupName {
            get {
                string unitGroupName = null;

                try {
                    unitGroupName = UnitTextBox.Attributes[_attributeUnitGroupName];

                } catch (Exception) {
                }

                return unitGroupName;
            }
            set {
                // if the attribute already exists it will not be added a second time
                UnitTextBox.Attributes.Add(_attributeUnitGroupName, value);
            }
        }

        /// <summary>
        /// Gets or sets the tag/label for the unit.
        /// For example: the label "Meter:" is used for meters.
        /// </summary>
        public String UnitNameTag {
            get {
                return UnitNameLabel.InnerText;
            }
            set {
                UnitNameLabel.InnerText = value;
            }
        }

        /// <summary>
        /// Gets or sets the symbol of the unit.
        /// For example: "m" is the symbol for Meter.
        /// </summary>
        public String UnitSymbol {
            get {
                return UnitTextBoxSymbol.InnerText;
            }
            set {

                // substitute m2, m3, ... with nicely looking superscript character
                //   more info: http://www.fileformat.info/info/unicode/char/b2/index.htm

                if (value.EndsWith("2")) {
                    value = value.Trim('2') + "\u00B2";

                } else if (value.EndsWith("3")) {
                    value = value.Trim('3') + "\u00B3";
                }

                UnitTextBoxSymbol.InnerText = value;

                UnitTextBox.Attributes.Add(_attributeUnitSymbol, value);
            }
        }

        /// <summary>
        /// Gets or sets the tooltip for the unit.
        /// </summary>
        public String Tooltip {
            get {
                string tooltip = null;

                try {
                    tooltip = UnitTextBox.Attributes["title"];

                } catch { }

                return tooltip;
            }
            set {
                UnitTextBox.Attributes.Add("title", value);
            }
        }

        /// <summary>
        /// Gets or sets the value of the unit.
        /// This is whatever the user entered, usually a number.
        /// </summary>
        public String Text {
            get {
                return UnitTextBox.Value;
            }
            set {
                UnitTextBox.Value = value ?? string.Empty;
            }
        }

        /// <summary>
        /// Sets the attribute that enables the group to collapse (show/hide).
        /// </summary>
        /// <param name="groupType"></param>
        /// <param name="collapsed"></param>
        public void SetCollapseAttribute(string groupType, bool collapsed = false) {

            string attributeGroupType = "collapse" + groupType;
            string attributeCollapse = collapsed ? "collapse" : "in";

            UnitHeadElement.Attributes.Add("class",
                string.Format("{0} {1} {2}", UnitHeadElement.Attributes["class"], attributeGroupType, attributeCollapse).Trim());
        }
    }
}