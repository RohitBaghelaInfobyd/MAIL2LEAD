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
    public partial class frmLeadUniqueIdentifier : System.Web.UI.Page
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
                        FillLeadActionColumnDropDown();
                        FillUniqueIdentifierDetail();
                        setupLeadUniqueIdentifierInfo();
                    }
                }
                catch (Exception ex)
                {
                    ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }

        private void setupLeadUniqueIdentifierInfo()
        {
            if (dropdownAction.Items.Count > 0)
            {
                int UserId = Convert.ToInt32(Session["ViewUserId"]);
                if (UserId > 0)
                {
                    DataTable dt = dataBaseProvider.getUserGmailInfoById(UserId);
                    if (dt.Rows.Count > 0)
                    {
                        dropdownAction.SelectedValue = dt.Rows[0]["existingInfoEvent"].ToString();
                    }
                    if (Convert.ToInt32(dropdownAction.SelectedValue.ToString()) == 3)
                    {
                        PnlupdateLeadColumn.Visible = true;
                        FileColumnToBeUpdateList();
                    }
                    else
                    {
                        PnlupdateLeadColumn.Visible = false;
                    }
                }
            }
        }

        public void FillLeadActionColumnDropDown()
        {
            int UserId = Convert.ToInt32(Session["ViewUserId"]);
            if (UserId > 0)
            {
                DataTable dt = dataBaseProvider.GetExistingEntryEvent();
                dropdownAction.DataSource = dt;
                dropdownAction.DataTextField = "event";
                dropdownAction.DataValueField = "id";
                dropdownAction.DataBind();
            }
        }

        public void FileColumnToBeUpdateList()
        {
            int UserId = Convert.ToInt32(Session["ViewUserId"]);
            if (UserId > 0)
            {
                DataTable dt = dataBaseProvider.getListofColumnHeaderOfLeadToMailTable(UserId, 0);
                if (dt.Rows.Count > 0)
                {
                    chkLeadColumnToUpdate.DataSource = dt;
                    chkLeadColumnToUpdate.DataTextField = "leadColumnHeader";
                    chkLeadColumnToUpdate.DataValueField = "id";
                    chkLeadColumnToUpdate.DataBind();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dt.Rows[i]["isToBeUpdate"].ToString()) > 0)
                        {
                            chkLeadColumnToUpdate.Items.FindByValue(dt.Rows[i]["id"].ToString()).Selected = true;
                        }
                    }
                }
                else
                {
                    PnlupdateLeadColumn.Visible = false;
                }
            }
        }

        protected void dropdownAction_SelectedIndexChanged(object sender, EventArgs e)
        {

            int actionType = Convert.ToInt32(dropdownAction.SelectedValue.ToString());
            int UserId = Convert.ToInt32(Session["ViewUserId"]);
            DataTable dt = dataBaseProvider.UpdateExistingRecordType(UserId, actionType);
            if (dt.Rows.Count > 0)
            {
                string result = dt.Rows[0][0].ToString();
                if (result.Equals("SUCCESS"))
                {
                    lblMsg.Text = "Action Type update successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    if (actionType == 3)
                    {
                        FileColumnToBeUpdateList();
                        PnlupdateLeadColumn.Visible = true;
                    }
                    else
                    {
                        PnlupdateLeadColumn.Visible = false;
                    }
                }
                else
                {
                    lblMsg.Text = result;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                lblMsg.Visible = true;
            }
        }

        public void FillLeadColumnDropDown()
        {
            int UserId = Convert.ToInt32(Session["ViewUserId"]);
            if (UserId > 0)
            {
                DataTable dt = dataBaseProvider.getListOfAllUnUsedUniqueIdentifier(UserId);
                if (dt.Rows.Count < 1)
                {
                    AddNewUniqueIdentifier.Visible = false;
                    lblMsg.Text = lblMsg.Text = "Please Add or Activate Lead Column from Lead Info.<a href='/frmLeadInfo.aspx'>Click here to Activate.</a>";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Visible = true;
                    dropdownAction.Enabled = false;
                }
                else
                {
                    dropdownAction.Enabled = true;
                    AddNewUniqueIdentifier.Visible = true;
                    dropDownMailToLeadColumn.DataSource = dt;
                    dropDownMailToLeadColumn.DataTextField = "leadColumnHeader";
                    dropDownMailToLeadColumn.DataValueField = "id";
                    dropDownMailToLeadColumn.DataBind();
                }
            }
        }

        public void FillUniqueIdentifierDetail()
        {
            int UserId = Convert.ToInt32(Session["ViewUserId"]);
            if (UserId > 0)
            {
                DataTable dt = dataBaseProvider.getListOfAllUniqueIdentifier(UserId);
                ViewState["UniqueIdentifierTable"] = dt;
                if (dt.Rows.Count < 1)
                {
                    GridUniqueIdentifierDetail.Visible = false;
                    lblExistingLableRule.Visible = false;
                    actionPanel.Visible = false;
                    actionRulePanel.Visible = false;
                }
                else
                {
                    GridUniqueIdentifierDetail.AutoGenerateColumns = false;
                    GridUniqueIdentifierDetail.Visible = true;
                    GridUniqueIdentifierDetail.DataSource = dt;
                    GridUniqueIdentifierDetail.DataBind();
                    GridUniqueIdentifierDetail.Visible = true;
                    lblExistingLableRule.Visible = true;
                    actionPanel.Visible = true;
                    actionRulePanel.Visible = true;
                }

                FillLeadColumnDropDown();
                if (dt.Rows.Count < 1)
                {
                    pnlRuleExpresstion.Visible = false;
                    pnlCurrentExpresstion.Visible = false;
                }
                else
                {
                    fillExpresstion();
                }
            }
        }

        private void fillExpresstion()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["UniqueIdentifierTable"];
                if (dt.Rows.Count > 0)
                {
                    pnlRuleExpresstion.Visible = true;
                    pnlCurrentExpresstion.Visible = true;
                    string expresstion = null;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (expresstion != null)
                        {
                            expresstion = "( " + expresstion + " " + dt.Rows[i]["actionType"].ToString() + " " + dt.Rows[i]["leadColumnHeader"].ToString() + ") ";
                        }
                        else
                        {
                            expresstion = dt.Rows[i]["leadColumnHeader"].ToString();
                        }
                    }
                    lblCurrentExpression.Text = expresstion;
                    if (dt.Rows.Count < 3)
                    {
                        expresstion = "( " + expresstion + " " + dropDownActionType.SelectedItem.ToString() + " " + dropDownMailToLeadColumn.SelectedItem.ToString() + ") ";
                        lblExpression.Text = expresstion;
                        lblExpression.ForeColor = System.Drawing.Color.Black;
                        AddNewUniqueIdentifier.Enabled = true;
                    }
                    else
                    {
                        lblExpression.Text = "Maximum 3 Column allow for unique Identifier rule.";
                        lblExpression.ForeColor = System.Drawing.Color.Red;
                        AddNewUniqueIdentifier.Enabled = false;
                    }

                }
            }
            catch (Exception ex)
            { }
        }

        protected void btnAddNewEvent_Click(object sender, EventArgs e)
        {
            int UserId = Convert.ToInt32(Session["ViewUserId"]);
            string ActionType;
            int LeadColumnHeaderId;

            LeadColumnHeaderId = Convert.ToInt32(dropDownMailToLeadColumn.SelectedValue.ToString());
            if (LeadColumnHeaderId > 0)
            {
                DataTable dt = (DataTable)ViewState["UniqueIdentifierTable"];
                if (dt.Rows.Count < 1)
                {
                    ActionType = "-";
                }
                else
                {
                    ActionType = dropDownActionType.SelectedValue.ToString();
                }

                dt = dataBaseProvider.InsertNewUniqueIdentifier(LeadColumnHeaderId, UserId, ActionType);
                if (dt.Rows[0][0].ToString().Equals("SUCCESS"))
                {
                    lblMsg.Text = "Unique Identifier Added Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    FillUniqueIdentifierDetail();
                }
                else
                {
                    lblMsg.Text = dt.Rows[0][0].ToString();
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblMsg.Text = lblMsg.Text = "Please Add or Activate Lead Column from Lead Info.<a href='/frmLeadInfo.aspx'>Click here to Activate.</a>";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

            lblMsg.Visible = true;
        }

        protected void GridUniqueIdentifierDetail_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridUniqueIdentifierDetail.EditIndex = e.NewEditIndex;
            FillUniqueIdentifierDetail();
        }

        protected void GridUniqueIdentifierDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridUniqueIdentifierDetail.EditIndex = -1;
            FillUniqueIdentifierDetail();
        }

        protected void GridUniqueIdentifierDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int UniqueIdentifierId = Convert.ToInt32(GridUniqueIdentifierDetail.DataKeys[e.RowIndex].Values["Id"]);
            string result = dataBaseProvider.DeleteUniqueIdentifierInfo(UniqueIdentifierId);
            if (result.Equals("SUCCESS"))
            {
                lblMsg.Text = "Unique Identifier Deleted Sucessfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                FillUniqueIdentifierDetail();
            }
            else
            {
                lblMsg.Text = "Some Error Occured";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            lblMsg.Visible = true;
        }

        protected void GridUniqueIdentifierDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string ActionType;
            int IdentifierId;
            ActionType = (((DropDownList)GridUniqueIdentifierDetail.Rows[e.RowIndex].FindControl("dropDownActionTypeEdit")).SelectedValue.ToString());
            IdentifierId = Convert.ToInt32(((HiddenField)GridUniqueIdentifierDetail.Rows[e.RowIndex].FindControl("hiddenUniqueIdentifierId")).Value.Trim());

            string result = dataBaseProvider.UpdateUniqueIdentifierId(IdentifierId, ActionType);
            if (result.Equals("SUCCESS"))
            {
                lblMsg.Text = "Unique Identifier Info Update Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                GridUniqueIdentifierDetail.EditIndex = -1;
                FillUniqueIdentifierDetail();
            }
            else
            {
                lblMsg.Text = "Some Error Occured";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            lblMsg.Visible = true;
        }

        protected void btnSaveUpdateLeadColumnList_Click(object sender, EventArgs e)
        {
            foreach (ListItem listitem in chkLeadColumnToUpdate.Items)
            {
                int columnHeaderId = Convert.ToInt32(listitem.Value);
                Boolean isSelected = listitem.Selected;
                dataBaseProvider.UpdateLeadColumnUpdateColumnValue(columnHeaderId, isSelected);
            }
        }

        protected void dropDownMailToLeadColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillExpresstion();
        }

        protected void GridUniqueIdentifierDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "actionType").ToString().Replace('-', ' ').Trim().Length < 2)
                {
                    e.Row.Cells[3].Controls[0].Visible = false;
                    e.Row.Cells[3].Controls[1].Visible = false;
                    e.Row.Cells[3].Controls[2].Visible = false;
                }
            }
        }
    }
}