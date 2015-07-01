using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;
using System.Web.SessionState;
using System.IO;

namespace PersonalTools
{
    class Logging_Tools
    {
            /// <summary>
            /// Private LogName 
            /// </summary>
            private const string LogName = "REPLACE ME";

            /// <summary>
            /// Private object to lock writing to file
            /// </summary>
            private static readonly object padlock = new object();

            /// <summary>
            /// private Location 
            /// </summary>
            private static string fileloc;

            /// <summary>
            /// Private Log Mode event log or file to save
            /// </summary>
            private enum LogMode
            {
                /// <summary>
                /// Log to file
                /// </summary>
                File,

                /// <summary>
                /// Log to Event Log
                /// </summary>
                Event
            }


            /// <summary>
            /// Private Message Type
            /// </summary>
            private enum MessageType
            {
                /// <summary>
                /// Error MEssage
                /// </summary>
                Error,

                /// <summary>
                /// Information message
                /// </summary>
                Report
            }

            /// <summary>
            /// Gets Private property for email recipiants. Config variable is ErrorEmails 
            /// </summary>
            private static string EmailRecipients
            {
                get
                {
                    if (ConfigurationManager.AppSettings["ErrorEmails"] == null)
                    {
                        return "brokererror@is2.co.uk";
                    }

                    return ConfigurationManager.AppSettings["ErrorEmails"];
                }
            }

            /// <summary>
            /// Gets Private property Application Location. Config key ApplicationName, o/w UnknownApplication
            /// </summary>
            private static string ApplicationName
            {
                get
                {
                    if (ConfigurationManager.AppSettings["ApplicationName"] == null)
                    {
                        if (string.IsNullOrEmpty(fileloc))
                        {
                            return "UnknownApplication";
                        }
                        else
                        {
                            return fileloc;
                        }
                    }

                    return ConfigurationManager.AppSettings["ApplicationName"];
                }
            }

            /// <summary>
            /// Gets Private Property set based on location is set or not. If set file o/w event
            /// </summary>
            private static LogMode Mode
            {
                get
                {
                    if (string.IsNullOrEmpty(fileloc))
                    {
                        return LogMode.Event;
                    }

                    return LogMode.File;
                }
            }

            /// <summary>
            /// Gets private property set based on SMTPHost config key if not set 192.168.0.18 is used
            /// </summary>
            private static string SMTPServer
            {
                get
                {
                    if (ConfigurationManager.AppSettings["SMTPHost"] == null)
                    {
                        return "mail.is2.co.uk";
                    }
                    else
                    {
                        return ConfigurationManager.AppSettings["SMTPHost"];
                    }
                }
            }

            /// <summary>
            /// Gets ErrorFile key from config. O/w empty
            /// </summary>
            private static string ErrorFileLocation
            {
                get
                {
                    if (ConfigurationManager.AppSettings["ErrorFile"] == null)
                    {
                        return String.Empty;
                    }

                    return ConfigurationManager.AppSettings["ErrorFile"];
                }
            }

            /// <summary>
            /// Gets LogFile key from config. O/w empty
            /// </summary>
            private static string LogFileLocation
            {
                get
                {
                    if (ConfigurationManager.AppSettings["LogFile"] == null)
                    {
                        return String.Empty;
                    }

                    return ConfigurationManager.AppSettings["LogFile"];
                }
            }

            /// <summary>
            /// Static Log Errors. option to send emails.
            /// </summary>
            /// <param name="exc">Exception details.</param>
            /// <param name="errorLoc">Description of location.</param>
            /// <param name="sendEmail">Email flag to send email or not. </param>
            public static void LogError(Exception exc, string errorLoc, bool sendEmail)
            {
                fileloc = ErrorFileLocation;
                string errStr = String.Format("[{0} {1}] ", System.DateTime.Now.ToShortDateString(), System.DateTime.Now.ToShortTimeString());
                errStr += String.Format("Error occured in {0} (Source:{1})\n", errorLoc, ApplicationName);
                errStr += exc.ToString();
                WriteToLog(errStr, MessageType.Error);

                if (sendEmail)
                {
                    SendMailReport(errorLoc, exc);
                }
            }

            /// <summary>
            ///  Static Log Errors and Email  details
            /// </summary>
            /// <param name="exc">Exception details</param>
            /// <param name="errorLoc">Error location</param>
            public static void LogError(Exception exc, string errorLoc)
            {
                fileloc = ErrorFileLocation;
                string errStr = String.Format("[{0} {1}] ", System.DateTime.Now.ToShortDateString(), System.DateTime.Now.ToShortTimeString());
                errStr += String.Format("Error occured in {0} (Source:{1})\n", errorLoc, ApplicationName);
                if (exc != null)
                    errStr += exc.ToString();

                WriteToLog(errStr, MessageType.Error);
                SendMailReport(errorLoc, exc);

            }

