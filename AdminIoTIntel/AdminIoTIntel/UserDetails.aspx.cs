using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using adminiotintel.Bussiness;
using adminiotintel.model;

namespace adminiotintel
{
    public partial class UserDetails : PageBase
    {
        UsersBussiness obj = new UsersBussiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDDLTypeUser();
                int userID = 0;
                int.TryParse(Request["id"], out userID);
                if (userID != 0)
                {
                    txtPassWord.Visible = false;
                    LoadDetailUser(userID);
                }
                else
                {
                    imgThumbs.Visible = false;
                    lbtChangeImages.Visible = false;
                }    
            }
        }
        private void LoadDetailUser(int userID)
        {
            Users item = obj.GetUserByID(userID);
            if (item != null)
            {
                lbtReset.Visible = true;
                ddlTypeUser.SelectedValue = item.GroupID.ToString();
                txtFullName.Text = item.FullName;
                txtUserName.Text = item.UserName;
                txtPassWord.Text = item.Password;
                txtEmail.Text = item.Email;
                txtPhone.Text = item.Phone;
                imgThumbs.ImageUrl = item.Images;
                chkStatus.Checked = Convert.ToBoolean(item.Status);
                txtEmail.Text = item.Email;

                showPassword.Visible = false;

                FileUploadControl.Visible = false;
                imgThumbs.Visible = true;
                lbtChangeImages.Visible = true;
            }
        }
        public void loadDDLTypeUser()
        {
            ListItem item = new ListItem();
            item.Text = "Administrator";
            item.Value = "1";
            ddlTypeUser.Items.Add(item);
            ListItem item1 = new ListItem();
            item1.Text = "User";
            item1.Value = "2";
            ddlTypeUser.Items.Add(item1);

            ddlTypeUser.SelectedValue = "1";
        }
        protected void lbtSave_Click(object sender, EventArgs e)
        {
            int userID = 0;
            int.TryParse(Request["id"], out userID);

            if (userID == 0)
                InsertUser();
            else
                UpdateUser(userID);
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            DeleteUser();
        }

        protected void lbtReset_Click(object sender, EventArgs e)
        {
            int userID = 0;
            int.TryParse(Request["id"], out userID);
            if (userID > 0)
            {
                string password = CommonClass.StringValidator.GetMD5String("123456");
                string passDisplay = "123456";
                ResetPassword(userID, password, passDisplay);
            }
        }

        private void ResetPassword(int userID, string newPass, string passDisplay)
        {

            if (obj.ResetPassword(userID, newPass))
            {
                SendMail("Email change password form Website Admin Iot Intel", "You recive email from Website Admin IoT Intel <br/><br/> Your new password: 123456", "Administrator IoT Intel", txtEmail.Text.Trim(),"");
                CommonClass.MessageBox.Show("Reset Password success, Website Admin IoT Intel will send to your email have new password. Please check your email.");
            }
            else
                CommonClass.MessageBox.Show("Reset password generating an error. Please contact to Administrator.");
        }

        protected void lbtChangeImages_Click(object sender, EventArgs e)
        {
            if (FileUploadControl.Visible == false)
            {
                FileUploadControl.Visible = true;
                imgThumbs.Visible = false;
                lbtChangeImages.Text = "Close";
            }
            else
            {
                FileUploadControl.Visible = false;
                imgThumbs.Visible = true;
                lbtChangeImages.Text = "Select";
            }
        }


        private void InsertUser()
        {
            if (ValidateData() == false)
                return;

            string fullName = txtFullName.Text.Trim();
            string userName = txtUserName.Text.Trim();
            string passWord = txtPassWord.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string imagesPath = imgThumbs.ImageUrl;
            imagesPath = UploadFile();
            if (imagesPath.Contains("Error file type"))
            {
                CommonClass.MessageBox.Show("Please Upload image of type .jpg,.png.");
                return;
            }    
            int userCreate = this.UserIDLogin;
            byte active = Convert.ToByte(chkStatus.Checked);

            Users users = new Users();
            users.GroupID = int.Parse(ddlTypeUser.SelectedValue.ToString());
            users.FullName = fullName;
            users.UserName = userName;
            users.Password = CommonClass.StringValidator.GetMD5String(passWord);
            users.Email = email;
            users.Phone = phone;
            users.Status = active;
            users.Images = imagesPath;
            users.KeyUser = "";
            users.UserCreatedID = userCreate;
            users.UserUpdatedID = userCreate;
            users.DateCreated = DateTime.Now;
            users.DateUpdated = DateTime.Now;

            bool result = obj.InsertUser(users);
            if (result == true)
            {
                SendMail("Email create new user form Website Admin Iot Intel", "You had created a new account from Website Admin IoT Intel <br/><br/> UserName: "+ userName  + "<br/> Password: "+ passWord + "", "Administrator IoT Intel", txtEmail.Text.Trim(), "");
                Response.Redirect("~/UserLists.aspx");
            }
            else
                CommonClass.MessageBox.Show("Error can't insert User. Please contact Administrator.");
        }

        private void UpdateUser(int userID)
        {
            if (ValidateData() == false)
                return;

            int groupID = int.Parse(ddlTypeUser.SelectedValue.ToString());
            string fullName = txtFullName.Text.Trim();
            string userName = txtUserName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string imagesPath = imgThumbs.ImageUrl;
            if (imagesPath.Contains("Error file type"))
            {
                CommonClass.MessageBox.Show("Please Upload image of type .jpg,.png.");
                return;
            }
            imagesPath = UploadFile();
            int userUpdate = this.UserIDLogin;
            byte active = Convert.ToByte(chkStatus.Checked);
            bool result = obj.UpdatetUser(userID, groupID, fullName, userName, email, phone, active, imagesPath, userUpdate);
            if (result == true)
            {
                //SendMail("Email change password form Website Admin Iot Intel", "You recive email from Website Admin IoT Intel <br/><br/> Your new password: 123456", "Administrator IoT Intel", txtEmail.Text.Trim());
                Response.Redirect("~/UserLists.aspx");
            }
            else
                CommonClass.MessageBox.Show("Error can't insert User. Please contact Administrator.");
        }

        private void DeleteUser()
        {
            int userID = 0;
            int.TryParse(Request["id"], out userID);
            if (userID > 0)
            {
                if (obj.CheckDelete(userID))
                {
                    if (obj.DeleteUser(userID) == true)
                    {
                        Response.Redirect("~/UserLists.aspx");
                    }
                    else
                    {
                        CommonClass.MessageBox.Show("Error Delete this Account. Please contact Administrator");
                        return;
                    }
                }
                else
                {
                    CommonClass.MessageBox.Show("Can't delete this user because user have used.");
                    return;
                }    
            }
        }

        private bool ValidateData()
        {
            int userID = 0;
            int.TryParse(Request["id"], out userID);
            string fullName = txtFullName.Text.Trim();
            if (string.IsNullOrEmpty(fullName))
            {
                CommonClass.MessageBox.Show("FullName can't null. Please input FullName.");
                return false;
            }
            string userName = txtUserName.Text.Trim();
            if (string.IsNullOrEmpty(userName))
            {
                CommonClass.MessageBox.Show("UserName can't null. Please input UserName.");
                return false;
            }
           
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                CommonClass.MessageBox.Show("Email can't null. Please input Email.");
                return false;
            }
            if (userID == 0)
            {
                if (obj.CheckInsert(userName, email) ==false)
                {
                    CommonClass.MessageBox.Show("UserName or Email is exists. Please check again.");
                    return false;
                }
                string passWord = txtPassWord.Text.Trim();
                if (string.IsNullOrEmpty(passWord))
                {
                    CommonClass.MessageBox.Show("PassWord can't null. Please input PassWord.");
                    return false;
                }
            }
            else
            {
                if (obj.CheckUpdate(userID, userName, email) == false)
                {
                    CommonClass.MessageBox.Show("UserName or Email is exists. Please check again.");
                    return false;
                }
            }    
            return true;
        }

        private string UploadFile()
        {
            string imagesPath = "";
            if (!string.IsNullOrEmpty(FileUploadControl.FileName) && CheckExtention(FileUploadControl, 0) == true)
            {
                string strFolderPath = Server.MapPath("~/Images/upload/");
                string strGeneralString = "_" + DateTime.Now.Second.ToString() + ".";
                //string strFileName = FileUploadControl.FileName.Replace(".", strGeneralString);
                string filetype = FileUploadControl.FileName.Substring(FileUploadControl.FileName.LastIndexOf('.'));
                if (filetype.Contains("jpg") || filetype.Contains("png"))
                {
                    string strFileName = txtUserName.Text.Trim() + filetype;
                    Bitmap strFilehinh = CreateThumbnail(FileUploadControl.PostedFile.InputStream, 2000, 2000);

                    if (System.IO.File.Exists(Path.Combine(strFolderPath, strFileName))) // xóa hình cũ, upload lại hình mới
                        System.IO.File.Delete(Path.Combine(strFolderPath, strFileName));

                    strFilehinh.Save(Path.Combine(strFolderPath, strFileName));
                    imagesPath = "~/Images/upload/" + strFileName;
                }
                else
                {
                    imagesPath = "Error file type";
                }
            }
            return imagesPath;
        }
    }
}