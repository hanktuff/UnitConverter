using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dolaris.UnitConverter;

namespace Dolaris.UnitConverter.Web {
    public partial class UnitControl : System.Web.UI.UserControl {

        protected void Page_Load(object sender, EventArgs e) {
        }


        /// <summary>
        /// Initializes an UnitControl object using information from an IUnit.
        /// Initialize() is the preferred way of setting properties.
        /// </summary>
        /// <param name="unit"></param>
        public void Initialize(WebUnit webUnit) {

            Name = webUnit.Name;
            Symbol = webUnit.Symbol;
            UnitID = webUnit.UnitID;

            //UnitTextBox.Attributes.Add("data-unit-id", webUnit.UnitID.ToString());
            //UnitTextBox.ClientIDMode = ClientIDMode.AutoID;



            //this.ID = $"UnitTextBox-{unit.Type.ToString()}-{unit.Name}";
            //UnitName = unit.ID.ToString();
            //UnitGroupName = unit.Type.ToString();
            //UnitNameTag = unit.Name + ":";
            //UnitSymbol = unit.Symbol;
            //Tooltip = unit.GetPlural();

            //SetCollapseAttribute(unit.Type.ToString(), startCollapsed);

            //// show or hide the helper links (+1, -1, ...)

            //UnitHelperActionClean.Visible = UnitHelperActionMin.Visible = UnitHelperActionMax.Visible = false;

            //if (unit.MinValue == 0) {
            //    UnitHelperActionClean.Visible = true;

            //} else if (unit.MinValue < 0) {
            //    UnitHelperActionMin.Visible = true;
            //}

            //if (unit.MaxValue != double.MaxValue) {
            //    UnitHelperActionMax.Visible = true;
            //}
        }

        /// <summary>
        /// Gets or sets the name of the unit. For example: "Nautical Miles".
        /// </summary>
        public String Name {
            get {
                return _name;
            }
            set {
                _name = value;

                NamePlaceHolder.Controls.Clear();
                NamePlaceHolder.Controls.Add(new Literal() { Text = value + ":" });
            }
        }
        private string _name;

        

        ///// <summary>
        ///// Gets or sets the tag/label for the unit.
        ///// For example: the label "Meter:" is used for meters.
        ///// </summary>
        //public String UnitNameTag {
        //    get {
        //        return UnitNameLabel.InnerText;
        //    }
        //    set {
        //        UnitNameLabel.InnerText = value;
        //    }
        //}

        /// <summary>
        /// Gets or sets the symbol of the unit. For example: "m" is the symbol for Meter.
        /// </summary>
        public String Symbol {
            get {
                return _symbol;
            }
            set {
                _symbol = value;

                SymbolPlaceHolder.Controls.Clear();
                SymbolPlaceHolder.Controls.Add(new Literal() { Text = value });
            }
        }
        private string _symbol;


        public UnitID UnitID {
            get {
                return _unitID;
            }
            set {
                _unitID = value;
            }
        }
        private UnitID _unitID;

        ///// <summary>
        ///// Gets or sets the tooltip for the unit.
        ///// </summary>
        //public String Tooltip {
        //    get {
        //        string tooltip = null;

        //        try {
        //            tooltip = UnitTextBox.Attributes["title"];

        //        } catch { }

        //        return tooltip;
        //    }
        //    set {
        //        UnitTextBox.Attributes.Add("title", value);
        //    }
        //}

        ///// <summary>
        ///// Sets the attribute that enables the group to collapse (show/hide).
        ///// </summary>
        ///// <param name="groupType"></param>
        ///// <param name="collapsed"></param>
        //public void SetCollapseAttribute(string groupType, bool collapsed = false) {

        //    string attributeGroupType = "collapse" + groupType;
        //    string attributeCollapse = collapsed ? "collapse" : "in";

        //    UnitHeadElement.Attributes.Add("class",
        //        string.Format("{0} {1} {2}", UnitHeadElement.Attributes["class"], attributeGroupType, attributeCollapse).Trim());
        //}
    }
}