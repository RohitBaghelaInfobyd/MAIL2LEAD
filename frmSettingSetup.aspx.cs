using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminTool
{
    public partial class frmSettingSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int LoggedInuserId, userId;
                LoggedInuserId = Convert.ToInt32(Session["LoggedInuserId"]);
                userId = Convert.ToInt32(Session["ViewUserId"]);
                if (userId < 0)
                {
                    userId = LoggedInuserId;
                }
                if (LoggedInuserId < 1)
                {
                    Response.Redirect("~/default.aspx");
                }
            }
        }
        
        protected void btnUserProfileSeeting_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/frmSettingUser.aspx");
        }

        protected void btnLeadAssignment_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/frmLeadAssignment.aspx");
        }

        protected void btnCrmSetting_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/frmSettingCRM.aspx");
        }

        protected void btnUniqueIdentifier_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/frmLeadUniqueIdentifier.aspx");
        }

        protected void btnForceSync_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/frmForceSync.aspx");
        }

        protected void btnPaymentModule_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/frmPayment.aspx");
        }
    }
}