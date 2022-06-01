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
    public partial class BookingEventsDetail : PageBase
    {
        RoomBussiness objRoom = new RoomBussiness();
        BookingEventBussiness objBk = new BookingEventBussiness();
        UsersBussiness objUs = new UsersBussiness();
        CabinetBussiness objCa = new CabinetBussiness();
        List<getListBookingUser_Result> lstBookingUser = new List<getListBookingUser_Result>();
        BookingCabinetBussiness objBkCa = new BookingCabinetBussiness();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDDLData();
                decimal eventID = 0;
                decimal.TryParse(Request["id"], out eventID);
                if (eventID > 0)
                {
                    LoadDetailBooking(eventID);
                    radGridBookingUsers.Visible = true;
                    hplAddUser.Visible = true;
                    Booking_Cabinet booking_Cabinet = objBkCa.getBookingByEventID(eventID);
                    if (booking_Cabinet != null)
                    {
                        chkBkCabinet.Checked = true;
                        ddlTCabinet.SelectedValue = booking_Cabinet.CabinetID.ToString();
                        txtPincode.Text = booking_Cabinet.Pincode.ToString();
                        txtTimeStartCabinet.Text = booking_Cabinet.TimeStart.ToString("dd-MM-yyyy HH:mm");
                        txtTimeEndCabinet.Text = booking_Cabinet.TimeEnd.ToString("dd-MM-yyyy HH:mm");
                    }    
                }
                else
                {
                    txtTimeStart.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                    txtTimeEnd.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                    radGridBookingUsers.Visible = false;
                    hplAddUser.Visible = false;
                    chkBkCabinet.Checked = false;
                }
                setEnableControl(chkBkCabinet.Checked);
            }
        }

        private void LoadDDLData()
        {
            ddlTRoom.Items.Clear();
            ddlTUserHost.Items.Clear();
            List<Rooms> lstRoom = objRoom.getListRoomBySearch("").OrderBy(x => x.Name).ToList();
            List<Users> lstUser = objUs.getListAllUser().OrderBy(x => x.FullName).ToList();
            List<Cabinet> lstCabinet = objCa.getListCabinetBySearch("").Where(x => x.Status == 1).OrderBy(x => x.Name).ToList();
            if (lstRoom.Count > 0)
            {
                foreach (Rooms item in lstRoom)
                {
                    ListItem newItem = new ListItem();
                    newItem.Text = item.Name;
                    newItem.Value = item.RoomID.ToString();
                    ddlTRoom.Items.Add(newItem);
                }
                ddlTRoom.SelectedValue = lstRoom.FirstOrDefault().RoomID.ToString();
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
        }

        private void LoadDetailBooking(decimal eventID)
        {
            Booking_Event booking = objBk.getBookingEventByID(eventID);
            if (booking != null)
            {
                ddlTRoom.SelectedValue = booking.RoomID.ToString();
                txtTitle.Text = booking.Title;
                ddlTUserHost.SelectedValue = booking.UserHostID.ToString();
                txtTimeStart.Text = booking.TimeStart.ToString("dd-MM-yyyy HH:mm");
                txtTimeEnd.Text = booking.TimeEnd.ToString("dd-MM-yyyy HH:mm");
                txtDescription.Text = booking.Description;
                LoadBookingUser(eventID);
                hplAddUser.NavigateUrl = string.Format("~/UserChoose.aspx?eventid={0}", booking.EventID);
            }
        }

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            decimal eventID = 0;
            decimal.TryParse(Request["id"], out eventID);
            if (ValidateForm(eventID))
            {
                if (eventID == 0)
                {
                    InsertBookingEvent();
                }
                else
                {
                    UpdateBookingEvent(eventID);
                }
            }

        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            decimal eventID = 0;
            decimal.TryParse(Request["id"], out eventID);
            if (eventID > 0)
            {
                if (objBk.CheckDelete(eventID))
                {
                    DateTime timeStart = DateTime.Parse(txtTimeStart.Text.ToString());
                    DateTime timeEnd = DateTime.Parse(txtTimeEnd.Text.ToString());
                    int userID = int.Parse(ddlTUserHost.SelectedValue.ToString());
                    Users users = objUs.GetUserByID(userID);
                    Rooms room = objRoom.getRoomByID(int.Parse(ddlTRoom.SelectedValue.ToString()));
                    string strSubjectMail = "Email confirm booking event";
                    string strContentMail = "Your's book room " + room.Name + ", from " + timeStart.ToString("dd/MM/yyyy HH:mm") + " to " + timeEnd.ToString("dd/MM/yyyy HH:mm") + " had deleted<br/>";
                    string nameTo = "Administrator IoT Intel";
                    string emailTo = users.Email;
                    string ccList = getCCListEmail(userID);
                   
                    if (objBk.DeleteBooking(eventID) == true)
                    {
                        Response.Redirect("~/BookingEventsList.aspx");
                        SendEmail(strSubjectMail, strContentMail, nameTo, emailTo, ccList);
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

        private bool ValidateForm(decimal eventID)
        {
            DateTime timeStart = DateTime.Parse(txtTimeStart.Text.ToString());
            DateTime timeEnd = DateTime.Parse(txtTimeEnd.Text.ToString());
            int roomID = int.Parse(ddlTRoom.SelectedValue.ToString());

            if (timeStart > timeEnd)
            {
                CommonClass.MessageBox.Show("Time Start must smaller Time End.");
                return false;
            }
            //if (timeStart < DateTime.Now || timeEnd < DateTime.Now)
            if (DateTime.Compare(timeStart, DateTime.Now) < 0 || DateTime.Compare(timeEnd, DateTime.Now) < 0)
            {
                CommonClass.MessageBox.Show("Time Start and Time End are must be greater than current.");
                return false;
            }
            if (eventID == 0)
            {
                if (objBk.CheckInsert(roomID, timeStart, timeEnd) == false)
                {
                    CommonClass.MessageBox.Show("Cabinet exist, please select other time.");
                    return false;
                }
            }
            else
            {
                if (objBk.CheckUpdate(eventID, roomID, timeStart, timeEnd) == false)
                {
                    CommonClass.MessageBox.Show("Cabinet exist, please select other time.");
                    return false;
                }
            }

            if (chkBkCabinet.Checked)
            {
                DateTime timeStartCa = DateTime.Parse(txtTimeStartCabinet.Text.ToString());
                DateTime timeEndCa = DateTime.Parse(txtTimeEndCabinet.Text.ToString());
                Booking_Cabinet booking_Cabinet = objBkCa.getBookingByEventID(eventID);
                int cabinetID = int.Parse(ddlTCabinet.SelectedValue.ToString());
                decimal bookingID = 0;
                if (booking_Cabinet != null)
                    bookingID = booking_Cabinet.BookingID;

                if (timeStartCa > timeEndCa)
                {
                    CommonClass.MessageBox.Show("Time Start must smaller Time End.");
                    return false;
                }
                if (DateTime.Compare(timeStartCa, DateTime.Now) < 0 || DateTime.Compare(timeEndCa, DateTime.Now) < 0)
                {
                    CommonClass.MessageBox.Show("Time Start and Time End are must be greater than current.");
                    return false;
                }
                if (bookingID == 0)
                {
                    if (objBkCa.CheckInsert(cabinetID, timeStartCa, timeEndCa) == false)
                    {
                        CommonClass.MessageBox.Show("Cabinet exist, please select other time.");
                        return false;
                    }
                }
                else
                {
                    if (objBkCa.Checkupdate(bookingID, cabinetID, timeStartCa, timeEndCa) == false)
                    {
                        CommonClass.MessageBox.Show("Cabinet exist, please select other time.");
                        return false;
                    }
                }
            }    

            return true;
        }

        private void InsertBookingEvent()
        {
            string strSubjectMail = "Email confirm booking event";
            string strContentMail = "";

            Booking_Event booking = new Booking_Event();
            booking.RoomID = int.Parse(ddlTRoom.SelectedValue.ToString());
            booking.Title = txtTitle.Text.Trim();
            booking.TimeStart = DateTime.Parse(txtTimeStart.Text.ToString());
            booking.TimeEnd = DateTime.Parse(txtTimeEnd.Text.ToString());
            booking.UserHostID = int.Parse(ddlTUserHost.SelectedValue.ToString());
            booking.Description = txtDescription.Text.Trim();
            booking.UserCreatedID = this.UserIDLogin;
            booking.DateCreated = DateTime.Now;
            booking.UserUpdatedID = this.UserIDLogin;
            booking.DateUpdated = DateTime.Now;
            List<BookingUsers> lstBkUser = getListBookingUser();
            if (objBk.InsertBooking(booking, lstBkUser))
            {
                DateTime timeStart = DateTime.Parse(txtTimeStart.Text.ToString());
                DateTime timeEnd = DateTime.Parse(txtTimeEnd.Text.ToString());
                Rooms room = objRoom.getRoomByID(int.Parse(ddlTRoom.SelectedValue.ToString()));
                strContentMail = "You had booked room " + room.Name + ", from " + timeStart.ToString("dd/MM/yyyy HH:mm") + " to " + timeEnd.ToString("dd/MM/yyyy HH:mm") + "<br/>";
                if (chkBkCabinet.Checked)
                {                                                                                       
                    Booking_Cabinet bkCabinet = new Booking_Cabinet();
                    bkCabinet.EventID = booking.EventID;
                    bkCabinet.CabinetID = int.Parse(ddlTCabinet.SelectedValue.ToString());
                    bkCabinet.Pincode = getRandomPincode();
                    bkCabinet.TimeStart = DateTime.Parse(txtTimeStartCabinet.Text.ToString());
                    bkCabinet.TimeEnd = DateTime.Parse(txtTimeEndCabinet.Text.ToString());
                    bkCabinet.UserHostID = int.Parse(ddlTUserHost.SelectedValue.ToString());
                    bkCabinet.UserCreatedID = this.UserIDLogin;
                    bkCabinet.DateCreated = DateTime.Now;
                    bkCabinet.UserUpdatedID = this.UserIDLogin;
                    bkCabinet.DateUpdated = DateTime.Now;
                    if (objBkCa.InsertBooking(bkCabinet))
                    {
                        DateTime timeStartCa = DateTime.Parse(txtTimeStartCabinet.Text.ToString());
                        DateTime timeEndCa = DateTime.Parse(txtTimeEndCabinet.Text.ToString());
                        strContentMail += "You had booked cabinet " + ddlTCabinet.Text + ", from " + timeStartCa.ToString("dd/MM/yyyy HH:mm") + " to " + timeEndCa.ToString("dd/MM/yyyy HH:mm") + ", Pincode = " + bkCabinet.Pincode + "<br/>";
                    }    
                }

                Users users = objUs.GetUserByID(int.Parse(ddlTUserHost.SelectedValue.ToString()));                               
                string nameTo = "Administrator IoT Intel";
                string emailTo = users.Email;
                string ccList = "";

                SendEmail(strSubjectMail, strContentMail, nameTo, emailTo, ccList);
                radGridBookingUsers.Visible = true;
                hplAddUser.Visible = true;
                string urlDetail = string.Format("~/BookingEventsDetail.aspx?id={0}", booking.EventID);
                Response.Redirect(urlDetail);
                //Response.Redirect("~/BookingEventsList.aspx");
            }
            else
                CommonClass.MessageBox.Show("Error can't insert booking. Please contact Administrator.");
        }

        private void UpdateBookingEvent(decimal eventID)
        {
            string strContentMail = "";
            int roomID = int.Parse(ddlTRoom.SelectedValue.ToString());
            string title = txtTitle.Text.Trim();
            int userHost = int.Parse(ddlTUserHost.SelectedValue.ToString());
            DateTime timeStart = DateTime.Parse(txtTimeStart.Text.ToString());
            DateTime timeEnd = DateTime.Parse(txtTimeEnd.Text.ToString());
            string description = txtDescription.Text.Trim();
            List<BookingUsers> lstBkUser = getListBookingUser();
            if (objBk.UpdatetBooking(eventID, roomID, title, userHost, timeStart, timeEnd, description, this.UserIDLogin, lstBkUser))
            {
                Rooms room = objRoom.getRoomByID(int.Parse(ddlTRoom.SelectedValue.ToString()));
                strContentMail = "Your's book had update, room " + room.Name + ", from " + timeStart.ToString("dd/MM/yyyy HH:mm") + " to " + timeEnd.ToString("dd/MM/yyyy HH:mm") + "<br/>";
                if (chkBkCabinet.Checked)
                {
                    Booking_Cabinet booking_Cabinet = objBkCa.getBookingByEventID(eventID);
                    if (booking_Cabinet == null)
                    {
                        Booking_Cabinet bkCabinet = new Booking_Cabinet();
                        bkCabinet.EventID = eventID;
                        bkCabinet.CabinetID = int.Parse(ddlTCabinet.SelectedValue.ToString());
                        bkCabinet.Pincode = getRandomPincode();
                        bkCabinet.TimeStart = DateTime.Parse(txtTimeStartCabinet.Text.ToString());
                        bkCabinet.TimeEnd = DateTime.Parse(txtTimeEndCabinet.Text.ToString());
                        bkCabinet.UserHostID = int.Parse(ddlTUserHost.SelectedValue.ToString());
                        bkCabinet.UserCreatedID = this.UserIDLogin;
                        bkCabinet.DateCreated = DateTime.Now;
                        bkCabinet.UserUpdatedID = this.UserIDLogin;
                        bkCabinet.DateUpdated = DateTime.Now;
                        if(objBkCa.InsertBooking(bkCabinet))
                        {
                            DateTime timeStartCa = DateTime.Parse(txtTimeStartCabinet.Text.ToString());
                            DateTime timeEndCa = DateTime.Parse(txtTimeEndCabinet.Text.ToString());
                            strContentMail += "You had booked cabinet " + ddlTCabinet.Text + ", from " + timeStartCa.ToString("dd/MM/yyyy HH:mm") + " to " + timeEndCa.ToString("dd/MM/yyyy HH:mm") + ", Pincode = " + bkCabinet.Pincode + "<br/>";
                        }    
                    } 
                    else
                    {
                        int cabinetID = int.Parse(ddlTCabinet.SelectedValue.ToString());
                        int pincode = int.Parse(txtPincode.Text.Trim());
                        int userHostCa = int.Parse(ddlTUserHost.SelectedValue.ToString());
                        DateTime timeStartCa = DateTime.Parse(txtTimeStartCabinet.Text.ToString());
                        DateTime timeEndCa = DateTime.Parse(txtTimeEndCabinet.Text.ToString());
                        if (objBkCa.UpdatetBooking(booking_Cabinet.BookingID, userHostCa, pincode, userHost, timeStartCa, timeEndCa, this.UserIDLogin))
                        {
                            strContentMail += "You had booked cabinet " + ddlTCabinet.Text + ", from " + timeStartCa.ToString("dd/MM/yyyy HH:mm") + " to " + timeEndCa.ToString("dd/MM/yyyy HH:mm") + ", Pincode = " + pincode + "<br/>";
                        }    
                    }    
                }    
                Users users = objUs.GetUserByID(int.Parse(ddlTUserHost.SelectedValue.ToString()));
                string strSubjectMail = "Email confirm booking event";               
                string nameTo = "Administrator IoT Intel";
                string emailTo = users.Email;
                string ccList = "";
                SendEmail(strSubjectMail, strContentMail, nameTo, emailTo, ccList);
                Response.Redirect("~/BookingEventsList.aspx");
            }
            else
                CommonClass.MessageBox.Show("Error can't update booking. Please contact Administrator.");
        }

        private void SendEmail(string strSubjectMail, string strContentMail, string nameTo, string emailTo, string ccList)
        {
            SendMail(strSubjectMail, strContentMail, nameTo, emailTo, ccList);
        }

        private void LoadBookingUser(decimal eventID)
        {
            lstBookingUser = new List<getListBookingUser_Result>();
            lstBookingUser = objBk.getListBookingUser(eventID);
            radGridBookingUsers.DataSource = lstBookingUser;
            radGridBookingUsers.DataBind();
        }

        protected void radGridBookingUsers_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                getListBookingUser_Result bkUser = (getListBookingUser_Result)e.Item.DataItem;
                Literal ltrUserID = e.Item.FindControl("ltrUserID") as Literal;
                Literal ltrFullname = e.Item.FindControl("ltrFullname") as Literal;
                Literal ltrEmail = e.Item.FindControl("ltrEmail") as Literal;
                Literal ltrPhone = e.Item.FindControl("ltrPhone") as Literal;
                Literal ltrDelete = e.Item.FindControl("ltrDelete") as Literal;

                ltrUserID.Text = bkUser.UserID.ToString();
                ltrFullname.Text = bkUser.FullName;
                ltrEmail.Text = bkUser.Email;
                ltrPhone.Text = bkUser.Phone;
                //ltrDelete.Text = string.Format("<a href='' title='Event detail' style='text-decoration:none;'>{0}</a> ", bkUser.Delete);
            }
        }

        protected void radGridBookingUsers_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            decimal eventID = 0;
            decimal.TryParse(Request["id"], out eventID);
            lstBookingUser = new List<getListBookingUser_Result>();
            lstBookingUser = objBk.getListBookingUser(eventID);
            radGridBookingUsers.DataSource = lstBookingUser;
            //radGridBookingUsers.DataBind();
        }

        protected void radGridBookingUsers_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            GridDataItem item = e.Item as GridDataItem;
            int userID = int.Parse(item.GetDataKeyValue("UserID").ToString());
            decimal eventID = 0;
            decimal.TryParse(Request["id"], out eventID);
            if (objBk.DeleteBookingUser(eventID, userID))
            {
                lstBookingUser = new List<getListBookingUser_Result>();
                lstBookingUser = objBk.getListBookingUser(eventID);
                radGridBookingUsers.DataSource = lstBookingUser;
            }
        }

        private List<BookingUsers> getListBookingUser()
        {
            List<BookingUsers> lstResult = new List<BookingUsers>();
            if (radGridBookingUsers.Items.Count > 0)
            {
                foreach (GridDataItem row in radGridBookingUsers.Items)
                {
                    Literal ltrUserID = (Literal)row.FindControl("ltrUserID");
                    int userIDRow = int.Parse(ltrUserID.Text);
                    if (lstResult.Where(x => x.UserID == userIDRow) == null)
                    {
                        BookingUsers bookingUsers = new BookingUsers();
                        bookingUsers.BookingID = 0;
                        bookingUsers.UserID = int.Parse(ddlTUserHost.SelectedValue.ToString());
                        lstResult.Add(bookingUsers);
                    }
                }
            }
            else
            {
                BookingUsers bookingUsers = new BookingUsers();
                bookingUsers.BookingID = 0;
                bookingUsers.UserID = int.Parse(ddlTUserHost.SelectedValue.ToString());
                lstResult.Add(bookingUsers);
            }
            return lstResult;
        }

        protected void chkBkCabinet_CheckedChanged(object sender, EventArgs e)
        {
            decimal eventID = 0;
            decimal.TryParse(Request["id"], out eventID);
            if (chkBkCabinet.Checked)
            {
                setEnableControl(true);
                if (eventID == 0 || txtTimeStartCabinet.Text == "" || txtTimeEndCabinet.Text == "")
                {
                    txtTimeStartCabinet.Text = txtTimeStart.Text;
                    txtTimeEndCabinet.Text = txtTimeEnd.Text;
                }
            }
            else
            {
                setEnableControl(false);
            }
        }

        private void setEnableControl(bool bRe)
        {
            ddlTCabinet.Enabled = bRe;
            txtTimeStartCabinet.Enabled = bRe;
            txtTimeEndCabinet.Enabled = bRe;
        }

        private string getCCListEmail(int userID)
        {
            string ccList = "";
            if (radGridBookingUsers.Items.Count > 0)
            {               
                foreach (GridDataItem row in radGridBookingUsers.Items)
                {
                    Literal ltrUserID = (Literal)row.FindControl("ltrUserID");
                    int userIDRow = int.Parse(ltrUserID.Text);
                    if (userIDRow != userID)
                    {
                        Users users = objUs.GetUserByID(userIDRow);
                        ccList += users.Email + ";";
                    }    
                }    
            }
            return ccList;
        }

        public int getRandomPincode()
        {
            int pincode = 0;
            Random rmd = new Random();
            pincode = rmd.Next(1000, 10000);
            return pincode;
        }
    }
}