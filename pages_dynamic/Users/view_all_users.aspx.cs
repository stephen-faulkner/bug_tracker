using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using bug_tracker.code;
using System.Web.Security;
using System.Web;
using System.Web.Services;

namespace bug_tracker.pages_dynamic.Users
{
    public partial class view_all_users : System.Web.UI.Page
    {
        private WebFunctions wFunctions = new WebFunctions();
        private string connStr { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            connStr = wFunctions.GetConnectionString();

            if(!IsPostBack)
            {
                LoadAllUsers();
            }
        }

        private void LoadAllUsers()
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.Append("<table id='tblUsers' class='display'>");
                sb.Append("<thead>");
                sb.Append("<th><strong>Username</strong></th>");
                sb.Append("<th><strong>Email</strong></th>");
                sb.Append("<th><strong>Created</strong></th>");
                sb.Append("<th><strong>Status</strong></th>");
                sb.Append("<th><strong>Last Login</strong></th>");
                sb.Append("<th><strong>Unlock</strong></th>");
                sb.Append("<th><strong>Password</strong></th>");
                sb.Append("</thead>");

                MembershipUserCollection users = Membership.GetAllUsers();

                sb.Append("<tbody>");
                foreach(MembershipUser user in users)
                {
                    sb.Append("<tr>");

                    sb.AppendFormat("<td>{0}</td>", user.UserName);
                    sb.AppendFormat("<td>{0}</td>", user.Email);
                    sb.AppendFormat("<td>{0:dd/MM/yyyy}</td>", user.CreationDate);
                    sb.AppendFormat("<td>{0}</td>", user.IsLockedOut == false ? "Open" : "Locked");
                    sb.AppendFormat("<td>{0: dd/MM/yyyy HH:mm}</td>", user.LastLoginDate);
                    sb.AppendFormat("<td>{0}</td>", String.Format(user.IsLockedOut == false ? "" : "<input type='button' value='Unlock' onclick='return false;' data-userid='{0}' class='UnlockUser'/>", user.UserName));
                    sb.AppendFormat("<td><input type='button' onclick='return false' value='Reset' data-userid='{0}' class='ResetUserPassowrd'/></td>", user.UserName);

                    sb.Append("</tr>");
                }
                sb.Append("</tbody>");

                sb.Append("</table>");
            }
            catch(Exception ex)
            {
                sb.Append(ex.ToString());
            }

            ltlUsers.Text = sb.ToString();
        }

        [WebMethod]
        public static bool UnlockAccount(string user_id)
        {
            bool unlocked = false;

            try
            {
                MembershipUser user = Membership.GetUser(user_id);
                user.UnlockUser();

                unlocked = true;
            }
            catch(Exception ex)
            {
                LogsDB.AddLog(String.Format("Error unlocking account for user id {0}", user_id), LogsDB.GetLogType("error").id, ex);
            }

            return unlocked;
        }

        [WebMethod]
        public static bool ResetPassword(string user_id)
        {
            bool password_reset = false;
            WebFunctions wf = new WebFunctions();
            try
            {
                MembershipUser user = Membership.GetUser(user_id);

                /*NOT AVAILABLE FOR DEMO*/
                /*UNCOMMENT WHEN EMAIL SETTINGS ARE READY*/
                //wf.SendNewUserEmail(user.UserName, Membership.GeneratePassword(8, 2), user.Email);

                password_reset = true;
            }
            catch (Exception ex)
            {
                LogsDB.AddLog(String.Format("Error resetting password for user id {0}", user_id), LogsDB.GetLogType("error").id, ex);
            }

            return password_reset;
        }
    }
}