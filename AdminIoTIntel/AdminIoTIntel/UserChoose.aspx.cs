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
    public partial class UserChoose : System.Web.UI.Page
    {
        UsersBussiness objUs = new UsersBussiness();
        BookingEventBussiness objBk = new BookingEventBussiness();
        RoomBussiness objRoom = new RoomBussiness();
        public decimal _eventID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            decimal.TryParse(Request["eventID"], out _eventID);
            if (!IsPostBack)
            {
                LoadGrid();
            }
        }

        private void LoadGrid()
        {
            string key = txtKey.Text.Trim();
            List<Users> lstUser = objUs.getListUserBySearch(key, 100);
            radGridUser.DataSource = lstUser;
            radGridUser.DataBind();
        }

        protected void lbtFilter_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }

        protected void radGridUser_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                Users user = (Users)e.Item.DataItem;
                Literal ltrUserID = e.Item.FindControl("ltrUserID") as Literal;
                Literal ltrFullName = e.Item.FindControl("ltrFullName") as Literal;
                Literal ltrEmail = e.Item.FindControl("ltrEmail") as Literal;
                Literal ltrPhone = e.Item.FindControl("ltrPhone") as Literal;
                HiddenField hdUserID = e.Item.FindControl("hdUserID") as HiddenField;
                CheckBox chkChose = e.Item.FindControl("chkChose") as CheckBox;

                hdUserID.Value = user.UserID.ToString();
                ltrUserID.Text = user.UserID.ToString();
                ltrFullName.Text = user.FullName;
                ltrEmail.Text = user.Email;
                ltrPhone.Text = user.Phone;


                List<getListBookingUser_Result> lstBkUser = objBk.getListBookingUser(_eventID);
                getListBookingUser_Result bkUser = lstBkUser.Where(x=>x.UserID == Convert.ToDecimal(hdUserID.Value)).FirstOrDefault();
                if (bkUser != null)
                {
                    chkChose.Checked = true;
                }    
            }
        }

        protected void radGridUser_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            string key = txtKey.Text.Trim();
            List<Users> lstUser = objUs.getListUserBySearch(key, 100);
            radGridUser.DataSource = lstUser;
            radGridUser.DataBind();
        }

        protected void lbtAddUser_Click(object sender, EventArgs e)
        {
            List<BookingUsers> lstBkUser = new List<BookingUsers>();
            Booking_Event booking = objBk.getBookingEventByID(_eventID);
            Users users = objUs.GetUserByID(booking.UserHostID);
            Rooms room = objRoom.getRoomByID(booking.RoomID);

            string strSubjectMail = "Email confirm booking event";
            string strContentMail = "You had invited join booked room " + room.Name + ", from " + booking.TimeStart.ToString("dd/MM/yyyy HH:mm") + " to " + booking.TimeEnd.ToString("dd/MM/yyyy HH:mm") + "<br/>";
            string nameTo = "Administrator IoT Intel";
            string emailTo = users.Email;
            string ccList = "";

            foreach (GridDataItem item in radGridUser.Items)
            {
                var chkAdd = (CheckBox)item.FindControl("chkChose");
                var hdProductID = (HiddenField)item.FindControl("hdUserID");
                int userID = int.Parse(hdProductID.Value);
                if (chkAdd.Checked == true)
                {
                    BookingUsers bk = new BookingUsers();
                    bk.BookingID = _eventID;
                    bk.UserID = userID;
                    lstBkUser.Add(bk);
                    Users user = objUs.GetUserByID(booking.UserHostID);
                    ccList += user.Email + ";";
                }    
            }
            if (lstBkUser.Count > 0)
            {
                if (objBk.InsertBookingUser(lstBkUser, _eventID))
                {
                    SendEmail(strSubjectMail, strContentMail, nameTo, emailTo, ccList);
                }    
            }
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }

        private void SendEmail(string strSubjectMail, string strContentMail, string nameTo, string emailTo, string ccList)
        {
            //PageBase obj = new PageBase();
            //Users users = objUs.GetUserByID(int.Parse(ddlTUserHost.SelectedValue.ToString()));
            //Rooms room = objRoom.getRoomByID(int.Parse(ddlTRoom.SelectedValue.ToString()));
            //DateTime timeStart = DateTime.Parse(txtTimeStart.Text.ToString());
            //DateTime timeEnd = DateTime.Parse(txtTimeEnd.Text.ToString());

            //string strSubjectMail = "Email confirm booking cabinet";
            //string strContentMail = "You had booked room " + room.Name + ", from " + timeStart.ToString("dd/MM/yyyy HH:mm") + " to " + timeEnd.ToString("dd/MM/yyyy HH:mm") + "<br/>";
            //string nameTo = "Administrator IoT Intel";
            //string emailTo = "";
            //emailTo = users.Email;
            //string ccList = "";
            //obj.SendMail(strSubjectMail, strContentMail, nameTo, emailTo, ccList);
        }
    }
}