using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using System.Text;
using AdminTool.DataBase;

namespace AdminTool
{
    public partial class frmViewMailReport : System.Web.UI.Page
    {
        static DataBaseProvider dataBaseProvider = new DataBaseProvider();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int LoggedInuserId, userId;
                    LoggedInuserId = Convert.ToInt32(Session["LoggedInuserId"]);
                    userId = Convert.ToInt32(Session["ViewUserId"]);
                    if (userId < 0)
                    {
                        userId = LoggedInuserId;
                    }
                    if (LoggedInuserId < 1)
                    {
                        Response.Redirect("~/default.aspx");

                    }
                    else
                    {

                        tbEndDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                        tbStartDate.Text = DateTime.Now.Date.AddDays(-30).ToString("dd/MM/yyyy"); ;
                        GetMailStatus(userId);
                        FillDropDown();

                    }
                }
                catch (Exception exc)
                {
                    ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, exc.Message);
                }
            }

        }

        public void GetMailStatus(int UserId)
        {
            DataTable dt = new DataTable();
            string startDate = tbStartDate.Text.ToString();
            string endDate = tbEndDate.Text.ToString();
            string StatusType = dropDownStatusOfReport.SelectedValue.ToString();
            int SubjectId = Convert.ToInt32(dropDpownListOfAllSubjectList.SelectedValue.ToString());
            dt = dataBaseProvider.getMailReport(UserId, startDate, endDate, SubjectId, StatusType);
            ViewState["DefaultMailReportDataTable"] = dt;

            if (dt.Rows.Count < 1)
            {
                GridViewMailReport.Visible = false;

            }
            else
            {
                lblMsg.Visible = false;
                GridViewMailReport.Visible = true;
                GridViewMailReport.DataSource = dt;
                GridViewMailReport.DataBind();
            }

        }

        protected void GridViewMailReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewMailReport.PageIndex = e.NewPageIndex;
            //Bind grid

        }
        protected void ImageGoBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/frmApiReport.aspx");
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            string searchstring = txtSearchBox.Text.Trim();
            GridViewMailReport.EditIndex = -1;
            if (string.IsNullOrEmpty(searchstring))
            {
                hdnSearchTxt.Value = "";
                FillDefaultGridView(false);
            }
            else
            {
                hdnSearchTxt.Value = searchstring;
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["DefaultMailReportDataTable"];
                if (dt.Rows.Count == 0)
                {
                    FillDefaultGridView(false);
                    dt = (DataTable)ViewState["DefaultMailReportDataTable"];
                }

                Session["SearchString"] = searchstring;
                dt = FilterData(searchstring, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    GridViewMailReport.DataSource = dt;
                    GridViewMailReport.DataBind();
                    ImgExportToCSV.Enabled = true;
                    ImgExportToExcel.Enabled = true;
                    ImgExportToPDF.Enabled = true;
                }
                else
                {
                    DataTable dtnew = dt;
                    dtnew.Clear();
                    GridViewMailReport.DataSource = dtnew;
                    GridViewMailReport.DataBind();
                    lblMsg.Text = "No Data Found";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    ImgExportToCSV.Enabled = false;
                    ImgExportToExcel.Enabled = false;
                    ImgExportToPDF.Enabled = false;
                }
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

        private void FillDefaultGridView(bool refresh)
        {
            int ViewUserId;
            ViewUserId = Convert.ToInt32(Session["ViewUserId"]);
            DataTable dt = new DataTable();

            string startDate = tbStartDate.Text.ToString();
            string endDate = tbEndDate.Text.ToString();
            int SubjectId = Convert.ToInt32(dropDpownListOfAllSubjectList.SelectedValue.ToString());
            string StatusType = dropDownStatusOfReport.SelectedValue.ToString();
            dt = dataBaseProvider.getMailReport(ViewUserId, startDate, endDate, SubjectId, StatusType);
            ViewState["DefaultSubjectDataTable"] = dt;
            lblMsg.Visible = false;
            if (!string.IsNullOrEmpty(hdnSearchTxt.Value))
            {
                dt = FilterData(hdnSearchTxt.Value, dt);
            }

            GridViewMailReport.DataSource = dt;
            GridViewMailReport.DataBind();

            if (dt != null && dt.Rows.Count > 0)
            {
                ImgExportToCSV.Enabled = true;
                ImgExportToExcel.Enabled = true;
                ImgExportToPDF.Enabled = true;
                GridViewMailReport.Visible = true;
            }
            else
            {
                ImgExportToCSV.Enabled = false;
                ImgExportToExcel.Enabled = false;
                ImgExportToPDF.Enabled = false;
                GridViewMailReport.Visible = false;
                lblMsg.Text = "No DataFound";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Visible = true;
            }
        }


        private void FillDropDown()
        {
            int ViewUserId;
            ViewUserId = Convert.ToInt32(Session["ViewUserId"]);
            DataTable dt = dataBaseProvider.getListOfAllUserSubject(ViewUserId, 0, 0);

            DataRow dr = dt.NewRow();
            dr[0] = "0";
            dr[1] = "All";
            dr[2] = "0";
            dt.Rows.InsertAt(dr, 0);
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
            }
        }

        private void FillStatusDropDown()
        {
        }

        protected void ImgExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string FileName = "MailReport";
                DataTable dt = GetDataTable();

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

                HttpContext.Current.Response.Write("<Table border='1' " +
                 "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
                 "style='font-size:10.0pt; font-family:Calibri;'> <TR style='background-color:Yellow'>");
                //am getting my grid's column headers

                for (int j = 0; j < dt.Columns.Count; j++)
                {      //write in new column
                    HttpContext.Current.Response.Write("<Td>");
                    //Get column headers  and make it as bold in excel columns
                    HttpContext.Current.Response.Write("<B>");
                    HttpContext.Current.Response.Write(dt.Columns[j].ColumnName.ToString());
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

        protected void ImgExportToCSV_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string FileName = "MailReport";
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

        protected void ImgExportToPDF_Click(object sender, EventArgs e)
        {
            try
            {
                string FileName = "MailReport";
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
            DataTable dt = (System.Data.DataTable)ViewState["DefaultMailReportDataTable"];
            return dt;
        }

        protected void txtSearchBox_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        protected void btnApplyFilter_Click(object sender, EventArgs e)
        {
            FillDefaultGridView(true);
        }

        protected void imgMailDetailInfo_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton img = sender as ImageButton;
            int databaseId = Convert.ToInt32(img.CommandArgument);
            Session["DatabaseId"] = databaseId;
            Response.Redirect("~/frmUserMailDetailInfo.aspx");
        }
    }
}


