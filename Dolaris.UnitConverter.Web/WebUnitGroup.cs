using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Dolaris.UnitConverter;

namespace Dolaris.UnitConverter.Web {

    [DebuggerDisplay("{GroupName}, {WebUnits.Count} units")]
    public class WebUnitGroup {

        public WebUnitGroup(string name, UnitType unitType) {

            GroupName = name;
            GroupType = unitType;
        }


        public String GroupName { get; }

        public UnitType GroupType { get; }

        public Boolean Enabled { get; set; }

        public String Description { get; set; }

        public Boolean StartCollapsed { get; set; }
    }
}