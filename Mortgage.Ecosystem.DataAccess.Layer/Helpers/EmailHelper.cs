using Mortgage.Ecosystem.DataAccess.Layer.Request;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Mortgage.Ecosystem.DataAccess.Layer.Helpers
{
    // Email Help
    public static class EmailHelper
    {
        // new registered employee
        public static bool IsEmployeeApprovalRegistrationSent(MailParameter user, out string message)
        {
            message = string.Empty;

            var builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(@"Views/Shared/_EmployeeRegistrationTemplate.cshtml"))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("[realname]", user.RealName);
            builder.Replace("[username]", user.UserName);
            builder.Replace("[registrationapprover]", user.RegistrationApprover);
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


        //Loan RejecttionMail
        public static bool IsLoanRejecttionMailSent(MailParameter user, out string message)
        {
            message = string.Empty;

            var builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(@"Views/Shared/_LoanRejectionTemplate.cshtml"))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("[realname]", user.RealName);
            builder.Replace("[username]", user.UserName);
            builder.Replace("[remark]", user.Remark);
            builder.Replace("[registrationapprover]", user.RegistrationApprover);
            builder.Replace("[link]", $"");
            builder.Replace("[usercompany]", user.UserCompany);
            builder.Replace("[companyMail]", user.COmpanyMail);
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
            mail.Subject = "Loan Disapproval";
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


        //loan review approval

        public static bool IsLoanReviewApprovalMailSent(MailParameter user, out string message)
        {
            message = string.Empty;

            var builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(@"Views/Shared/_LoanReviewApprovalTemplate.cshtml"))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("[realname]", user.RealName);
            builder.Replace("[username]", user.UserName);
            builder.Replace("[registrationapprover]", user.RegistrationApprover);
            builder.Replace("[link]", $"");
            builder.Replace("[usercompany]", user.UserCompany);
            builder.Replace("[companyMail]", user.COmpanyMail);
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
            mail.Subject = "Loan Approval";
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


        // Send Loan To credit

        public static bool IsLoanpprovalToCreditMailSent(MailParameter user, out string message)
        {
            message = string.Empty;

            var builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(@"Views/Shared/_SendLoanToCreditTemplate.cshtml"))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("[realname]", user.RealName);
            builder.Replace("[username]", user.UserName);
            builder.Replace("[registrationapprover]", user.RegistrationApprover);
            builder.Replace("[link]", $"");
            builder.Replace("[usercompany]", user.UserCompany);
            builder.Replace("[companyMail]", user.COmpanyMail);
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
            mail.Subject = "Loan Approval";
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

        //user token
        public static bool IsUserTokenSent(MailParameter user, out string message)
        {
            message = string.Empty;

            var builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(@"Views/Shared/_UserTokenTemplate.cshtml"))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("[token]", user.UserToken);
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
            mail.Subject = "Password Reset";
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



        // newl registered company
        public static bool IsCompanyApprovalRegistrationSent(MailParameter user, out string message)
        {
            message = string.Empty;

            var builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(@"Views/Shared/_CompanyRegistrationTemplate.cshtml"))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("[realname]", user.RealName);
            builder.Replace("[newcompany]", user.NewCompany);
            builder.Replace("[registrationapprover]", user.RegistrationApprover);
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

        //Contribution EMAIL

        public static bool IsContributionMailSent(MailParameter user, out string message)
        {
            message = string.Empty;

            var builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(@"Views/Shared/_ContributionNotificationTemplate.cshtml"))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("[realname]", user.RealName);
            builder.Replace("[username]", user.UserName);
            builder.Replace("[password]", user.UserPassword);
            builder.Replace("[nhfnumber]", user.NhfNumber);
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
            mail.Subject = "Loan Eligibility Notification";
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



        //Registration Rejection Email

        public static bool IsRegistrationRejectionMailSent(MailParameter user, out string message)
        {
            message = string.Empty;

            var builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(@"Views/Shared/_RejectionTemplate.cshtml"))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("[realname]", user.RealName);
            builder.Replace("[remark]", user.Remark);
            builder.Replace("[username]", user.UserName);
            builder.Replace("[password]", user.UserPassword);
            builder.Replace("[nhfnumber]", user.NhfNumber);
            builder.Replace("[approveremail]", user.ApproverEmail);
            builder.Replace("[link]", $"");
            builder.Replace("[usercompany]", user.UserCompany);
            builder.Replace("[company]", GlobalConstant.COMPANY_NAME);
            builder.Replace("[year]", DateTime.Now.Year.ToString());
            builder.Replace("[reserved]", GlobalConstant.RESERVED);
            builder.Replace("[UserEmail]", user.UserEmail);

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
            mail.Subject = "Customer Registration";
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
            builder.Replace("[nhfnumber]", user.NhfNumber);
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
        // Approval Setup Email
        public static bool IsApprovalSetupSent(List<MailParameter> approvals, out string message)
        {
            message = string.Empty;
            var sent = false;
            var repeat = true;
            var counter = GlobalConstant.ZERO;

            foreach (var approval in approvals)
            {
                while (repeat && counter < approvals.Count)
                {
                    var builder = new StringBuilder();
                    using (StreamReader reader = new StreamReader(@"Views/Shared/_ApprovalTemplate.cshtml"))
                    {
                        builder.Append(reader.ReadToEnd());
                    }

                    builder.Replace("[realname]", approval.RealName);
                    builder.Replace("[usercompany]", approval.UserCompany);
                    builder.Replace("[process]", approval.ProcessName);
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
                    mail.To.Add(approval.UserEmail);
                    mail.Subject = "Approval Setup Notification";
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
                        sent = true;
                        repeat = sent;
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
                        sent = false;
                        repeat = sent;
                    }
                    counter++;
                }
            }
            return sent;
        }

        public static bool IsETicketEmailSent(MailParameter user, out string message)
        {
            message = string.Empty;

            var builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(@"Views/Shared/_ETicketApprovalTemplate.cshtml"))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("[realname]", user.RealName);
            builder.Replace("[username]", user.UserName);
            builder.Replace("[messagetype]", user.MessageType);
            builder.Replace("[ticketnumber]", user.TicketNumber);
            //builder.Replace("[password]", user.UserPassword);
            //builder.Replace("[link]", $"");
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
            mail.Subject = "Customer Support Request";
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

        public static bool IsETicketPending(MailParameter user, out string message)
        {
            message = string.Empty;

            var builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(@"Views/Shared/_ETicketPendingTemplate.cshtml"))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("[contactperson]", user.ContactPerson);
            builder.Replace("[requestnumber]", user.RequestNumber);
            builder.Replace("[messagetype]", user.MessageType);
            builder.Replace("[message]", user.Message);
            builder.Replace("[nhfnumber]", user.NhfNumber);
            builder.Replace("[subject]", user.Subject);
            builder.Replace("[link]", $"");
            builder.Replace("[usercompany]", user.UserCompany);
            builder.Replace("[company]", GlobalConstant.COMPANY_NAME);
            builder.Replace("[year]", DateTime.Now.Year.ToString());
            builder.Replace("[reserved]", GlobalConstant.RESERVED);
            builder.Replace("[useremail]", user.UserEmail);
            builder.Replace("[datesubmitted]", user.DateSubmitted);

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
            mail.To.Add(user.ContactPersonEmail);
            mail.Subject = user.MessageType + ' ' + "Ticket" + '-' + "In-Progress";
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

        public static bool IsETicketClosed(MailParameter user, out string message)
        {
            message = string.Empty;

            var builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(@"Views/Shared/_ETicketClosedTemplate.cshtml"))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("[contactperson]", user.ContactPerson);
            builder.Replace("[requestnumber]", user.RequestNumber);
            builder.Replace("[messagetype]", user.MessageType);
            builder.Replace("[message]", user.Message);
            builder.Replace("[nhfnumber]", user.NhfNumber);
            builder.Replace("[subject]", user.Subject);
            builder.Replace("[link]", $"");
            builder.Replace("[usercompany]", user.UserCompany);
            builder.Replace("[company]", GlobalConstant.COMPANY_NAME);
            builder.Replace("[year]", DateTime.Now.Year.ToString());
            builder.Replace("[reserved]", GlobalConstant.RESERVED);
            builder.Replace("[useremail]", user.UserEmail);
            builder.Replace("[datesubmitted]", user.DateSubmitted);

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
            mail.To.Add(user.ContactPersonEmail);
            mail.Subject = user.MessageType + ' ' + "Ticket" + '-' + "Closed";
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

        public static bool IsContactPmbSent(MailParameter user, out string message)
        {
            message = string.Empty;

            var builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(@"Views/Shared/_PmbContactTemplate.cshtml"))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("[realname]", user.RealName);
            builder.Replace("[username]", user.UserName);
            builder.Replace("[pmbname]", user.PmbName);
            builder.Replace("[contactperson]", user.ContactPerson);
            //builder.Replace("[password]", user.UserPassword);
            //builder.Replace("[link]", $"");
            //builder.Replace("[usercompany]", user.UserCompany);
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
            mail.To.Add(user.ContactPersonEmail);
            mail.Subject = "Customer Support Request";
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


        public static bool IsSuccessfulLoanEmail(MailParameter user, out string message)
        {
            message = string.Empty;

            var builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(@"Views/Shared/_SuccessfulLoanInitiationTemplate.cshtml"))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("[realname]", user.RealName);
            builder.Replace("[username]", user.UserName);
            builder.Replace("[contactperson]", user.ContactPerson);
            //builder.Replace("[password]", user.UserPassword);
            //builder.Replace("[link]", $"");
            //builder.Replace("[usercompany]", user.UserCompany);
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
            mail.Subject = "Customer Loan Application";
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


        //ETicket Email

        public static bool IsETicketSent(MailParameter user, out string message)
        {
            message = string.Empty;

            var builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(@"Views/Shared/_ETicketTemplate.cshtml"))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("[eticketapprover]", user.EticketApprover);
            builder.Replace("[requestnumber]", user.RequestNumber);
            builder.Replace("[messagetype]", user.MessageType);
            builder.Replace("[message]", user.Message);
            builder.Replace("[nhfnumber]", user.NhfNumber);
            builder.Replace("[subject]", user.Subject);
            builder.Replace("[username]", user.UserName);
            builder.Replace("[userphonenumber]", user.UserPhoneNumber);
            builder.Replace("[link]", $"");
            builder.Replace("[usercompany]", user.UserCompany);
            builder.Replace("[company]", GlobalConstant.COMPANY_NAME);
            builder.Replace("[year]", DateTime.Now.Year.ToString());
            builder.Replace("[reserved]", GlobalConstant.RESERVED);
            builder.Replace("[useremail]", user.UserEmail);
            builder.Replace("[datesubmitted]", user.DateSubmitted);
            builder.Replace("[employeename]", user.EmployeeName);

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
            mail.To.Add(user.ApproverEmail);
            mail.Subject = user.MessageType + ' ' + "Ticket";
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


        //Feedback Email

        public static bool IsFeedbackSent(MailParameter user, out string message)
        {
            message = string.Empty;

            var builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(@"Views/Shared/_FeedbackTemplate.cshtml"))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("[eticketapprover]", user.EticketApprover);
            builder.Replace("[message]", user.Message);
            builder.Replace("[subject]", user.Subject);
            builder.Replace("[username]", user.UserName);
            builder.Replace("[link]", $"");
            builder.Replace("[usercompany]", user.UserCompany);
            builder.Replace("[company]", GlobalConstant.COMPANY_NAME);
            builder.Replace("[year]", DateTime.Now.Year.ToString());
            builder.Replace("[reserved]", GlobalConstant.RESERVED);
            builder.Replace("[useremail]", user.UserEmail);

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
            mail.To.Add(user.ApproverEmail);
            mail.Subject = "Feedback on Mortgage Ecosystem Product/Service" + '-' + user.Subject;
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

        // Approved Profile Update email
        public static bool IsApprovedCustomerUpdateEmailSent(MailParameter user, out string message)
        {
            message = string.Empty;

            var builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(@"Views/Shared/_EmployeeUpdateApprovedTemplat.cshtml"))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("[realname]", user.RealName);
            builder.Replace("[username]", user.UserName);
            builder.Replace("[password]", user.UserPassword);
            builder.Replace("[nhfnumber]", user.NhfNumber);
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
            mail.Subject = "Profile Update Request - Approved";
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

        //Customer Profile Update Rejection Email

        public static bool IsCustomerUpdateRejectionMailSent(MailParameter user, out string message)
        {
            message = string.Empty;

            var builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(@"Views/Shared/_UpdateRejectionTemplate.cshtml"))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("[realname]", user.RealName);
            builder.Replace("[remark]", user.Remark);
            builder.Replace("[username]", user.UserName);
            builder.Replace("[password]", user.UserPassword);
            builder.Replace("[nhfnumber]", user.NhfNumber);
            builder.Replace("[approveremail]", user.RegistrationApprover);
            builder.Replace("[link]", $"");
            builder.Replace("[usercompany]", user.UserCompany);
            builder.Replace("[company]", GlobalConstant.COMPANY_NAME);
            builder.Replace("[year]", DateTime.Now.Year.ToString());
            builder.Replace("[reserved]", GlobalConstant.RESERVED);
            builder.Replace("[UserEmail]", user.UserEmail);

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
            mail.Subject = "Profile Update Request – Not Approved";
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

        // Employee Profile Update
        public static bool IsEmployeeUpdateApprovalSent(MailParameter user, out string message)
        {
            message = string.Empty;

            var builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(@"Views/Shared/_EmployeeUpdateTemplate.cshtml"))
            {
                builder.Append(reader.ReadToEnd());
            }

            builder.Replace("[realname]", user.RealName);
            builder.Replace("[username]", user.UserName);
            builder.Replace("[registrationapprover]", user.RegistrationApprover);
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
            mail.Subject = "Profile Update Approval Required";
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