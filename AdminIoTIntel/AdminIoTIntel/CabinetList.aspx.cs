using adminiotintel.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using adminiotintel.model;
using Telerik.Web.UI;

namespace adminiotintel
{
    public partial class ListCabinet : PageBase
    {
        CabinetBussiness obj = new CabinetBussiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGrid();
            }
        }

        private void LoadGrid()
        {
            string keySearch = txtKey.Text.Trim();
            List<Cabinet> lstRoom = obj.getListCabinetBySearch(keySearch);
            radGridCabinet.DataSource = lstRoom;
            radGridCabinet.DataBind();
        }

        protected void radGridCabinet_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                Cabinet cabinet = (Cabinet)e.Item.DataItem;
                Literal ltrCabinetID = e.Item.FindControl("ltrCabinetID") as Literal;
                Literal ltrName = e.Item.FindControl("ltrName") as Literal;
                Literal ltrStatus = e.Item.FindControl("ltrStatus") as Literal;
                Literal ltrUserCreated = e.Item.FindControl("ltrUserCreated") as Literal;
                Literal ltrCreatedDate = e.Item.FindControl("ltrCreatedDate") as Literal;

                string urlDetail = string.Format("~/CabinetDetail.aspx?id={0}", cabinet.CabinetID);
                ltrName.Text = string.Format("<a href='{0}' title='Cabinet detail' style='text-decoration:none;'>{1}</a> ", ResolveUrl(urlDetail), cabinet.Name);
                ltrCabinetID.Text = string.Format("<a href='{0}' title='Cabinet detail' style='text-decoration:none;'>{1}</a> ", ResolveUrl(urlDetail), cabinet.CabinetID);

                ltrUserCreated.Text = cabinet.UserCreatedID.ToString();
                ltrCreatedDate.Text = cabinet.DateCreated.ToString("yyyy-MM-dd HH:mm");

                if (cabinet.Status == 1)
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
            if (radGridCabinet.Items.Count <= 0)
            {
                CommonClass.MessageBox.Show("No record found.");
                return;
            }
            radGridCabinet.Columns[0].Visible = true;
            radGridCabinet.Columns[1].Visible = true;
            radGridCabinet.Columns[2].Visible = true;
            radGridCabinet.Columns[3].Visible = true;
            ConfigureExport();
            radGridCabinet.ExportSettings.Excel.Format = Telerik.Web.UI.GridExcelExportFormat.Biff;
            radGridCabinet.MasterTableView.ExportToExcel();
        }

        public void ConfigureExport()
        {
            radGridCabinet.ExportSettings.ExportOnlyData = true;
            radGridCabinet.ExportSettings.IgnorePaging = true;
            radGridCabinet.ExportSettings.OpenInNewWindow = true;
        }

        protected void radGridCabinet_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string key = txtKey.Text.Trim();
            List<Cabinet> lstAdmin = obj.getListCabinetBySearch(key);
            radGridCabinet.DataSource = lstAdmin;
        }
    }
}