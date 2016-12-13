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
    public partial class mailChart : System.Web.UI.Page
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
                        FillUserChart(userId);
                    }
                }
                catch (Exception exc)
                {
                    ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, exc.Message);
                }
            }
        }

        public void FillUserChart(int UserId)
        {
            DataTable dt = new DataTable();
            dt = dataBaseProvider.getUserMailChartInfo(UserId);
            ViewState["DefaultApiReportDataTable"] = dt;

            if (dt.Rows.Count < 1)
            {
                //  emptyListMsg.Visible = true;
                chartFunnel.Visible = false;

            }
            else
            {
                // emptyListMsg.Visible = false;
                chartFunnel.Visible = true;
                chartFunnel.DataSource = dt;
                chartFunnel.DataBind();
            }
        }
    }
}