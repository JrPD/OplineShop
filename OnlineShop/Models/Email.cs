using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace chatdna.Models
{
    public class ResetPasswordModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SendPassRestoreRequest : EmailSender
    {
        public static ResetPasswordModel Request { get; set; }

        public SendPassRestoreRequest(ResetPasswordModel request)
        {
            AddressTo = request.Email;
            Subject = "Restore Password";
            //Body = BuildEmail();
            Body = request.UserName;
            IsHtml = true;
        }

        /// <summary>
        /// build email body
        /// </summary>
        /// <returns>email body</returns>
        private static string BuildEmail()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Register data:");
            sb.AppendLine();

            sb.AppendFormat("Password: {0}", Request.Password);
            sb.AppendLine();
            sb.AppendFormat("Request date: {0} {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
            return sb.ToString();
        }
    }

    public static class SenderData
    {
        public static string SmtpClient
        {
            get { return ConfigurationManager.AppSettings["SmtpClient"]; }
        }

        public static string SentContactsTo
        {
            get { return ConfigurationManager.AppSettings["SentContactsTo"]; }
        }

        public static string Login
        {
            get { return ConfigurationManager.AppSettings["Login"]; }
        }

        public static string Pass
        {
            get { return ConfigurationManager.AppSettings["Pass"]; }
        }
    }

    public abstract class EmailSender
    {
        public string AddressTo { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
        public bool IsHtml { get; set; }

        public bool SendEmail()
        {
            Debug.WriteLine("Begin Sending Email");
            try
            {
                using (var msg = new MailMessage())
                {
                    msg.To.Add(AddressTo);
                    msg.From = new MailAddress(SenderData.SentContactsTo);
                    msg.Subject = Subject;
                    msg.Body = Body;
                    if (IsHtml)
                    {
                        msg.IsBodyHtml = true;
                    }

                    var client = new SmtpClient(SenderData.SmtpClient)
                    {
                        Timeout = 20000,
                        EnableSsl = false,
                        Port = 587,
                        Host = SenderData.SmtpClient,
                        //Setup credentials to login to our sender email address ("UserName", "Password")
                        Credentials = new NetworkCredential(SenderData.Login, SenderData.Pass)
                    };
                    // sending email
                    client.Send(msg);
                    Debug.WriteLine("End Sending Email");
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("ERROR Sending Email");
                return false;
            }
            return true;
        }
    }
}