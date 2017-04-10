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
    public partial class frmLeadInfo : System.Web.UI.Page
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
                    AddNewLeadInfo.Visible = false;
                    tbNewLeadInfo.Text = "";
                    if (LoggedInuserId < 1)
                    {
                        Response.Redirect("~/default.aspx");
                    }
                    else
                    {
                        SetUserLeadMailInfo();
                    }
                }
                catch (Exception ex)
                {
                    ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                }
            }
        }


        public void SetUserLeadMailInfo()
        {
            int UserId = Convert.ToInt32(Session["ViewUserId"]);
            if (UserId > 0)
            {
                DataTable dt = dataBaseProvider.getListofColumnHeaderOfLeadToMailTable(UserId, 1);
                if (dt.Rows.Count < 1)
                {
                    GridLeadDetail.Visible = false;
                    lblMsg.Text = "No Data Found";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    GridLeadDetail.AutoGenerateColumns = false;
                    GridLeadDetail.Visible = true;
                    GridLeadDetail.DataSource = dt;
                    GridLeadDetail.DataBind();
                }
            }
        }

        protected void ImgAddNewLeadColumn_Click1(object sender, EventArgs e)
        {
            btnAddNewLeadInfo.Enabled = false;
            AddNewLeadInfo.Visible = true;
            tbNewLeadInfo.Text = "";
            lblMsg.Visible = false;
            GridLeadDetail.EditIndex = -1;
            SetUserLeadMailInfo();
        }

        protected void imgBtnNewLeadInfo_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = Convert.ToInt32(Session["ViewUserId"]);
                string LeadColumnHeader = tbNewLeadInfo.Text.ToString();

                if (string.IsNullOrEmpty(LeadColumnHeader.Trim()))
                {
                    lblMsg.Text = "Please enter valid Filed Name";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    string result = dataBaseProvider.AddNewColumnHeaderOfLeadToMail(LeadColumnHeader, userId);
                    if (result.Equals("SUCCESS"))
                    {
                        lblMsg.Text = "Filed Name added successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        GridLeadDetail.EditIndex = -1;
                        tbNewLeadInfo.Text = "";
                    }
                    else
                    {
                        lblMsg.Text = result;
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                    SetUserLeadMailInfo();
                }
            }
            catch (Exception ex)
            {
                ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                lblMsg.Text = "Some error occured";
                lblMsg.ForeColor = System.Drawing.Color.Red;

            }
            lblMsg.Visible = true;
        }

        protected void imgBtnNewLeadInfoCancel_Click(object sender, EventArgs e)
        {
            btnAddNewLeadInfo.Enabled = true;
            AddNewLeadInfo.Visible = false;
            tbNewLeadInfo.Text = "";
            SetUserLeadMailInfo();
            lblMsg.Visible = false;
        }

        protected void GridLeadDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int LeadId = Convert.ToInt32(GridLeadDetail.DataKeys[e.RowIndex].Values["Id"]);
            string result = dataBaseProvider.DeleteColumnHeaderOfLeadToMail(LeadId);
            if (result.Equals("SUCCESS"))
            {
                lblMsg.Text = "Filed Name deleted sucessfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                SetUserLeadMailInfo();
            }
            else
            {
                lblMsg.Text = result;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            lblMsg.Visible = true;
        }


        protected void GridLeadDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            lblMsg.Visible = false;
            GridLeadDetail.EditIndex = -1;
            SetUserLeadMailInfo();
        }

        protected void GridLeadDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int UserId = Convert.ToInt32(Session["ViewUserId"]);
                int LeadId = Convert.ToInt32(((HiddenField)GridLeadDetail.Rows[e.RowIndex].FindControl("hiddenLeadId")).Value.Trim());
                string LeadColumnHeader = ((TextBox)GridLeadDetail.Rows[e.RowIndex].FindControl("tbLeadColumnHeader")).Text.Trim();
                bool isSubscribe = ((CheckBox)GridLeadDetail.Rows[e.RowIndex].FindControl("chkIsSubscribe")).Checked;

                if (string.IsNullOrEmpty(LeadColumnHeader.Trim()))
                {
                    lblMsg.Text = "Please enter valid Filed Name";
                    lblMsg.ForeColor = System.Drawing.Color.Red;

                }
                else
                {
                    string result = dataBaseProvider.UpdateColumnHeaderOfLeadToMailInfo(LeadId, LeadColumnHeader, UserId, isSubscribe);
                    if (result.Equals("SUCCESS"))
                    {
                        lblMsg.Text = "Filed Name update successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        GridLeadDetail.EditIndex = -1;
                        SetUserLeadMailInfo();
                    }
                    else
                    {
                        lblMsg.Text = result;
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                lblMsg.Visible = true;
            }
            catch (Exception ex)
            {
                ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                lblMsg.Text = "Some error occured";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void GridLeadDetail_RowEditing(object sender, GridViewEditEventArgs e)
        {
            btnAddNewLeadInfo.Enabled = true;
            AddNewLeadInfo.Visible = false;
            tbNewLeadInfo.Text = "";
            lblMsg.Visible = false;
            GridLeadDetail.EditIndex = e.NewEditIndex;
            SetUserLeadMailInfo();
        }

        protected void imgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton img = sender as ImageButton;
            int LeadId = Convert.ToInt32(img.CommandArgument);
            string result = dataBaseProvider.DeleteColumnHeaderOfLeadToMail(LeadId);
            if (result.Equals("SUCCESS"))
            {
                lblMsg.Text = "Filed Name deleted successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                GridLeadDetail.EditIndex = -1;
                SetUserLeadMailInfo();
            }
            else
            {
                lblMsg.Text = result;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

            lblMsg.Visible = true;
        }

        protected void GridTemplateDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int columnType = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "columnType").ToString());
                if (columnType < 1)
                {
                    e.Row.Cells[5].Controls[2].Visible = false;
                }
            }
        }
    }
}
