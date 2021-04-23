using System.Collections.Generic;
using System.Diagnostics;

namespace Dolaris.UnitConverter {
    public class UnitString {

        [DebuggerDisplay("{Text}     ({IsNumeric})")]
        public struct Element {

            public string Text;
            public bool IsNumeric;

            public static Element CreateNumeric(string text) {
                return new Element { Text = text, IsNumeric = true };
            }

            public static Element CreateNonNumeric(string text) {
                return new Element { Text = text, IsNumeric = false };
            }
        }


        public IEnumerable<UnitString.Element> Split(string s) {

            var elements = new List<UnitString.Element>();

            while (string.IsNullOrWhiteSpace(s) == false) {

                bool foundNumeric = TryGetFirstNumericString(s, out string n);

                if (foundNumeric) {

                    int pos = s.IndexOf(n);

                    if (pos > 0) {
                        elements.Add(Element.CreateNonNumeric(s.Substring(0, pos)));
                    }

                    elements.Add(Element.CreateNumeric(s.Substring(pos, n.Length)));

                    s = s.Substring(pos + n.Length);

                } else {

                    elements.Add(Element.CreateNonNumeric(s));
                    break;
                }
            }

            return elements;
        }

        /// <summary>
        /// Tries finding the first string that is a number in the provided string.
        /// If found returns True.
        /// For example: in "a33bc5.4 qwerty-7e-2xyz" finds "33".
        /// </summary>
        /// <param name="s"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private bool TryGetFirstNumericString(string s, out string n) {

            n = null;

            for (int i = 0; i < s.Length; i++) {

                for (int j = s.Length - i; j >= 0; j--) {

                    string substring = s.Substring(i, j);
                    bool isNumeric = double.TryParse(substring, out double number);

                    if (isNumeric) {

                        n = substring.Trim(' ', ',');
                        return true;
                    }
                }
            }

            return false;
        }
    }

}
