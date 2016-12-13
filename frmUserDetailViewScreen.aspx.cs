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
    public partial class frmUserDetailViewScreen : System.Web.UI.Page
    {

        static DataBaseProvider dataBaseProvider = new DataBaseProvider();
        static int UserType, SelectedExistingEntryId, SelectedReportType;
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
                        EnableDisable(false);
                        SetUserInfoIntoForm(UserId);
                        getBasicInfoUser(UserId);
                        tbGmailPassword.Attributes.Add("value", tbGmailPassword.Text);
                        tbSmsUserPassword.Attributes.Add("value", tbSmsUserPassword.Text);
                        tbPassword.Attributes.Add("value", tbPassword.Text);
                    }
                     ((Label)(Master).FindControl("lblUserName")).Text = Session["UserName"].ToString();
                }
                catch (Exception ex)
                { }
            }
        }

        protected void ImageGoBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/frmApiReport.aspx");
        }

        public void SetUserInfoIntoForm(int userId)
        {
            if (userId > 0)
            {
                DataTable UserInfo = dataBaseProvider.getUserInfoById(userId);
                if (UserInfo.Rows.Count > 0)
                {
                    UserType = Convert.ToInt32(UserInfo.Rows[0]["type"].ToString());
                    tbFirstName.Text = UserInfo.Rows[0]["FirstName"].ToString();
                    tbLastName.Text = UserInfo.Rows[0]["LastName"].ToString();
                    tbEmail.Text = UserInfo.Rows[0]["EmailId"].ToString();


                    tbGmailId.Text = UserInfo.Rows[0]["gmailId"].ToString();
                    tbConfigurationToken.Text = UserInfo.Rows[0]["configurationAuthToken"].ToString();
                    SelectedExistingEntryId = Convert.ToInt32(UserInfo.Rows[0]["existingInfoEvent"].ToString());

                    tbSmsUserID.Text = UserInfo.Rows[0]["SmsUserID"].ToString();
                    tbAppKey.Text = UserInfo.Rows[0]["AppKey"].ToString();
                    tbAppSecretKey.Text = UserInfo.Rows[0]["AppSecretKey"].ToString();
                    tbSmsConfigurationToken.Text = UserInfo.Rows[0]["smsConfigurationAuthToken"].ToString();
                    tbModuleName.Text = UserInfo.Rows[0]["module_name"].ToString();
                    tbSmsFrom.Text = UserInfo.Rows[0]["SmsFrom"].ToString();

                    tbSmsUserPassword.Text = UserInfo.Rows[0]["SmsPassword"].ToString();
                    tbPassword.Text = UserInfo.Rows[0]["password"].ToString();
                    tbGmailPassword.Text = UserInfo.Rows[0]["gmailPassword"].ToString();

                    if (Convert.ToInt32(UserInfo.Rows[0]["useDefault"].ToString()) > 0)
                    {
                        chkIsUseDefault.Checked = true;
                        tbSmsUserID.Enabled = false;
                        tbSmsUserPassword.Enabled = false;
                        tbAppKey.Enabled = false;
                        tbAppSecretKey.Enabled = false;
                    }
                    else
                    {
                        chkIsUseDefault.Checked = false;
                    }

                    ImgCrmSetting.Visible = true;
                    GetExistingEntryEvent();
                    fileReportDropDown();
                    EnableDisable(false);
                }
            }
            else
            {
                tbFirstName.Text = string.Empty;
                tbLastName.Text = string.Empty;
                tbEmail.Text = string.Empty;
                tbPassword.Text = string.Empty;
                ImgCrmSetting.Visible = false;
                btnSave.Visible = true;
                btnSaveCancel.Visible = true;
                EnableDisable(true);

            }
        }

        private void GetExistingEntryEvent()
        {
            DataTable exisingEntry = dataBaseProvider.GetExistingEntryEvent();
            if (exisingEntry.Rows.Count > 0)
            {
                dropExistingEntry.DataSource = exisingEntry;
                dropExistingEntry.DataTextField = "event";
                dropExistingEntry.DataValueField = "id";
                dropExistingEntry.DataBind();
                dropExistingEntry.SelectedIndex = SelectedExistingEntryId - 1;
            }

        }

        private void fileReportDropDown()
        {
            DataTable reporttype = dataBaseProvider.GetReportType();
            dropdownReporttype.DataSource = reporttype;
            dropdownReporttype.DataTextField = "type";
            dropdownReporttype.DataValueField = "id";
            dropdownReporttype.DataBind();
            dropdownReporttype.SelectedIndex = SelectedReportType - 1;

        }


        private void EnableDisable(bool isEnable)
        {
            tbFirstName.Enabled = isEnable;
            tbLastName.Enabled = isEnable;
            ImgViewSubject.Visible = !isEnable;
            ImgViewLeadColumnHeader.Visible = !isEnable;
            ImgSync.Visible = !isEnable;
            tbEmail.Enabled = isEnable;
            tbPassword.Enabled = isEnable;
            AddNewUser.Visible = isEnable;
            UpdateDiv.Visible = !isEnable;
            ImgCrmSetting.Visible = !isEnable;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void ImgViewSubject_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmViewSubjectInfo.aspx");
        }

        protected void ImgViewLeadColumnHeader_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmUserMailToLead.aspx");
        }


        protected void ImgTestApi_Click(object sender, EventArgs e)
        {
            int ViewUserId = Convert.ToInt32(Session["ViewUserId"]);
            if (ViewUserId > 0)
            {
                /* Format  
                 * SendEmailStarted(int UserId, int numberOfDays, int IsApprovedOrAll, int ViewSubjectId)
                 * 
                 * UserId = Loged in user Id
                 * numberOfDays =how many days old mail to check
                 * ViewSubjectId= Id of particular subject which user want to sync
                 * IsApprovedOrAll = value may either 0 or 1, 
                 *                  CASE 0 sync all the mail no matter approve or not
                 *                  CASE 1 sync only approved mail 
                 * 
                  */

                MainTimeTicker.SubmitEmailFromMailToCRM(ViewUserId, 1, 1, 0, "ForceSync");
            }
            else
            {
                lblMsg.Text = "Some Error Occured";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Visible = true;
            }
        }

        protected void btnSaveCancel_Click(object sender, EventArgs e)
        {
            int ViewUserId = Convert.ToInt32(Session["ViewUserId"]);
            SetUserInfoIntoForm(ViewUserId);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int ViewUserId = Convert.ToInt32(Session["ViewUserId"]);
            int LoggedInuserId = Convert.ToInt32(Session["LoggedInuserId"]);
            int userType = 1;

            string FirstName, LastName, EmailId, password = string.Empty, result = string.Empty;
            FirstName = tbFirstName.Text;
            LastName = tbLastName.Text;
            EmailId = tbEmail.Text;
            password = tbPassword.Text.Trim();

            EnableDisable(true);
            if (ViewUserId > 0)
            {
                result = dataBaseProvider.updateExisingUserInfoIntoDataBase(ViewUserId, FirstName, LastName, EmailId, userType, password);
                if (result.Equals("SUCCESS"))
                {
                    lblMsg.Text = "Information Update Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    SetUserInfoIntoForm(ViewUserId);
                    EnableDisableCRM(true);
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
            lblMsg.Visible = true;
        }

        protected void btnUpdateCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmUserList.aspx");

        }

        protected void btnUpdate_Click1(object sender, EventArgs e)
        {
            if (!tbFirstName.Enabled)
            {
                EnableDisable(true);
                lblMsg.Visible = false;
            }
        }


        private void EnableDisableCRM(bool isEnable)
        {

            tbGmailId.Enabled = isEnable;
            tbGmailPassword.Enabled = isEnable;
            tbConfigurationToken.Enabled = isEnable;
            dropExistingEntry.Enabled = isEnable;
            dropdownReporttype.Enabled = isEnable;
            SaveCRMinfoDiv.Visible = isEnable;

        }

        protected void btnSaveCRM_Click(object sender, EventArgs e)
        {
            int ViewUserId = Convert.ToInt32(Session["ViewUserId"]);
            int existingEntry, reportType;

            string GmailId, GmailPassword, ConfigurationToken, result;
            GmailId = tbGmailId.Text;
            GmailPassword = tbGmailPassword.Text;
            ConfigurationToken = tbConfigurationToken.Text;
            existingEntry = Convert.ToInt32(dropExistingEntry.SelectedValue.ToString());
            reportType = Convert.ToInt32(dropdownReporttype.SelectedValue.ToString());

            EnableDisable(true);
            if (ViewUserId > 0)
            {
                result = dataBaseProvider.InsertUserGmailInfo(ViewUserId, GmailId, GmailPassword, ConfigurationToken, existingEntry, reportType);
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
            lblMsg.Visible = true;
        }

        public void getBasicInfoUser(int userId)
        {
            if (userId > 0)
            {
                DataTable UserInfo = dataBaseProvider.getUserBasicInfo(userId);
                if (UserInfo.Rows.Count > 0)
                {
                    UserType = Convert.ToInt32(UserInfo.Rows[0]["type"].ToString());
                    tbFirstName.Text = UserInfo.Rows[0]["FirstName"].ToString();
                    tbLastName.Text = UserInfo.Rows[0]["LastName"].ToString();
                    tbEmail.Text = UserInfo.Rows[0]["EmailId"].ToString();
                    tbPassword.Text = UserInfo.Rows[0]["password"].ToString();
                    btnSave.Visible = true;
                    btnSaveCancel.Visible = true;

                    ImgCrmSetting.Visible = true;
                    GetExistingEntryEvent();
                    fileReportDropDown();
                    EnableDisable(false);

                }
            }
        }

        protected void dropExistingEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropExistingEntry.SelectedValue.ToLower().Contains("update"))
            {
                lblUpdateNote.Visible = true;

            }
            else
            {
                lblUpdateNote.Visible = false;
            }
        }

        protected void btnSmsSave_Click(object sender, EventArgs e)
        {
            int ViewUserId = Convert.ToInt32(Session["ViewUserId"]);
            bool useDefault;
            string SmsUserId, SmsUserPassword, ModuleName, SmsAppKey, SmsAppSecreyKey, SmsConfigurationToken, SmsFrom, result;
            SmsUserId = tbSmsUserID.Text;
            SmsUserPassword = tbSmsUserPassword.Text;
            SmsAppKey = tbAppKey.Text;
            SmsAppSecreyKey = tbAppSecretKey.Text;
            ModuleName = tbModuleName.Text;
            SmsConfigurationToken = tbConfigurationToken.Text;
            SmsFrom = tbSmsFrom.Text;
            useDefault = chkIsUseDefault.Checked;

            if (useDefault)
            {
                SmsUserId = "Na";
                SmsUserPassword = "Na";
                SmsAppKey = "Na";
                SmsAppSecreyKey = "Na";

            }
            EnableDisable(true);
            if (ViewUserId > 0)
            {
                result = dataBaseProvider.InsertUserSmsInfo(ViewUserId, SmsUserId, SmsUserPassword, SmsAppKey, SmsAppSecreyKey, SmsConfigurationToken, SmsFrom, useDefault, ModuleName);
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
            lblMsg.Visible = true;

        }

        protected void btnSmsCancel_Click(object sender, EventArgs e)
        {

        }

        public void getGmailInfoUser(int userId)
        {
            if (userId > 0)
            {
                DataTable UserInfo = dataBaseProvider.getUserGmailInfoById(userId);
                if (UserInfo.Rows.Count > 0)
                {

                    tbGmailId.Text = UserInfo.Rows[0]["gmailId"].ToString();
                    tbPassword.Text = UserInfo.Rows[0]["gmailPassword"].ToString();
                    tbConfigurationToken.Text = UserInfo.Rows[0]["configurationAuthToken"].ToString();
                    SelectedExistingEntryId = Convert.ToInt32(UserInfo.Rows[0]["existingInfoEvent"].ToString());
                    SelectedReportType = Convert.ToInt32(UserInfo.Rows[0]["reportType"].ToString());

                    // ImgCrmSetting.Visible = true;
                    GetExistingEntryEvent();
                    fileReportDropDown();
                    EnableDisable(false);

                }
            }
        }
    }
}