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

        private UnitID _unitID;


        public WebUnit(UnitID unitID) {
            _unitID = unitID;
        }

        
        public UnitID UnitID {
            get {
                return _unitID;
            }
        }

        public Boolean Enabled { get; set; }

        public String Description { get; set; }

        public Boolean IsMetric { get; set; }
    }
}