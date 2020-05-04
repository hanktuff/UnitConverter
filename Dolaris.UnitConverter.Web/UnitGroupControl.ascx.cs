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

            UnitType = webUnitGroup.UnitType;
            Title = webUnitGroup.GroupName;
            Description = webUnitGroup.Description;
        }


        /// <summary>
        /// Gets or sets the unit type. For example: "DigitalStorage".
        /// </summary>
        public UnitType UnitType { get; set; }

        /// <summary>
        /// Gets or sets the title of the unit group. For example: "Length" or "Digital Storage".
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// Gets or sets the description of the unit group. For example: "Area is the extent of a shape in the plane.".
        /// </summary>
        public String Description { get; set; }


        /// <summary>
        /// Adds a UnitControl element to the placeholder in the unit group.
        /// </summary>
        /// <param name="control"></param>
        public void AddUnitControl(UnitControl control) {

            UnitControlPlaceHolder.Controls.Add(control);
        }
    }
}