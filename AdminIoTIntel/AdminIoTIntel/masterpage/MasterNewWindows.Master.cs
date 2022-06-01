using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminiotintel.masterpage
{
    public partial class MasterNewWindows : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ltrJquey.Text = string.Format("<script src='{0}' type='text/javascript'></script>", ResolveUrl("~/js/jquery.js"));
                ltrNewWindows.Text = string.Format("<link href='{0}' rel='stylesheet' type='text/css' />", ResolveUrl("~/css/styleNewWindows.css"));
                ltrJqueryUI.Text = string.Format("<link href='{0}' rel='stylesheet' type='text/css' />", ResolveUrl("~/css/jquery-ui.css"));
                ltrJquery18.Text = string.Format("<script src='{0}' type='text/javascript'></script>", ResolveUrl("~/js/jquery-1.8.3.js"));

                ltrJqueryuijs.Text = string.Format("<script src='{0}' type='text/javascript'></script>", ResolveUrl("~/js/jquery-ui.js"));
                ltrTimepickerCss.Text = string.Format("<link href='{0}' rel='stylesheet' type='text/css' />", ResolveUrl("~/js/jquery.ui.timepicker.css"));
                ltrTimepickerJs.Text = string.Format("<script src='{0}' type='text/javascript'></script>", ResolveUrl("~/js/jquery.ui.timepicker.js"));
                ltrAccountting.Text = string.Format("<script src='{0}' type='text/javascript'></script>", ResolveUrl("~/js/accounting.js"));

                ltrtimepickercss1.Text = string.Format("<link href='{0}' rel='stylesheet' type='text/css' />", ResolveUrl("~/css/jquery.datetimepicker.css"));
                ltrtimepickerjs1.Text = string.Format("<script src='{0}' type='text/javascript'></script>", ResolveUrl("~/js/jquery.datetimepicker.js"));

                ltrScript.Text = string.Format("<script type='text/javascript'>var urlIconDate = '{0}'</script>", ResolveUrl("~/images/date.jpg"));
                ltrScreen.Text = string.Format("<link href='{0}' rel='stylesheet' type='text/css' />", ResolveUrl("~/css/screen.css"));
            }
        }
    }
}