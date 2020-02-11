using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bug_tracker.code;
using System.Web.Security;
using System.Text;
using System.Web.Services;

namespace bug_tracker.pages_dynamic.Projects
{
    public partial class projects_edit : System.Web.UI.Page
    {
        public string strTitle { get; set; }
        
        private project this_project = null;
        private Int16 login_type { get; set; }
        public static Int64 project_id {get;set;}
        protected void Page_Load(object sender, EventArgs e)
        {
            login_type = Convert.ToInt16(Session["login_type"]);

            if (login_type == 0)
                Response.Redirect("/index");

            if (String.IsNullOrEmpty(Request.QueryString["id"]))
                Response.Redirect("/index");
            else
            {
                this_project = ProjectsDB.GetProject(Convert.ToInt32(Request.QueryString["id"]));

                if(this_project.id == 0)
                    Response.Redirect("/index");

                project_id = this_project.id;
                strTitle = this_project.name;
                LoadProjectDetails();
            }
        }

        private void LoadProjectDetails()
        {
            ltlTitle.Text = this_project.name;
            lblProjectDescr.Text = this_project.description;

            if(login_type < 3)
            {
                pnlDevs.Visible = false;
                pnlUsers.Visible = false;
            }
            else
            {
                btnEditProjectDescr.Visible = true;
                dEditDescr.Visible = true;

                txtProjectDescr.Text = this_project.description;

                LoadDevs();
                LoadUsers();
                LoadTickets();

                LoadDevsNotOnProject();
                LoadUsersNotOnProject();
            }
        }

        private void LoadDevs()
        {
            //get total amount of developers assigned to project
            List<MembershipUser> devs = ProjectsDB.GetProjectDevs(this_project);
            ltlDevCount.Text = devs.Count.ToString();

            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDevelopers' class='display project-users'>");

            sb.Append("<thead>");
            sb.Append("<th><strong>Name</strong></th>");
            sb.Append("<th class='text-center'><strong>Total Tickets</strong></th>");
            sb.Append("<th><strong>Remove</strong></th>");
            sb.Append("</thead>");

            sb.Append("<tbody>");

            foreach (MembershipUser dev in devs)
            {
                sb.Append("<tr>");

                sb.AppendFormat("<td>{0}</td>", dev.UserName);
                sb.AppendFormat("<td>{0}</td>", TicketsDB.GetDevProjectTicketCount(this_project, dev));
                sb.AppendFormat("<td><input type='button' value='X' data-user='{0}' class='RemoveUserFromProject' onclick='RemoveUserFromProject(this);'/>", dev.ProviderUserKey);

                sb.Append("</tr>");
            }

            sb.Append("</tbody>");
            sb.Append("</table>");

            ltlDevs.Text = sb.ToString();
        }

        private void LoadUsers()
        {
            //get total amount of developers assigned to project
            List<MembershipUser> users = ProjectsDB.GetProjectUsers(this_project);
            ltlUserCount.Text = users.Count.ToString();

            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblUsers' class='display project-users'>");

            sb.Append("<thead>");
            sb.Append("<th><strong>Name</strong></th>");
            sb.Append("<th class='text-center'><strong>Total Tickets</strong></th>");
            sb.Append("<th><strong>Remove</strong></th>");
            sb.Append("</thead>");

            sb.Append("<tbody>");

            foreach (MembershipUser user in users)
            {
                sb.Append("<tr>");

                sb.AppendFormat("<td>{0}</td>", user.UserName);
                sb.AppendFormat("<td>{0}</td>", TicketsDB.GetUserProjectTicketCount(this_project, user));
                sb.AppendFormat("<td><input type='button' value='X' data-user='{0}' class='RemoveUserFromProject' onclick='RemoveUserFromProject(this);'/>", user.ProviderUserKey);

                sb.Append("</tr>");
            }

            sb.Append("</tbody>");
            sb.Append("</table>");

            ltlUsers.Text = sb.ToString();
        }

        private void LoadTickets()
        {
            List<project_detail> tickets = ProjectsDB.GetProjectTickets(this_project);
            ltlTicketCount.Text = tickets.Count().ToString();

            StringBuilder sb = new StringBuilder();

            sb.Append("<table id='tblTickets' class='display'>");

            sb.Append("<thead>");
            sb.Append("<th><strong>ID</strong></th>");
            sb.Append("<th><strong>Title</strong></th>");
            sb.Append("<th class='text-center'><strong>Created By</strong></th>");
            sb.Append("<th class='text-center'><strong>Created Date</strong></th>");
            sb.Append("<th><strong>Status</strong></th>");
            sb.Append("<th><strong>Priority</strong></th>");
            sb.Append("<th><strong>Assigned To</strong></th>");
            sb.Append("</thead>");

            sb.Append("<tbody>");

            foreach(project_detail ticket in tickets)
            {
                sb.Append("<tr>");

                sb.AppendFormat("<td>{0}</td>", TicketsDB.GenerateTicketNumber(TicketsDB.GetTicketHeader(ticket.ticket_id)));
                sb.AppendFormat("<td>{0}</td>", ticket.title);
                sb.AppendFormat("<td>{0}</td>", ticket.created_by);
                sb.AppendFormat("<td>{0: dd/MM/yyyy}</td>", ticket.created_date);
                sb.AppendFormat("<td>{0}</td>", ticket.status);
                sb.AppendFormat("<td>{0}</td>", ticket.priority);
                sb.AppendFormat("<td>{0}</td>", ticket.dev);

                sb.Append("</tr>");
            }

            sb.Append("</tbody>");
            sb.Append("</table>");

            ltlTicketsOpen.Text = sb.ToString();
        }

