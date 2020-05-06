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

            UnitID = webUnit.UnitID;
            Name = webUnit.Name;
            Plural = webUnit.Plural;
            Symbol = webUnit.Symbol;
            IsBaseUnit = webUnit.IsBaseUnit;
            Value = string.Empty;
        }
        

        /// <summary>
        /// Gets or sets the unit ID.  For example: "NauticalMile".
        /// </summary>
        public UnitID UnitID { get; set; }

        /// <summary>
        /// Gets or sets the name of the unit. For example: "Nautical Mile".
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the plural of the name of the unit. For example: "Feet".
        /// </summary>
        public String Plural { get; set; }

        /// <summary>
        /// Gets or sets the symbol of the unit. For example: "m" is the symbol for Meter.
        /// </summary>
        public String Symbol { get; set; }
        
        /// <summary>
        /// Returns True if the unit is a base unit. Base units are "Meter", "Joule", "Watt", etc. .
        /// </summary>
        public Boolean IsBaseUnit { get; set; }

        /// <summary>
        /// Gets or sets the value of the unit. For example: "12.3".
        /// </summary>
        public String Value { get; set; }
    }
}