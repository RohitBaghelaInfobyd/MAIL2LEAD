using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WingtipToys.Checkout
{
    public partial class CheckoutCancel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int LoggedInuserId;
            LoggedInuserId = Convert.ToInt32(Session["LoggedInuserId"]);
            if (LoggedInuserId < 1)
            {
                Response.Redirect("~/default.aspx");

            }
            else
            {
                Response.Redirect("~/frmDashBoard.aspx");
            }
        }
    }
}