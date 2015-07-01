using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Text;
//using System.Net.Mail;
//using System.Threading.Tasks;
//using System.Collections;
using System.Collections.Generic;
//using System.Collections.Specialized;
using System.Configuration;
using System.Web.SessionState;
using System.Diagnostics;

namespace PersonalTools
{
    public class EmailNotification
    {
        /// <summary>
        /// Gets the sender email.
        /// </summary>
        private MailAddress SenderEmail
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SenderEmail"]))
                {
                    if (string.IsNullOrEmpty(this.displayName))
                    {
                        return new MailAddress("support@isisbroker.co.uk");
                    }
                    return new MailAddress("support@isisbroker.co.uk", this.displayName);
                }
                if (string.IsNullOrEmpty(this.displayName))
                {
                    return new MailAddress(ConfigurationManager.AppSettings["SenderEmail"]);
                }
                return new MailAddress(ConfigurationManager.AppSettings["SenderEmail"], this.displayName);
            }
        }

        private string displayName;
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName
        {
            get
            {
                return displayName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ClientDisplayNameInEmail"]))
                    {
                        displayName = string.Empty;
                    }
                    displayName = ConfigurationManager.AppSettings["ClientDisplayNameInEmail"];
                }
                else
                {
                    displayName = value;
                }
            }
        }

        /// <summary>
        /// Gets the SMTP client.
        /// </summary>
        private SmtpClient SmtpClient
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SMTPHost"]))
                {
                    return new SmtpClient("mail.is2.co.uk");
                }
                return new SmtpClient(ConfigurationManager.AppSettings["SMTPHost"]);
            }
        }

        /// <summary>
        /// Gets or sets the mail MSG.
        /// </summary>
        /// <value>
        /// The mail MSG.
        /// </value>
        private MailMessage MailMsg
        {
            get
            {
                MailMessage m = new MailMessage();
                m.From = this.SenderEmail;
                if (!string.IsNullOrEmpty(this.Subject))
                    m.Subject = this.Subject.Trim();
                if (this.AddTo.Contains(";"))
                {
                    SplitAddresses(MailFieldType.MailTO, this.AddTo, m);
                }
                else
                    m.To.Add(this.AddTo.Trim());
                if (!string.IsNullOrEmpty(this.AddBcc))
                {
                    if (this.AddBcc.Contains(";"))
                    {
                        SplitAddresses(MailFieldType.MailBCC, this.AddBcc, m);
                    }
                    else
                    {
                        m.Bcc.Add(this.AddBcc.Trim());
                    }
                }
                if (!string.IsNullOrEmpty(this.AddCc))
                {
                    if (this.AddCc.Contains(";"))
                    {
                        SplitAddresses(MailFieldType.MailCC, this.AddCc, m);
                    }
                    else
                    {
                        m.CC.Add(this.AddCc.Trim());
                    }
                }
                if (!string.IsNullOrEmpty(this.Body))
                    m.Body = this.Body;
                if (Attachments != null && this.Attachments.Count > 0)
                {
                    foreach (Attachment att in Attachments)
                    {
                        m.Attachments.Add(att);
                    }
                }
                m.BodyEncoding = Encoding.UTF8;
                if (this.IsHtml)
                    m.IsBodyHtml = true;
                else
                    m.IsBodyHtml = false;
                if (this.IsHighPriority)
                    m.Priority = MailPriority.High;
                else
                    m.Priority = MailPriority.Normal;
                return m;
            }
            set { this.MailMsg = value; }
        }

        /// <summary>
        /// attach documents to the email
        /// </summary>
        /// <value>
        /// The attachments.
        /// </value>
        public List<Attachment> Attachments { get; set; }

        /// <summary>
        /// Carbon copy email string comma seperated if more than one address
        /// </summary>
        /// <value>
        /// The add cc.
        /// </value>
        public string AddCc { get; set; }

        /// <summary>
        /// Blind copy email string comma seperated if more than one address
        /// </summary>
        /// <value>
        /// The add BCC.
        /// </value>
        public string AddBcc { get; set; }

        /// <summary>
        /// Recipient Email address
        /// </summary>
        /// <value>
        /// The add to.
        /// </value>
        public string AddTo { get; set; }

        //email subject
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// Body of the email
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public string Body { get; set; }

        /// <summary>
        /// is this email html or plain text
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is HTML; otherwise, <c>false</c>.
        /// </value>
        public bool IsHtml { get; set; }

        /// <summary>
        /// mark the email as high priority or normal
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is high priority; otherwise, <c>false</c>.
        /// </value>
        public bool IsHighPriority { get; set; }

        /// <summary>
        /// Gets Generic Email Template to String
        /// </summary>
        private string EmailTemplate
        {
            get
            {
                try
                {
                    if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["GenericEmailTemplate"])))
                    {
                        Logging_Tools.LogMessage("Failed to find generic email template");
                        return string.Empty;
                    }

                    using (StreamReader sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["GenericEmailTemplate"])))
                    {
                        return sr.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    Logging_Tools.LogError(ex, "EmailTemplate");
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailNotification"/> class.
        /// </summary>
        public EmailNotification() { this.Attachments = new List<Attachment>(); }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailNotification"/> class.
        /// </summary>
        /// <param name="displayname">The displayname.</param>
        /// <param name="cc">The cc.</param>
        /// <param name="bcc">The BCC.</param>
        public EmailNotification([Optional, DefaultParameterValue("")] string displayname,
            [Optional, DefaultParameterValue("")] string cc, [Optional, DefaultParameterValue("")] string bcc)
        {
            this.DisplayName = displayName;
            this.AddCc = cc;
            this.AddBcc = bcc;
        }

        /// <summary>
        /// Send a plain text payment completion email
        /// </summary>
        /// <param name="pessageParameters">The pessage parameters.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="toAdd">To add.</param>
        public void PlainTextPaymentNotification(Dictionary<string, string> pessageParameters, [Optional, DefaultParameterValue("")]string subject,
            [Optional, DefaultParameterValue("")] string toAdd)
        {
            this.AddTo = toAdd;

            StringBuilder sb = new StringBuilder("Notification Details\r\n\r\n");
            foreach (var p in pessageParameters)
            {
                sb.Append(p.Key + " :" + p.Value + "\r\n\r");
            }
            sb.Append("Thank You\r\n\r\nIS2 Support");

            MailMessage m = this.MailMsg;
            m.Body = sb.ToString();
            m.Subject = string.IsNullOrEmpty(subject) ? "Payment Notification" : subject;

            try
            {
                this.SmtpClient.Send(m);
            }
            catch (Exception e)
            {
                Logging_Tools.LogError(e, "Payment Notification Failed");
            }
        }


        /// <summary>
        /// Send a plain text notification email
        /// </summary>
        /// <param name="pessageParameters">The pessage parameters.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="toAdd">To add.</param>
        public void PlainTextNotification(Dictionary<string, string> pessageParameters, [Optional, DefaultParameterValue("")]string subject,
            [Optional, DefaultParameterValue("")] string toAdd)
        {
            this.AddTo = toAdd;

            StringBuilder sb = new StringBuilder("Notification Details\r\n\r\n");
            foreach (var p in pessageParameters)
            {
                sb.Append(p.Key + " :" + p.Value + "\r\n\r");
            }
            sb.Append("Thank You\r\n\r\nIS2 Support");

            MailMessage m = this.MailMsg;
            m.Body = sb.ToString();
            
            m.Subject = string.IsNullOrEmpty(subject) ? "Notification" : subject;

            try
            {
                this.SmtpClient.Send(m);
            }
            catch (Exception e)
            {
                Logging_Tools.LogError(e, "Notification Failed");
            }
        }

        /// <summary>
        /// send an email notification
        /// </summary>
        public void SendEmail()
        {
            try
            {
                lock (this)
                {
                    this.SmtpClient.Send(MailMsg);
                }
            }
            catch (System.Exception exc)
            {
                Logging_Tools.LogError(exc, "Send Email");
            }
        }

        /// <summary>
        /// Splits the addresses.
        /// </summary>
        /// <param name="MailAddressType">Type of the mail address.</param>
        /// <param name="MailAddressCollection">The mail address collection.</param>
        /// <param name="MailItem">The mail item.</param>
        public static void SplitAddresses(MailFieldType MailAddressType, string MailAddressCollection, MailMessage MailItem)
        {
            foreach (string str in MailAddressCollection.Trim().Split(new char[] { ';' }))
            {
                if (str.Trim() != "")
                {
                    switch (MailAddressType)
                    {
                        case MailFieldType.MailTO:
                            MailItem.To.Add(str.Trim());
                            break;

                        case MailFieldType.MailCC:
                            MailItem.CC.Add(str.Trim());
                            break;

                        case MailFieldType.MailBCC:
                            MailItem.Bcc.Add(str.Trim());
                            break;

                        case MailFieldType.Attachment:
                            {
                                Attachment item = new Attachment(str.Trim());
                                MailItem.Attachments.Add(item);
                                break;
                            }
                    }
                }
            }
        }

        // Nested Types
        public enum MailFieldType
        {
            Attachment = 4,
            MailBCC = 3,
            MailCC = 2,
            MailTO = 1
        }
    }
    public class EmailModel
    {
        public string SendNotificationEmail()//Policy oPolicy)
        {
            try
            {
                   var email = new EmailNotification();
                var body = new Dictionary<string, string>();

                string sSubject = "New Policy (Reference:  )";

                body.Add("Policy Details", "");
                // body.Add("Policy Reference", oPolicy.ProposalRef);
                body.Add("Policy Date and Time", DateTime.Now.ToString());

                string TitleAsString = "";
                try
                {
                    //       TitleAsString = entitys.TitleLookups.AsNoTracking().Where(x => x.ID == oPolicy.TitleID.Value).SingleOrDefault().Title;
                }
                catch
                { }
                 body.Add("Name", TitleAsString);

                //if (!String.IsNullOrEmpty(oPolicy.EquityName))
                //  {
                 body.Add("Equity Name", "Name");
                // }

                //  if (!String.IsNullOrEmpty(oPolicy.EquityMemNo))
                //   {
                //       body.Add("Equity Member Number", oPolicy.EquityMemNo);
                //   }

                //   body.Add("Phone", oPolicy.PhoneNo);
                //   body.Add("PhoneAlt ", oPolicy.PhoneNoAlt);
                //   body.Add("Email", oPolicy.EmailAddress);


                //   body.Add("Policy", oPolicy.CoverType);


                //   if (oPolicy.Total.HasValue)
                //   {
                //    body.Add("Premium", "£" + oPolicy.Total.Value.ToString());
                //  }

                //    if (oPolicy.BasicPremiumIPT.HasValue)
                //    {
                //        body.Add("IPT", string.Format("{0} (@ {1}%)", "£" + oPolicy.BasicPremiumIPT.Value.ToString(), "5"));
                //    }

                //     body.Add("Period of Cover", "Annual");

                //    if (oPolicy.PolStartDate.HasValue)
                //    {
                //        body.Add("Start Date", oPolicy.PolStartDate.Value.ToString("dd/MM/yyyy"));
                //        }

                //        if (oPolicy.PolEndDate.HasValue)
                //        {
                //            body.Add("End Date", oPolicy.PolEndDate.Value.ToString("dd/MM/yyyy"));
                //        }

                //        body.Add("Occupation list", "");
                //        body.Add("", oPolicy.ADDOccupationList);

                //        //    if (listInsuredPerson != null)

            string BCC=     ConfigurationManager.AppSettings["EmailNotificationBCC"];
            email.PlainTextNotification(body, sSubject, BCC);
                      return "Sucess";
            }
            catch (Exception ex)
            {
                
                  Logging_Tools.LogError(ex, "Failed to send client quote notification email");
                  return ex.ToString() + " Failed to send client quote notification email";
            }
        }

        public string SendEmailObject(object EmailOb)//Policy oPolicy)
        {
            /// <summary>
            /// This is a general email function for any object placed into it all the information will be emailed out.
            /// </summary>
            /// <param name="SaveItem"></param>
            /// <returns></returns>
            try
            {
                var email = new EmailNotification();
                var body = new Dictionary<string, string>();
                string sSubject = "New Policy (Reference:  )";
                foreach (var prop in EmailOb.GetType().GetProperties()) // Adds all strings into email values
                {
                    var aValue = EmailOb.GetType().GetProperty(prop.Name).GetValue(EmailOb, null);
                    if (prop.PropertyType.Name.Equals("String"))
                    {
                        body.Add(prop.Name, (string)aValue);
                    }
                }
                body.Add("Date and Time", DateTime.Now.ToString());
                string BCC = ConfigurationManager.AppSettings["EmailNotificationBCC"];
                email.PlainTextNotification(body, sSubject, BCC);
                return "Sucess";
            }
            catch (Exception ex)
            {

                Logging_Tools.LogError(ex, "Failed to send client quote notification email");
                return ex.ToString() + " Failed to send client quote notification email";
            }
        }
    }
}
