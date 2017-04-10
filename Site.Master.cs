using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

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
                    if (!string.IsNullOrEmpty(Session["UserName"].ToString()))
                    {
                        lblUserName.Text = Session["UserName"].ToString();
                    }
                    else
                    {
                        lblUserName.Text = "Welcome";
                    }
                }
                catch (Exception ex)
                {
                    lblUserName.Text = "Welcome";
                }
            }
        }

        protected void TreeView1_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
        {
            int userType = Convert.ToInt32(Session["UserType"]);
            if (userType > 1)
            {
                if (e.Node.Text.ToLower().Equals("dashboard"))
                {
                    TreeNode tree = new TreeNode();
                    tree.NavigateUrl = "frmUserList.aspx";
                    tree.Text = "Manage User";
                    TreeView1.Nodes.AddAt(0, tree);
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["LoggedInuserId"] = 0;
            Session["ViewUserId"] = 0;
            Response.Redirect("~/default.aspx");
        }
    }
}