using adminiotintel.Bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminiotintel.ctrl
{
    public partial class ctrlMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                setMenuActive();

                int typeUserID = 0;
                int.TryParse(Session["GroupID"].ToString(), out typeUserID);
                if (typeUserID == TypeUserIDCons.Administrator)
                {
                    liUser.Visible = true;
                }
                else
                {
                    liUser.Visible = false;
                }
            }
        }
        private void setMenuActive()
        {
            string urlNamePage = Request.Url.Segments[Request.Url.Segments.Length - 1];

            switch (urlNamePage)
            {
                case "ChangePass.aspx":
                case "UserLists.aspx":
                case "UserDetails.aspx":
                    hplSystem.Attributes.Clear();
                    hplSystem.Attributes.Add("class", "active");
                    break;
                case "ShopLists.aspx":
                case "ShopDetails.aspx":
                    hplRoom.Attributes.Clear();
                    hplRoom.Attributes.Add("class", "active");
                    break;

            }
        }
    }
}