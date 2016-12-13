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
namespace AdminTool
{
    public partial class frmApiReport : System.Web.UI.Page
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

                    }
                    int UserType = Convert.ToInt32(Session["UserType"].ToString());
                    if (UserType == 1)
                    {
                        // Response.Redirect("~/frmCategory.aspx");
                        ImageGoBack2.Visible = false;

                    }
                    else
                    {
                        //Response.Redirect("~/frmUserList.aspx");
                        ImageGoBack2.Visible = true;
                    }

                    ((Label)(Master).FindControl("lblUserName")).Text = Session["UserName"].ToString();
                }
                catch (Exception exc)
                {
                    ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, exc.Message);
                }
            }

        }

        public void GetApiStatus(int UserId)
        {
            DataTable dt = new DataTable();
            dt = dataBaseProvider.getApiStatusReport(UserId);
            ViewState["DefaultApiReportDataTable"] = dt;

            if (dt.Rows.Count < 1)
            {
                emptyListMsg.Visible = true;
                GridUserApiDetails.Visible = false;

            }
            else
            {
                emptyListMsg.Visible = false;
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

        protected void ImageGoBack2_Click(object sender, ImageClickEventArgs e)
        {


            int UserType = Convert.ToInt32(Session["UserType"].ToString());
            if (UserType == 1)
            {
                // Response.Redirect("~/frmCategory.aspx");
               
            }
            else
            {
                Response.Redirect("~/frmUserList.aspx");
            }

        }
    }
}