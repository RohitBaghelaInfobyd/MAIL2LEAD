using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using AdminTool.DataBase;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace AdminTool
{
    public partial class frmDashBoard : System.Web.UI.Page
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
                        GetApiStatus(userId);
                        FillTemplateStatusGrid(userId);
                        FillYearDropDown(userId);
                    }
                }
                catch (Exception exc)
                {
                    ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, exc.Message);
                }
            }
        }

        private void FillYearDropDown(int UserId)
        {
            try
            {
                DataTable dt = new DataTable();
                int year = DateTime.Now.Year;
                dt.Columns.Add("Year");
                for (int i = year; i > 2015; i--)
                {
                    dt.Rows.Add(i);
                }
                dropdowyear.DataSource = dt;
                dropdowyear.DataTextField = "Year";
                dropdowyear.DataValueField = "Year";
                dropdowyear.DataBind();
                FillUserYearlyReportData(UserId);
            }
            catch (Exception ex)
            {
            }
        }

        public void FillTemplateStatusGrid(int UserId)
        {
            DataTable dt = new DataTable();
            dt = dataBaseProvider.getUserTemplateStatus(UserId);

            if (dt.Rows.Count < 1)
            {
                lblMsg.Visible = true;
                GridTemplateStatus.Visible = false;
            }
            else
            {
                lblMsg.Visible = false;
                GridTemplateStatus.Visible = true;
                GridTemplateStatus.DataSource = dt;
                GridTemplateStatus.DataBind();
            }
            FileLineChartGraph(UserId);
            FileApiOverViewCountStatus(UserId);
        }

        public void FileLineChartGraph(int UserId)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dataBaseProvider.GetLastOneMonthReport(UserId);

                if (dt.Rows.Count < 1)
                {
                    lblMsg.Visible = true;
                    chartAPIStatusReport.Visible = false;
                }
                else
                {
                    lblMsg.Visible = false;
                    chartAPIStatusReport.Visible = true;

                    chartAPIStatusReport.DataSource = dt;

                    chartAPIStatusReport.ImageStorageMode = ImageStorageMode.UseImageLocation;

                    chartAPIStatusReport.ChartAreas[0].AxisX.Title = "Date";
                    chartAPIStatusReport.ChartAreas[0].AxisX.TitleFont = new Font("Arial", 11, FontStyle.Bold);

                    chartAPIStatusReport.ChartAreas[0].AxisY.Title = "Count";
                    chartAPIStatusReport.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 11, FontStyle.Bold);

                    chartAPIStatusReport.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                    chartAPIStatusReport.ChartAreas[0].BorderWidth = 2;


                    chartAPIStatusReport.Series[0].ChartType = SeriesChartType.Area;
                    chartAPIStatusReport.Series[0].XValueMember = "MailDate";
                    chartAPIStatusReport.Series[0].YValueMembers = "MailCount";

                    chartAPIStatusReport.DataBind();
                }
            }
            catch (Exception ex) { }
        }

        public void FileApiOverViewCountStatus(int UserId)
        {

            try
            {
                DataTable dt = new DataTable();
                dt = dataBaseProvider.GetOverViewOfAllApiCountStatus(UserId);

                if (dt.Rows.Count < 1)
                {
                    lblMsg.Visible = true;
                    chartApiCountOverView.Visible = false;
                }
                else
                {
                    lblMsg.Visible = false;

                    string[] XPointMember = new string[dt.Rows.Count];
                    int[] YPointMember = new int[dt.Rows.Count];

                    for (int count = 0; count < dt.Rows.Count; count++)
                    {
                        //storing Values for X axis  
                        XPointMember[count] = dt.Rows[count]["MailStatus"].ToString();
                        //storing values for Y Axis  
                        YPointMember[count] = Convert.ToInt32(dt.Rows[count]["count"]);

                    }
                    //binding chart control  
                    chartApiCountOverView.Series[0].Points.DataBindXY(XPointMember, YPointMember);

                    //Setting width of line  
                    chartApiCountOverView.Series[0].BorderWidth = 10;
                    //setting Chart type   
                    chartApiCountOverView.Series[0].ChartType = SeriesChartType.Pie;


                    foreach (Series charts in chartApiCountOverView.Series)
                    {
                        foreach (DataPoint point in charts.Points)
                        {
                            switch (point.AxisLabel)
                            {
                                case "ADD": point.Color = Color.Green; break;
                                case "UPDATE": point.Color = Color.Yellow; break;
                                case "ERROR": point.Color = Color.Maroon; break;
                            }
                            point.Label = string.Format("{0:0} - {1}", point.YValues[0], point.AxisLabel);

                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        public void FillUserYearlyReportData(int UserId)
        {
            try
            {
                DataTable dt = new DataTable();
                int year = Convert.ToInt32(dropdowyear.SelectedValue.ToString());
                dt = dataBaseProvider.GetYearlyReportForUser(UserId, year);

                if (dt.Rows.Count < 1)
                {
                    lblMsg.Visible = true;
                    chartApiYearlyStatus.Visible = false;
                }
                else
                {
                    lblMsg.Visible = false;
                    chartApiYearlyStatus.Visible = true;

                    chartApiYearlyStatus.DataSource = dt;

                    chartApiYearlyStatus.ImageStorageMode = ImageStorageMode.UseImageLocation;

                    chartApiYearlyStatus.ChartAreas[0].AxisX.Title = "Month";
                    chartApiYearlyStatus.ChartAreas[0].AxisX.TitleFont = new Font("Arial", 11, FontStyle.Bold);

                    chartApiYearlyStatus.ChartAreas[0].AxisY.Title = "Count";
                    chartApiYearlyStatus.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 11, FontStyle.Bold);

                    chartApiYearlyStatus.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
                    chartApiYearlyStatus.ChartAreas[0].BorderWidth = 2;

                    chartApiYearlyStatus.ChartAreas[0].AxisX.Interval = 1;
                    chartApiYearlyStatus.Series[0].ChartType = SeriesChartType.Column;
                    chartApiYearlyStatus.Series[0].XValueMember = "month";
                    chartApiYearlyStatus.Series[0].YValueMembers = "count";

                    chartApiYearlyStatus.DataBind();
                }
            }
            catch (Exception ex) { }
        }
        public void GetApiStatus(int UserId)
        {
            DataTable dt = new DataTable();
            dt = dataBaseProvider.getApiStatusReport(UserId);

            if (dt.Rows.Count < 1)
            {
                lblMsg.Visible = true;
                GridUserApiDetails.Visible = false;

            }
            else
            {
                lblMsg.Visible = false;
                GridUserApiDetails.Visible = true;
                GridUserApiDetails.DataSource = dt;
                GridUserApiDetails.DataBind();
            }
        }

        protected void imgBtnUserpayment_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmPayment.aspx");
        }

        protected void ImgReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmViewMailReport.aspx");

        }

        protected void ImgUpdateInfoNewUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmUserDetailViewScreen.aspx");
        }

        protected void dropdowyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["ViewUserId"]);
            FillUserYearlyReportData(userId);
            FileApiOverViewCountStatus(userId);
            FileLineChartGraph(userId);
        }
    }
}