using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bug_tracker.code;
using System.Web.Security;
using System.Text;

namespace bug_tracker.pages_dynamic.Projects
{
    public partial class projects_edit : System.Web.UI.Page
    {
        public string strTitle { get; set; }
        private Int64 project_id { get; set; }
        private project this_project = null;
        private Int16 login_type { get; set; } 
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

                strTitle = this_project.name;
                LoadProjectDetails();
            }
        }

        private void LoadProjectDetails()
        {
            ltlTitle.Text = this_project.name;
            ltlDescription.Text = this_project.description;

            if(login_type < 3)
            {
                pnlDevs.Visible = false;
                pnlUsers.Visible = false;
            }
            else
            {
                LoadDevs();
                LoadUsers();
                LoadTickets();
            }
        }

        private void LoadDevs()
        {
            //get total amount of developers assigned to project
            List<MembershipUser> devs = ProjectsDB.GetProjectDevs(this_project);
            ltlDevCount.Text = devs.Count.ToString();

            StringBuilder sb = new StringBuilder();
            sb.Append("<table id='tblDevelopers' class='display'>");

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
                sb.AppendFormat("<td><input type='button' value='X' data-project='{0}' data-user='{1}' class='RemoveUserFromProject'/>", this_project.id, dev.ProviderUserKey);

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
            sb.Append("<table id='tblUsers' class='display'>");

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
                sb.AppendFormat("<td><input type='button' value='X' data-project='{0}' data-user='{1}' class='RemoveUserFromProject'/>", this_project.id, user.ProviderUserKey);

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
    }
}