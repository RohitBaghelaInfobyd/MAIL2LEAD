using AdminTool.DataBase;
using AdminTool.Model;
using AdminTool.SMSService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminTool
{
    public partial class frmSettingUser : System.Web.UI.Page
    {

        static DataBaseProvider dataBaseProvider = new DataBaseProvider();
        static int UserType;
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
                        tbPassword.Attributes.Add("value", tbPassword.Text);
                    }
                }
                catch (Exception ex)
                { ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message); }
            }
        }

        public void SetUserInfoIntoForm(int userId)
        {
            lblMsg.Visible = false;
            if (userId > 0)
            {
                DataTable UserInfo = dataBaseProvider.getUserInfoById(userId);
                if (UserInfo.Rows.Count > 0)
                {
                    UserType = Convert.ToInt32(UserInfo.Rows[0]["type"].ToString());
                    tbFirstName.Text = UserInfo.Rows[0]["FirstName"].ToString();
                    tbLastName.Text = UserInfo.Rows[0]["LastName"].ToString();
                    tbEmail.Text = UserInfo.Rows[0]["EmailId"].ToString();
                    tbPassword.Text = UserInfo.Rows[0]["password"].ToString();

                    tbPassword.Attributes.Add("value", tbPassword.Text);
                    Session["UserName"] = "Welcome, " + tbFirstName.Text;
                    EnableDisable(false);
                }
            }
            else
            {
                tbFirstName.Text = string.Empty;
                tbLastName.Text = string.Empty;
                tbEmail.Text = string.Empty;
                tbPassword.Text = string.Empty;
                EnableDisable(true);
            }
        }

        private void EnableDisable(bool isEnable)
        {
            tbFirstName.Enabled = isEnable;
            tbLastName.Enabled = isEnable;
            tbEmail.Enabled = isEnable;
            tbPassword.Enabled = isEnable;
            btnUpdate.Text = isEnable ? "Save" : "Update";
            btnCancel.Visible = isEnable;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            int ViewUserId = Convert.ToInt32(Session["ViewUserId"]);
            if (ViewUserId > 0)
            {
                SetUserInfoIntoForm(ViewUserId);
                lblMsg.Visible = false;
                EnableDisable(false);
            }
            else
            {
                Response.Redirect("~/frmUserList.aspx");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            if (!tbEmail.Enabled)
            {
                EnableDisable(true);
            }
            else
            {
                int ViewUserId = Convert.ToInt32(Session["ViewUserId"]);
                int LoggedInuserId = Convert.ToInt32(Session["LoggedInuserId"]);
                int userType = 1;
                string FirstName, LastName, EmailId, password = string.Empty, result = string.Empty;
                FirstName = tbFirstName.Text.Trim();
                LastName = tbLastName.Text.Trim();
                EmailId = tbEmail.Text.Trim();
                password = tbPassword.Text.Trim();
               
                if (ViewUserId > 0)
                {
                    result = dataBaseProvider.updateExisingUserInfoIntoDataBase(ViewUserId, FirstName, LastName, EmailId, userType, password);
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
                else
                {
                    DataTable te = dataBaseProvider.AddNewUserIntoDatabase(LoggedInuserId, FirstName, LastName, EmailId, password, userType);
                    result = te.Rows[0]["result"].ToString().ToUpper();
                    if (result.Equals("SUCCESS"))
                    {
                        lblMsg.Text = "User Added Successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        Response.Redirect("~/frmUserList.aspx");
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
    }
}