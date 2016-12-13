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
    public partial class frmUserMailDetailInfo : System.Web.UI.Page
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
                    if (LoggedInuserId < 1)
                    {
                        Response.Redirect("~/default.aspx");

                    }
                    else
                    {
                       int databaseId=Convert.ToInt32(Session["DatabaseId"]);
                        FillMailDetail(databaseId);

                    }
                    ((Label)(Master).FindControl("lblUserName")).Text = Session["UserName"].ToString();
                }
                catch (Exception exc)
                {
                    ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, exc.Message);
                }
            }
        }

        public void FillMailDetail(int databaseId)
        {
            DataTable dt = new DataTable();
            dt = dataBaseProvider.GetLeadToMailColumnValueByMailId(databaseId);

            if (dt.Rows.Count < 1)
            {
                emptyListMsg.Visible = true;
                GridUserMailDetail.Visible = false;

            }
            else
            {
                emptyListMsg.Visible = false;
                GridUserMailDetail.Visible = true;
                GridUserMailDetail.DataSource = dt;
                GridUserMailDetail.DataBind();
            }
        }

        protected void ImageGoBack3_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/frmViewMailReport.aspx");
        }
    }
}