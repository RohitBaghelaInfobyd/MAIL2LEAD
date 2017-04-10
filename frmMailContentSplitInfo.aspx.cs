using AdminTool.DataBase;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminTool
{
    public partial class frmMailContentSplitInfo : System.Web.UI.Page
    {
        static DataBaseProvider dataBaseProvider = new DataBaseProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int LoggedInuserId, SubjectID;
                    LoggedInuserId = Convert.ToInt32(Session["LoggedInuserId"]);
                    SubjectID = Convert.ToInt32(Session["ViewUserSubjectId"]);
                    if (LoggedInuserId < 1)
                    {
                        Response.Redirect("~/default.aspx");
                    }
                    else
                    {
                        setupUserTemplateDropDown();
                        SetUserLeadMailSplitInfo(SubjectID);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                    lblMsg.Text = "Some Error Occured";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }


        public void setupUserTemplateDropDown()
        {
            int UserId = Convert.ToInt32(Session["ViewUserId"]);
            if (UserId > 0)
            {
                DataTable dt = dataBaseProvider.getListOfAllUserSubject(UserId, 0, 0);
                if (dt.Rows.Count < 1)
                {
                    dropDpownListOfAllSubjectList.DataSource = new DataTable();
                    dropDpownListOfAllSubjectList.DataBind();
                }
                else
                {
                    dropDpownListOfAllSubjectList.DataSource = dt;
                    dropDpownListOfAllSubjectList.DataTextField = "subjectLine";
                    dropDpownListOfAllSubjectList.DataValueField = "id";
                    dropDpownListOfAllSubjectList.DataBind();
                    int SubjectID = Convert.ToInt32(Session["ViewUserSubjectId"]);
                    dropDpownListOfAllSubjectList.SelectedValue = SubjectID.ToString();
                }
            }
        }
        public void SetUserLeadMailSplitInfo(int SubjectID)
        {
            if (SubjectID > 0)
            {
                DataTable dt = dataBaseProvider.getListOfMailContentSplitInfo(SubjectID);
                if (dt.Rows.Count < 1)
                {
                    GridSplitDetail.Visible = false;
                    lblMsg.Text = "No any split info found. Please provide content split info.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    pnlAddNewInfo.Visible = true;
                    ImgAddSplitInfo.Enabled = false;
                    FileHeaderComboBox();
                    lblMsg.Visible = true;
                }
                else
                {
                    GridSplitDetail.Visible = true;
                    GridSplitDetail.DataSource = dt;
                    GridSplitDetail.DataBind();
                }
            }
            else
            {
                Response.Redirect("~/default.aspx");
            }
        }

        private void FillDefaultGridView()
        {
            int SubjectID = Convert.ToInt32(Session["ViewUserSubjectId"]);
            DataTable dt = new DataTable();
            dt = dataBaseProvider.getListOfMailContentSplitInfo(SubjectID);
            GridSplitDetail.DataSource = dt;
            GridSplitDetail.DataBind();

            if (dt != null && dt.Rows.Count > 0)
            {
                GridSplitDetail.Visible = true;
            }
            else
            {
                GridSplitDetail.Visible = false;
                lblMsg.Text = "No any split info found. Please provide content split info.";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Visible = true;
            }
        }

        private void FileHeaderComboBox()
        {

            int ViewSubjectid, ViewUserId;
            ViewSubjectid = Convert.ToInt32(Session["ViewUserSubjectId"]);
            ViewUserId = Convert.ToInt32(Session["ViewUserId"]);
            DataTable dt = dataBaseProvider.getListOfunfiledHeaderofMailContentSplit(ViewSubjectid, ViewUserId);
            if (dt.Rows.Count < 1)
            {
                MailToLeadColumnHeader.DataSource = new DataTable();
                MailToLeadColumnHeader.DataBind();
                pnlAddNewInfo.Visible = false;
                ImgAddSplitInfo.Enabled = true;
                lblMsg.Visible = true;
                lblMsg.Text = "No more lead column available";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblMsg.Visible = false;
                dropdownIsDefaultValue.Visible = false;
                chkisValueSplit.Checked = false;
                checkIsDefaultValue.Checked = false;
                tbEndText.Enabled = true;
                tbStartText.Enabled = true;
                chkisValueSplit.Enabled = true;
                txtDefaultValue.Visible = false;
                lblValuetype.Visible = false;
                txtDefaultValue.Text = "";
                tbStartText.Text = "";
                tbEndText.Text = "";
                pnlAddNewInfo.Visible = true;
                ImgAddSplitInfo.Enabled = false;
                MailToLeadColumnHeader.DataSource = dt;
                MailToLeadColumnHeader.DataTextField = "leadColumnHeader";
                MailToLeadColumnHeader.DataValueField = "id";
                MailToLeadColumnHeader.Enabled = true;
                MailToLeadColumnHeader.DataBind();
                fillIsValueSplitDropDownValue();
            }
        }

        public void fillIsValueSplitDropDownValue()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("Value", typeof(string)));
            for (int te = 0; te < 11; te++)
            {
                dr = dt.NewRow();
                dr["Value"] = te;
                dt.Rows.Add(dr);
            }
            dropdownValueIndex.DataSource = dt;
            dropdownValueIndex.DataTextField = "Value";
            dropdownValueIndex.DataValueField = "Value";
            dropdownValueIndex.DataBind();


            dt = new DataTable();
            dt.Columns.Add(new DataColumn("Value", typeof(string)));
            dr = dt.NewRow();
            dr["Value"] = "Space";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            dr["Value"] = "NewLine";
            dr = dt.NewRow();
            dr["Value"] = "Other";
            dt.Rows.Add(dr);
            dropdownIsValueSplit.DataSource = dt;
            dropdownIsValueSplit.DataTextField = "Value";
            dropdownIsValueSplit.DataValueField = "Value";
            dropdownIsValueSplit.DataBind();
            txtValueToSplit.Text = "";
            txtValueToSplit.Visible = false;
            dropdownValueIndex.Visible = false;
            dropdownIsValueSplit.Visible = false;
            lblIndex.Visible = false;
            lblSplitFrom.Visible = false;
        }


        protected void ImgAddNewSplitInfo_Click(object sender, EventArgs e)
        {
            imgBtnAddNew.Text = "Add";
            lblMsg.Visible = false;
            GridSplitDetail.EditIndex = -1;
            FileHeaderComboBox();
            FillDefaultGridView();
        }


        protected void GridSplitDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridSplitDetail.EditIndex = -1;
            pnlAddNewInfo.Visible = false;
            ImgAddSplitInfo.Enabled = true;
            FillDefaultGridView();
        }

        protected void imgBtnCancel_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            pnlAddNewInfo.Visible = false;
            ImgAddSplitInfo.Enabled = true;
        }

        protected void imgBtnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton img = sender as ImageButton;
                int ColumnHeaderId = Convert.ToInt32(MailToLeadColumnHeader.SelectedValue);
                int SubjectId = Convert.ToInt32(Session["ViewUserSubjectId"]);
                string startText = tbStartText.Text.ToString();
                string endText = tbEndText.Text.ToString();


                string splitValueText = txtValueToSplit.Text.ToString();
                int splitIndex = Convert.ToInt32(dropdownValueIndex.SelectedValue.ToString());
                string SplitType = dropdownIsValueSplit.SelectedValue.ToString();
                Boolean isValueSplit = chkisValueSplit.Checked;


                Boolean IsDefaultValueCheck = checkIsDefaultValue.Checked;
                string IsDefaultValuetype = dropdownIsDefaultValue.SelectedValue.ToString();
                string defaultValue = txtDefaultValue.Text.ToString();

                if (!IsDefaultValueCheck)
                {
                    if (string.IsNullOrEmpty(startText.Trim()) || string.IsNullOrEmpty(endText.Trim()))
                    {
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        lblMsg.Text = "Start text & End text field should not be empty.";
                        lblMsg.Visible = true;
                        return;
                    }
                    IsDefaultValuetype = "Current Date";
                    defaultValue = "Na";
                }
                else
                {
                    isValueSplit = false;
                    startText = "---";
                    endText = "---";
                    if (IsDefaultValuetype.ToLower().Contains("other") && string.IsNullOrEmpty(defaultValue.Trim()))
                    {
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        lblMsg.Text = "Default value should not be empty.";
                        lblMsg.Visible = true;
                        return;
                    }
                    else if (IsDefaultValuetype.ToLower().Contains("date"))
                    {
                        defaultValue = "Na";
                    }
                }


                if (!isValueSplit)
                {
                    SplitType = "Other";
                    splitIndex = 0;
                    splitValueText = "Na";
                }
                else
                {
                    if (string.IsNullOrEmpty(splitValueText.Trim()) && SplitType.ToLower().Contains("other"))
                    {
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        lblMsg.Text = "Split value should not be empty.";
                        lblMsg.Visible = true;
                        return;
                    }
                    else if (!SplitType.ToLower().Contains("other"))
                    {
                        splitValueText = "Na";
                    }
                }

                string result = dataBaseProvider.AddNewMailContentSplitInfo(startText, endText, ColumnHeaderId, SubjectId, splitValueText, splitIndex, SplitType, isValueSplit, IsDefaultValueCheck, IsDefaultValuetype, defaultValue);

                if (result.Equals("SUCCESS"))
                {
                    lblMsg.Text = "Template content split information added successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    GridSplitDetail.EditIndex = -1;
                    pnlAddNewInfo.Visible = false;
                    ImgAddSplitInfo.Enabled = true;
                    FillDefaultGridView();
                }
                else if (result.Equals("UPDATE"))
                {
                    lblMsg.Text = "Template content split information update successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    GridSplitDetail.EditIndex = -1;
                    pnlAddNewInfo.Visible = false;
                    ImgAddSplitInfo.Enabled = true;
                    FillDefaultGridView();
                }
                else
                {
                    lblMsg.Text = "Some Error Occured";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                lblMsg.Text = "Some Error Occured";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            lblMsg.Visible = true;
        }

        public void fillDefaultValueDropDown()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt = new DataTable();
            dt.Columns.Add(new DataColumn("Value", typeof(string)));
            dr = dt.NewRow();
            dr["Value"] = "Current Date";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["Value"] = "Other";
            dt.Rows.Add(dr);
            dropdownIsDefaultValue.DataSource = dt;
            dropdownIsDefaultValue.DataTextField = "Value";
            dropdownIsDefaultValue.DataValueField = "Value";
            dropdownIsDefaultValue.DataBind();

        }

        protected void GridSplitDetail_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                lblMsg.Visible = false;
                ImgAddSplitInfo.Enabled = false;                
                pnlAddNewInfo.Visible = true;

                int SubjectId = Convert.ToInt32(Session["ViewUserSubjectId"]);
                int SplitRowId = Convert.ToInt32(((HiddenField)GridSplitDetail.Rows[e.NewEditIndex].FindControl("hiddenSplitId")).Value.Trim());
                int ColumnHeaderId = Convert.ToInt32(((HiddenField)GridSplitDetail.Rows[e.NewEditIndex].FindControl("hiddenColumnHeaderId")).Value.Trim());

                string startText = ((Label)GridSplitDetail.Rows[e.NewEditIndex].FindControl("lblStartText")).Text.Trim();
                string endText = ((Label)GridSplitDetail.Rows[e.NewEditIndex].FindControl("lblEndText")).Text.Trim();

                string leadColumnHeader = ((Label)GridSplitDetail.Rows[e.NewEditIndex].FindControl("lblLeadColumnHeader")).Text.Trim();
                string splitValueText = ((Label)GridSplitDetail.Rows[e.NewEditIndex].FindControl("lblSplitValueText")).Text.Trim();
                string splitIndex = ((Label)GridSplitDetail.Rows[e.NewEditIndex].FindControl("dropdownSplitIndex")).Text.ToString().Trim();
                string SplitType = ((Label)GridSplitDetail.Rows[e.NewEditIndex].FindControl("lblIsValueSplit")).Text.ToString();
                Boolean isValueSplit = ((CheckBox)GridSplitDetail.Rows[e.NewEditIndex].FindControl("lblIsValueSplit1")).Checked;
                string defaultValue = ((Label)GridSplitDetail.Rows[e.NewEditIndex].FindControl("lbldefaultValue")).Text.Trim();
                string IsDefaultValuetype = ((Label)GridSplitDetail.Rows[e.NewEditIndex].FindControl("lbldefaultValueType")).Text.ToString();
                Boolean IsDefaultValueCheck = ((CheckBox)GridSplitDetail.Rows[e.NewEditIndex].FindControl("lblisHaveDefaultValue")).Checked;



                DataTable dt = new DataTable();
                dt.Columns.Add("id");
                dt.Columns.Add("leadColumnHeader");
                dt.Rows.Add(ColumnHeaderId, leadColumnHeader);
                MailToLeadColumnHeader.DataSource = dt;
                MailToLeadColumnHeader.DataTextField = "leadColumnHeader";
                MailToLeadColumnHeader.DataValueField = "id";
                MailToLeadColumnHeader.DataBind();
                MailToLeadColumnHeader.Enabled = false;
                fillIsValueSplitDropDownValue();
                defaultValueChk(!IsDefaultValueCheck);
                valueSplitinfo(isValueSplit);

                tbStartText.Text = startText;
                tbEndText.Text = endText;

                if (SplitType.ToLower().Contains("other") && isValueSplit)
                { txtValueToSplit.Visible = true; }
                if (IsDefaultValuetype.ToLower().Contains("other") && IsDefaultValueCheck)
                { txtDefaultValue.Visible = true; }

                txtValueToSplit.Text = splitValueText;
                dropdownValueIndex.SelectedValue = splitIndex;
                dropdownIsValueSplit.SelectedValue = SplitType;
                chkisValueSplit.Checked = isValueSplit;

                checkIsDefaultValue.Checked = IsDefaultValueCheck;
                dropdownIsDefaultValue.SelectedValue = IsDefaultValuetype;
                txtDefaultValue.Text = defaultValue;

                imgBtnAddNew.Text = "Update";
            }
            catch (Exception ex)
            { }
        }

        protected void GridSplitDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int LeadSplitId = Convert.ToInt32(GridSplitDetail.DataKeys[e.RowIndex].Values["Id"]);
            string result = dataBaseProvider.DeleteMailContentSplitInfo(LeadSplitId);
            if (result.Equals("SUCCESS"))
            {
                lblMsg.Text = "Template content split information deleted sucessfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                FillDefaultGridView();
            }
            else
            {
                lblMsg.Text = "Some Error Occured";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            lblMsg.Visible = true;
        }

        protected void imgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton img = sender as ImageButton;
            int SplitId = Convert.ToInt32(img.CommandArgument);
            string result = dataBaseProvider.DeleteMailContentSplitInfo(SplitId);
            if (result.Equals("SUCCESS"))
            {
                lblMsg.Text = "Template content split information deleted successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                GridSplitDetail.EditIndex = -1;
                FillDefaultGridView();
            }
            else
            {
                lblMsg.Text = "Some Error Occured";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void dropdownIsValueSplit_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            lblMsg.Visible = false;
            string value = ddl.SelectedValue.ToString();
            if (value.ToLower().Contains("other"))
            {
                lblIndex.Visible = true;
                txtValueToSplit.Visible = true;
                dropdownValueIndex.Visible = true;
            }
            else
            {
                txtValueToSplit.Visible = false;
                txtValueToSplit.Text = "";
            }
        }

        protected void chkDefaultValueCheck_Clicked(Object sender, EventArgs e)
        {
            CheckBox check = (CheckBox)sender;
            lblMsg.Visible = false;
            defaultValueChk(!check.Checked);
        }

        public void defaultValueChk(Boolean b)
        {
            dropdownIsDefaultValue.Visible = !b;
            lblValuetype.Visible = !b;
            chkisValueSplit.Enabled = b;
            dropdownIsValueSplit.Enabled = b;
            dropdownValueIndex.Enabled = b;
            txtValueToSplit.Enabled = b;
            tbStartText.Enabled = b;
            tbEndText.Enabled = b;
            if (!b)
            {
                fillDefaultValueDropDown();
            }
            else
            {
                txtDefaultValue.Visible = false;
                txtDefaultValue.Text = "";
            }
        }

        protected void chkIsValueSplitInfo_Clicked(Object sender, EventArgs e)
        {
            CheckBox check = (CheckBox)sender;
            lblMsg.Visible = false;
            valueSplitinfo(check.Checked);
        }

        public void valueSplitinfo(Boolean b)
        {
            dropdownIsValueSplit.Visible = b;
            lblSplitFrom.Visible = b;
            lblIndex.Visible = b;
            txtValueToSplit.Enabled = b;
            dropdownValueIndex.Visible = b;
            dropdownIsValueSplit.Enabled = b;
            dropdownValueIndex.Enabled = b;

            if (b)
            {
                txtValueToSplit.Text = "";
                if (dropdownIsValueSplit.SelectedValue.ToString().ToLower().Contains("other"))
                { txtValueToSplit.Visible = true; }
            }
        }

        protected void dropdownIsDefaultValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            lblMsg.Visible = false;
            string value = ddl.SelectedValue.ToString();
            if (value.ToLower().Contains("other"))
            {
                txtDefaultValue.Visible = true;
            }
            else
            {
                txtDefaultValue.Visible = false;
                txtDefaultValue.Text = "";
            }
        }

        protected void ImageGoBack3_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/frmTemplate.aspx");
        }

        protected void dropDpownListOfAllSubjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            pnlAddNewInfo.Visible = false;
            ImgAddSplitInfo.Enabled = true;
            Session["ViewUserSubjectId"] = dropDpownListOfAllSubjectList.SelectedValue;
            SetUserLeadMailSplitInfo(Convert.ToInt32(dropDpownListOfAllSubjectList.SelectedValue));
        }

    }
}