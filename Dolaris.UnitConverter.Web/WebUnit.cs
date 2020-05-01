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

                Name = unit.Name;
                Symbol = unit.Symbol;
                UnitID = unit.ID;
            }
        }


        public String Name { get; set; }

        public String Symbol { get; set; }

        public UnitID UnitID { get; protected set; }

        public Boolean Enabled { get; set; }

        public Boolean IsMetric { get; set; }

        public String Info { get; set; }
    }
}