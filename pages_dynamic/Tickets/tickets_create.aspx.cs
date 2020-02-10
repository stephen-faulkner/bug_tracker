using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bug_tracker.code;

namespace bug_tracker.pages_dynamic.Tickets
{
    public partial class tickets_create : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProjects();
                LoadTicketPriority();
            }
        }

        private void LoadProjects()
        {
            List<project> projects = ProjectsDB.GetAllProjects();
            ListItem item;

            foreach(project project in projects)
            {
                item = new ListItem()
                {
                    Text = project.name,
                    Value = project.id.ToString()
                };

                ddlProjects.Items.Add(item);
            }
        }

        private void LoadTicketPriority()
        {
            List<ticket_priority> ticket_priorities = TicketsDB.GetTicket_Priorities();
            ListItem item;

            foreach(ticket_priority priority in ticket_priorities)
            {
                item = new ListItem()
                {
                    Text = priority.description,
                    Value = priority.id.ToString()
                };

                ddlTicketPriority.Items.Add(item);
            }
        }

        protected void btnCreateTicket_Click(object sender, EventArgs e)
        {
            ticket new_ticket = new ticket();
            string ticket_message = null;
            try
            {
                new_ticket.created_by = new Guid(Session["userid"].ToString().Trim());
                new_ticket.created_date = DateTime.Now;
                new_ticket.title = txtTicketTitle.Text;
                new_ticket.description = txtTicketDescr.Text;
                new_ticket.priority = Convert.ToInt32(ddlTicketPriority.SelectedValue);
                new_ticket.status = TicketsDB.GetStatus("Open").id;

                TicketsDB.AddEditTicket(new_ticket);

                ticket_message = TicketsDB.GenerateTicketNumber(new_ticket);
            }
            catch(Exception ex)
            {
                log_type type = LogsDB.GetLogType("error");
                LogsDB.AddLog("Error creating new ticket", type.id, ex);
                ticket_message = "Unexpected error creating ticket. Refresh page and try again or contact IT";
            }

            ltlMessage.Text = String.Format("<span class='success-message'>{0}</span>", ticket_message);
        }
    }
}