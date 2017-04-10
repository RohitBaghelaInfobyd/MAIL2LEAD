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
    public partial class frmSignup : System.Web.UI.Page
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

        protected void imgbtnSubmitSignup_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    string result = string.Empty, firstName = string.Empty, lastName = string.Empty, emailId = string.Empty, password = string.Empty;
                    int UserId;

                    firstName = tbFirstName.Text;
                    lastName = tbLastName.Text;
                    emailId = tbEmailID.Text;
                    password = tbPassword.Text;
                    if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(emailId) && password.Length > 5)
                    {
                        DataTable dt = databaseProvider.AddNewUserIntoDatabase(1, firstName, lastName, emailId, password, 1);
                        result = dt.Rows[0]["result"].ToString().ToUpper();
                        if (result.Equals("SUCCESS"))
                        {
                            UserId = Convert.ToInt32(dt.Rows[0]["id"].ToString());
                            if (UserId > 0)
                            {
                                result = summaryEmail.sendUserSignupWelcomeMail(UserId);
                                if (result.ToUpper().Equals("SUCCESS"))
                                {
                                    lblPasswordTitle.Text = "We have Sent you a verification email to your registerd email id. please verify you email to complete your signup process. \n\n Thanks, Infobyd Team";
                                    lblPasswordTitle.ForeColor = System.Drawing.Color.Green;
                                    tbFirstName.Visible = false;
                                    tbLastName.Visible = false;
                                    tbEmailID.Visible = false;
                                    lblFirstName.Visible = false;
                                    lbllastName.Visible = false;
                                    lblEmailId.Visible = false;
                                    lblMsg.Visible = false;
                                    imgbtnSubmitSignup.Visible = false;
                                    imgCancelButton.Visible = false;

                                }
                                else
                                {
                                    lblMsg.Text = "Some thing went Wrong Please try again later.";
                                    lblMsg.ForeColor = System.Drawing.Color.Green;
                                }
                            }
                        }
                        else
                        {
                            lblMsg.Text = result;
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Please provide all required information";
                        if (password.Length < 5)
                        {
                            lblMsg.Text = "Password should be greater than 5";
                        }
                        lblMsg.Visible = true;
                    }

                }
                catch (Exception ex)
                {
                    lblMsg.Text = "Some thing went wrong please try again later.";
                    lblMsg.Visible = true;
                }

            }
        }

        protected void imgCancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/default.aspx");
        }
    }
}