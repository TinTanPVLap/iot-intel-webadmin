using adminiotintel.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using adminiotintel.model;
using Telerik.Web.UI;
using System.Data;

namespace adminiotintel
{
    public partial class BookingCabinet : System.Web.UI.Page
    {
        UsersBussiness objUs = new UsersBussiness();
        BookingEventBussiness objBk = new BookingEventBussiness();
        RoomBussiness objRoom = new RoomBussiness();
        CabinetBussiness objCa = new CabinetBussiness();
        public decimal _eventID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadDataDDL();
            decimal.TryParse(Request["eventID"], out _eventID);
            if (!IsPostBack)
            {
                LoadData(_eventID);
            }
        }

        private void LoadDataDDL()
        {
            List<Cabinet> lstCabinet = objCa.getListCabinetBySearch("").Where(x => x.Status == 1).OrderBy(x => x.Name).ToList();
            if (lstCabinet.Count > 0)
            {
                foreach (Cabinet item in lstCabinet)
                {
                    ListItem newItem = new ListItem();
                    newItem.Text = item.Name;
                    newItem.Value = item.CabinetID.ToString();
                    ddlTCabinet.Items.Add(newItem);
                }
                ddlTCabinet.SelectedValue = lstCabinet.FirstOrDefault().CabinetID.ToString();
            }
            List<Users> lstUser = objUs.getListAllUser().OrderBy(x => x.FullName).ToList();
            if (lstUser.Count > 0)
            {
                foreach (Users item in lstUser)
                {
                    ListItem newItem = new ListItem();
                    newItem.Text = item.FullName;
                    newItem.Value = item.UserID.ToString();
                    ddlTUserHost.Items.Add(newItem);
                }
                ddlTUserHost.SelectedValue = lstUser.FirstOrDefault().UserID.ToString();
            }
        }

        private void LoadData(decimal eventID)
        {
            Booking_Event booking = objBk.getBookingEventByID(eventID);
            if (booking != null)
            {
                ddlTUserHost.SelectedValue = booking.UserHostID.ToString();
                txtTimeStart.Text = booking.TimeStart.ToString("dd-MM-yyyy HH:mm");
                txtTimeEnd.Text = booking.TimeEnd.ToString("dd-MM-yyyy HH:mm");
            }    
        }


    }
}