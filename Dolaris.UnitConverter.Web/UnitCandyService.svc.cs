using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Dolaris.UnitConverter;
using System.Net;
using System.Net.Mail;

namespace Dolaris.UnitConverter.Web {


    [ServiceContract(Namespace = "https://www.unitcandy.com/ws/")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class UnitCandyService {

        /// <summary>
        /// An instance of the WebManager in order to work with units.
        /// </summary>
        private WebManager _webman = new WebManager();


        /// <summary>
        /// Creates an instance of the UnitCandyService class.
        /// </summary>
        public UnitCandyService() {
        }


        #region Info
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        #endregion


        /// <summary>
        /// Returns date and time information when pinged.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        public String Ping() {
            return string.Format("Ping was received at: '{0}'.", DateTime.Now.ToString("dddd, yyyy/MM/dd HH:mm:ss.fff"));
        }

        /// <summary>
        /// Based on the unit and its new value, recalculates the values of all other units from the same group of units.
        /// Returns a list of all updated units.
        /// </summary>
        /// <param name="unitName">The name/ID of the unit (for example "Meter").</param>
        /// <param name="unitValue">The value of the unit as entered by the user. Does not have to be a number.</param>
        /// <returns></returns>
        [OperationContract]
        public List<FormattedUnit> Recalculate(string unitName, string unitValue) {
            //System.Threading.Thread.Sleep(3000);
            var result = new List<FormattedUnit>();

            // ensure that the unit name and unit value is okay

            if (string.IsNullOrEmpty(unitName) || string.IsNullOrEmpty(unitValue)) {
                throw new Exception("The unit name or the unit value is empty!");
            }

            UnitID? unitID = unitName.GetUnitID();

            if (unitID.HasValue == false) {
                throw new Exception(string.Format("The unit name '{0}' is unknown!", unitName));
            }

            // find the source unit and all other units of the same type

            var sourceUnit = WebManager.GetUnit(unitID.Value);

            // if user entered a number, recalculate all other units

            int? roundToDecimals = null;
            sourceUnit.Magnitude = double.NaN;

            IEnumerable<IUnit> units = new List<IUnit>() { sourceUnit };

            if (_webman.IsNumber(unitValue)) {

                sourceUnit.Magnitude = double.Parse(unitValue);
                roundToDecimals = sourceUnit.GetDecimalPlaces() + 4;

                if (sourceUnit.Magnitude >= sourceUnit.MinValue && sourceUnit.Magnitude <= sourceUnit.MaxValue) {
                    units = WebManager.Recalculate(sourceUnit);
                }
            }

            // create list of results to return to the caller

            foreach (IUnit unit in units) {

                if (unit.Type != sourceUnit.Type) { continue; }

                var webUnit = _webman.GetWebUnit(unit.ID);
                if (webUnit == null) { continue; }
                if (webUnit.Enabled == false) { continue; }

                if (roundToDecimals.HasValue) {

                    var placesBeforeDecimalPoint = unit.GetPlacesBeforeDecimalPoint();
                    if (placesBeforeDecimalPoint > 4) {
                        roundToDecimals = placesBeforeDecimalPoint;
                    }

                    var scientific = unit.ToScientificNotation(roundToDecimals.Value);
                    unit.Magnitude = scientific.ToDouble();
                }

                result.Add((FormattedUnit)(Unit)unit);
            }

            return result;
        }

        /// <summary>
        /// Based on the unit, value, and action, recalculates the values of all other units from the same group of units.
        /// Returns a list of all updated units.
        /// </summary>
        /// <param name="unitName">The name/ID if the unit (for example "Meter").</param>
        /// <param name="unitValue">The value of the unit as entered by the user. Does not have to be a number.</param>
        /// <param name="action">The action, like +1, -1, *10, etc.</param>
        /// <returns></returns>
        [OperationContract]
        public List<FormattedUnit> RecalculateWithHelperAction(string unitName, string unitValue, string action) {

            if (!_webman.IsNumber(unitValue)) {
                unitValue = "0";
            }

            try {
                switch (action) {

                    case "+1":
                        unitValue = (double.Parse(unitValue) + 1).ToString();
                        break;
                    case "-1":
                        unitValue = (double.Parse(unitValue) - 1).ToString();
                        break;
                    case "x10":
                        unitValue = (double.Parse(unitValue) * 10).ToString();
                        break;
                    case "/10":
                        unitValue = (double.Parse(unitValue) / 10).ToString();
                        break;
                    case "Clear":
                        unitValue = "0";
                        break;
                    case "Min":
                        unitValue = WebManager.GetUnit(unitName.GetUnitID().Value).MinValue.ToString();
                        break;
                    case "Max":
                        unitValue = WebManager.GetUnit(unitName.GetUnitID().Value).MaxValue.ToString();
                        break;
                    default:
                        break;
                }

            } catch (Exception) {
                unitValue = "0";
            }

            return Recalculate(unitName, unitValue);
        }

        /// <summary>
        /// Returns text formatted to be used for copying it to the users clipboard.
        /// </summary>
        /// <param name="unitGroupName"></param>
        /// <returns></returns>
        [OperationContract]
        public String GetCopyToClipboardText(string unitName, string unitValue) {
            StringBuilder result = new StringBuilder();

            IUnit sourceUnit = null;
            WebUnitGroup webUnitGroup = null;

            UnitID? unitID = unitName.GetUnitID();

            if (unitID.HasValue) {
                sourceUnit = WebManager.GetUnit(unitID.Value);
                webUnitGroup = _webman.WebUnitGroups.FirstOrDefault(p => p.GroupType == sourceUnit.Type);
            }

            var formattedUnits = new List<FormattedUnit>();

            if (webUnitGroup != null) {

                result.AppendLine(webUnitGroup.GroupName);
                result.AppendLine(webUnitGroup.Description);
                result.AppendLine();

                formattedUnits = Recalculate(unitName, unitValue);

                foreach (FormattedUnit formattedUnit in formattedUnits) {

                    if (formattedUnit.UnitType.GetUnitType() == sourceUnit.Type) {
                        result.AppendFormat("{0}:\t{1}{2}", formattedUnit.FriendlyUnitName, formattedUnit.UnitValue, formattedUnit.UnitSymbol);
                        result.AppendLine();
                    }
                }

                result.AppendLine();
                result.Append("https://www.unitcandy.com?" + unitValue + unitName);
                result.AppendLine();

            } else {

                result.AppendLine("UnitCandy");
                result.AppendLine();
                result.AppendLine("UnitCandy is trying to help you with your everyday unit conversion needs. It contains many units of measurement commonly encountered and converts them to other units fast and accurate. UnitCandy was designed to be nice-looking and user friendly.");
                result.AppendLine();
            }

            return result.ToString();
        }

        /// <summary>
        /// Returns the unit and magnitude parse from inputstring.
        /// If unitName is provided only the magnitude from inputstring is used and the unit is the unitName.
        /// For example: "1.2m" is 1.2 Meters.
        /// For example: "1.2m" and "Fahrenheit" is 1.2 Fahrenheit.
        /// </summary>
        /// <param name="inputstring"></param>
        /// <param name="unitName"></param>
        /// <returns></returns>
        [OperationContract]
        public FormattedUnit FindUnit(string inputstring, string unitName) {

            FormattedUnit result = null;

            if (!string.IsNullOrWhiteSpace(unitName)) {

                string remainder;
                var number = UnitConverterUtility.ParseDouble(inputstring, out remainder);

                if (number.HasValue) {

                    if (string.IsNullOrWhiteSpace(remainder)) {
                        inputstring = inputstring + unitName;

                    } else {
                        inputstring = inputstring.Replace(remainder, unitName);
                    }
                }
            }

            IEnumerable<IUnit> units;
            UnitConverterUtility.TryParseUnits(inputstring, out units);

            if (units != null) {
                if (units.Any()) {

                    result = (FormattedUnit)(Unit)units.First();
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the unit and magnitude from the url.
        /// For example: "https://www.unitcandy.com?1.2m" is 1.2 Meters.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [OperationContract]
        public FormattedUnit FindUnitFromUrl(string url) {

            // from "https://www.unitcandy.com?12.345m"
            // find 12.345 meters

            FormattedUnit result = null;

            int posQuestionMark = url.IndexOf('?');
            int posAmpersand = url.IndexOf('&');

            string inputstring = string.Empty;

            if (posQuestionMark >= 0) {

                if (posAmpersand < 0) {
                    inputstring = url.Substring(posQuestionMark).TrimStart('?');

                } else if (posAmpersand > posQuestionMark) {
                    inputstring = url.Substring(posQuestionMark, posAmpersand - posQuestionMark).TrimStart('?');
                }
            }

            if (!string.IsNullOrWhiteSpace(inputstring)) {
                result = FindUnit(inputstring, null);
            }

            return result;
        }

        /// <summary>
        /// Sends an email to unitcandycom@gmail.com with a message from a user.
        /// Returns an empty string if there were not problems.
        /// Returns an error message otherwise.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [OperationContract]
        public String SendFeedbackMail(string emailAddress, string message) {

            // the message cannot be empty

            if (string.IsNullOrWhiteSpace(message)) {
                return "The message is empty.";
            }

            // if there is an email address its format must be valid

            if (string.IsNullOrEmpty(emailAddress) == false) {

                try {
                    new MailAddress(emailAddress);

                } catch (Exception) {
                    return "The format of the e-mail address is not valid.";
                }
            }

            string result = string.Empty;

            var fromAddr = new MailAddress("unitcandycom@gmail.com", "Alen Milakovic");
            var toAddr = new MailAddress("unitcandycom@gmail.com", "UnitCandy");

            var body = new StringBuilder()
                .AppendFormat("From Email: {0}", emailAddress)
                .AppendLine()
                .Append("Message:")
                .AppendLine()
                .Append(message);

            var smtp = new SmtpClient {
                Host = "smtp.comcast.net",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new Dolaris.Common.Class1().GetNetworkCredential()
            };

            try {
                using (var email = new MailMessage(fromAddr, toAddr)) {

                    email.Subject = "A message from a UnitCandy user.";
                    email.Body = body.ToString();
                    smtp.Send(email);
                    result = string.Empty;
                }

            } catch (Exception ex) {
                Debug.Write(ex.Message);
                result = "Sending the message has failed.";
            }

            return result;
        }


        // Add more operations here and mark them with [OperationContract]


        /// <summary>
        /// Represents a unit of measurement that is formatted for being displayed on a web page.
        /// </summary>
        [DebuggerDisplay("{UnitValue,nq} {UnitName,nq}  (Error: {ErrorString})")]
        public class FormattedUnit {

            /// <summary>
            /// Creates an instance of the RecalculateResult class.
            /// </summary>
            public FormattedUnit() {
                UnitName = string.Empty;
                FriendlyUnitName = string.Empty;
                UnitValue = string.Empty;
                UnitSymbol = string.Empty;
                UnitType = string.Empty;
                ErrorString = string.Empty;
            }


            /// <summary>
            /// Gets or sets the unit name.
            /// For example: "Meter".
            /// </summary>
            public string UnitName { get; set; }

            /// <summary>
            /// Gets or sets the friendly name of the unit.
            /// For example: "Nautical Mile" instead of "NauticalMile".
            /// </summary>
            public string FriendlyUnitName { get; set; }

            /// <summary>
            /// Gets or sets the value of a unit.
            /// For example: "-3.5", "blah blah".
            /// </summary>
            public string UnitValue { get; set; }

            /// <summary>
            /// Gets or sets the symbol of the unit.
            /// For example: "m", "ft".
            /// </summary>
            public string UnitSymbol { get; set; }

            /// <summary>
            /// Gets or sets the name of the type of the unit.
            /// For example: "Length", "Pressure".
            /// </summary>
            public string UnitType { get; set; }

            /// <summary>
            /// Gets or sets the error message of a unit.
            /// For example: "The number is too small".
            /// </summary>
            public string ErrorString { get; set; }


            /// <summary>
            /// Converts a Unit into a RecalculateResult object.
            /// </summary>
            /// <param name="unit"></param>
            public static explicit operator FormattedUnit(Unit unit) {

                var result = new FormattedUnit() {

                    UnitName = unit.ID.ToString(),
                    FriendlyUnitName = unit.Name,
                    UnitSymbol = unit.Symbol,
                    UnitType = unit.Type.ToString()
                };

                string errorString;
                result.UnitValue = unit.GetFormattedString(out errorString);
                result.ErrorString = errorString;

                return result;
            }
        }
    }
}
