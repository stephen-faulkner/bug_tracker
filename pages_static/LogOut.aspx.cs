using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bug_tracker.pages_static
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();
            HttpCookie user_data = Request.Cookies["user_data"];
            if (user_data != null)
            {
                user_data.Expires = DateTime.Today.AddDays(-1);
                Response.Cookies.Add(user_data);
            }
            Response.Redirect("/index.aspx");
        }
    }
}