            /// <summary>
            /// Log Information
            /// </summary>
            /// <param name="message">Message to insert</param>
            public static void LogMessage(string message)
            {
                fileloc = LogFileLocation;
                string msgStr = String.Format("[{0} {1}] ", System.DateTime.Now.ToShortDateString(), System.DateTime.Now.ToShortTimeString());
                msgStr += message + "\n";
                WriteToLog(msgStr, MessageType.Report);
            }

            /// <summary>
            /// Private Email message of the error
            /// </summary>
            /// <param name="location">location description</param>
            /// <param name="exc">Exception details</param>
            private static void SendMailReport(string location, Exception exc)
            {
                //string msg = String.Format("An error has been caught at '{0}' on [{1} : {2}]\r\n\r\n", location.Trim(), System.DateTime.Now.ToShortDateString(), System.DateTime.Now.ToShortTimeString());

                //msg += "The following error was reported:\r\n";

                //Exception logexc = exc;

                //while (logexc != null)
                //{
                //    msg += logexc.ToString() + "\r\n\r\n------------------------------------\r\n\r\n";
                //    logexc = logexc.InnerException;
                //}

                //try
                //{
                //MailMessage eewMessage = new MailMessage();
                //eewMessage.From = new MailAddress("brokererror@is2.co.uk");
                //eewMessage.To.Add(EmailRecipients);
                //eewMessage.Subject = String.Format("Error occurred in client [{0}]", ApplicationName);
                //eewMessage.Body = msg;
                //eewMessage.IsBodyHtml = false;
                //     SmtpClient smtp = new SmtpClient(SMTPServer); // NEED TO REACTIVATE
                //smtp.Send(eewMessage); // NEED TO REACTIVATE
                //}
                //catch (Exception ex)
                //{
                //    LogError(ex, "SendMailReport");
                //}
            }

            /// <summary>
            /// Writes to Error/log file or event log
            /// </summary>
            /// <param name="message">message to log</param>
            /// <param name="logType">Log tye (only for event log) error or information</param>
            private static void WriteToLog(string message, MessageType logType)
            {
                if (Mode == LogMode.File)
                {
                    WriteToLog(message);
                }
                else
                {
                    /* if (logType == MessageType.Error)
                     {
                         EventLog.WriteEntry(ApplicationName, message, EventLogEntryType.Error);
                     }
                     else
                     {
                         EventLog.WriteEntry(ApplicationName, message, EventLogEntryType.Information);
                     }*/
                }
            }

            /// <summary>
            /// Writes to Log/Error file
            /// </summary>
            /// <param name="message">message to log</param>
            private static void WriteToLog(string message)
            {
                if (Mode == LogMode.Event)
                {
                    WriteToLog(message, MessageType.Error);
                }
                try
                {
                    lock (padlock)
                    {
                        using (StreamWriter errorOutput = new StreamWriter(fileloc, true))
                        {
                            errorOutput.WriteLine(message);
                            errorOutput.Flush();
                            errorOutput.Close();
                        }
                        if (System.Web.HttpContext.Current != null)
                            System.Web.HttpContext.Current.Server.ClearError();
                    }
                }
                catch (Exception e)
                {
                    string msg = String.Format("ERROR Handler Write to log Failed. File trying to log was : {0}. Error :{1}", fileloc, e.Message);
                    fileloc = String.Empty;
                    WriteToLog(msg, MessageType.Error);
                }
            }

            public static void LogSessionState(string source)
            {
                if (System.Web.HttpContext.Current == null)
                    return;
                HttpSessionState Session = System.Web.HttpContext.Current.Session;

                StringBuilder sBuilder = new StringBuilder(source + ":");
                sBuilder.Append(Environment.NewLine);

                sBuilder.Append("*** Session Data Dump ***");
                sBuilder.Append(Environment.NewLine);

                sBuilder.Append("Session ID: ");
                sBuilder.Append(Session.SessionID);
                sBuilder.Append(Environment.NewLine);

                sBuilder.Append("Is Session Read Only: ");
                sBuilder.Append(Session.IsReadOnly);
                sBuilder.Append(Environment.NewLine);

                sBuilder.Append("Session Values");
                sBuilder.Append(Environment.NewLine);

                foreach (String s in Session.Keys)
                {
                    sBuilder.Append(string.Format("Key: {0}, Value: {1}", s, Session[s].ToString()));
                    sBuilder.Append(Environment.NewLine);
                }
                sBuilder.Append("*** End Session Data Dump ***");
                WriteToLog(sBuilder.ToString(), MessageType.Report);
            }

            /// <summary>
            /// Check the event source
            /// </summary>
            private static void CheckEventSource()
            {
                if (!EventLog.SourceExists(ApplicationName))
                {
                    EventLog.CreateEventSource(ApplicationName, "Application");
                }
            }
        
    }
}
