using AdminTool.DataBase;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;


namespace AdminTool
{
    public partial class frmUserList : System.Web.UI.Page
    {
        static DataBaseProvider dataBaseProvider = new DataBaseProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Style.Remove("color");
            lblMsg.Text = "";
            if (!IsPostBack)
            {
                try
                {
                    int LoggedInuserId;
                    LoggedInuserId = Convert.ToInt32(Session["LoggedInuserId"]);
                    Session["ViewUserId"] = LoggedInuserId;
                    if (LoggedInuserId < 1)
                    { Response.Redirect("~/default.aspx"); }
                    else
                    {
                        GetAllUserList(LoggedInuserId);
                    }
                    ((Label)(Master).FindControl("lblUserName")).Text = Session["UserName"].ToString();
                }
                catch (Exception exc)
                {
                    ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, exc.Message);
                }
            }

        }

        public void GetAllUserList(int UserId)
        {
            DataTable dt = dataBaseProvider.getListOfallUser(UserId);
            ViewState["DefaultDataTable"] = dt;
            if (dt.Rows.Count < 1)
            {
                lblMsg.Visible = true;
                GridUserDetails.Visible = false;
                ImgExportToCSV.Enabled = false;
                ImgExportToExcel.Enabled = false;
                ImgExportToPDF.Enabled = false;
            }
            else
            {
                lblMsg.Visible = false;
                ImgExportToCSV.Enabled = true;
                ImgExportToExcel.Enabled = true;
                ImgExportToPDF.Enabled = true;
                GridUserDetails.Visible = true;
                GridUserDetails.DataSource = dt;
                GridUserDetails.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            search();
        }

        private void search()
        {
            string searchstring = txtSearchBox.Text.Trim();
            GridUserDetails.EditIndex = -1;
            if (string.IsNullOrEmpty(searchstring))
            {
                hdnSearchTxt.Value = "";
                FillDefaultGridView(false);
            }
            else
            {
                hdnSearchTxt.Value = searchstring;
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["DefaultDataTable"];
                if (dt.Rows.Count == 0)
                {
                    FillDefaultGridView(false);
                    dt = (DataTable)ViewState["DefaultDataTable"];
                }

                Session["SearchString"] = searchstring;
                dt = FilterData(searchstring, dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    GridUserDetails.DataSource = dt;
                    GridUserDetails.DataBind();
                    ImgExportToCSV.Enabled = true;
                    ImgExportToExcel.Enabled = true;
                    ImgExportToPDF.Enabled = true;
                }
                else
                {
                    DataTable dtnew = dt;
                    dtnew.Clear();
                    GridUserDetails.DataSource = dtnew;
                    GridUserDetails.DataBind();
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
            DataTable dnew = new DataTable();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] foundRows = dt.Select("FirstName LIKE '%" + searchString + "%' OR LastName LIKE '%" +
                    searchString + "%' OR EmailId LIKE '%" + searchString + "%'");
                if (foundRows.Count() > 0)
                {
                    dnew = foundRows.CopyToDataTable();
                }
            }

            return dnew;
        }


        private void FillDefaultGridView(bool refresh)
        {
            int LoggedInuserId;
            LoggedInuserId = Convert.ToInt32(Session["LoggedInuserId"]);
            DataTable dt = new DataTable();
            dt = dataBaseProvider.getListOfallUser(LoggedInuserId);
            ViewState["DefaultDataTable"] = dt;

            if (!string.IsNullOrEmpty(hdnSearchTxt.Value))
            {
                dt = FilterData(hdnSearchTxt.Value, dt);
            }

            GridUserDetails.DataSource = dt;
            GridUserDetails.DataBind();

            if (dt != null && dt.Rows.Count > 0)
            {
                ImgExportToCSV.Enabled = true;
                ImgExportToExcel.Enabled = true;
                ImgExportToPDF.Enabled = true;
                // Panel1.Enabled = true;
            }
            else
            {

                ImgExportToCSV.Enabled = false;
                ImgExportToExcel.Enabled = false;
                ImgExportToPDF.Enabled = false;
                // Panel1.Enabled = false;
                lblMsg.Text = "No DataFound";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        public DataTable FilterDataForExport(DataTable dt, string searchstring)
        {
            DataTable dtnew = new DataTable();
            DataRow[] foundRows = dt.Select("FirstName LIKE '%" + searchstring + "%' OR LastName LIKE '%" + searchstring + "%' OR EmailId LIKE '%" + searchstring + "%' OR Organization LIKE '%" + searchstring + "%' OR Region LIKE '%" + searchstring + "%'");
            if (foundRows.Count() > 0)
            {
                dtnew = foundRows.CopyToDataTable();

            }
            else
            {
                dtnew = null;
            }
            return dtnew;
        }


        protected void GridUserDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridUserDetails.EditIndex = e.NewEditIndex;
            FillDefaultGridView(false);
        }

        protected void GridUserDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int userId = Convert.ToInt32(GridUserDetails.DataKeys[e.RowIndex].Values["Id"]);
            string result = dataBaseProvider.deleteUser(userId);
            if (result.Equals("SUCCESS"))
            {
                lblMsg.Text = "User Deleted Sucessfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                FillDefaultGridView(true);
            }
            else
            {
                lblMsg.Text = "Some Error Occured";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }


        }

        protected void GridUserDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridUserDetails.EditIndex = -1;
            FillDefaultGridView(false);
        }

        protected void GridUserDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int CreatorId = Convert.ToInt32(Convert.ToInt32(Session["LoggedInuserId"]));
                int ViewUserId = Convert.ToInt32(((HiddenField)GridUserDetails.Rows[e.RowIndex].FindControl("hiddenUserId")).Value.Trim());
                string FirstName = ((TextBox)GridUserDetails.Rows[e.RowIndex].FindControl("tbFirstName")).Text.Trim();
                string LastName = ((TextBox)GridUserDetails.Rows[e.RowIndex].FindControl("tbLastName")).Text.Trim();
                string EmailId = ((TextBox)GridUserDetails.Rows[e.RowIndex].FindControl("tbEmailId")).Text.Trim();
                string Password = ((HiddenField)GridUserDetails.Rows[e.RowIndex].FindControl("hiddenPassword")).Value.Trim();
                string result = dataBaseProvider.updateExisingUserInfoIntoDataBase(ViewUserId, FirstName, LastName, EmailId, 1, Password);
                if (result.Equals("SUCCESS"))
                {
                    lblMsg.Text = "User Update Successfully";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    GridUserDetails.EditIndex = -1;
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
        
        protected void imgBtnDelete_Click(object sender, ImageClickEventArgs e)
        {

            ImageButton img = sender as ImageButton;
            int userId = Convert.ToInt32(img.CommandArgument);
            string result = dataBaseProvider.deleteUser(userId);
            if (result.Equals("SUCCESS"))
            {
                lblMsg.Text = "User Deleted Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                txtSearchBox.Text = "";
                hdnSearchTxt.Value = "";
                GridUserDetails.EditIndex = -1;
                FillDefaultGridView(true);
            }
            else
            {
                lblMsg.Text = "Some Error Occured";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void ImgExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string FileName = "UserList";
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
            try
            {

                string FileName = "UserList";
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
            DataTable dt = (System.Data.DataTable)ViewState["DefaultDataTable"];
            dt.Columns.Remove("id");
            dt.Columns.Remove("creatorId");
            dt.Columns.Remove("type");
            dt.Columns.Remove("profileImage");
            dt.Columns.Remove("password");
            dt.Columns.Remove("isActive");
            dt.Columns.Remove("creationDate");
            dt.Columns.Remove("modificationDate");
            return dt;
        }

        protected void imgBtnUserDetail_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton img = sender as ImageButton;
            int userId = Convert.ToInt32(img.CommandArgument);
            Session["ViewUserId"] = userId;
            Response.Redirect("~/frmDashBoard.aspx");
        }

        protected void ImgAddNewUser_Click1(object sender, EventArgs e)
        {
            Session["ViewUserId"] = 0;
            Response.Redirect("~/frmSettingUser.aspx");
        }

        protected void txtSearchBox_TextChanged(object sender, EventArgs e)
        {
            search();
        }
    }
}