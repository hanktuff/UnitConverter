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
            Bookmark = webUnitGroup.GroupType.ToString();

            SetCollapseAttribute(webUnitGroup.GroupType.ToString(), webUnitGroup.StartCollapsed);
            SetUtilitiesAttributes(webUnitGroup.GroupType.ToString());
        }

        /// <summary>
        /// Adds a UnitControl element to the placeholder in the unit group.
        /// </summary>
        /// <param name="control"></param>
        public void AddUnitControl(UnitControl control) {

            UnitControlPlaceHolder.Controls.Add(control);
        }

        /// <summary>
        /// Gets or sets the name of the unit group.
        /// </summary>
        public String Title {
            get {
                return UnitGroupTitle.InnerText;
            }
            set {
                UnitGroupTitle.InnerText = value;
            }
        }

        /// <summary>
        /// Gets or sets the description of the unit group.
        /// </summary>
        public String Description {
            get {
                return UnitGroupDescription.InnerText;
            }
            set {
                UnitGroupDescription.InnerText = value;
            }
        }

        /// <summary>
        /// Gets or sets the Bookmark at the top of the control.
        /// This can be used as a target when linking to the control.
        /// </summary>
        public String Bookmark {
            get {
                return MainDiv.ID;
            }
            set {
                MainDiv.ID = value;
            }
        }

        /// <summary>
        /// Gets or sets the background color for the unit group control.
        /// </summary>
        public String Background {
            get {
                return MainDiv.Attributes["class"];
            }
            set {
                MainDiv.Attributes.Add("class", value);
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

            UnitGroupTitle.Attributes.Add("data-target", "." + attributeGroupType);
            UnitGroupTitle.Style["cursor"] = "pointer";
            UnitGroupTitle.Attributes.Add("title", Description);

            UnitGroupDescription.Attributes.Add("class", 
                string.Format("{0} {1} {2}", UnitGroupDescription.Attributes["class"], attributeGroupType, attributeCollapse).Trim());
        }

        /// <summary>
        /// Sets the attributes for the utilities elements (copy, clear, ...) enabling them to work properly.
        /// </summary>
        /// <param name="groupType"></param>
        public void SetUtilitiesAttributes(string groupType) {

            CopyLinkToClipboard.Attributes.Add("onclick", string.Format("copyLinkToClipboard('{0}')", groupType));
            //CopyAnchorToClipboard.Attributes.Add("onclick", string.Format("copyAnchorToClipboard('{0}')", groupType));

            ClearAllFields.Attributes.Add("onclick", string.Format("clearUnitGroup('{0}')", groupType));
        }
    }
}