using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;
using System.Web.Security;
using System.Drawing;
using bug_tracker.code;
using System.Text;

namespace bug_tracker
{
    public partial class index : System.Web.UI.Page
    {
        private WebFunctions wFunctions = new WebFunctions();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] != null)
                {
                    MembershipUser user = Membership.GetUser(Session["username"].ToString());
                    wFunctions.SendToHomePage();
                }                
            }
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string pword = txtPWord.Text;

            bool success = Membership.ValidateUser(username, pword);            

            if (!success)
            {
                lblMessage.BackColor = Color.Red;
                lblMessage.Text = "Username and/or Password not found.";
            }
            else
            {
                MembershipUser user = Membership.GetUser(username);

                wFunctions.SetUserSessionVars(user);
                if (chkRememberMe.Checked)
                    wFunctions.SetUserCookie(user);

                Response.Redirect("/pages_dynamic/home.aspx");
            }
        }

        protected void Default_Login(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            MembershipUser user = null;

            switch (btn.ID)
            {
                case "btnAdminLogin":
                    user = Membership.GetUser("Admin Test");
                    break;
                case "btnProjectLogin":
                    user = Membership.GetUser("Project Manager");
                    break;
                case "btnDevLogin":
                    user = Membership.GetUser("Developer Test");
                    break;
                case "btnUser":
                    user = Membership.GetUser("User test");
                    break;
            }

            wFunctions.SetUserSessionVars(user);
            Response.Redirect("/pages_dynamic/home.aspx");
        }
    }
}