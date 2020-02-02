using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bug_tracker.code;

namespace bug_tracker.pages_dynamic.Projects
{
    public partial class projects_add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreateProject_Click(object sender, EventArgs e)
        {
            string project_name = txtProjectName.Text;
            project new_project = ProjectsDB.GetProject(project_name);

            if (new_project != null)
                ltlMessage.Text = String.Format("<span class='error-message'>A project with name {0} already exists</span>", project_name);
            else
            {
                new_project = new project();
                new_project.name = project_name;
                new_project.description = txtProjectDescription.Text;

                new_project = ProjectsDB.AddEditProject(new_project);

                if (new_project != null)
                {
                    ltlMessage.Text = "<span class='success-message'>New Project Created</span>";
                    txtProjectName.Text = string.Empty;
                    txtProjectDescription.Text = string.Empty;

                    txtProjectName.Focus();
                }
            }
        }
    }
}