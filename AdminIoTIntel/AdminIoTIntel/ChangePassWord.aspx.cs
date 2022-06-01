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
    public partial class ChangePassWord : PageBase
    {
        UsersBussiness obj = new UsersBussiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userName = Session["UserName"].ToString();
                txtUserName.Text = userName;
            }
        }
        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string passwordold = CommonClass.StringValidator.GetMD5String(txtPassOld.Text.Trim());          
            int userIDLogin = this.UserIDLogin;
            Users item = obj.checkLogin(username, passwordold);
            if (item != null)
            {
                if (ValidateForm())
                {
                    string passwordnew = CommonClass.StringValidator.GetMD5String(txtPassNew.Text.Trim());
                    if (obj.UpdatePassWord(item.UserID, passwordnew, userIDLogin) == true)
                    {
                        CommonClass.MessageBox.Show("Password was successfully changed.");
                    }
                    else
                        CommonClass.MessageBox.Show("Error change password. Please contact Administrator.");
                }
            }
            else
                CommonClass.MessageBox.Show("Old password is invalid. Please input Password other.");
        }
        private bool ValidateForm()
        {
            string newPass = txtPassNew.Text.Trim();
            if (string.IsNullOrEmpty(newPass))
            {
                CommonClass.MessageBox.Show("New Password can't null. Please input New Password.");
                return false;
            }    
            string ConfirmPass = txtPassNewAgain.Text.Trim();
            if (string.IsNullOrEmpty(newPass))
            {
                CommonClass.MessageBox.Show("Confirm Password can't null. Please input Confirm Password.");
                return false;
            }
            if (newPass != ConfirmPass)
            {
                CommonClass.MessageBox.Show("New Password and Confirm Password is diffirent.");
                return false;
            }    
            return true;
        }
    }
}