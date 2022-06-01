using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace adminiotintel.Bussiness
{
    public class ITServicesUtil
    {
        #region Send Mail
        public static bool SendMail(string fromName, string fromAddress, string toName, string toAddress, string subject, string content)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                MailMessage email = new MailMessage();
                email.From = new MailAddress(fromAddress, fromName);
                email.To.Add(new MailAddress(toAddress, toName));
                email.Subject = subject;
                email.BodyEncoding = Encoding.UTF8;
                email.SubjectEncoding = Encoding.UTF8;
                email.IsBodyHtml = true;
                email.Body = content;
                email.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


                //Send Email
                client.Send(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        public static bool SendMail(string fromName, string fromAddress, string toName, string toAddress, string subject, string content, string replyTo)
        {
            bool sendSuccess = true;
            try
            {
                SmtpClient client = new SmtpClient();
                MailMessage email = new MailMessage();
                email.From = new MailAddress(fromAddress, fromName);
                email.To.Add(new MailAddress(toAddress, toName));
                email.Subject = subject;
                email.BodyEncoding = Encoding.UTF8;
                email.SubjectEncoding = Encoding.UTF8;
                email.IsBodyHtml = true;
                email.Body = content;
                email.ReplyTo = (new MailAddress(replyTo, fromName));
                email.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                //Send Email
                client.Send(email);
            }
            catch
            {
                sendSuccess = false;
            }
            return sendSuccess;
        }

        public static bool SendMailGmail(string fromName, string fromEmail, string toName, string toAddress, string ccList, string subject, string body,
         string host, string emailServer, string passWord)
        {
            bool sendSuccess = true;
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            try
            {
                MailAddress fromAddress = new MailAddress(fromEmail, fromName);
                message.From = fromAddress;
                message.To.Add(new MailAddress(toAddress, toName));
                if (ccList != null && ccList != string.Empty)
                    message.CC.Add(ccList);
                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Body = body;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                // We use gmail as our smtp client
                smtpClient.Host = host;
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtpClient.Credentials = new System.Net.NetworkCredential(emailServer, passWord);

                smtpClient.Send(message);
                Thread.Sleep(3000);
            }
            catch
            {
                sendSuccess = false;
            }
            return sendSuccess;
        }
        #endregion

        public static DataSet ImportByFileExcel(string pathFile)
        {
            DataSet dsData = new DataSet();
            OleDbConnection con = new OleDbConnection(string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=Yes;IMEX=1'", pathFile));
            OleDbDataAdapter da = new OleDbDataAdapter("select * from [Sheet1$]", con);

            da.Fill(dsData, "UserList");

            con.Close();

            dsData = RemoveRowEmty(dsData);

            dsData = RemoveColumnEmty(dsData);

            if (File.Exists(pathFile))
                File.Delete(pathFile);

            return dsData;
        }
        public static DataSet RemoveRowEmty(DataSet dsData)
        {
            for (int i = 0; i < dsData.Tables[0].Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(dsData.Tables[0].Rows[i][1].ToString()))
                {
                    dsData.Tables[0].Rows[i].Delete();
                }
            }
            dsData.Tables[0].AcceptChanges();
            return dsData;
        }
        public static DataSet RemoveColumnEmty(DataSet dsData)
        {
            int desiredSize = 11;
            if (dsData.Tables[0].Columns.Count > desiredSize)
            {
                while (dsData.Tables[0].Columns.Count > desiredSize)
                {
                    dsData.Tables[0].Columns.RemoveAt(desiredSize);
                }
            }
            return dsData;
        }
    }
}