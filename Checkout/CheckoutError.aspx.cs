using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdminTool.DataBase;

namespace AdminTool.Checkout
{
    public partial class CheckoutError : System.Web.UI.Page
    {
        static DataBaseProvider dataBaseProvider = new DataBaseProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //  int userId=Convert.ToInt32(Session["ViewUserId"].ToString());
                //  dataBaseProvider.UpdatePaymentInfoByUserId(userId, "", "Failed");
            }

        }
    }
}