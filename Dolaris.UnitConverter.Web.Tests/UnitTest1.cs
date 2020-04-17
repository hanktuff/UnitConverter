using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Dolaris.UnitConverter.Web.Tests {
    [TestClass]
    public class UnitTest1 {

        [TestMethod]
        public void TestMethod1() {
        }

        [TestMethod]
        public void SendMailTest() {

            var fromAddr = new MailAddress("almi21@almiweb.net", "Alen Milakovic");
            var toAddr = new MailAddress("alen@almiweb.net", "UnitCandy");
            var body = new StringBuilder();

            var smtp = _makeSmtpClient("smtp.comcast.net", new Dolaris.Common.Class1().GetNetworkCredential());

            body.AppendFormat("From Email: {0}", fromAddr.Address)
                .AppendLine()
                .AppendLine()
                .AppendLine("Message:")
                .AppendLine()
                .AppendFormat("This is a test message sent on {0}.", smtp.Host);

            using (var email = new MailMessage(fromAddr, toAddr)) {

                email.Body = body.ToString();
                smtp.Send(email);
            }
        }

        private SmtpClient _makeSmtpClient(string host, NetworkCredential credential, int port = 587, bool Ssl = true) {

            return
                new SmtpClient {
                    Host = host,
                    Port = port,
                    EnableSsl = Ssl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = credential
                };
        }
    }
}
