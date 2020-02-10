using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using bug_tracker.code;

namespace bug_tracker.pages_dynamic.Projects
{
    public partial class projects_all : System.Web.UI.Page
    {
        private StringBuilder sbOpenProjects = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Int16 login_type = Convert.ToInt16(Session["login_type"]);

                sbOpenProjects.Append("<table id='tblProjects_Open' class='display'>");

                switch (login_type)
                {
                    case 4:
                        sbOpenProjects.Append(LoadAdminOpenProjects());
                        break;
                    default:
                        sbOpenProjects.Append(LoadOpenProjectsByUser());
                        break;
                }

                sbOpenProjects.Append("</table>");

                ltlProjects.Text = sbOpenProjects.ToString();
            }
        }

        private string LoadAdminOpenProjects()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<thead>");
            sb.Append("<th><strong>Name</strong></th>");
            sb.Append("<th><strong>Created</strong></th>");
            sb.Append("<th class='text-center'><strong>Total Tickets</strong></th>");
            sb.Append("<th class='text-center'><strong>Open Tickets</strong></th>");
            sb.Append("<th class='text-center'><strong>Managers</strong></th>");
            sb.Append("<th class='text-center'><strong>Devs</strong></th>");
            sb.Append("<th class='text-center'><strong>Users</strong></th>");
            sb.Append("</thead>");

            sb.Append("<tbody>");

            foreach(projects_tickets_detail project in ProjectsDB.GetActiveProjectsDetails())
            {
                sb.Append("<tr>");
                sb.AppendFormat("<td><a href='/pages_dynamic/projects/projects_edit.aspx?id={1}'>{0}</a></td>", project.name, project.project_id);
                sb.AppendFormat("<td>{0:dd/MM/yyyy}</td>", project.created_date);
                sb.AppendFormat("<td class='text-center'>{0}</td>", project.Total_Tickets);
                sb.AppendFormat("<td class='text-center'>{0}</td>", project.Open_Tickets);
                sb.AppendFormat("<td class='text-center'>{0}</td>", project.Managers);
                sb.AppendFormat("<td class='text-center'>{0}</td>", project.Devs);
                sb.AppendFormat("<td class='text-center'>{0}</td>", project.Users);
                sb.Append("</tr>");
            }

            sb.Append("</tbody>");


            return sb.ToString();
        }

        private string LoadOpenProjectsByUser()
        {
            StringBuilder sb = new StringBuilder();


            return sb.ToString();
        }
    }
}