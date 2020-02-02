using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Security;
using bug_tracker.code;

namespace bug_tracker.masters
{
    public partial class master_main : System.Web.UI.MasterPage
    {
        private WebFunctions wFunctions = new WebFunctions();

        protected void Page_Load(object sender, EventArgs e)
        {
            wFunctions.LogInUserFromCookie();
            if (Session["username"] == null)
                Response.Redirect("/pages_static/logout");
            else
                lblSignIn.Text = wFunctions.BuildSignedInInfo();

            if (!IsPostBack)
            {
                wFunctions.BuildMenu(ltlMenu);
                
            }
        }
    }
}