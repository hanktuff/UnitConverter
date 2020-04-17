using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolaris.UnitConverter {

    public interface IUnit {

        UnitID ID { get; set; }

        Double? Magnitude { get; set; }
        Double MaxValue { get; set; }
        Double MinValue { get; set; }

        String Name { get; set; }
        String Symbol { get; set; }

        UnitType Type { get; set; }

        Double Sloap { get; set; }
        Double Intercept { get; set; }

        Func<Double> ConvertToBaseUnit { get; set; }
        Func<Double, Double> ConvertFromBaseUnit { get; set; }
    }
}

