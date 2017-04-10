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
    public partial class frmCrmSetting : System.Web.UI.Page
    {
        static DataBaseProvider dataBaseProvider = new DataBaseProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int LoggedInuserId, UserId;
                    LoggedInuserId = Convert.ToInt32(Session["LoggedInuserId"]);
                    UserId = Convert.ToInt32(Session["ViewUserId"]);
                    if (LoggedInuserId < 1)
                    {
                        Response.Redirect("~/default.aspx");
                    }
                    else
                    {
                        SetUserInfoIntoForm(UserId);
                        tbGmailPassword.Attributes.Add("value", tbGmailPassword.Text);
                    }
                }
                catch (Exception ex)
                { ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message); }
            }

        }

        public void SetUserInfoIntoForm(int userId)
        {
            if (userId > 0)
            {
                if (userId > 0)
                {
                    DataTable UserInfo = dataBaseProvider.getUserGmailInfoById(userId);
                    if (UserInfo.Rows.Count > 0)
                    {
                        tbGmailId.Text = UserInfo.Rows[0]["EmailId"].ToString();
                        tbGmailPassword.Text = UserInfo.Rows[0]["password"].ToString();
                        tbConfigurationToken.Text = UserInfo.Rows[0]["configurationAuthToken"].ToString();
                        EnableDisable(false);
                    }
                }
            }
            else
            {
                tbGmailId.Text = string.Empty;
                tbGmailPassword.Text = string.Empty;
                tbConfigurationToken.Text = string.Empty;
                EnableDisable(true);
            }
        }

        private void EnableDisable(bool isEnable)
        {
            tbGmailId.Enabled = isEnable;
            tbGmailPassword.Enabled = isEnable;
            tbConfigurationToken.Enabled = isEnable;
            btnUpdate.Text = isEnable ? "Save" : "Update";
            btnCancel.Visible = isEnable;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            if (!tbGmailId.Enabled)
            {
                EnableDisable(true);
            }
            else
            {
                int ViewUserId = Convert.ToInt32(Session["ViewUserId"]);

                string GmailId, GmailPassword, ConfigurationToken, result;
                GmailId = tbGmailId.Text;
                GmailPassword = tbGmailPassword.Text;
                ConfigurationToken = tbConfigurationToken.Text;
                if (ViewUserId > 0)
                {
                    result = dataBaseProvider.InsertUserGmailInfo(ViewUserId, GmailId, GmailPassword, ConfigurationToken);
                    if (result.Equals("SUCCESS"))
                    {
                        lblMsg.Text = "Information Update Successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        SetUserInfoIntoForm(ViewUserId);
                    }
                    else
                    {
                        lblMsg.Text = "Some Error Occured";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                SetUserInfoIntoForm(ViewUserId);
                lblMsg.Visible = true;
            }
        }

        protected void btnUpdateCancel_Click(object sender, EventArgs e)
        {
            int UserId;
            UserId = Convert.ToInt32(Session["ViewUserId"]);
            lblMsg.Visible = false;
            SetUserInfoIntoForm(UserId);
        }
    }
}