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
    public partial class ListRooms : PageBase
    {
        RoomBussiness obj = new RoomBussiness();
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
            List<Rooms> lstRoom = obj.getListRoomBySearch(keySearch);
            radGridRooms.DataSource = lstRoom;
            radGridRooms.DataBind();
        }

        protected void radGridRooms_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                Rooms room = (Rooms)e.Item.DataItem;
                Literal ltrRoomID = e.Item.FindControl("ltrRoomID") as Literal;
                Literal ltrName = e.Item.FindControl("ltrName") as Literal;
                Literal ltrDescription = e.Item.FindControl("ltrDescription") as Literal;
                Literal ltrUserCreated = e.Item.FindControl("ltrUserCreated") as Literal;
                Literal ltrCreatedDate = e.Item.FindControl("ltrCreatedDate") as Literal;

                string urlDetail = string.Format("~/RoomDetail.aspx?id={0}", room.RoomID);
                ltrName.Text = string.Format("<a href='{0}' title='Room detail' style='text-decoration:none;'>{1}</a> ", ResolveUrl(urlDetail), room.Name);
                ltrRoomID.Text = string.Format("<a href='{0}' title='Room detail' style='text-decoration:none;'>{1}</a> ", ResolveUrl(urlDetail), room.RoomID);
    
                ltrUserCreated.Text = room.UserCreatedID.ToString();
                ltrCreatedDate.Text = room.DateCreated.ToString("yyyy-MM-dd HH:mm");
            }
        }

        protected void lbtFilter_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        protected void lbtExportExcel_Click(object sender, EventArgs e)
        {
            if (radGridRooms.Items.Count <= 0)
            {
                CommonClass.MessageBox.Show("No record found.");
                return;
            }
            radGridRooms.Columns[0].Visible = true;
            radGridRooms.Columns[1].Visible = true;
            radGridRooms.Columns[2].Visible = true;
            radGridRooms.Columns[3].Visible = true;
            ConfigureExport();
            radGridRooms.ExportSettings.Excel.Format = Telerik.Web.UI.GridExcelExportFormat.Biff;
            radGridRooms.MasterTableView.ExportToExcel();
        }

        public void ConfigureExport()
        {
            radGridRooms.ExportSettings.ExportOnlyData = true;
            radGridRooms.ExportSettings.IgnorePaging = true;
            radGridRooms.ExportSettings.OpenInNewWindow = true;
        }

        protected void radGridRooms_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string key = txtKey.Text.Trim();
            List<Rooms> lstAdmin = obj.getListRoomBySearch(key);
            radGridRooms.DataSource = lstAdmin;
        }
    }
}