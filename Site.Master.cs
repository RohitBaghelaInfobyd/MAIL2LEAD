using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminTool
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (string.IsNullOrEmpty(Session["UserName"].ToString()))
                    {
                        lblUserName.Text = Session["UserName"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    lblUserName.Text = "Hello";
                }
            }
        }


        protected void linkLogout_Click(object sender, EventArgs e)
        {
            Session["LoggedInuserId"] = 0;
            Session["ViewUserId"] = 0;
            Response.Redirect("~/default.aspx");
        }
    }
}