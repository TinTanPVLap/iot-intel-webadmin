using System;
using System.Globalization;
using System.Text;
using System.Web;

namespace adminiotintel.Bussiness
{
    public sealed class LanguageUtility
    {
        /// <summary>
        /// 
        /// </summary>
        public static void ChangeToChosenLanguage()
        {
            string langId = GetCurrentLanguageId();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(langId);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(langId);
        }

        /// <summary>
        /// Current Chosen Language Id
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentLanguageId()
        {
            if (System.Web.HttpContext.Current.Session != null && System.Web.HttpContext.Current.Session[LanguageConstant.LANGUAGE_ID] != null)
            {
                return System.Web.HttpContext.Current.Session[LanguageConstant.LANGUAGE_ID].ToString();
            }
            return "vi-VN";
        }

        public static void writeCookieWithEncrypt(string cookieName, string cookieValue, int expiredMins)
        {
            //MEncryption encryptTool = new MEncryption();
            HttpContext.Current.Response.Cookies[cookieName].Value = cookieValue;
            HttpContext.Current.Response.Cookies[cookieName].Expires = DateTime.Now.AddMinutes(expiredMins);
        }

        /// <summary>
        /// read encrypted cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static string ReadEncryptedCookie(string cookieName)
        {
            //MEncryption encryptTool = new MEncryption();

            if (HttpContext.Current.Request.Cookies[cookieName] != null)
                return HttpContext.Current.Request.Cookies[cookieName].Value;

            return string.Empty;
        }

        /// <summary>
        /// Doc cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static string ReadCookie(string cookieName)
        {
            if (HttpContext.Current.Request.Cookies[cookieName] != null)
                return HttpContext.Current.Request.Cookies[cookieName].Value;

            return string.Empty;
        }

        /// <summary>
        /// ghi cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static void WriteCookie(string cookieName, string cookieValue, int expiredMins)
        {
            HttpContext.Current.Response.Cookies[cookieName].Value = cookieValue;
            HttpContext.Current.Response.Cookies[cookieName].Expires = DateTime.Now.AddMinutes(expiredMins);
        }

        /// <summary>
        /// xoa cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        public static void DeleteCookie(string cookieName)
        {
            HttpContext.Current.Response.Cookies[cookieName].Expires = DateTime.Now.AddYears(-30);
        }
    }

    public class DefineConstans
    {
    }

    public sealed class TypeUserIDCons
    {
        public const int Administrator = 1;
        public const int User = 2;
    }
    public sealed class TypeContactID
    {
        public const int Contact = 1;
        public const int Register = 2;
        public const int PostComment = 3;
        public const int Jobs = 4;

    }
    public sealed class TypeCategoryID
    {
        public const int News = 1;
        public const int Products = 2;
        public const int AboutUS = 3;
    }

    public sealed class ContentTypeID
    {
        public const int Banner = 1;
        public const int Video = 2;
        public const int Partner = 3;
        public const int News = 4;

    }
    public sealed class LanguageConstant
    {
        public const string VIETNAM_LANGUAGE = "vi-vn";
        public const string ENGLISH_LANGUAGE = "en-us";
        public const string JAPAN_LANGUAGE = "ja-jp";
        public const string CHINA_LANGUAGE = "zh-hk";
        public const string LANGUAGE_ID = "LANGUAGE_ID_For_Weconnectfr";
    }
    public enum EnumLenghtCode
    {
        CodeLenghtContent = 5
    }
    public class LibraryCommon
    {
        public String DropText(string strBegin, int intNumber)
        {
            if (strBegin.Length > intNumber)
            {
                int intSpace = strBegin.IndexOf(' ', intNumber);
                if (intSpace > 0)
                    return string.Format("{0}...", strBegin.Substring(0, intSpace));
                else
                    return strBegin;
            }
            else if (strBegin.Length == intNumber)
                return strBegin;
            else
                return string.Format("{0}", strBegin);

        }

        public string RemoveUnicode(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        private static readonly string[] VietnameseSigns = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        public string FormatUrlString(string urlString)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    urlString = urlString.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            urlString = urlString.Trim();
            urlString = urlString.Replace("  ", " ");
            urlString = urlString.Replace(" ", "-");
            urlString = urlString.Replace("--", "-");
            urlString = urlString.Replace("--", "-");
            urlString = urlString.Replace(":", string.Empty);
            urlString = urlString.Replace(";", string.Empty);
            urlString = urlString.Replace("(", string.Empty);
            urlString = urlString.Replace(")", string.Empty);
            urlString = urlString.Replace("/", string.Empty);
            urlString = urlString.Replace("+", string.Empty);
            urlString = urlString.Replace("?", string.Empty);
            urlString = urlString.Replace("%", string.Empty);
            urlString = urlString.Replace("#", string.Empty);
            urlString = urlString.Replace("&", string.Empty);
            urlString = urlString.Replace("<", string.Empty);
            urlString = urlString.Replace(">", string.Empty);
            urlString = urlString.Replace(@"\", string.Empty);
            urlString = urlString.Replace("\"", string.Empty);
            urlString = urlString.Replace("'", string.Empty);
            urlString = urlString.Replace("`", string.Empty);
            urlString = urlString.Replace("$", string.Empty);
            urlString = urlString.Replace(".", string.Empty);
            urlString = urlString.Replace(",", string.Empty);
            urlString = urlString.Replace("=", string.Empty);
            urlString = urlString.Replace("@", string.Empty);
            urlString = urlString.Replace("{", string.Empty);
            urlString = urlString.Replace("}", string.Empty);
            urlString = urlString.Replace("|", string.Empty);
            urlString = urlString.Replace("^", string.Empty);
            urlString = urlString.Replace("~", string.Empty);
            urlString = urlString.Replace("[", string.Empty);
            urlString = urlString.Replace("]", string.Empty);
            urlString = urlString.Replace("’", string.Empty);
            urlString = urlString.Replace("û", "u");

            return HttpUtility.HtmlEncode(urlString.ToLower());
        }


    }


}