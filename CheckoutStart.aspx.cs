using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminTool.Checkout
{
    public partial class CheckoutStart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int LoggedInuserId;
                    LoggedInuserId = Convert.ToInt32(Session["LoggedInuserId"]);
                    if (LoggedInuserId < 1)
                    {
                        Response.Redirect("~/default.aspx");

                    }
                    else
                    {
                        NVPAPICaller payPalCaller = new NVPAPICaller();
                        string retMsg = "";
                        string token = "";

                        if (Session["payment_amt"] != null)
                        {
                            string amt = Session["payment_amt"].ToString();

                            bool ret = payPalCaller.ShortcutExpressCheckout(amt, ref token, ref retMsg);
                            if (ret)
                            {
                                Session["token"] = token;
                                Response.Redirect(retMsg);
                            }
                            else
                            {
                                Response.Redirect("CheckoutError.aspx?" + retMsg);
                            }
                        }
                        else
                        {
                            Response.Redirect("CheckoutError.aspx?ErrorCode=AmtMissing");
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }
    }
}