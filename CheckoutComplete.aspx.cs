using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdminTool.DataBase;

namespace AdminTool.Checkout
{
    public partial class CheckoutComplete : System.Web.UI.Page
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
                        // Verify user has completed the checkout process.
                        if ((string)Session["userCheckoutCompleted"] != "true")
                        {
                            Session["userCheckoutCompleted"] = string.Empty;
                            Response.Redirect("CheckoutError.aspx?" + "Desc=Unvalidated%20Checkout.");
                        }

                        NVPAPICaller payPalCaller = new NVPAPICaller();
                        string retMsg = "";
                        string token = "";
                        string finalPaymentAmount = "";
                        string PayerID = "";
                        NVPCodec decoder = new NVPCodec();

                        token = Session["token"].ToString();
                        PayerID = Session["payerId"].ToString();
                        finalPaymentAmount = Session["payment_amt"].ToString();

                        bool ret = payPalCaller.DoCheckoutPayment(finalPaymentAmount, token, PayerID, ref decoder, ref retMsg);
                        if (ret)
                        {
                            // Retrieve PayPal confirmation value.
                            string TransactionId = decoder["PAYMENTINFO_0_TRANSACTIONID"].ToString();
                            int UserId = Convert.ToInt32(Session["ViewUserId"].ToString());
                            string PaymentAmount = Session["payment_amt"].ToString();
                            int ApiCount = Convert.ToInt32(Session["ApiCount"].ToString());
                            dataBaseProvider.UpdatePaymentInfoByUserId(UserId, TransactionId, "PayPal", PaymentAmount, ApiCount);
                            lblTransactionId.Text = "Payment Transaction ID: " + TransactionId;
                        }
                        else
                        {
                            Response.Redirect("CheckoutError.aspx?" + retMsg);
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }

        protected void Continue_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmDashBoard.aspx");
        }
    }
}