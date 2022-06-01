using Itech.Utils;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;
//using Itech.Utils;

namespace adminiotintel.Bussiness
{
    public class PageBase : System.Web.UI.Page
    {
        UsersBussiness objUser = new UsersBussiness();
        LibraryCommon objLibrary = new LibraryCommon();

        private string userNameLogin = "";
        public string UserNameLogin
        {
            get { return Session["UserName"].ToString(); }
            set { userNameLogin = Session["UserName"].ToString(); }
        }
        public int UserIDLogin
        {
            get { return int.Parse(Session["UserID"].ToString()); }
            set { UserIDLogin = int.Parse(Session["UserID"].ToString()); }
        }

        private string GetLenghtIDRemain(int lenghtUserID, int numUser)
        {
            string result = string.Empty;
            result = numUser.ToString().PadLeft(lenghtUserID, '0');
            return result;
        }

        public void SendMail(string strSubjectMail, string strContentMail, string nameTo, string emailTo, string ccList)
        {
            System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            System.Net.Configuration.MailSettingsSectionGroup settings = (System.Net.Configuration.MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            string strName = ConfigurationManager.AppSettings["NameAdminEmail"];
            string strHost = settings.Smtp.Network.Host;
            string strEmailServer = settings.Smtp.Network.UserName;
            string strPassServer = settings.Smtp.Network.Password;

            ITServicesUtil.SendMailGmail(strName, strEmailServer, nameTo, emailTo, ccList, strSubjectMail, strContentMail, strHost, strEmailServer, strPassServer);
        }

        //Kiểm tra file upload đúng định dạng chưa
        // intType loại file check, 0 là file hình ảnh, 1 la file pdf
        protected bool CheckExtention(FileUpload name)
        {
            string Extentsion = CommonFunctions.getFileFormat(name.FileName);
            bool checkExtension = false;
            foreach (string strTempExtentsion in ConfigurationManager.AppSettings["FILE_FORMAT_UPLOAD"].ToString().Split(".".ToCharArray()))
            {
                if (Extentsion.ToLower() == strTempExtentsion.ToLower())
                {
                    checkExtension = true;
                    return true;
                }
            }
            if (checkExtension == false)
            {
                CommonClass.MessageBox.Show("File Upload phải thuộc các định dạng: .gif ; .jpeg ; .jpg; .pdf");
                return false;
            }
            return false;
        }
        protected bool CheckExtention(FileUpload name, int intType)
        {
            string Extentsion = CommonFunctions.getFileFormat(name.FileName);
            bool checkExtension = false;

            if (intType == 0)
            {
                foreach (string strTempExtentsion in ConfigurationManager.AppSettings["FILE_FORMAT_UPLOAD"].ToString().Split(".".ToCharArray()))
                {
                    if (Extentsion.ToLower() == strTempExtentsion.ToLower())
                    {
                        checkExtension = true;
                        return true;
                    }
                }
                if (checkExtension == false)
                {
                    CommonClass.MessageBox.Show("File Upload phải thuộc các định dạng: .gif ; .jpeg ; .jpg; .pdf");
                    return false;
                }
            }
            else
            {
                foreach (string strTempExtentsion in ConfigurationManager.AppSettings["PROFILE_FORMAT_UPLOAD"].ToString().Split(".".ToCharArray()))
                {
                    if (Extentsion.ToLower() == strTempExtentsion.ToLower())
                    {
                        checkExtension = true;
                        return true;
                    }
                }
                if (checkExtension == false)
                {
                    CommonClass.MessageBox.Show("File Upload phải thuộc các định dạng: .docx; .pdf");
                    return false;
                }
            }

            return false;
        }
        public static Bitmap CreateThumbnail(Stream lcFilename, int lnWidth, int lnHeight)
        {

            System.Drawing.Bitmap bmpOut = null;
            try
            {
                Bitmap loBMP = new Bitmap(lcFilename);
                ImageFormat loFormat = loBMP.RawFormat;

                decimal lnRatio;
                int lnNewWidth = 0;
                int lnNewHeight = 0;

                if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)
                    return loBMP;

                if (loBMP.Width > loBMP.Height)
                {
                    lnRatio = (decimal)lnWidth / loBMP.Width;
                    lnNewWidth = lnWidth;
                    decimal lnTemp = loBMP.Height * lnRatio;
                    lnNewHeight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)lnHeight / loBMP.Height;
                    lnNewHeight = lnHeight;
                    decimal lnTemp = loBMP.Width * lnRatio;
                    lnNewWidth = (int)lnTemp;
                }


                bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;

                g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);

                loBMP.Dispose();
            }
            catch
            {
                return null;
            }
            return bmpOut;
        }

        #region Config ngôn ngữ...
        protected override void InitializeCulture()
        {
            LanguageUtility.ChangeToChosenLanguage();
            base.InitializeCulture();
        }
        public string DefaultLanguageID
        {
            set
            {
                if (ViewState[UniqueID + "LanguageID"] != null)
                {
                    ViewState[UniqueID + "LanguageID"] = value;
                }
                else
                {
                    ViewState.Add(UniqueID + "LanguageID", value);
                }
            }
            get
            {
                if (ViewState[UniqueID + "LanguageID"] == null)
                {
                    ViewState.Add(UniqueID + "LanguageID", "vi-VN");
                }
                return (string)ViewState[UniqueID + "LanguageID"];
            }

        }

        public string CurrentLanguageID
        {
            get { return LanguageUtility.GetCurrentLanguageId().ToLower(); }
        }
        #endregion
    }
}