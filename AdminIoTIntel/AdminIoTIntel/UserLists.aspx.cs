using adminiotintel.Bussiness;
using adminiotintel.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace adminiotintel
{
    public partial class UserLists : PageBase
    {
        UsersBussiness obj = new UsersBussiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDDLTypeUser();
                LoadGrid();
            }           
        }

        public void loadDDLTypeUser()
        {
            ListItem item0 = new ListItem();
            item0.Text = "------ All ------";
            item0.Value = "100";
            ddlTypeUser.Items.Add(item0);

            ListItem item1 = new ListItem();
            item1.Text = "------ Activated ------";
            item1.Value = "1";
            ddlTypeUser.Items.Add(item1);

            ListItem item2 = new ListItem();
            item2.Text = "------Not Activated ------";
            item2.Value = "0";

            ddlTypeUser.Items.Add(item2);
        }

        private void LoadGrid()
        {
            string key = txtKey.Text.Trim();
            int typeUserID = int.Parse(ddlTypeUser.SelectedValue);
            List<Users> lstAdmin = obj.getListUserBySearch(key, typeUserID);
            radGridUser.DataSource = lstAdmin;
            radGridUser.DataBind();
        }

        protected void radGridUser_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                Users user = (Users)e.Item.DataItem;
                Literal ltrUUserID = e.Item.FindControl("ltrUUserID") as Literal;
                Literal ltrFullName = e.Item.FindControl("ltrFullName") as Literal;
                Literal ltrTypeUser = e.Item.FindControl("ltrTypeUserID") as Literal;
                Literal ltrStatus = e.Item.FindControl("ltrStatus") as Literal;
                Literal ltrUserCreated = e.Item.FindControl("ltrUserCreated") as Literal;
                Literal ltrCreatedDate = e.Item.FindControl("ltrCreatedDate") as Literal;
                            
                string urlDetail = string.Format("~/UserDetails.aspx?id={0}", user.UserID);
                ltrFullName.Text = string.Format("<a href='{0}' title='User detail' style='text-decoration:none;'>{1}</a> ", ResolveUrl(urlDetail), user.FullName);
                ltrUUserID.Text = string.Format("<a href='{0}' title='User detail' style='text-decoration:none;'>{1}</a> ", ResolveUrl(urlDetail), user.UserID);
                ltrTypeUser.Text = user.GroupID == 1 ? "Administrator" : "User";
                ltrUserCreated.Text = user.UserCreatedID.ToString();
                ltrCreatedDate.Text = user.DateCreated.ToString("yyyy-MM-dd HH:mm");

                if (user.Status == 1)
                    ltrStatus.Text = "Active";
                else
                    ltrStatus.Text = "DeActive";
            }
        }
        protected void lbtFilter_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        protected void lbtExportExcel_Click(object sender, EventArgs e)
        {
            if (radGridUser.Items.Count <= 0)
            {
                CommonClass.MessageBox.Show("No record found.");
                return;
            }
            radGridUser.Columns[0].Visible = true;
            radGridUser.Columns[1].Visible = true;
            radGridUser.Columns[2].Visible = true;
            radGridUser.Columns[3].Visible = true;
            ConfigureExport();
            radGridUser.ExportSettings.Excel.Format = Telerik.Web.UI.GridExcelExportFormat.Biff;
            radGridUser.MasterTableView.ExportToExcel();
        }

        public void ConfigureExport()
        {
            radGridUser.ExportSettings.ExportOnlyData = true;
            radGridUser.ExportSettings.IgnorePaging = true;
            radGridUser.ExportSettings.OpenInNewWindow = true;
        }

        protected void radGridUser_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string key = txtKey.Text.Trim();
            int typeUserID = int.Parse(ddlTypeUser.SelectedValue);

            List<Users> lstAdmin = obj.getListUserBySearch(key, typeUserID);
            radGridUser.DataSource = lstAdmin;
        }
    }
}