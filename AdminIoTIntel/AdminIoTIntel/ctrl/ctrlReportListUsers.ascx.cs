using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using adminiotintel.Bussiness;
using adminiotintel.model;

namespace adminiotintel.ctrl
{
    public partial class ctrlReportListUsers : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }    
        }

        private void LoadData()
        {
            UsersBussiness obj = new UsersBussiness();
            List<Users> lstUsers = obj.getListAllUser();
            ltrAllUser.Text = lstUsers.Count.ToString("N0");
            if (lstUsers.Where(x => x.Status == 1).ToList().Count > 0)
                ltrActivated.Text = lstUsers.Where(x => x.Status == 1).ToList().Count.ToString("N0");
            else
                ltrActivated.Text = "0";

            if (lstUsers.Where(x => x.Status == 0).ToList().Count > 0)
                ltrNotActivated.Text = lstUsers.Where(x => x.Status == 0).ToList().Count.ToString("N0");
            else
                ltrNotActivated.Text = "0";
        }
    }
}