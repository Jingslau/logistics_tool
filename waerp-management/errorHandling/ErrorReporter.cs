using System;
using System.Net.Mail;
using waerp_management.config.SettingsStore;
using waerp_management.main;
using waerp_management.models;
using waerp_management.sql;

namespace waerp_management.errorHandling
{
    internal class ErrorReporter
    {
        public static bool SendErrorReport(string ErrorType, DateTime ErrorTime, string ErrorLog)
        {
            try
            {

                string activeUser = string.Empty;

                if (MainWindowViewModel.username == "")
                {
                    activeUser = "NONE - Loginmask";
                }
                else
                {
                    activeUser = MainWindowViewModel.username;
                }

                System.Net.Mail.SmtpClient mySmtpClient = new System.Net.Mail.SmtpClient();

                mySmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mySmtpClient.UseDefaultCredentials = false;
                mySmtpClient.Credentials = new System.Net.NetworkCredential("dominik@waerp.software", "050582496031dDp!");
                mySmtpClient.Port = 587;
                mySmtpClient.Host = "smtp.strato.de";
                mySmtpClient.EnableSsl = true;

                // set smtp-client with basicAuthentication

                // add from,to mailaddresses
                MailAddress from = new MailAddress("dominik@waerp.software", CompanySettings.Default.CompanyName);
                MailAddress to = new MailAddress("dominik@waerp.software", "ERROR REPORT");
                System.Net.Mail.MailMessage myMail = new System.Net.Mail.MailMessage(from, to);

                // add ReplyTo
                MailAddress replyTo = new MailAddress("dominik@waerp.software");
                myMail.ReplyToList.Add(replyTo);

                // set subject and encoding
                myMail.Subject = $"ERROR REPORT: {CompanySettings.Default.CompanyNameShort} - {ErrorType} - {ErrorTime.ToString("G")}";
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                // set body-message and encoding
                myMail.Body = $"<b>The following {ErrorType} occured at {ErrorTime.ToString("G")} at {CompanySettings.Default.CompanyName}:</b><br><br>Active User: <b>{activeUser}</b><br><br>SQL Query: {ErrorHandlerModel.SQLQuery} <br><br><br>{ErrorLog} <br><br><br><b> ERROR REVIEW CONTACT INFORMATION:</b> <br> {CompanySettings.Default.CompanyName} <br> {CompanySettings.Default.CompanyMail} <br> {CompanySettings.Default.CompanyPhone}";
                myMail.BodyEncoding = System.Text.Encoding.UTF8;
                // text or html
                myMail.IsBodyHtml = true;

                mySmtpClient.Send(myMail);
                return true;
            }

            catch (SmtpException ex)
            {
                throw new ApplicationException
                  ("SmtpException has occured: " + ex.Message);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static bool SendErrorReportLocation()
        {
            try
            {

                string activeUser = string.Empty;

                if (MainWindowViewModel.username == "")
                {
                    activeUser = "NONE - Loginmask";
                }
                else
                {
                    activeUser = MainWindowViewModel.username;
                }

                System.Net.Mail.SmtpClient mySmtpClient = new System.Net.Mail.SmtpClient();

                mySmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mySmtpClient.UseDefaultCredentials = false;
                mySmtpClient.Credentials = new System.Net.NetworkCredential(AdministrationQueries.RunSql("SELECT * FROM company_settings WHERE settings_name = 'company_mail'").Tables[0].Rows[0][2].ToString(), AdministrationQueries.RunSql("SELECT * FROM company_settings WHERE settings_name = 'company_mail_password'").Tables[0].Rows[0][2].ToString());
                mySmtpClient.Port = int.Parse(AdministrationQueries.RunSql("SELECT * FROM company_settings WHERE settings_name = 'company_mail_port'").Tables[0].Rows[0][2].ToString());
                mySmtpClient.Host = AdministrationQueries.RunSql("SELECT * FROM company_settings WHERE settings_name = 'company_mail_host'").Tables[0].Rows[0][2].ToString();
                mySmtpClient.EnableSsl = true;

                // set smtp-client with basicAuthentication

                // add from,to mailaddresses
                MailAddress from = new MailAddress(AdministrationQueries.RunSql("SELECT * FROM company_settings WHERE settings_name = 'company_mail'").Tables[0].Rows[0][2].ToString(), AdministrationQueries.RunSql("SELECT * FROM company_settings WHERE settings_name = 'company_name'").Tables[0].Rows[0][2].ToString());
                MailAddress to = new MailAddress(AdministrationQueries.RunSql("SELECT * FROM company_settings WHERE settings_name = 'global_stockerror_mail'").Tables[0].Rows[0][2].ToString(), "FALSCHER LAGERBESTAND MELDUNG");
                System.Net.Mail.MailMessage myMail = new System.Net.Mail.MailMessage(from, to);

                // add ReplyTo
                //MailAddress replyTo = new MailAddress("dominik@waerp.software");
                //myMail.ReplyToList.Add(replyTo);

                // set subject and encoding
                myMail.Subject = $"LAGERBESTAND FALSCH | GEMELDET UM {DateTime.Now.ToString("G")}";
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;

                // set body-message and encoding
                myMail.Body = $"<b>Es besteht ein falscher Lagerbestand bei dem Artikel mit der Artikelnummer {ReportWrongLocationModel.ItemIdent} im Lagerort {ReportWrongLocationModel.LocationIdent}.</b>" +
                    $"<br><br>Mitarbeiter: <b>{MainWindowViewModel.Fullname}</b><br><br>" +
                    $"Beschreibung:  " +
                    $"<br><br>" +
                    $"{ReportWrongLocationModel.Description}";

                myMail.BodyEncoding = System.Text.Encoding.UTF8;
                // text or html
                myMail.IsBodyHtml = true;

                mySmtpClient.Send(myMail);
                return true;
            }

            catch
            {

                return false;

            }

        }
    }
}
