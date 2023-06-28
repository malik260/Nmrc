using Mortgage.Ecosystem.DataAccess.Layer.Request;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers
{
    // Email Help
    public static class EmailHelper
    {
        // Password email
        public static bool IsPasswordEmailSent(MailParameter user, out string message)
        {
            message = string.Empty;

            var builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(@"Views/Shared/_PasswordTemplate.cshtml"))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("[realname]", user.RealName);
            builder.Replace("[username]", user.UserName);
            builder.Replace("[password]", user.UserPassword);
            builder.Replace("[link]", $"");
            builder.Replace("[usercompany]", user.UserCompany);
            builder.Replace("[company]", GlobalConstant.COMPANY_NAME);
            builder.Replace("[year]", DateTime.Now.Year.ToString());
            builder.Replace("[reserved]", GlobalConstant.RESERVED);

            NetworkCredential credential = new NetworkCredential
            {
                UserName = GlobalConstant.CREDENTIAL_USERNAME,
                Password = GlobalConstant.CREDENTIAL_PASSWORD
            };

            MailMessage mail = new MailMessage
            {
                IsBodyHtml = true,
                From = new MailAddress(GlobalConstant.MAIL_FROM)
            };
            mail.To.Add(user.UserEmail);
            mail.Subject = "Password Notification";
            mail.Body = builder.ToString();

            SmtpClient smtp = new SmtpClient
            {
                Host = GlobalConstant.SMTP_HOST,
                UseDefaultCredentials = false,
                Credentials = credential,
                Port = GlobalConstant.SMTP_PORT,
                EnableSsl = GlobalConstant.SMTP_SSL
            };

            try
            {
                smtp.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (!string.IsNullOrEmpty(e.InnerException.Message))
                    {
                        message = e.InnerException.Message;
                    }
                }
                else
                {
                    message = e.Message;
                }
                return false;
            }
        }
    }
}