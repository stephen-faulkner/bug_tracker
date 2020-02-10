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
using System.Web.UI;

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
                sbMenu.Append("<div class='d-block main-menu'>");

                foreach(bug_tracker_menu_header menu_header in MenusDB.GetMenu())
                {
                    sbMenu.Append("<div class='main-menu-header'>");

                    if (menu_header.has_submenu == true)
                    {
                        sbMenu.AppendFormat("<a class='d-flex menu-title' data-sub='{0}'>", menu_header.id);
                        sbMenu.Append("<i class='material-icons menu-right'>keyboard_arrow_right</i>");
                        sbMenu.Append("<i class='material-icons menu-down' style='display:none;'>keyboard_arrow_down</i>");
                        sbMenu.AppendFormat("<span>{0}</span>", menu_header.menu_title);
                        sbMenu.Append("</a>");

                        sbMenu.Append(BuildSubMenus(menu_header));
                        sbMenu.Append(BuildMenuPages(menu_header));
                    }
                    else
                        sbMenu.AppendFormat("<a href='{0}' class='d-flex menu-title'><i class='material-icons'>keyboard_arrow_right</i><span>{1}</span></a>", MenusDB.GetMenuHeaderPage(menu_header.id).page_url, menu_header.menu_title);


                    sbMenu.Append("</div>");
                }

                sbMenu.Append("</div>");
            }
            catch(Exception ex)
            {
                LogsDB.AddLog("Error building main side menu", LogsDB.GetLogType("error").id, ex);
            }

            ltlMenu.Text = sbMenu.ToString();
        }

        private string BuildSubMenus(bug_tracker_menu_header menu_item)
        {
            StringBuilder sb = new StringBuilder();

            List<bug_tracker_menu_header> sub_menus = MenusDB.GetSubMenus(menu_item.id);

            if(sub_menus.Count > 0)
            {
                sb.Append("<div class='menu-holder'>");

                foreach (bug_tracker_menu_header sub_menu in MenusDB.GetSubMenus(menu_item.id))
                {
                    sb.Append("<div class='menu-submenu'>");

                    if(sub_menu.has_submenu == true)
                    {
                        sb.AppendFormat("<a class='d-flex menu-title' data-sub='{0}'>", sub_menu.id);
                        sb.Append("<i class='material-icons menu-right'>keyboard_arrow_right</i>");
                        sb.Append("<i class='material-icons menu-down' style='display:none;'>keyboard_arrow_down</i>");
                        sb.AppendFormat("<span>{0}</span>",sub_menu.menu_title);
                        sb.Append("</a>");

                        sb.Append(BuildSubMenus(sub_menu));
                        sb.Append(BuildMenuPages(sub_menu));
                    }
                    else
                        sb.AppendFormat("<div class='d-flex'><a href='{0}' class='d-flex menu-title'><i class='material-icons'>keyboard_arrow_right</i><span>{1}</span></a></div>", MenusDB.GetMenuHeaderPage(sub_menu.id).page_url, sub_menu.menu_title);

                    sb.Append("</div>");
                }

                sb.Append("</div>");
            }

            return sb.ToString();
        }

        private string BuildMenuPages(bug_tracker_menu_header menu_item)
        {
            StringBuilder sb = new StringBuilder();

            List< bug_tracker_page > pages = MenusDB.GetMenuPages(menu_item.id);

            if(pages.Count > 0)
            {
                sb.Append("<div class='menu-holder'>");

                foreach (bug_tracker_page page in MenusDB.GetMenuPages(menu_item.id))
                {
                    sb.Append("<div class='menu-item'>");

                    sb.AppendFormat("<a href='{0}' class='d-flex menu-title'><span>{1}<span></a>", page.page_url, page.name);

                    sb.Append("</div>");
                }

                sb.Append("</div>");
            }

            return sb.ToString();
        }

        public void SetUserSessionVars(MembershipUser user)
        {
            HttpContext.Current.Session["username"] = user.UserName;
            HttpContext.Current.Session["userid"] = user.ProviderUserKey.ToString();
            HttpContext.Current.Session["role"] = Roles.GetRolesForUser(user.UserName)[0];
            SetLoginType(user);
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

        public void SetLoginType(MembershipUser user)
        {
            object ltype;
            using (SqlConnection sqlCon = new SqlConnection(GetConnectionString()))
            {
                sqlCon.Open();
                using(SqlCommand sqlCmd = new SqlCommand("", sqlCon))
                {
                    sqlCmd.CommandText = "SELECT login_type from bug_tracker_roles_id left outer join aspnet_Roles on role_id = RoleId where RoleName = @role";
                    sqlCmd.Parameters.AddWithValue("@role", HttpContext.Current.Session["role"]);

                    ltype = sqlCmd.ExecuteScalar();
                }
                sqlCon.Close();
            }

            HttpContext.Current.Session["login_type"] = ltype;
        }

        public void SendNewUserEmail(string username, string tempPword, string to_email)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(ConfigurationManager.AppSettings["EmailDefaultFrom"]);
            mail.To.Add(to_email);

            mail.Subject = "Bug Tracker User Account Created";

            mail.Body = String.Format("You have had an account created for the bug tracker. Your username is {0}, your password is {1}", username, tempPword);

            SendEmail(mail);
        }
    }
}