using AdminTool.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminTool
{
    public partial class frmpassword : System.Web.UI.Page
    {
        static DataBaseProvider dataBaseProvider = new DataBaseProvider();
        private static int userId, type;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    LinkButton lb = (LinkButton)this.Page.Master.FindControl("linkLogout") as LinkButton;
                    lb.Visible = false;

                }
                catch (Exception ex)
                { }

                try
                {
                    if (Request.QueryString.Count >= 2)
                    {
                        string QueryType = Request.QueryString[0];
                        if (QueryType.Contains("240410248a413cc2b4cb03bdba2ed77"))
                        {
                            string userToken = Request.QueryString[1];
                            DataTable UserInfo = dataBaseProvider.CheckUserToken(userToken);
                            if (UserInfo.Rows.Count > 0)
                            {
                                // Section when user comes to reset password 
                                Session["UserName"] = UserInfo.Rows[0]["firstName"].ToString();
                                userId = Convert.ToInt32(UserInfo.Rows[0]["id"].ToString());
                                lblPasswordTitle.Text = "Hi " + Session["UserName"].ToString() + ",\n\n Please create new password for login";
                                lblHeader.Text = "Reset Password";
                                type = 1;
                            }
                            else
                            {
                                Response.Redirect("~/default.aspx");
                            }
                        }
                        else if (QueryType.Contains("121sdf16s6456fa413cc2b4cb03bdba2ed77"))
                        {
                            string userToken = Request.QueryString[1];
                            DataTable UserInfo = dataBaseProvider.CheckUserToken(userToken);
                            if (UserInfo.Rows.Count > 0)
                            {
                                // Section when user comes to Confirm email and set new password 
                                Session["UserName"] = UserInfo.Rows[0]["firstName"].ToString();
                                userId = Convert.ToInt32(UserInfo.Rows[0]["id"].ToString());
                                lblHeader.Text = "Signup Process";
                                lblPasswordTitle.Text = "Hi " + Session["UserName"].ToString() + ",\n\n Please create password to complete signup process & login.";
                                type = 2;
                            }
                            else
                            {
                                Response.Redirect("~/default.aspx");
                            }
                        }
                        else
                        {
                            Response.Redirect("~/default.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("~/default.aspx");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                    dataBaseProvider.logApplicationError("ERROR In " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Password Page");

                    Response.Redirect("~/default.aspx");
                }
            }
        }

        protected void imgbtnRedirectlogin_Click(object sender, EventArgs e)
        {
            if (userId > 0)
            {
                Session["LoggedInuserId"] = userId;
            }
            Response.Redirect("~/default.aspx");
        }

        protected void imgbtnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                string password, confirmpassword, result;
                password = tbPassword.Text;
                confirmpassword = tbConfirmPassword.Text;
                lblMsg.Visible = false;
                if (password == confirmpassword && !string.IsNullOrEmpty(password.Trim()))
                {
                    if (userId > 0)
                    {
                        result = dataBaseProvider.sp_reset_new_password(userId, password);
                        if (result.Equals("SUCCESS"))
                        {
                            tbPassword.Visible = false;
                            tbConfirmPassword.Visible = false;
                            lblpassword.Visible = false;
                            lblconfirmpassword.Visible = false;
                            lblMsg.Visible = false;
                            imgbtnResetPassword.Visible = false;
                            if (type > 1)
                            {
                                lblPasswordTitle.Text = "You has been successfully complete signup process. Please <a href='http://mail2lead.infobyd.com/'>Click Here</a> to Continue Login.";
                            }
                            else
                            {
                                lblPasswordTitle.Text = "You has been successfully change your password. Please <a href='http://mail2lead.infobyd.com/'>Click Here</a> to Continue Login.";
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Some Error Occured please try again later.";
                            lblMsg.Visible = true;
                        }
                    }
                }
                else
                {
                    lblMsg.Text = "Password and Confirm password must be same & should be contain any alphanumeric character ";
                    lblMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                dataBaseProvider.logApplicationError("ERROR In " + MethodBase.GetCurrentMethod().DeclaringType.Name + " " + MethodBase.GetCurrentMethod() + " " + ex.Message + " Line Number " + trace.GetFrame(0).GetFileLineNumber(), "Password Page");
                lblMsg.Text = "Some error occured please try again later";
                lblMsg.Visible = true;
            }
        }
    }
}