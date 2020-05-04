using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Dolaris.UnitConverter;

namespace Dolaris.UnitConverter.Web {

    [DebuggerDisplay("{UnitID}")]
    public class WebUnit {

        public WebUnit(Dolaris.UnitConverter.IUnit unit = null) {

            if (unit != null) {

                UnitID = unit.ID;
                Name = unit.Name;
                Plural = unit.GetPlural();
                Symbol = unit.Symbol;
                IsBaseUnit = unit.Sloap == 1 && unit.Intercept == 0 ? true : false;
            }
        }

        public UnitID UnitID { get; protected set; }

        public String Name { get; set; }

        public String Plural { get; set; }

        public String Symbol { get; set; }

        public Boolean IsBaseUnit { get; set; }

        public Boolean Enabled { get; set; }

        public Boolean IsMetric { get; set; }

        public String Info { get; set; }
    }
}