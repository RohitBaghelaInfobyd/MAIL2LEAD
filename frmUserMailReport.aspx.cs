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
using System.Text;
using System.IO;

namespace AdminTool
{
    public partial class frmUserMailReport : System.Web.UI.Page
    {
        static DataBaseProvider dataBaseProvider = new DataBaseProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int LoggedInuserId, UserId;
                    LoggedInuserId = Convert.ToInt32(Session["LoggedInuserId"]);
                    UserId = Convert.ToInt32(Session["ViewUserId"]);
                    SetupUserReport(UserId);
                    ((Label)(Master).FindControl("lblUserName")).Text = Session["UserName"].ToString();
                }

                catch (Exception ex)
                { }
            }
        }

        private void SetupUserReport(int userId)
        {
            if (userId < 0)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                DataTable dt = dataBaseProvider.GetUserMailReportByUserId(userId);
                ViewState["DefaultUserMailReportDataTable"] = dt;
                if (dt.Rows.Count < 1)
                {
                    ImgExportToCSV.Enabled = false;
                    ImgExportToExcel.Enabled = false;
                    ImgExportToPDF.Enabled = false;
                    GridUserMailReport.Visible = false;
                    lblMsg.Text = "No Data Found";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    GridUserMailReport.Visible = true;
                    ImgExportToCSV.Enabled = true;
                    ImgExportToExcel.Enabled = true;
                    ImgExportToPDF.Enabled = true;
                    GridUserMailReport.DataSource = dt;
                    GridUserMailReport.DataBind();
                }
            }
        }
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void ImgExportToExcel_Click(object sender, EventArgs e)
        {


            try
            {
                StringBuilder sb = new StringBuilder();
                string FileName = "UserMailReport";
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


        protected void ImgExportToPDF_Click(object sender, EventArgs e)
        {
            //     try
            //    {

            //        string FileName = "UserList";
            //        DataTable dt = GetDataTable();
            //        GridView GridView1 = new GridView();

            //        GridView1.AllowPaging = false;
            //        GridView1.DataSource = dt;
            //        GridView1.DataBind();
            //        GridView1.HeaderRow.BackColor = System.Drawing.Color.LightBlue;

            //        Response.ContentType = "application/pdf";
            //        Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".pdf");

            //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //        StringWriter sw = new StringWriter();
            //        HtmlTextWriter hw = new HtmlTextWriter(sw);
            //        GridView1.RenderControl(hw);
            //        StringReader sr = new StringReader(sw.ToString());
            //        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            //        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            //        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //        pdfDoc.Open();
            //        htmlparser.Parse(sr);
            //        pdfDoc.Close();
            //        Response.Write(pdfDoc);
            //        Response.End();
            //    }
            //    catch (Exception ex)
            //    {
            //        lblMsg.Text = "Some Error Occured . Please Try Again Later";
            //        lblMsg.Style.Add("color", "Red");
            //        lblMsg.Style.Add("display", "block");
            //    }

        }

        protected void GridUserMailReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        DataTable GetDataTable()
        {
            DataTable dt = (System.Data.DataTable)ViewState["DefaultUserMailReportDataTable"];
            //dt.Columns.Remove("");
            //dt.Columns.Remove("");
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

    }  
}