using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dolaris.UnitConverter.Web {
    public partial class TestPage : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void buttonWriteToFile_Click(object sender, EventArgs e) {

            //string path = System.IO.Path.Combine(Environment.CurrentDirectory, "unitcandylog.txt");
            //path = @"i:\unitcandylog.txt";
            //System.IO.File.AppendAllText(path, logString + Environment.NewLine);

            string logfile = Server.MapPath("./logfile.txt");

            try {
                //string errorpage = Server.MapPath("./error.aspx");

                var logfileLines = new List<string>();

                if (File.Exists(logfile)) {
                    logfileLines = File.ReadAllLines(logfile).ToList();
                }

                logfileLines.Add(String.Format("This is line {0}. Date: {1}", logfileLines.Count + 1, DateTime.Now.ToString()));

                File.WriteAllLines(logfile, logfileLines, System.Text.Encoding.UTF8);

                labelWriteToFile.Text = string.Format("Writing to '{0}' was successfull. The file has {1} lines.", logfile, logfileLines.Count);

                //var allLines = System.IO.File.ReadAllLines(errorpage);

                //string firstLines = "";
                //allLines.Take(10).Where(p => p.Length > 10).ToList().ForEach(p => firstLines += p.Replace('<','|').Replace('>', '|').Substring(0, 10) + "{end_of_line}");

                ////labelWriteToFile.Text = string.Format("Root Path: '{0}',", Server.MapPath("."));
                //labelWriteToFile.Text = firstLines;

                //// On the server we get this exception:
                ////   Access to the path 'D:\CustomerData\webspaces\webspace_00142242\wwwroot\unitcandy.com\logfile.txt' is denied. (UnauthorizedAccessException).

                //var newlines = new List<string>();
                //newlines.Add("This is line 1");
                //System.IO.File.WriteAllLines(logfile, newlines, System.Text.Encoding.UTF8);


                //throw new System.IO.FileNotFoundException("exption test");

            } catch (Exception ex) {
                labelWriteToFile.Text = string.Format("Writing to '{0}' failed! Exception message and type: '{1}'  ({2}).", logfile, ex.Message, ex.GetType().Name);
            }
        }
    }
}