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
    public partial class frmPaymentHistory : System.Web.UI.Page
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
                        getPaymentHistory(userId);
                    }
                    ((Label)(Master).FindControl("lblUserName")).Text = Session["UserName"].ToString();
                }
                catch (Exception exc)
                {
                    ExceptionAndErrorClass.StoretheErrorLog(System.Reflection.MethodBase.GetCurrentMethod().Name, exc.Message);
                }
            }


        }

        public void getPaymentHistory(int UserId)
        {
            DataTable dt = new DataTable();
            dt = dataBaseProvider.getUserPaymentHistory(UserId);
            ViewState["DefaultUserPaymentDetailDataTable"] = dt;
            if (dt.Rows.Count < 1)
            {
                lblMsg.Text = "No Payment Detail Available";
                lblMsg.Visible = true;
                GridUserPaymentDetails.Visible = false;
            }
            else
            {
                lblMsg.Visible = false;
                GridUserPaymentDetails.Visible = true;
                GridUserPaymentDetails.DataSource = dt;
                GridUserPaymentDetails.DataBind();
            }
        }

        protected void ImageGoBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmPayment.aspx");
        }


        protected void GridUserPaymentDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridUserPaymentDetails.EditIndex = e.NewEditIndex;
            int userId = Convert.ToInt32(Session["ViewUserId"]);
            getPaymentHistory(userId);
        }

        protected void GridUserPaymentDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridUserPaymentDetails.EditIndex = -1;
            int userId = Convert.ToInt32(Session["ViewUserId"]);
            getPaymentHistory(userId);
        }

        protected void GridUserPaymentDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            int PaymentID = Convert.ToInt32(((HiddenField)GridUserPaymentDetails.Rows[e.RowIndex].FindControl("hiddenPaymentId")).Value.Trim());
            Boolean IsApproved = ((CheckBox)GridUserPaymentDetails.Rows[e.RowIndex].FindControl("chkIsApproved")).Checked;
            int PaymentStatus = (IsApproved) ? 1 : 0;
            string result = dataBaseProvider.updateUserPaymentStatus(PaymentID, PaymentStatus);
            if (result.Equals("SUCCESS"))
            {
                int userId = Convert.ToInt32(Session["ViewUserId"]);
                lblMsg.Text = "Payment Update Successfully";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                GridUserPaymentDetails.EditIndex = -1;
                getPaymentHistory(userId);
            }
            else
            {
                lblMsg.Text = "Some Error Occured";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }
            lblMsg.Visible = true;
        }

        protected void GridUserPaymentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int UserType = Convert.ToInt32(Session["UserType"].ToString());
            if (UserType <= 1)
            {
                e.Row.Cells[7].Visible = false;
            }
        }

        protected void ImageGoBack3_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/frmPayment.aspx");
        }
    }
}