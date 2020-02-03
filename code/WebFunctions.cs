using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace bug_tracker.code
{
    public class WebFunctions
    {
        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
        }

        public bool SendEmail(MailMessage mail)
        {
            bool sent = false;

            try
            {
                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["OutlookServer"], Convert.ToInt32(ConfigurationManager.AppSettings["OutlookPort"]));
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailDefaultFrom"], ConfigurationManager.AppSettings["EmailPword"]);

                client.Send(mail);
            }
            catch(Exception ex)
            {
                LogsDB.AddLog(string.Format("Error sending email to {0}", string.Join(",", mail.To.ToList())), LogsDB.GetLogType("error").id, ex);
            }
            return sent;
        }

        public string BuildSignedInInfo()
        {
            StringBuilder sb = new StringBuilder();

            MembershipUser user = Membership.GetUser(HttpContext.Current.Session["username"].ToString());
            sb.AppendFormat("<div class='d-flex mr-15-px'><div class='border-right-solid pr-15-px mr-15-px'>Logged in as <a href=''>{0}</a></div><a href='/pages_static/LogOut.aspx'>Log Out</a></div>", user.UserName);

            return sb.ToString();
        }

        public void SendToHomePage()
        {
            HttpContext.Current.Response.Redirect("/pages_dynamic/home");
        }

        public void BuildMenu(Literal ltlMenu)
        {
            StringBuilder sbMenu = new StringBuilder();

            try
            {
                sbMenu.Append("<div class='d-block'>");

                foreach(bug_tracker_menu_header menu_header in MenusDB.GetMenu())
                {
                    sbMenu.AppendFormat("<div>{0}</div>", menu_header.menu_title);
                    sbMenu.Append(BuildSubMenus(menu_header));
                    sbMenu.Append(BuildMenuPages(menu_header));
                }

                sbMenu.Append("</div>");
            }
            catch(Exception ex)
            {

            }

            ltlMenu.Text = sbMenu.ToString();
        }

        private string BuildSubMenus(bug_tracker_menu_header menu_item)
        {
            StringBuilder sb = new StringBuilder();

            foreach(bug_tracker_menu_header sub_menu in MenusDB.GetSubMenus(menu_item.id))
            {
                sb.Append("<div class='d-block'>");

                sb.AppendFormat("<span>{0}</span>", sub_menu.menu_title);

                sb.Append(BuildSubMenus(sub_menu));
                sb.Append(BuildMenuPages(sub_menu));

                sb.Append("</div>");
            }

            return sb.ToString();
        }

        private string BuildMenuPages(bug_tracker_menu_header menu_item)
        {
            StringBuilder sb = new StringBuilder();

            foreach(bug_tracker_page page in MenusDB.GetMenuPages(menu_item.id))
            {
                sb.Append("<div class='d-block'>");

                sb.AppendFormat("<span>{0}</span>", page.name);

                sb.Append("</div>");
            }

            return sb.ToString();
        }

        public void SetUserSessionVars(MembershipUser user)
        {
            HttpContext.Current.Session["username"] = user.UserName;
            HttpContext.Current.Session["userid"] = user.ProviderUserKey.ToString();
            HttpContext.Current.Session["role"] = Roles.GetRolesForUser(user.UserName)[0];            
        }

        public void SetUserCookie(MembershipUser user)
        {
            HttpCookie user_data = new HttpCookie("user_data");
            user_data["username"] = user.UserName;
            user_data["userid"] = user.ProviderUserKey.ToString();
            user_data.Expires = DateTime.Today.AddMonths(1);

            HttpContext.Current.Response.Cookies.Add(user_data);
        }

        public void LogInUserFromCookie()
        {
            HttpCookie user_data = HttpContext.Current.Request.Cookies["user_data"];

            if(user_data != null)
            {
                SetUserSessionVars(Membership.GetUser(user_data["username"]));
            }
        }
    }
}