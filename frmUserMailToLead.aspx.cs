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
    public partial class frmUserMailToLead : System.Web.UI.Page
    {
        static DataBaseProvider dataBaseProvider = new DataBaseProvider();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                /* Session["LoggedInuserId"] = 1;
                 Session["ViewUserId"] = 5;
                 Session["ViewUserSubjectId"] = 12;*/
                try
                {
                    int LoggedInuserId, UserId;
                    LoggedInuserId = Convert.ToInt32(Session["LoggedInuserId"]);
                    UserId = Convert.ToInt32(Session["ViewUserId"]);
                    AddNewLeadInfo.Visible = false;
                    tbNewLeadInfo.Text = "";
                    tbNewMailInfo.Text = "";
                    if (LoggedInuserId < 1)
                    { Response.Redirect("~/default.aspx"); }
                    else
                    {
                        SetUserLeadMailInfo(UserId);
                    }
                    ((Label)(Master).FindControl("lblUserName")).Text = Session["UserName"].ToString();
                }
                catch (Exception ex)
                {
                    ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                    lblMsg.Text = "Some Error Occured";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }


        public void SetUserLeadMailInfo(int userId)
        {
            if (userId > 0)
            {
                DataTable dt = dataBaseProvider.getListofColumnHeaderOfLeadToMailTable(userId);
                ViewState["DefaultLeadDataTable"] = dt;
                if (dt.Rows.Count < 1)
                {
                    GridLeadDetail.Visible = false;
                    ImgExportToCSV.Enabled = false;
                    ImgExportToExcel.Enabled = false;
                    ImgExportToPDF.Enabled = false;
                    lblMsg.Text = "No Data Found";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    GridLeadDetail.AutoGenerateColumns = false;
                    GridLeadDetail.Visible = true;
                    ImgExportToCSV.Enabled = true;
                    ImgExportToExcel.Enabled = true;
                    ImgExportToPDF.Enabled = true;
                    GridLeadDetail.DataSource = dt;
                    GridLeadDetail.DataBind();
                }
            }
            else
            {
                Response.Redirect("~/default.aspx");
            }
        }

        protected void ImageGoBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/frmUserDetailViewScreen.aspx");
        }

       private void Search()
        {
            string searchstring = txtSearchBox.Text.Trim();
            GridLeadDetail.EditIndex = -1;
            if (string.IsNullOrEmpty(searchstring))
            {
                hdnSearchTxt.Value = "";
                FillDefaultGridView(false);
            }
            else
            {
                hdnSearchTxt.Value = searchstring;
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["DefaultLeadDataTable"];
                if (dt.Rows.Count == 0)
                {
                    FillDefaultGridView(false);
                    dt = (DataTable)ViewState["DefaultLeadDataTable"];
                }

                Session["SearchString"] = searchstring;
                dt = FilterData(searchstring, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    GridLeadDetail.DataSource = dt;
                    GridLeadDetail.DataBind();
                    ImgExportToCSV.Enabled = true;
                    ImgExportToExcel.Enabled = true;
                    ImgExportToPDF.Enabled = true;
                }
                else
                {
                    DataTable dtnew = dt;
                    dtnew.Clear();
                    GridLeadDetail.DataSource = dtnew;
                    GridLeadDetail.DataBind();
                    lblMsg.Text = "No Data Found";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    ImgExportToCSV.Enabled = false;
                    ImgExportToExcel.Enabled = false;
                    ImgExportToPDF.Enabled = false;
                }
            }
        }
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            Search();
        }


        private void FillDefaultGridView(bool refresh)
        {
            int ViewUserId;
            ViewUserId = Convert.ToInt32(Session["ViewUserId"]);
            DataTable dt = new DataTable();
            dt = dataBaseProvider.getListofColumnHeaderOfLeadToMailTable(ViewUserId);
            AddNewLeadInfo.Visible = false;
            ViewState["DefaultLeadDataTable"] = dt;

            if (!string.IsNullOrEmpty(hdnSearchTxt.Value))
            {
                dt = FilterData(hdnSearchTxt.Value, dt);
            }

            GridLeadDetail.DataSource = dt;
            GridLeadDetail.DataBind();

            if (dt != null && dt.Rows.Count > 0)
            {
                ImgExportToCSV.Enabled = true;
                ImgExportToExcel.Enabled = true;
                ImgExportToPDF.Enabled = true;
                GridLeadDetail.Visible = true;
            }
            else
            {

                ImgExportToCSV.Enabled = false;
                ImgExportToExcel.Enabled = false;
                ImgExportToPDF.Enabled = false;
                AddNewLeadInfo.Visible = true;
                GridLeadDetail.Visible = false;
                lblMsg.Text = "No DataFound";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected DataTable FilterData(string searchString, DataTable dt)
        {
            DataTable dtnew = new DataTable();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] foundRows = dt.Select("leadColumnHeader LIKE '%" + searchString + "%' OR mailColumnHeader LIKE '%" +
                    searchString + "%'");
                dtnew = foundRows.CopyToDataTable();
            }

            return dtnew;
        }

        protected void ImgAddNewLeadColumn_Click1(object sender, EventArgs e)
        {
            AddNewLeadInfo.Visible = true;
            tbNewLeadInfo.Text = "";
            tbNewMailInfo.Text = "";
            lblMsg.Visible = false;
        }

        protected void imgBtnNewLeadInfo_Click(object sender, EventArgs e)
        {
            try
            {

                int userId = Convert.ToInt32(Session["ViewUserId"]);
                string MailColumnHeader = tbNewMailInfo.Text.ToString();
                string LeadColumnHeader = tbNewLeadInfo.Text.ToString();


                if (string.IsNullOrEmpty(MailColumnHeader.Trim()) || string.IsNullOrEmpty(LeadColumnHeader.Trim()))
                {
                    lblMsg.Text = "Filed Should be not empty";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {


                    string result = dataBaseProvider.AddNewColumnHeaderOfLeadToMail(MailColumnHeader, LeadColumnHeader, userId);
                    if (result.Equals("SUCCESS"))
                    {
                        lblMsg.Text = "Lead Info Added Successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        GridLeadDetail.EditIndex = -1;
                        tbNewLeadInfo.Text = "";
                        tbNewMailInfo.Text = "";
                    }
                    else
                    {
                        lblMsg.Text = result;
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                lblMsg.Visible = true;
                FillDefaultGridView(true);
            }
            catch (Exception ex)
            {
                ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
                lblMsg.Text = "Some Error Occured";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

        }

        protected void imgBtnNewLeadInfoCancel_Click(object sender, EventArgs e)
        {
            AddNewLeadInfo.Visible = false;
            tbNewMailInfo.Text = "";
            tbNewLeadInfo.Text = "";
            FillDefaultGridView(true);

        }

        protected void GridLeadDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int LeadId = Convert.ToInt32(GridLeadDetail.DataKeys[e.RowIndex].Values["Id"]);
            string result = dataBaseProvider.DeleteColumnHeaderOfLeadToMail(LeadId);
            if (result.Equals("SUCCESS"))
            {
                lblMsg.Text = "Lead Info Deleted Sucessfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                FillDefaultGridView(true);
            }
            else
            {
                lblMsg.Text = result;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void GridLeadDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridLeadDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridLeadDetail.EditIndex = -1;
            FillDefaultGridView(false);

        }

        protected void GridLeadDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int UserId = Convert.ToInt32(Session["ViewUserId"]);
                int LeadId = Convert.ToInt32(((HiddenField)GridLeadDetail.Rows[e.RowIndex].FindControl("hiddenLeadId")).Value.Trim());
                string MailColumnHeader = ((TextBox)GridLeadDetail.Rows[e.RowIndex].FindControl("tbMailColumnHeader")).Text.Trim();
                string LeadColumnHeader = ((TextBox)GridLeadDetail.Rows[e.RowIndex].FindControl("tbLeadColumnHeader")).Text.Trim();

               
                if (string.IsNullOrEmpty(MailColumnHeader.Trim()) || string.IsNullOrEmpty(LeadColumnHeader.Trim()))
                {
                    lblMsg.Text = "Filed Should be not empty";
                    lblMsg.ForeColor = System.Drawing.Color.Red;

                }
                else
                {
                    string result = dataBaseProvider.UpdateColumnHeaderOfLeadToMailInfo(LeadId, MailColumnHeader, LeadColumnHeader, UserId);
                    if (result.Equals("SUCCESS"))
                    {
                        lblMsg.Text = "Lead Info Update Successfully";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        GridLeadDetail.EditIndex = -1;
                        FillDefaultGridView(true);
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
                lblMsg.Text = "Some Error Occured";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void GridLeadDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GridLeadDetail_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridLeadDetail.EditIndex = e.NewEditIndex;

            FillDefaultGridView(false);
        }

        protected void imgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton img = sender as ImageButton;
            int LeadId = Convert.ToInt32(img.CommandArgument);
            string result = dataBaseProvider.DeleteColumnHeaderOfLeadToMail(LeadId);
            if (result.Equals("SUCCESS"))
            {
                lblMsg.Text = "Lead Info Deleted Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                txtSearchBox.Text = "";
                hdnSearchTxt.Value = "";
                GridLeadDetail.EditIndex = -1;
                FillDefaultGridView(true);
            }
            else
            {
                lblMsg.Text = result;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void ImgExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string FileName = "UserMailToLead";
                DataTable dt = GetDataTable();
                GridView GridView1 = new GridView();

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName + ".xls");

                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                //sets font
                HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
                HttpContext.Current.Response.Write("<BR><BR><BR>");
                //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
                HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
                  "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
                  "style='font-size:10.0pt; font-family:Calibri; background:lightblue;'> <TR>");
                //am getting my grid's column headers
                int columnscount = GridView1.Columns.Count;

                for (int j = 0; j < columnscount; j++)
                {      //write in new column
                    HttpContext.Current.Response.Write("<Td>");
                    //Get column headers  and make it as bold in excel columns
                    HttpContext.Current.Response.Write("<B>");
                    HttpContext.Current.Response.Write(GridView1.Columns[j].HeaderText.ToString());
                    HttpContext.Current.Response.Write("</B>");
                    HttpContext.Current.Response.Write("</Td>");
                }
                HttpContext.Current.Response.Write("</TR>");
                foreach (DataRow row in dt.Rows)
                {//write in new row
                    HttpContext.Current.Response.Write("<TR>");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        HttpContext.Current.Response.Write("<Td>");
                        HttpContext.Current.Response.Write(row[i].ToString());
                        HttpContext.Current.Response.Write("</Td>");
                    }

                    HttpContext.Current.Response.Write("</TR>");
                }
                HttpContext.Current.Response.Write("</Table>");
                HttpContext.Current.Response.Write("</font>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();

            }
            catch (Exception ex)
            {
                lblMsg.Text = "Some Error Occured . Please Try Again Later";
                lblMsg.Style.Add("color", "Red");
                lblMsg.Style.Add("display", "block");
            }
        }




        protected void ImgExportToPDF_Click(object sender, EventArgs e)
        {

            try
            {

                string FileName = "UserMailToLead";
                DataTable dt = GetDataTable();
                GridView GridView1 = new GridView();

                GridView1.AllowPaging = false;
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.HeaderRow.BackColor = System.Drawing.Color.LightBlue;

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".pdf");

                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                GridView1.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                Response.End();
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Some Error Occured . Please Try Again Later";
                lblMsg.Style.Add("color", "Red");
                lblMsg.Style.Add("display", "block");
            }
        }

        DataTable GetDataTable()
        {
            DataTable dt = (System.Data.DataTable)ViewState["DefaultSubjectDataTable"];
            //dt.Columns.Remove("id");
            //dt.Columns.Remove("creatorId");
            //dt.Columns.Remove("type");
            //dt.Columns.Remove("profileImage");
            //dt.Columns.Remove("password");
            //dt.Columns.Remove("apiLimit");
            //dt.Columns.Remove("isActive");
            //dt.Columns.Remove("isApproved");
            //dt.Columns.Remove("creationDate");
            //dt.Columns.Remove("modificationDate");
            return dt;
        }

        protected void ImgExportToCSV_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string FileName = "UserList";
                DataTable dt = GetDataTable();
                GridView GridView1 = new GridView();

                GridView1.AllowPaging = false;
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.HeaderRow.BackColor = System.Drawing.Color.LightBlue;

                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                {
                    sb.Append(cell.Text.Trim() + ',');
                }
                sb.Append("\r\n");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".csv");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        sb.Append(Convert.ToString(dt.Rows[i][j]) + ',');
                    }
                    sb.Append("\r\n");
                }
                string b = System.Net.WebUtility.HtmlDecode(sb.ToString());
                Response.Write(b);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Some Error Occured . Please Try Again Later";
                lblMsg.Style.Add("color", "Red");
                lblMsg.Style.Add("display", "block");
            }

        }

        protected void txtSearchBox_TextChanged(object sender, EventArgs e)
        {
            Search();
        }
    }
}
