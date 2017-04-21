using AdminTool.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using System.Text;

namespace AdminTool
{
    public partial class frmTemplate : System.Web.UI.Page
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
                    AddNewTemplateInfo.Visible = false;
                    tbNewTemplateInfo.Text = "";
                    if (LoggedInuserId < 1)
                    {
                        Response.Redirect("~/default.aspx");
                    }
                    else
                    {
                        SetUserTemplateInfo();
                    }
                }
                catch (Exception ex)
                {
                    ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }


        public void SetUserTemplateInfo()
        {
            int UserId = Convert.ToInt32(Session["ViewUserId"]);
            if (UserId > 0)
            {
                DataTable dt = dataBaseProvider.getListOfAllUserSubject(UserId, 0, 0);
                if (dt.Rows.Count < 1)
                {
                    GridTemplateDetail.Visible = false;
                    lblMsg.Text = "No Data Found";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Visible = true;
                }
                else
                {
                    GridTemplateDetail.AutoGenerateColumns = false;
                    GridTemplateDetail.Visible = true;
                    GridTemplateDetail.DataSource = dt;
                    GridTemplateDetail.DataBind();
                }
            }
        }

        protected void btnAddNewTemplate_Click(object sender, EventArgs e)
        {
            btnAddNewTemplate.Enabled = false;
            AddNewTemplateInfo.Visible = true;
            tbNewTemplateInfo.Text = "";
            lblMsg.Visible = false;
            GridTemplateDetail.EditIndex = -1;
            SetUserTemplateInfo();
        }

        protected void btnAddNewTemplateInfoAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = Convert.ToInt32(Session["ViewUserId"]);
                string templateText = tbNewTemplateInfo.Text.ToString();

                if (string.IsNullOrEmpty(templateText.Trim()))
                {
                    lblMsg.Text = "Please enter valid subject title";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    string subjectType = dropDownTemplateType.SelectedValue.ToString();
                    string result = dataBaseProvider.AddNewSubject(templateText, userId, subjectType);
                    if (result.Equals("SUCCESS"))
                    {
                        lblMsg.Text = "New Template Added Successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        GridTemplateDetail.EditIndex = -1;
                        tbNewTemplateInfo.Text = "";
                        AddNewTemplateInfo.Visible = false;
                    }
                    else
                    {
                        lblMsg.Text = result;
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                    SetUserTemplateInfo();
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

        protected void btnAddNewTemplateInfoCancel_Click(object sender, EventArgs e)
        {
            AddNewTemplateInfo.Visible = false;
            tbNewTemplateInfo.Text = "";
            lblMsg.Visible = false;
            btnAddNewTemplate.Enabled = true;
            SetUserTemplateInfo();
        }

        protected void GridTemplateDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int subjectId = Convert.ToInt32(GridTemplateDetail.DataKeys[e.RowIndex].Values["Id"]);
            string result = dataBaseProvider.DeleteSubjectById(subjectId);
            if (result.Equals("SUCCESS"))
            {
                lblMsg.Text = "Template Deleted Sucessfully";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                SetUserTemplateInfo();
            }
            else
            {
                lblMsg.Text = result;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            lblMsg.Visible = true;
        }


        protected void GridTemplateDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            lblMsg.Visible = false;
            GridTemplateDetail.EditIndex = -1;
            SetUserTemplateInfo();
        }

        protected void GridTemplateDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int UserId;
                UserId = Convert.ToInt32(Session["ViewUserId"]);
                int SubjectId = Convert.ToInt32(((HiddenField)GridTemplateDetail.Rows[e.RowIndex].FindControl("hiddenSubjectId")).Value.Trim());
                string SubjectLine = ((TextBox)GridTemplateDetail.Rows[e.RowIndex].FindControl("tbSubjectTitleHeader")).Text.Trim();
                Boolean IsApproved = ((CheckBox)GridTemplateDetail.Rows[e.RowIndex].FindControl("chkIsApproved")).Checked;
                string subjectType = ((DropDownList)GridTemplateDetail.Rows[e.RowIndex].FindControl("gridDropDownTemplateType")).SelectedValue;
                string result = dataBaseProvider.UpdateSubjectInfoById(SubjectLine, SubjectId, IsApproved, UserId, subjectType);
                if (result.Equals("SUCCESS"))
                {
                    lblMsg.Text = "Template Update Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    GridTemplateDetail.EditIndex = -1;
                    SetUserTemplateInfo();
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

        protected void GridTemplateDetail_RowEditing(object sender, GridViewEditEventArgs e)
        {
            btnAddNewTemplate.Enabled = true;
            AddNewTemplateInfo.Visible = false;
            tbNewTemplateInfo.Text = "";
            lblMsg.Visible = false;
            GridTemplateDetail.EditIndex = e.NewEditIndex;
            SetUserTemplateInfo();
        }

        protected void imgBtnSubjectDetail_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton img = sender as ImageButton;
            int SubjectId = Convert.ToInt32(img.CommandArgument);
            Session["ViewUserSubjectId"] = SubjectId;
            Response.Redirect("~/frmMailContentSplitInfo.aspx");
        }
    }
}
