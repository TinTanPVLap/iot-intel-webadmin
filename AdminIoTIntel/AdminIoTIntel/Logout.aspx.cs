using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminiotintel
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserID"] = string.Empty;
            Session["UserName"] = string.Empty;
            Session["PassWord"] = false;
            Session["GroupID"] = 0;
            Response.Redirect("~/Login.aspx");
        }
    }
}