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
    public partial class frmViewSubjectInfo : System.Web.UI.Page
    {
        static DataBaseProvider dataBaseProvider = new DataBaseProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            /*  Session["ViewUserId"] = 5;
              Session["LoggedInuserId"] = 1;*/
            if (!IsPostBack)
            {
                try
                {
                    int LoggedInuserId, UserId;
                    LoggedInuserId = Convert.ToInt32(Session["LoggedInuserId"]);
                    UserId = Convert.ToInt32(Session["ViewUserId"]);
                    AddNewSubject.Visible = false;
                    tbNewSubjectLine.Text = "";
                    if (LoggedInuserId < 1)
                    { Response.Redirect("~/default.aspx"); }
                    else
                    {
                        SetUserSubjectInfo(UserId);
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

        public void SetUserSubjectInfo(int userId)
        {
            if (userId > 0)
            {
                DataTable dt = dataBaseProvider.getListOfAllUserSubject(userId, 0, 0);
                ViewState["DefaultSubjectDataTable"] = dt;
                if (dt.Rows.Count < 1)
                {
                    GridSubjectDetail.Visible = false;
                    lblMsg.Text = "No Data Found";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    ImgExportToExcel.Enabled = false;
                    ImgExportToCSV.Enabled = false;
                    ImgExportToPDF.Enabled = false;
                }
                else
                {
                    GridSubjectDetail.AutoGenerateColumns = false;
                    GridSubjectDetail.Visible = true;
                    GridSubjectDetail.DataSource = dt;
                    GridSubjectDetail.DataBind();
                    ImgExportToExcel.Enabled = true;
                    ImgExportToCSV.Enabled = true;
                    ImgExportToPDF.Enabled = true;
                }
            }
            else
            {
                Response.Redirect("~/default.aspx");
            }
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            string searchstring = txtSearchBox.Text.Trim();
            GridSubjectDetail.EditIndex = -1;
            if (string.IsNullOrEmpty(searchstring))
            {
                hdnSearchTxt.Value = "";
                FillDefaultGridView(false);
            }
            else
            {
                hdnSearchTxt.Value = searchstring;
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["DefaultSubjectDataTable"];
                if (dt.Rows.Count == 0)
                {
                    FillDefaultGridView(false);
                    dt = (DataTable)ViewState["DefaultSubjectDataTable"];
                }

                Session["SearchString"] = searchstring;
                dt = FilterData(searchstring, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    GridSubjectDetail.DataSource = dt;
                    GridSubjectDetail.DataBind();
                    ImgExportToCSV.Enabled = true;
                    ImgExportToExcel.Enabled = true;
                    ImgExportToPDF.Enabled = true;
                }
                else
                {
                    DataTable dtnew = dt;
                    dtnew.Clear();
                    GridSubjectDetail.DataSource = dtnew;
                    GridSubjectDetail.DataBind();
                    lblMsg.Text = "No Data Found";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    ImgExportToCSV.Enabled = false;
                    ImgExportToExcel.Enabled = false;
                    ImgExportToPDF.Enabled = false;
                }
            }
        }

        private void FillDefaultGridView(bool refresh)
        {
            int ViewUserId;
            ViewUserId = Convert.ToInt32(Session["ViewUserId"]);
            DataTable dt = new DataTable();
            dt = dataBaseProvider.getListOfAllUserSubject(ViewUserId, 0, 0);
            AddNewSubject.Visible = false;
            ViewState["DefaultSubjectDataTable"] = dt;

            if (!string.IsNullOrEmpty(hdnSearchTxt.Value))
            {
                dt = FilterData(hdnSearchTxt.Value, dt);
            }

            GridSubjectDetail.DataSource = dt;
            GridSubjectDetail.DataBind();

            if (dt != null && dt.Rows.Count > 0)
            {
                ImgExportToCSV.Enabled = true;
                ImgExportToExcel.Enabled = true;
                ImgExportToPDF.Enabled = true;
                GridSubjectDetail.Visible = true;
            }
            else
            {
                ImgExportToCSV.Enabled = false;
                ImgExportToExcel.Enabled = false;
                ImgExportToPDF.Enabled = false;
                GridSubjectDetail.Visible = false;
                lblMsg.Text = "No DataFound";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected DataTable FilterData(string searchString, DataTable dt)
        {
            DataTable dtnew = new DataTable();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] foundRows = dt.Select("subjectLine LIKE '%" + searchString + "%'");
                dtnew = foundRows.CopyToDataTable();
            }

            return dtnew;
        }
        protected void ImgAddSubject_Click(object sender, EventArgs e)
        {
            AddNewSubject.Visible = true;
            lblMsg.Visible = false;
            if (GridSubjectDetail.Rows.Count <= 0)
            {
                ImgExportToCSV.Enabled = false;
                ImgExportToExcel.Enabled = false;
                ImgExportToPDF.Enabled = false;
            }
        }

        protected void ImgExportToPDF_Click(object sender, EventArgs e)
        {
            try
            {

                string FileName = "SubjectInfo";
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
            //dt.Columns.Remove("");
            //dt.Columns.Remove("");
            //dt.Columns.Remove("");
            //dt.Columns.Remove("");
            //dt.Columns.Remove("");
            //dt.Columns.Remove("");
            //dt.Columns.Remove("");
            //dt.Columns.Remove("");
            //dt.Columns.Remove("");
            //dt.Columns.Remove("");
            return dt;
        }



        protected void ImgExportToCSV_Click(object sender, EventArgs e)
        {

            try
            {
                StringBuilder sb = new StringBuilder();
                string FileName = "Subjectinfo";
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




        protected void ImgExportToExcel_Click(object sender, EventArgs e)
        {


            try
            {
                StringBuilder sb = new StringBuilder();
                string FileName = "SubjectInfo";
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


        protected void GridSubjectDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int SubjectId = Convert.ToInt32(GridSubjectDetail.DataKeys[e.RowIndex].Values["Id"]);
            string result = dataBaseProvider.DeleteSubjectById(SubjectId);
            if (result.Equals("SUCCESS"))
            {
                lblMsg.Text = "Subject Deleted Sucessfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                FillDefaultGridView(true);
            }
            else
            {
                lblMsg.Text = "Some Error Occured";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

        }

        protected void GridSubjectDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridSubjectDetail.EditIndex = -1;
            FillDefaultGridView(false);
        }

        protected void GridSubjectDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int SubjectId = Convert.ToInt32(((HiddenField)GridSubjectDetail.Rows[e.RowIndex].FindControl("hiddenSubjectId")).Value.Trim());
                string SubjectLine = ((TextBox)GridSubjectDetail.Rows[e.RowIndex].FindControl("tbSubjectLine")).Text.Trim();
                Boolean IsApproved = ((CheckBox)GridSubjectDetail.Rows[e.RowIndex].FindControl("chkIsApproved")).Checked;
                string result = dataBaseProvider.UpdateSubjectInfoById(SubjectLine, SubjectId, IsApproved);
                if (result.Equals("SUCCESS"))
                {
                    lblMsg.Text = "Subject Update Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    GridSubjectDetail.EditIndex = -1;
                    FillDefaultGridView(true);
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

        }

        protected void GridSubjectDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GridSubjectDetail_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridSubjectDetail.EditIndex = e.NewEditIndex;

            FillDefaultGridView(false);

        }

        protected void GridSubjectDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void imgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton img = sender as ImageButton;
            int SubjectId = Convert.ToInt32(img.CommandArgument);
            string result = dataBaseProvider.DeleteSubjectById(SubjectId);
            if (result.Equals("SUCCESS"))
            {
                lblMsg.Text = "Subject Deleted Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                txtSearchBox.Text = "";
                hdnSearchTxt.Value = "";
                GridSubjectDetail.EditIndex = -1;
                FillDefaultGridView(true);
            }
            else
            {
                lblMsg.Text = "Some Error Occured";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void imgBtnNewSubjectLine_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewSubject.Visible = false;
                int userId = Convert.ToInt32(Session["ViewUserId"]);
                string SubjectLine = tbNewSubjectLine.Text.ToString();
                string result = dataBaseProvider.AddNewSubject(SubjectLine, userId);
                if (result.Equals("SUCCESS"))
                {
                    lblMsg.Text = "Subject Added Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    GridSubjectDetail.EditIndex = -1;
                    tbNewSubjectLine.Text = "";
                    FillDefaultGridView(true);
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


        }

        protected void imgBtnNewSubjectLineCancel_Click(object sender, EventArgs e)
        {
            AddNewSubject.Visible = false;
            tbNewSubjectLine.Text = "";
            FillDefaultGridView(true);

        }

        protected void ImageGoBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/frmUserDetailViewScreen.aspx");
        }

        protected void imgBtnSubjectDetail_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton img = sender as ImageButton;
            int SubjectId = Convert.ToInt32(img.CommandArgument);
            Session["ViewUserSubjectId"] = SubjectId;
            Response.Redirect("~/frmMailContentSplitInfo.aspx");
        }

        protected void txtSearchBox_TextChanged(object sender, EventArgs e)
        {
            Search();
        }
    }
}