using System;
using System.Net;
using System.Net.Mail;

namespace VivaBillingDesktopApplication
{
    public class VivaEmailServices
    {
        public static void SendEmail(string to, string subject, string body)
        {
           try
            {
                SmtpClient smtp = new SmtpClient();
                string from, username, password;
                from = Program.EmailConfigFromEmailID;
                username = Program.EmailConfigUserName;
                password = Program.EmailConfigPassword;
                using (MailMessage mm = new MailMessage(from, to))
                {
                    mm.From = new MailAddress(from, Program.EmailConfigDisplayName);
                    mm.Subject = subject;
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    smtp.Host = Program.EmailConfigSMTPHost;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new NetworkCredential(username, password); ;
                    smtp.Port = Convert.ToInt32(Program.EmailConfigSMTPPort);
                    smtp.Send(mm);
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}