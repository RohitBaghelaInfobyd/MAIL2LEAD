using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AdminTool.DataBase;
using System.Configuration;

namespace AdminTool
{
    public partial class frmPayment : System.Web.UI.Page
    {
        static DataBaseProvider dataBaseProvider = new DataBaseProvider();
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
                        GetPaymentInfo(LoggedInuserId);

                    }
                    ((Label)(Master).FindControl("lblUserName")).Text = Session["UserName"].ToString();
                }
                catch (Exception exc)
                {
                    ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, exc.Message);
                }
            }

        }

        public void GetPaymentInfo(int UserId)
        {
            DataTable dt = new DataTable();
            dt = dataBaseProvider.getAvailablePaymentOption(UserId);

            if (dt.Rows.Count < 1)
            {
                emptyListMsg.Visible = true;
                GridPaymentDetails.Visible = false;

            }
            else
            {
                emptyListMsg.Visible = false;
                GridPaymentDetails.Visible = true;
                GridPaymentDetails.DataSource = dt;
                GridPaymentDetails.DataBind();
            }
        }


        protected void ImageGoBack3_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmApiReport.aspx");
        }

        protected void imgButtonbuy_Command(object sender, CommandEventArgs e)
        {

            int PaymentId = Convert.ToInt32(e.CommandArgument.ToString()) - 1;
            int UserId, ApiCount;
            string TransactionId, PaymentSource, PaymentAmount, PaymentDescription, result;
            UserId = Convert.ToInt32(Session["ViewUserId"].ToString());
            PaymentAmount = ((HiddenField)GridPaymentDetails.Rows[PaymentId].FindControl("hiddenPaymentAmout")).Value.Trim();
            ApiCount = Convert.ToInt32(((HiddenField)GridPaymentDetails.Rows[PaymentId].FindControl("hiddenApiCount")).Value.Trim());
            Session["ApiCount"] = ApiCount;
            Session["payment_qty"] = "1";
            Session["BRANDNAME"] = "Mail2Lead";
            string Username = (string)Request.QueryString["Username"];

            int UserType = Convert.ToInt32(Session["UserType"].ToString());
            if (UserType > 1)
            {
                PaymentSource = "Admin";
                TransactionId = "000";
                result=dataBaseProvider.UpdatePaymentInfoByUserId(UserId, TransactionId, PaymentSource, PaymentAmount, ApiCount);
                if (result.Equals("SUCCESS"))
                {
                    lblMsg.Text = "Payment Done Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMsg.Text = "Some Error Occured";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                lblMsg.Visible = true;
            }
            else
            {
                PaymentDescription = ((Label)GridPaymentDetails.Rows[PaymentId].FindControl("lblDescription")).Text.Trim();
                Session["payment_amt"] = Convert.ToInt32(PaymentAmount);
                Session["payment_name"] = PaymentDescription;
                Session["payment_qty"] = "1";
                Session["BRANDNAME"] = "Mail2Lead";
                Response.Redirect("Checkout/CheckoutStart.aspx");
            }


        }

        protected void ImgPaymentHistory_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmPaymentHistory.aspx");
        }

    }
}