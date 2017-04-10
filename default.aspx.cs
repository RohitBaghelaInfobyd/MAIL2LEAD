using AdminTool.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminTool
{
    public partial class _default : System.Web.UI.Page
    {
        DataBaseProvider databaseProvider = new DataBaseProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                EnableViewState = true;
                ErrorMessage.Visible = false;
            }
        }


        protected void linkforgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmForgotPassword.aspx");
        }

        protected void SignupButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmSignup.aspx");
        }
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string userName = "Welcome", userType = "1", EmailId = string.Empty, PasswordText = string.Empty;
            int UserId;
            DataTable dt;
            UserId = Convert.ToInt32(Session["LoggedInuserId"]);
            if (UserId > 0)
            {
                Response.Redirect("~/frmDashBoard.aspx");
            }
            else
            {
                EmailId = UserName.Text;
                PasswordText = Password.Text;
                dt = databaseProvider.LoginUser(EmailId, PasswordText);
                if (dt.Rows.Count > 0)
                {
                    UserId = Convert.ToInt32(dt.Rows[0]["id"].ToString());
                    userName = dt.Rows[0]["name"].ToString();
                    userType = dt.Rows[0]["userType"].ToString();
                }

                if (UserId > 0)
                {
                    Session["LoggedInuserId"] = UserId;
                    Session["ViewUserId"] = UserId;
                    Session["UserName"] = "Welcome, " + userName;
                    Session["UserType"] = userType;
                    if (Convert.ToInt32(userType) > 1)
                    {
                        Response.Redirect("~/frmUserList.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/frmDashBoard.aspx");
                    }
                }
                else
                {
                    ErrorMessage.Text = "Invalid username or password.";
                    ErrorMessage.Visible = true;
                }
            }
        }
    }
}