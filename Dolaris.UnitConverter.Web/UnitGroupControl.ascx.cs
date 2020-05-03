using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dolaris.UnitConverter.Web {
    public partial class UnitGroupControl : System.Web.UI.UserControl {


        protected void Page_Load(object sender, EventArgs e) {
        }


        /// <summary>
        /// Initializes an UnitGroupControl object using information from an WebUnitGroup.
        /// Initialize() is the preferred way of setting properties.
        /// </summary>
        /// <param name="webUnitGroup"></param>
        public void Initialize(WebUnitGroup webUnitGroup) {

            Title = webUnitGroup.GroupName;
            Description = webUnitGroup.Description;
            UnitType = webUnitGroup.UnitType;


            //SetCollapseAttribute(webUnitGroup.GroupType.ToString(), webUnitGroup.StartCollapsed);
            //SetUtilitiesAttributes(webUnitGroup.GroupType.ToString());
        }

        /// <summary>
        /// Adds a UnitControl element to the placeholder in the unit group.
        /// </summary>
        /// <param name="control"></param>
        public void AddUnitControl(UnitControl control) {

            UnitControlPlaceHolder.Controls.Add(control);
        }

        /// <summary>
        /// Gets or sets the title of the unit group. For example: "Length" or "Digital Storage".
        /// </summary>
        public String Title {
            get {
                return _title;
            }
            set {
                _title = value;

                TitlePlaceHolder.Controls.Clear();
                TitlePlaceHolder.Controls.Add(new Literal() { Text = value });
            } 
        }
        private string _title;

        /// <summary>
        /// Gets or sets the description of the unit group. For example: "Area is the extent of a shape in the plane.".
        /// </summary>
        public String Description {
            get {
                return _description;
            }
            set {
                _description = value;

                DescriptionPlaceHolder.Controls.Clear();
                DescriptionPlaceHolder.Controls.Add(new Literal() { Text = value });
            }
        }
        private string _description;


        public UnitType UnitType {
            get {
                return _unitType;
            }
            set {
                _unitType = value;
            }
        }
        private UnitType _unitType;



        /// <summary>
        /// Sets the attribute that enables the group to collapse (show/hide).
        /// </summary>
        /// <param name="groupType"></param>
        /// <param name="collapsed"></param>
        public void SetCollapseAttribute(string groupType, bool collapsed = false) {

            
        }

        ///// <summary>
        ///// Sets the attributes for the utilities elements (copy, clear, ...) enabling them to work properly.
        ///// </summary>
        ///// <param name="groupType"></param>
        //public void SetUtilitiesAttributes(string groupType) {

        //    CopyLinkToClipboard.Attributes.Add("onclick", string.Format("copyLinkToClipboard('{0}')", groupType));
        //    //CopyAnchorToClipboard.Attributes.Add("onclick", string.Format("copyAnchorToClipboard('{0}')", groupType));

        //    ClearAllFields.Attributes.Add("onclick", string.Format("clearUnitGroup('{0}')", groupType));
        //}
    }
}