using AdminTool.DataBase;
using System;
using System.Collections.Generic;
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
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {

            if (IsValid)
            {
                String EmailId = string.Empty, PasswordText = string.Empty;
                int UserId;
                UserId = Convert.ToInt32(Session["LoggedInuserId"]);
                if (UserId > 0)
                {
                    Response.Redirect("~/frmCategory.aspx");
                }

                {
                    EmailId = UserName.Text;
                    PasswordText = Password.Text;
                    UserId = databaseProvider.LoginUser(EmailId, PasswordText);

                    if (UserId > 0)
                    {
                        Session["LoggedInuserId"] = UserId;
                        Session["ViewUserId"] = UserId;
                        Response.Redirect("~/frmCategory.aspx");
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
}