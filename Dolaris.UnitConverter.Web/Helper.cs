using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Dolaris.UnitConverter.Web {

    public class Helper {

        /// <summary>
        /// Returns a collection of all System.Web.UI.Controls in the provided control collection.
        /// </summary>
        /// <typeparam name="T">The type of System.Web.UI.Control, for example System.Web.UI.HtmlControls</typeparam>
        /// <param name="controlCollection">Collection of Controls</param>
        /// <param name="resultCollection">List of all controls of type T</param>
        public static void GetControlList<T>(ControlCollection controlCollection, List<T> resultCollection) where T : Control {

            foreach (Control control in controlCollection) {

                if (control is T) {
                    resultCollection.Add((T)control);
                }

                if (control.HasControls()) {
                    GetControlList(control.Controls, resultCollection);
                }
            }
        }
    }
}