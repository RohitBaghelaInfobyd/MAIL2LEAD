﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminTool.Checkout
{
    public partial class CheckoutReview : System.Web.UI.Page
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
                        string PayerID = "";
                        NVPCodec decoder = new NVPCodec();
                        token = Session["token"].ToString();

                        bool ret = payPalCaller.GetCheckoutDetails(token, ref PayerID, ref decoder, ref retMsg);
                        if (ret)
                        {
                            Session["payerId"] = PayerID;

                            // Verify total payment amount as set on CheckoutStart.aspx.
                            try
                            {
                                decimal paymentAmountOnCheckout = Convert.ToDecimal(Session["payment_amt"].ToString());
                                decimal paymentAmoutFromPayPal = Convert.ToDecimal(decoder["AMT"].ToString());

                                lblTotalPayment.Text = "Total Payment : " + paymentAmountOnCheckout.ToString() + "$";
                                lblAPICount.Text = Session["payment_name"].ToString();
                                if (paymentAmountOnCheckout != paymentAmoutFromPayPal)
                                {
                                    Response.Redirect("CheckoutError.aspx?" + "Desc=Amount%20total%20mismatch.");
                                }
                            }
                            catch (Exception)
                            {
                                Response.Redirect("CheckoutError.aspx?" + "Desc=Amount%20total%20mismatch.");
                            }

                            // Get DB context.
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }

        protected void CheckoutConfirm_Click(object sender, EventArgs e)
        {
            Session["userCheckoutCompleted"] = "true";
            Response.Redirect("~/CheckoutComplete.aspx");
        }
    }
}