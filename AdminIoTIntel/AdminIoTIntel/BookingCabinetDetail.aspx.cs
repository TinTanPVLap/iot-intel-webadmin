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
    public partial class BookingCabinetDetail : PageBase
    {
        CabinetBussiness objCabin = new CabinetBussiness();
        BookingCabinetBussiness objBk = new BookingCabinetBussiness();
        UsersBussiness objUs = new UsersBussiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDDLData();
                decimal bookingID = 0;
                decimal.TryParse(Request["id"], out bookingID);
                if (bookingID > 0)
                {
                    LoadDetailBooking(bookingID);
                }
                else
                {
                    txtTimeStart.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                    txtTimeEnd.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                }
            }
        }

        private void LoadDDLData()
        {
            ddlTCabinet.Items.Clear();
            ddlTUserHost.Items.Clear();
            List<Cabinet> lstCabinet = objCabin.getListCabinetBySearch("").OrderBy(x => x.Name).ToList();
            List<Users> lstUser = objUs.getListAllUser().OrderBy(x => x.FullName).ToList();
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
            if (lstUser.Count > 0)
            {
                foreach (Users item in lstUser)
                {
                    ListItem newItem = new ListItem();
                    newItem.Text = item.FullName;
                    newItem.Value = item.UserID.ToString();
                    ddlTUserHost.Items.Add(newItem);
                }
                ddlTUserHost.SelectedValue = this.UserIDLogin.ToString();
            }
        }

        private void LoadDetailBooking(decimal bookingID)
        {
            Booking_Cabinet booking = objBk.getBookingByID(bookingID);
            if (booking != null)
            {
                ddlTCabinet.SelectedValue = booking.CabinetID.ToString();
                txtPincode.Text = booking.Pincode.ToString();
                ddlTUserHost.SelectedValue = booking.UserHostID.ToString();
                txtTimeStart.Text = booking.TimeStart.ToString("dd-MM-yyyy HH:mm");
                txtTimeEnd.Text = booking.TimeEnd.ToString("dd-MM-yyyy HH:mm");
            }
        }

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            decimal bookingID = 0;
            decimal.TryParse(Request["id"], out bookingID);
            if (ValidateForm(bookingID))
            {
                if (bookingID == 0)
                {
                    InsertBookingCabinet();
                }
                else
                {
                    UpdateBookingCabinet(bookingID);
                }
            }

        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            decimal bookingID = 0;
            decimal.TryParse(Request["id"], out bookingID);
            if (bookingID > 0)
            {
                if (objBk.CheckDelete(bookingID))
                {
                    if (objBk.DeleteBooking(bookingID) == true)
                    {
                        Response.Redirect("~/BookingCabinetsList.aspx");
                    }
                    else
                    {
                        CommonClass.MessageBox.Show("Error Delete this booking. Please contact Administrator");
                        return;
                    }
                }
                else
                {
                    CommonClass.MessageBox.Show("Can't delete this booking because it have used.");
                    return;
                }
            }
        }

        private bool ValidateForm(decimal bookingID)
        {
            DateTime timeStart = DateTime.Parse(txtTimeStart.Text.ToString());
            DateTime timeEnd = DateTime.Parse(txtTimeEnd.Text.ToString());
            int cabinetID = int.Parse(ddlTCabinet.SelectedValue.ToString());
            if (timeStart > timeEnd)
            {
                CommonClass.MessageBox.Show("Time Start must smaller Time End.");
                return false;
            }
            if (DateTime.Compare(timeStart, DateTime.Now) < 0 || DateTime.Compare(timeEnd, DateTime.Now) < 0)
            {
                CommonClass.MessageBox.Show("Time Start and Time End are must be greater than current.");
                return false;
            }    
            if (bookingID == 0)
            {
                if (objBk.CheckInsert(cabinetID, timeStart, timeEnd) == false)
                {
                    CommonClass.MessageBox.Show("Cabinet exist, please select other time.");
                    return false;
                }
            }              
            else
            {
                if (objBk.Checkupdate(bookingID, cabinetID, timeStart, timeEnd) == false)
                {
                    CommonClass.MessageBox.Show("Cabinet exist, please select other time.");
                    return false;
                }
            }    

            return true;
        }

        private void InsertBookingCabinet()
        {
            Booking_Cabinet booking = new Booking_Cabinet();
            booking.CabinetID = int.Parse(ddlTCabinet.SelectedValue.ToString());
            booking.Pincode = getRandomPincode();
            booking.TimeStart = DateTime.Parse(txtTimeStart.Text.ToString());
            booking.TimeEnd = DateTime.Parse(txtTimeEnd.Text.ToString());
            booking.UserHostID = int.Parse(ddlTUserHost.SelectedValue.ToString());
            booking.UserCreatedID = this.UserIDLogin;
            booking.DateCreated = DateTime.Now;
            booking.UserUpdatedID = this.UserIDLogin;
            booking.DateUpdated = DateTime.Now;
            if (objBk.InsertBooking(booking))
            {
                SendEmail();
                Response.Redirect("~/BookingCabinetsList.aspx");
            }
            else
                CommonClass.MessageBox.Show("Error can't insert booking. Please contact Administrator.");
        }

        private void UpdateBookingCabinet(decimal bookingID)
        {
            int cabinetID = int.Parse(ddlTCabinet.SelectedValue.ToString());
            int pincode = int.Parse(txtPincode.Text.Trim());
            int userHost = int.Parse(ddlTUserHost.SelectedValue.ToString());
            DateTime timeStart = DateTime.Parse(txtTimeStart.Text.ToString());
            DateTime timeEnd = DateTime.Parse(txtTimeEnd.Text.ToString());
            if (objBk.UpdatetBooking(bookingID, cabinetID, pincode, userHost, timeStart, timeEnd, this.UserIDLogin))
            {
                SendEmail();
                Response.Redirect("~/BookingCabinetsList.aspx");
            }
            else
                CommonClass.MessageBox.Show("Error can't update booking. Please contact Administrator.");
        }

        public int getRandomPincode()
        {
            int pincode = 0;
            Random rmd = new Random();
            pincode = rmd.Next(1000, 10000);
            return pincode;
        }

        private void SendEmail()
        {
            Users users = objUs.GetUserByID(int.Parse(ddlTUserHost.SelectedValue.ToString()));
            Cabinet cabinet = objCabin.getCabinetByID(int.Parse(ddlTCabinet.SelectedValue.ToString()));
            DateTime timeStart = DateTime.Parse(txtTimeStart.Text.ToString());
            DateTime timeEnd = DateTime.Parse(txtTimeEnd.Text.ToString());

            string strSubjectMail = "Email conform booking cabinet";
            string strContentMail = "You bad booked cabinet " + cabinet.Name + ", from " + timeStart.ToString("dd/MM/yyyy HH:mm") + " to " + timeEnd.ToString("dd/MM/yyyy HH:mm") + "<br/>";
            strContentMail += "Your new pincode: " + txtPincode.Text.Trim();
            string nameTo = "Administrator IoT Intel";
            string emailTo = "";          
            emailTo = users.Email;
            string ccList = "";
            SendMail(strSubjectMail, strContentMail, nameTo, emailTo, ccList);
        }

        

    }
}