using AdminTool.DataBase;
using AdminTool.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminTool
{
    public partial class frmForgotPassword : System.Web.UI.Page
    {
        DataBaseProvider databaseProvider = new DataBaseProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                EnableViewState = true;
                try
                {
                    LinkButton lb = (LinkButton)this.Page.Master.FindControl("linkLogout") as LinkButton;
                    lb.Visible = false;

                }
                catch (Exception ex)
                { }
            }
        }

        protected void imgCancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/default.aspx");
        }

        protected void imgbtnSubmitSignup_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    string result = string.Empty, emailId = string.Empty;
                    int UserId;
                    emailId = tbEmailID.Text;
                    if (!string.IsNullOrEmpty(emailId))
                    {
                        DataTable dt = databaseProvider.getUserInfoByEmailID(emailId);
                        if (dt.Rows.Count > 0)
                        {

                            UserId = Convert.ToInt32(dt.Rows[0]["id"].ToString());
                            if (UserId > 0)
                            {
                                result = summaryEmail.sendUserResetPasswordEmail(UserId);
                                if (result.ToUpper().Equals("SUCCESS"))
                                {
                                    lblPasswordTitle.Text = "We have successfully Sent you a reset password email to your registerd email id. please check your inbox to reset your password. \n\n Thanks, Infobyd Team";
                                    lblPasswordTitle.ForeColor = System.Drawing.Color.Green;
                                    lblEmailId.Visible = false;
                                    lblMsg.Visible = false;
                                    imgbtnSubmitSignup.Visible = false;
                                    imgCancelButton.Visible = false;
                                }
                                else
                                {
                                    lblMsg.Text = "Some thing went Wrong Please try again later.";
                                    lblMsg.ForeColor = System.Drawing.Color.Red;
                                    lblMsg.Visible = true;
                                }
                            }
                            else
                            {
                                lblMsg.Text = "Some thing went Wrong Please try again later.";
                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                lblMsg.Visible = true;
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Please provide valid email address.";
                            lblMsg.Visible = true;
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Please provide valid email address.";
                        lblMsg.Visible = true;
                    }

                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}