using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdminTool.DataBase;
using System.Data;

namespace AdminTool
{
    public partial class frmCategory : System.Web.UI.Page
    {
        static DataBaseProvider dataBaseProvider = new DataBaseProvider();
        static int UserId, UserType;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    UserId = Convert.ToInt32(Session["LoggedInuserId"]);
                    if (UserId > 0)
                    {
                        DataTable UserInfo = dataBaseProvider.getUserBasicInfo(UserId);
                        if (UserInfo.Rows.Count > 0)
                        {
                            UserType = Convert.ToInt32(UserInfo.Rows[0]["type"].ToString());
                            Session["UserType"] = UserType;
                            Session["UserName"] = UserInfo.Rows[0]["firstName"].ToString();
                        }
                        else
                        {
                            Response.Redirect("~/default.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("~/default.aspx");
                    }
                    ((Label)(Master).FindControl("lblUserName")).Text = Session["UserName"].ToString();
                    redirect();
                }
                catch (Exception ex)
                { }

            }
        }

        private void redirect()
        {
            Session["CategoryType"] = 1;
            if (UserType < 2)
            {
                Response.Redirect("~/frmApiReport.aspx");
            }
            else
            {
                Response.Redirect("~/frmUserList.aspx");
            }
        }

        protected void ImgButtonIAssist_Click(object sender, EventArgs e)
        {
            try
            {
                Session["CategoryType"] = 1;
                if (UserType < 2)
                {
                    Response.Redirect("~/frmApiReport.aspx");
                }
                else
                {
                    Response.Redirect("~/frmUserList.aspx");
                }
            }
            catch (Exception ex)
            { }
        }

        protected void ImgButtonITest_Click(object sender, EventArgs e)
        {
            try
            {
                Session["CategoryType"] = 2;
                if (UserType < 2)
                {
                    Response.Redirect("~/frmUserDetailViewScreenSMS.aspx");
                }
                else
                {
                    //Response.Redirect("~/frmUserDetailViewScreenSMS.aspx");
                    Response.Redirect("~/frmUserList.aspx");
                }
            }
            catch (Exception ex)
            { }
        }


    }
}