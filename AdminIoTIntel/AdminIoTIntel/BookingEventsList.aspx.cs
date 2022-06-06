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
    public partial class ListBookingEvents : PageBase
    {
        BookingEventBussiness obj = new BookingEventBussiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDDLRoom();
                LoadGrid();
            }
        }

        public void loadDDLRoom()
        {
            ddlRoom.Items.Clear();
            RoomBussiness objRoom = new RoomBussiness();
            List<Rooms> lstRoom = objRoom.getListRoomBySearch("");
            ListItem itemDefault = new ListItem();
            itemDefault.Text = "------ All ------";
            itemDefault.Value = "1000";
            ddlRoom.Items.Add(itemDefault);

            if (lstRoom.Count > 0)
            {
                foreach (Rooms content in lstRoom)
                {
                    ListItem item = new ListItem();
                    item.Text = content.Name;
                    item.Value = content.RoomID.ToString();
                    ddlRoom.Items.Add(item);
                }
            }
        }

        private void LoadGrid()
        {
            int groupID = int.Parse(Session["GroupID"].ToString());
            int userID = this.UserIDLogin;
            if (groupID == TypeUserIDCons.Administrator)
                userID = 0;

            string keySearch = txtKey.Text.Trim();
            int roomID = int.Parse(ddlRoom.SelectedValue);
            List<GetListBookingEvent_Result> lstEvent = obj.getListBookingEvent(keySearch, roomID, userID);
            radGridBooking.DataSource = lstEvent;
            radGridBooking.DataBind();
        }

        protected void radGridBooking_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GetListBookingEvent_Result booking = (GetListBookingEvent_Result)e.Item.DataItem;
                Literal ltrEventID = e.Item.FindControl("ltrEventID") as Literal;
                Literal ltrTitle = e.Item.FindControl("ltrTitle") as Literal;
                Literal ltrRoomName = e.Item.FindControl("ltrRoomName") as Literal;
                Literal ltrTimeStart = e.Item.FindControl("ltrTimeStart") as Literal;
                Literal ltrTimeEnd = e.Item.FindControl("ltrTimeEnd") as Literal;
                Literal ltrUserbook = e.Item.FindControl("ltrUserbook") as Literal;
                Literal ltrUserCreated = e.Item.FindControl("ltrUserCreated") as Literal;
                Literal ltrCreatedDate = e.Item.FindControl("ltrCreatedDate") as Literal;

                string urlDetail = string.Format("~/BookingEventsDetail.aspx?id={0}", booking.EventID);
                ltrTitle.Text = string.Format("<a href='{0}' title='Event detail' style='text-decoration:none;'>{1}</a> ", ResolveUrl(urlDetail), booking.Title);
                ltrEventID.Text = string.Format("<a href='{0}' title='Event detail' style='text-decoration:none;'>{1}</a> ", ResolveUrl(urlDetail), booking.EventID);
                ltrRoomName.Text = booking.RoomName;
                ltrTimeStart.Text = booking.TimeStart.ToString("yyyy-MM-dd HH:mm");
                ltrTimeEnd.Text = booking.TimeEnd.ToString("yyyy-MM-dd HH:mm");
                ltrUserbook.Text = booking.FullName;
                ltrUserCreated.Text = booking.UserCreatedID.ToString();
                ltrCreatedDate.Text = booking.DateCreated.ToString("yyyy-MM-dd HH:mm");
            }
        }

        protected void lbtFilter_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        protected void lbtExportExcel_Click(object sender, EventArgs e)
        {
            if (radGridBooking.Items.Count <= 0)
            {
                CommonClass.MessageBox.Show("No record found.");
                return;
            }
            radGridBooking.Columns[0].Visible = true;
            radGridBooking.Columns[1].Visible = true;
            radGridBooking.Columns[2].Visible = true;
            radGridBooking.Columns[3].Visible = true;
            radGridBooking.Columns[4].Visible = true;
            radGridBooking.Columns[5].Visible = true;
            ConfigureExport();
            radGridBooking.ExportSettings.Excel.Format = Telerik.Web.UI.GridExcelExportFormat.Biff;
            radGridBooking.MasterTableView.ExportToExcel();
        }

        public void ConfigureExport()
        {
            radGridBooking.ExportSettings.ExportOnlyData = true;
            radGridBooking.ExportSettings.IgnorePaging = true;
            radGridBooking.ExportSettings.OpenInNewWindow = true;
        }

        protected void radGridBooking_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            int groupID = int.Parse(Session["GroupID"].ToString());
            int userID = this.UserIDLogin;
            if (groupID == TypeUserIDCons.Administrator)
                userID = 0;

            string keySearch = txtKey.Text.Trim();
            int roomID = int.Parse(ddlRoom.SelectedValue);
            List<GetListBookingEvent_Result> lstEvent = obj.getListBookingEvent(keySearch, roomID, userID);
            radGridBooking.DataSource = lstEvent;
        }
    }
}