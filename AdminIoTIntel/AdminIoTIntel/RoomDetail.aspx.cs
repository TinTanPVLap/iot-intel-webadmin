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
    public partial class RoomDetail : PageBase
    {
        RoomBussiness obj = new RoomBussiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int roomID = 0;
                int.TryParse(Request["id"], out roomID);
                if (roomID != 0)
                {
                    LoadDetailRoom(roomID);
                }
            }    
        }

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            int roomID = 0;
            int.TryParse(Request["id"], out roomID);

            if (ValidateForm(roomID))
            {
                int userCreate = this.UserIDLogin;
                string roomName = txtName.Text.Trim();
                string description = txtDescription.Text.Trim();
                if (roomID == 0) // insert
                {
                    Rooms rooms = new Rooms();
                    rooms.Name = roomName;
                    rooms.Description = description;
                    rooms.UserCreatedID = userCreate;
                    rooms.DateCreated = DateTime.Now;
                    rooms.UserUpdatedID = userCreate;
                    rooms.DateUpdated = DateTime.Now;
                    bool result = obj.InsertRoom(rooms);
                    if (result == true)
                        Response.Redirect("~/RoomList.aspx");
                    else
                        CommonClass.MessageBox.Show("Error can't insert Room. Please contact Administrator.");
                }
                else //update
                {
                    bool result = obj.UpdatetRoom(roomID, roomName, description, userCreate);
                    if (result == true)
                        Response.Redirect("~/RoomList.aspx");
                    else
                        CommonClass.MessageBox.Show("Error can't update User. Please contact Administrator.");
                }    
            }    
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            int roomID = 0;
            int.TryParse(Request["id"], out roomID);
            if (roomID > 0)
            {
                if (obj.CheckDelete(roomID))
                {
                    if (obj.DeleteRoom(roomID) == true)
                    {
                        Response.Redirect("~/RoomList.aspx");
                    }
                    else
                    {
                        CommonClass.MessageBox.Show("Error Delete this Room. Please contact Administrator");
                        return;
                    }
                }
                else
                {
                    CommonClass.MessageBox.Show("Can't delete this room because it have used.");
                    return;
                }
            }    
        }

        private void LoadDetailRoom(int roomID)
        {
            Rooms rooms = obj.getRoomByID(roomID);
            if (rooms != null)
            {
                txtName.Text = rooms.Name;
                txtDescription.Text = rooms.Description;
            }    
        }

        private bool ValidateForm(int roomID)
        {
            string roomName = txtName.Text.Trim();
            if (string.IsNullOrEmpty(roomName))
            {
                CommonClass.MessageBox.Show("Name can't null. Please input Name.");
                return false;
            }
            if (roomID == 0)
            {
                if (obj.CheckInsert(roomName) == false)
                {
                    CommonClass.MessageBox.Show("Name is exists. Please check again.");
                    return false;
                }    
            }
            else
            {
                if (obj.Checkupdate(roomID, roomName) == false)
                {
                    CommonClass.MessageBox.Show("Name is exists. Please check again.");
                    return false;
                }
            }    
            return true;
        }
    }
}