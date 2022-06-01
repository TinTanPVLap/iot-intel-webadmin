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
    public partial class CabinetDetail : PageBase
    {
        CabinetBussiness obj = new CabinetBussiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int cabinetID = 0;
                int.TryParse(Request["id"], out cabinetID);
                if (cabinetID != 0)
                {
                    LoadDetailCabinet(cabinetID);
                }
            }
        }

        protected void lbtSave_Click(object sender, EventArgs e)
        {
            int cabinetID = 0;
            int.TryParse(Request["id"], out cabinetID);

            if (ValidateForm(cabinetID))
            {
                int userCreate = this.UserIDLogin;
                string cabinetname = txtName.Text.Trim();
                int Status = Convert.ToInt16(chkStatus.Checked);
                if (cabinetID == 0) // insert
                {
                    Cabinet cabinet = new Cabinet();
                    cabinet.Name = cabinetname;
                    cabinet.Status = (byte)Status;
                    cabinet.UserCreatedID = userCreate;
                    cabinet.DateCreated = DateTime.Now;
                    cabinet.UserUpdatedID = userCreate;
                    cabinet.DateUpdated = DateTime.Now;
                    bool result = obj.InsertCabinet(cabinet);
                    if (result == true)
                        Response.Redirect("~/CabinetList.aspx");
                    else
                        CommonClass.MessageBox.Show("Error can't insert Cabinet. Please contact Administrator.");
                }
                else //update
                {
                    bool result = obj.UpdatetRoom(cabinetID, cabinetname, (byte)Status, userCreate);
                    if (result == true)
                        Response.Redirect("~/CabinetList.aspx");
                    else
                        CommonClass.MessageBox.Show("Error can't insert Cabinet. Please contact Administrator.");
                }
            }
        }

        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            int cabinetID = 0;
            int.TryParse(Request["id"], out cabinetID);
            if (cabinetID > 0)
            {
                if (obj.CheckDelete(cabinetID))
                {
                    if (obj.DeleteRoom(cabinetID) == true)
                    {
                        Response.Redirect("~/CabinetList.aspx");
                    }
                    else
                    {
                        CommonClass.MessageBox.Show("Error Delete this Cabinet. Please contact Administrator");
                        return;
                    }
                }
                else
                {
                    CommonClass.MessageBox.Show("Can't delete this Cabinet because it have used.");
                    return;
                }
            }
        }

        private void LoadDetailCabinet(int cabinetID)
        {
            Cabinet cabinet = obj.getCabinetByID(cabinetID);
            if (cabinet != null)
            {
                txtName.Text = cabinet.Name;
                chkStatus.Checked = Convert.ToBoolean(cabinet.Status);
            }
        }
        private bool ValidateForm(int cabinetID)
        {
            string cabinetName = txtName.Text.Trim();
            if (string.IsNullOrEmpty(cabinetName))
            {
                CommonClass.MessageBox.Show("Name can't null. Please input Name.");
                return false;
            }
            if (cabinetID == 0)
            {
                if (obj.CheckInsert(cabinetName) == false)
                {
                    CommonClass.MessageBox.Show("Name is exists. Please check again.");
                    return false;
                }
            }
            else
            {
                if (obj.Checkupdate(cabinetID, cabinetName) == false)
                {
                    CommonClass.MessageBox.Show("Name is exists. Please check again.");
                    return false;
                }
            }
            return true;
        }
    }
}