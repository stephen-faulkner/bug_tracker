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
    public partial class master_plain : System.Web.UI.MasterPage
    {
        private WebFunctions wFunctions = new WebFunctions();
        
        protected void Page_Init(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if(Session["username"] == null)
            {
                sb.Append("<a href='/index.aspx'>Log In</a>");
            }
            else
            {
                MembershipUser user = Membership.GetUser(Session["username"].ToString());
                sb.AppendFormat(wFunctions.BuildSignedInInfo());
            }

            lblSignIn.Text = sb.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}