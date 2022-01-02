using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Riddhasoft.Services.Common
{
    public class MailCommon
    {
        public MailMessage CreateMessage(MailAddress FromMailAddress, MailAddress ToMailAddress, string Subject, string Body, string AttachmentPath)
        {
            MailMessage msgMail = new MailMessage();

            msgMail.From = FromMailAddress;
            msgMail.To.Add(ToMailAddress);
            msgMail.Body = Body;
            msgMail.IsBodyHtml = true;
            msgMail.Attachments.Add(new Attachment(AttachmentPath));
            return msgMail;
        }
        public MailMessage CreateMessage(MailAddress FromMailAddress, MailAddress ToMailAddress, string Subject, string Body)
        {
            MailMessage msgMail = new MailMessage();

            msgMail.From = FromMailAddress;
            msgMail.To.Add(ToMailAddress);
            msgMail.Body = Body;
            msgMail.IsBodyHtml = true;
            return msgMail;
        }
        public MailMessage CreateMessage(MailAddress FromMailAddress, MailAddress ToMailAddress, string Subject, string Body, Attachment attachment)
        {
            MailMessage msgMail = new MailMessage();

            msgMail.From = FromMailAddress;
            msgMail.To.Add(ToMailAddress);
            msgMail.Body = Body;
            msgMail.IsBodyHtml = true;
            msgMail.Attachments.Add(attachment);
            return msgMail;
        }
        //private bool SendEmail(MailMessage msgMail, NetworkCredential cred)
        //{  
        //    #region smtp
        //    SmtpClient mailClient;
        //    smtpHost host = smtpHost.webMail;
        //    string hostName = "";

        //    hostName = GetValueAsString(host);
        //    mailClient = new SmtpClient(hostName, 587);

        //    mailClient.UseDefaultCredentials = false;
        //    mailClient.EnableSsl = true;
        //    mailClient.Credentials = cred;
        //    #endregion

        //    try
        //    {
        //        mailClient.Send(msgMail);
        //    }
        //    catch (System.IO.IOException ex)
        //    {

        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        //do something if required
        //    }
        //    return true;

        //}

        private bool SendEmail(MailMessage msgMail, NetworkCredential cred)
        {
            #region smtp
            SmtpClient mailClient;
            smtpHost host = smtpHost.webMail;
            string hostName = "";


            hostName = GetValueAsString(host);
            mailClient = new SmtpClient(hostName, 587);
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            mailClient.UseDefaultCredentials = false;
            mailClient.EnableSsl = true;
            mailClient.Credentials = cred;
            #endregion

            try
            {
                mailClient.Send(msgMail);
            }
            catch (System.IO.IOException ex)
            {

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                //do something if required
            }
            return true;

        }

        public void SendMail(string receiver, string subject, string message)
        {



            MailAddress from = new MailAddress(EmailInfo.GmailUserName.GetAppSettingValue());
            NetworkCredential cred = new NetworkCredential(EmailInfo.GmailUserName.GetAppSettingValue(), EmailInfo.GmailPassword.GetAppSettingValue());

            MailAddress to = new MailAddress(receiver);

            var mailMsg = CreateMessage(from, to, subject, message);

            mailMsg.Subject = string.Format(subject);
            SendEmail(mailMsg, cred);
            mailMsg.Dispose();

        }
        public void SendMail(string receiver, string subject, string message, string sender, string password)
        {

            MailAddress from = new MailAddress(sender);
            NetworkCredential cred = new NetworkCredential(sender, password);
            MailAddress to = new MailAddress(receiver);
            var mailMsg = CreateMessage(from, to, subject, message);

            mailMsg.Subject = string.Format(subject);
            SendEmail(mailMsg, cred);
            mailMsg.Dispose();
        }
        public static string GetValueAsString(smtpHost host)
        {
            // get the field 
            var field = host.GetType().GetField(host.ToString());
            var customAttributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (customAttributes.Length > 0)
            {
                return (customAttributes[0] as DescriptionAttribute).Description;
            }
            else
            {
                return host.ToString();
            }
        }
    }
    public enum smtpHost
    {
        [Description("smtp.gmail.com")]
        gmail = 0,
        [Description("smtp.gmail.com")]
        live = 1,
        [Description("smtp.mail.yahoo.com")]
        yahoo = 2,
        [Description("smtp.aim.com")]
        aim = 3,
        [Description("mail.pronaati.com.au")]
        webMail = 4
    }

    public static class EmailInfo
    {

        public static string GetAppSettingValue(this string key)
        {
            string value = System.Configuration.ConfigurationManager.AppSettings[key];
            return value;
        }
        public static string GmailUserName { get { return "GmailUserName"; } }
        public static string GmailPassword { get { return "GmailPassword"; } }
    }

}
