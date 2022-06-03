using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using adminiotintel.Bussiness;
using adminiotintel.model;

namespace adminiotintel
{
    public partial class Login : PageBase
    {
        UsersBussiness obj = new UsersBussiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtPassword.Attributes.Add("value", "test");
        }
        protected void lbtLogin_Click(object sender, EventArgs e)
        {
            string strUserName = CommonClass.StringValidator.GetSafeString(txtUserName.Text.Trim());
            string strPassWord = CommonClass.StringValidator.GetMD5String(txtPassword.Text.Trim());
            Users users = obj.CheckingLogin(strUserName, strPassWord);
            if (users != null)
            {
                Session["UserName"] = strUserName;
                Session["PassWord"] = true;
                Session["UserID"] = users.UserID;
                Session["GroupID"] = users.GroupID;
                this.UserNameLogin = strUserName;
                if (users.GroupID == TypeUserIDCons.Administrator)
                    Response.Redirect("~/UserLists.aspx");
                else
                    Response.Redirect("~/BookingCabinetsList.aspx");
            }
            else
            {
                CommonClass.MessageBox.Show("Tên truy cập hoặc mật khẩu truy cập không đúng");
                txtPassword.Text = string.Empty;
                txtPassword.Focus();
            }
        }
    }
}