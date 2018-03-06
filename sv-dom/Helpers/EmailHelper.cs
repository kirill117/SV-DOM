using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Helpers
{
    public static class EmailHelper
    {
        public static bool SendMail(string email, string strMessage = "", string subject = "")
        {
            var result = true;
            try
            {
                var server = ConfigurationManager.AppSettings["MailServer"];
                var user = ConfigurationManager.AppSettings["MailUser"];
                var password = ConfigurationManager.AppSettings["MailPassword"];

                //var server = "smtp.yandex.ru";
                //var user = "kirill117@yandex.ru";
                //var password = "Inga30031";
                //email = "baranovka@quarta.su";

                //SmtpClient client = new SmtpClient(server, 25);
                //client.EnableSsl = true;

                SmtpClient client = new SmtpClient(server, 587);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(user, password);
                MailAddress from = new MailAddress(user, user, System.Text.Encoding.UTF8);
                MailAddress to = new MailAddress(email);
                MailMessage message = new MailMessage(from, to);
                message.Body = strMessage;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;

                client.Send(message);

                message.Dispose();
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
    }
}