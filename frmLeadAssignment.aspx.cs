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
    public partial class frmLeadAssignment : System.Web.UI.Page
    {
        static DataBaseProvider dataBaseProvider = new DataBaseProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int LoggedInuserId;
                    LoggedInuserId = Convert.ToInt32(Session["LoggedInuserId"]);
                    if (LoggedInuserId < 1)
                    {
                        Response.Redirect("~/default.aspx");
                    }
                    else
                    {
                        FillLeadColumnDropDown();
                    }
                }
                catch (Exception ex)
                {
                    ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }

        private void setupUserAssignmentSetting()
        {
            int UserId = Convert.ToInt32(Session["ViewUserId"]);
            if (UserId > 0)
            {
                DataTable dt = dataBaseProvider.GetUserAssignmentType(UserId);
                if (dt.Rows.Count < 1)
                {
                    lblMsg.Text = "Please Provide your CRM information to continue.<a href='/frmSettingCRM.aspx'>Click here to configure.</a>";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Visible = true;
                }
                else
                {
                    if (Convert.ToInt32(dt.Rows[0][0].ToString()) > 1)
                    {
                        radioTypeRoundRobin.Checked = true;
                    }
                    else
                    {
                        radioTypeNormal.Checked = true;
                    }
                    setUserAssignmentInfo();
                }
            }
        }

        protected void radioTypeNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (radioTypeNormal.Checked)
            {
                int UserId = Convert.ToInt32(Session["ViewUserId"]);
                DataTable dt = dataBaseProvider.UpdateUserAssignmentType(UserId, 1);
                if (dt.Rows.Count > 0)
                {
                    string result = dt.Rows[0][0].ToString();
                    if (result.Equals("SUCCESS"))
                    {
                        lblMsg.Text = "Lead Owner Assignment type Update Successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMsg.Text = result;
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                    lblMsg.Visible = true;
                    setUserAssignmentInfo();
                }
            }
        }

        protected void radioTypeRoundRobin_CheckedChanged(object sender, EventArgs e)
        {
            if (radioTypeRoundRobin.Checked)
            {
                int UserId = Convert.ToInt32(Session["ViewUserId"]);
                DataTable dt = dataBaseProvider.UpdateUserAssignmentType(UserId, 2);
                if (dt.Rows.Count > 0)
                {
                    string result = dt.Rows[0][0].ToString();
                    if (result.Equals("SUCCESS"))
                    {
                        lblMsg.Text = "Lead Owner Assignment type Update Successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblMsg.Text = result;
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                    lblMsg.Visible = true;
                    setUserAssignmentInfo();
                }
            }
        }
        public void setUserAssignmentInfo()
        {
            int UserId = Convert.ToInt32(Session["ViewUserId"]);
            if (UserId > 0)
            {
                DataTable dt = dataBaseProvider.GetLeadAssignmentInfo(UserId);
                if (dt.Rows.Count < 1)
                {
                    GridLeadAssignmentDetail.Visible = false;
                    AddNewLeadAssignmentInfo.Visible = true;
                }
                else
                {
                    GridLeadAssignmentDetail.AutoGenerateColumns = false;
                    GridLeadAssignmentDetail.Visible = true;
                    GridLeadAssignmentDetail.DataSource = dt;
                    GridLeadAssignmentDetail.DataBind();

                    if (Convert.ToInt32(dt.Rows[0]["assignmentType"].ToString()) < 2)
                    {
                        AddNewLeadAssignmentInfo.Visible = false;
                    }
                    else
                    {
                        AddNewLeadAssignmentInfo.Visible = true;
                    }
                }
            }
            tbAddNewLeadAssignmentInfo.Text = "";
        }

        public void FillLeadColumnDropDown()
        {
            int UserId = Convert.ToInt32(Session["ViewUserId"]);
            if (UserId > 0)
            {
                int assignmentId = dataBaseProvider.getLeadOwnerColumnIdForLeadAssignment(UserId, 0);
                if (assignmentId < 1)
                {
                    lblMsg.Text = "Please Add or Activate Lead Column from CRM Configuration. <a href='/frmLeadInfo.aspx'>Click here to Activate.</a>";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Visible = true;
                    GroupDetails.Visible = false;
                    AddNewLeadAssignmentInfo.Visible = false;
                    radioTypeNormal.Enabled = false;
                    radioTypeRoundRobin.Enabled = false;
                }
                else
                {
                    Session["LeadOwnerAssignmentId"] = assignmentId;
                    setupUserAssignmentSetting();
                    radioTypeNormal.Enabled = true;
                    radioTypeRoundRobin.Enabled = true;
                }
            }
        }

        protected void GridLeadAssignmentDetail_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridLeadAssignmentDetail.EditIndex = e.NewEditIndex;
            setUserAssignmentInfo();
        }

        protected void GridLeadAssignmentDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int LeadSplitId = Convert.ToInt32(GridLeadAssignmentDetail.DataKeys[e.RowIndex].Values["Id"]);
            string result = dataBaseProvider.DeleteLeadAssignmentInfo(LeadSplitId);
            if (result.Equals("SUCCESS"))
            {
                lblMsg.Text = "Lead Owner Name Deleted Sucessfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                setUserAssignmentInfo();
            }
            else
            {
                lblMsg.Text = "Some Error Occured";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            lblMsg.Visible = true;
        }

        protected void GridLeadAssignmentDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridLeadAssignmentDetail.EditIndex = -1;
            setUserAssignmentInfo();
        }

        protected void GridLeadAssignmentDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string AssignmentName;
            int AssignmentId, Status;
            AssignmentName = ((TextBox)GridLeadAssignmentDetail.Rows[e.RowIndex].FindControl("tbAssignmentTitle")).Text.Trim();
            Status = Convert.ToInt32(((CheckBox)GridLeadAssignmentDetail.Rows[e.RowIndex].FindControl("chkAssignmentStatus")).Checked);
            AssignmentId = Convert.ToInt32(((HiddenField)GridLeadAssignmentDetail.Rows[e.RowIndex].FindControl("hiddenAssignmentId")).Value.Trim());

            string result = dataBaseProvider.UpdateLeadAssignmentInfo(AssignmentName, AssignmentId, Status);
            if (result.Equals("SUCCESS"))
            {
                lblMsg.Text = "Lead Owner Name Update Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                GridLeadAssignmentDetail.EditIndex = -1;
                setUserAssignmentInfo();
            }
            else
            {
                lblMsg.Text = "Some Error Occured";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            lblMsg.Visible = true;
        }

        protected void btnAddNewLeadAssignmentInfo_Click(object sender, EventArgs e)
        {
            int UserId = Convert.ToInt32(Session["ViewUserId"]);
            string AssignmentName;
            int AssignmentType, LeadColumnHeaderId;
            if (radioTypeNormal.Checked)
            {
                AssignmentType = 1;
            }
            else
            {
                AssignmentType = 2;
            }
            LeadColumnHeaderId = Convert.ToInt32(Session["LeadOwnerAssignmentId"].ToString());
            if (LeadColumnHeaderId > 0)
            {
                AssignmentName = tbAddNewLeadAssignmentInfo.Text;
                if (!string.IsNullOrWhiteSpace(AssignmentName))
                {
                    DataTable dt = dataBaseProvider.InstertLeadAssignmentInfo(AssignmentName, UserId, AssignmentType, LeadColumnHeaderId);
                    if (dt.Rows[0][0].ToString().Equals("SUCCESS"))
                    {
                        lblMsg.Text = "Lead Owner Name Added Successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        setUserAssignmentInfo();
                    }
                    else
                    {
                        lblMsg.Text = dt.Rows[0][0].ToString();
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMsg.Text = "Lead Owner Name Filed Should not be Empty";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblMsg.Text = "Please Add or Activate Lead Column from CRM Configuration";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

            lblMsg.Visible = true;
        }


        protected void btnCancelNewLeadAssignmentInfo_Click(object sender, EventArgs e)
        {
            tbAddNewLeadAssignmentInfo.Text = "";
        }
    }
}