using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bug_tracker.code;
using System.Web.Security;

namespace bug_tracker.pages_static
{
    public partial class change_password : System.Web.UI.Page
    {
        WebFunctions wFunctions = new WebFunctions();
        MembershipUser user;
        const string error_message = "error-message", success_message = "success-message", txt_class = "form-control", txt_error = "error-input";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtCurrentPword.Focus();
            }

        }

        protected void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            try
            {
                user = Membership.GetUser("Stephen Faulkner");
                
                if (user.ChangePassword(txtCurrentPword.Text, txtNewPword.Text))
                {
                    txtCurrentPword.CssClass = txt_class;
                    txtNewPword.CssClass = txt_class;
                    ltlMessage.Text = String.Format("<span class='{0}'>Password successfully changed.</span>", success_message);
                }
                else
                {
                    ltlMessage.Text = String.Format("<span class='{0}'>Password not recognized. Confirm entered passwords and try again.</span>", error_message);
                    txtCurrentPword.CssClass = String.Format("{0} {1}", txt_class, txt_error);
                    txtNewPword.CssClass = String.Format("{0} {1}", txt_class, txt_error);
                }
            }
            catch(Exception ex)
            {
                ltlMessage.Text = String.Format("<span class='{0}'>Unexpected error changing password. Refresh page and try again.</span>", error_message);
            }
        }
    }
}