        private void LoadDevsNotOnProject()
        {
            MembershipUserCollection devs = UsersDB.GetUsersToAddToProject(project_id, "developer");

            ListItem item;
            foreach(MembershipUser dev in devs)
            {
                item = new ListItem()
                {
                    Text = dev.UserName,
                    Value = dev.ProviderUserKey.ToString()
                };

                ddlAddProjectDeveloper.Items.Add(item);
            }
        }

        private void LoadUsersNotOnProject()
        {
            MembershipUserCollection devs = UsersDB.GetUsersToAddToProject(project_id, "user");

            ListItem item;
            foreach (MembershipUser dev in devs)
            {
                item = new ListItem()
                {
                    Text = dev.UserName,
                    Value = dev.ProviderUserKey.ToString()
                };

                ddlAddProjectUser.Items.Add(item);
            }
        }

        [WebMethod]
        public static bool SaveProjectDescr(string project_descr)
        {
            bool updated = false;
            object obj = HttpContext.Current.Request.QueryString["id"];
            
            try
            {
                project this_project = ProjectsDB.GetProject(project_id);
                this_project.description = project_descr;

                this_project = ProjectsDB.AddEditProject(this_project);

                if (this_project != null)
                    updated = true;
            }
            catch(Exception ex)
            {
                LogsDB.AddLog("Error updating project description", LogsDB.GetLogType("error").id, project_id, ex);
            }

            return updated;
        }

        [WebMethod]
        public static bool AddUserToProject(string user_id)
        {
            bool added = false;

            try
            {
                ProjectsDB.AddUserToProject(project_id, user_id);
                added = true;
            }
            catch(Exception ex)
            {
                LogsDB.AddLog(String.Format("Project - Add User. Error adding user {0} to project id {1}", user_id, project_id), LogsDB.GetLogType("error").id, project_id, ex);
            }

            return added;
        }

        [WebMethod]
        public static bool RemoveUserFromProject(string user_id)
        {
            bool removed = false;            

            try
            {
                ProjectsDB.RemoveUserFromProject(project_id, user_id);
                removed = true;
            }
            catch(Exception ex)
            {
                LogsDB.AddLog(String.Format("Project - Remove user. Error removing user {0} from project {1}", user_id, project_id), LogsDB.GetLogType("error").id, project_id, ex);
            }

            return removed;
        }

        [WebMethod]
        public static object[] LoadProjectDevelopers()
        {
            StringBuilder sb = new StringBuilder();
            Int16 devCount = 0;
            //get total amount of developers assigned to project
            List<MembershipUser> devs = ProjectsDB.GetProjectDevs(ProjectsDB.GetProject(project_id));
            List<ProjectUser> all_devs = new List<ProjectUser>();
            try
            {
                project this_project = ProjectsDB.GetProject(project_id);
                devCount = (Int16)devs.Count;

                foreach (MembershipUser dev in devs)
                {
                    ProjectUser new_dev = new ProjectUser()
                    {
                        Username = dev.UserName,
                        User_id = dev.ProviderUserKey.ToString(),
                        Ticket_count = TicketsDB.GetDevProjectTicketCount(this_project, dev),
                        Remove_button = String.Format("<input type='button' value='X' data-user='{0}' class='RemoveUserFromProject' onclick='RemoveUserFromProject(this);'/>", dev.ProviderUserKey)
                    };

                    all_devs.Add(new_dev);
                }
            }
            catch(Exception ex)
            {
                LogsDB.AddLog("Error building project developers table", LogsDB.GetLogType("error").id, project_id, ex);
            }

            return new object[] { all_devs, devCount };
        }

        [WebMethod]
        public static object[] LoadProjectUsers()
        {
            StringBuilder sb = new StringBuilder();
            Int16 devCount = 0;
            //get total amount of developers assigned to project
            List<MembershipUser> users = ProjectsDB.GetProjectUsers(ProjectsDB.GetProject(project_id));
            List<ProjectUser> all_users = new List<ProjectUser>();
            try
            {
                project this_project = ProjectsDB.GetProject(project_id);
                devCount = (Int16)users.Count;

                foreach (MembershipUser user in users)
                {
                    ProjectUser new_user = new ProjectUser()
                    {
                        Username = user.UserName,
                        User_id = user.ProviderUserKey.ToString(),
                        Ticket_count = TicketsDB.GetUserProjectTicketCount(this_project, user),
                        Remove_button = String.Format("<input type='button' value='X' data-user='{0}' class='RemoveUserFromProject' onclick='RemoveUserFromProject(this);'/>", user.ProviderUserKey)
                    };

                    all_users.Add(new_user);
                }
            }
            catch (Exception ex)
            {
                LogsDB.AddLog("Error building project developers table", LogsDB.GetLogType("error").id, project_id, ex);
            }

            return new object[] { all_users, devCount };
        }

        private class ProjectUser
        {
            public string Username { get; set; }
            public string User_id { get; set; }
            public Int32 Ticket_count { get; set; }
            public string Remove_button { get; set; }
        }

    }
}