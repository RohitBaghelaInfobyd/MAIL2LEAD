using AdminTool.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminTool
{
    public partial class frmForceSync : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int LoggedInuserId, UserId;
                    LoggedInuserId = Convert.ToInt32(Session["LoggedInuserId"]);
                    UserId = Convert.ToInt32(Session["ViewUserId"]);
                    if (LoggedInuserId < 1)
                    {
                        Response.Redirect("~/default.aspx");
                    }
                    else
                    {
                        calStartDate.SelectedDate = DateTime.Now.AddDays(-10);
                        calEndDate.SelectedDate = DateTime.Now;
                    }
                }
                catch (Exception ex)
                { ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message); }
            }
        }

        protected void btnForceSync_Click(object sender, EventArgs e)
        {
            try
            {
                int ViewUserId = Convert.ToInt32(Session["ViewUserId"]);
                string date= tbStartDate.Text;
                DateTime startDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                date = tbEndDate.Text;
                DateTime endDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (startDate > endDate)
                {
                    lblMsg.Text = "Start Date should be less than End date";
                    lblMsg.Visible = true;
                    return;
                }

                int numberOfDays = 1;
                try
                {
                    numberOfDays = Convert.ToInt32((endDate - startDate).TotalDays);
                }
                catch (Exception ex)
                { }
                if (numberOfDays > 0)
                {
                    if (ViewUserId > 0)
                    {
                        /* Format  
                         * SendEmailStarted(int UserId, int numberOfDays, int IsApprovedOrAll, int ViewSubjectId)
                         * 
                         * UserId = Loged in user Id
                         * numberOfDays =how many days old mail to check
                         * ViewSubjectId= Id of particular subject which user want to sync
                         * IsApprovedOrAll = value may either 0 or 1, 
                         *                  CASE 0 sync all the mail no matter approve or not
                         *                  CASE 1 sync only approved mail 
                         * 
                         */

                        MainTimeTicker.SubmitEmailFromMailToCRM(ViewUserId, numberOfDays, 1, 0, "ForceSync");
                        lblMsg.Text = "Force Sync Completed";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                    }
                }
                else
                {
                    lblMsg.Text = "Start Date Should be greater than current date";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Some Error Occured";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            lblMsg.Visible = true;
        }
    }
}