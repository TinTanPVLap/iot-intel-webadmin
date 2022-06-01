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
    public partial class ListBookingCabinets : PageBase
    {
        BookingCabinetBussiness obj = new BookingCabinetBussiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDDLCabinet();
                LoadGrid();
            }
        }

        public void loadDDLCabinet()
        {
            ddlCabinet.Items.Clear();
            CabinetBussiness objRoom = new CabinetBussiness();
            List<Cabinet> lstCabinet = objRoom.getListCabinetBySearch("");
            ListItem itemDefault = new ListItem();
            itemDefault.Text = "------ All ------";
            itemDefault.Value = "1000";
            ddlCabinet.Items.Add(itemDefault);

            if (lstCabinet.Count > 0)
            {
                foreach (Cabinet content in lstCabinet)
                {
                    ListItem item = new ListItem();
                    item.Text = content.Name;
                    item.Value = content.CabinetID.ToString();
                    ddlCabinet.Items.Add(item);
                }
            }
        }

        private void LoadGrid()
        {
            string keySearch = txtKey.Text.Trim();
            int cabinetID = int.Parse(ddlCabinet.SelectedValue);
            List<GetListBookingCabinet_Result> lstBooking = obj.getListBookingCabinet(keySearch, cabinetID);
            radGridBooking.DataSource = lstBooking;
            radGridBooking.DataBind();
        }

        protected void radGridBooking_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GetListBookingCabinet_Result booking = (GetListBookingCabinet_Result)e.Item.DataItem;
                Literal ltrBookingID = e.Item.FindControl("ltrBookingID") as Literal;
                Literal ltrCabinetName = e.Item.FindControl("ltrCabinetName") as Literal;
                Literal ltrPincode = e.Item.FindControl("ltrPincode") as Literal;
                Literal ltrTimeStart = e.Item.FindControl("ltrTimeStart") as Literal;
                Literal ltrTimeEnd = e.Item.FindControl("ltrTimeEnd") as Literal;
                Literal ltrUserbook = e.Item.FindControl("ltrUserbook") as Literal;
                Literal ltrUserCreated = e.Item.FindControl("ltrUserCreated") as Literal;
                Literal ltrCreatedDate = e.Item.FindControl("ltrCreatedDate") as Literal;

                string urlDetail = string.Format("~/BookingCabinetDetail.aspx?id={0}", booking.BookingID);
                ltrCabinetName.Text = string.Format("<a href='{0}' title='Room detail' style='text-decoration:none;'>{1}</a> ", ResolveUrl(urlDetail), booking.CabinetName);
                ltrBookingID.Text = string.Format("<a href='{0}' title='Room detail' style='text-decoration:none;'>{1}</a> ", ResolveUrl(urlDetail), booking.BookingID);
                ltrPincode.Text = booking.Pincode.ToString();
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
            string keySearch = txtKey.Text.Trim();
            int cabinetID = int.Parse(ddlCabinet.SelectedValue);
            List<GetListBookingCabinet_Result> lstBooking = obj.getListBookingCabinet(keySearch, cabinetID);
            radGridBooking.DataSource = lstBooking;
        }
    }
}