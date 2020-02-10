using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bug_tracker.code;

namespace bug_tracker.pages_dynamic.Projects
{
    public partial class projects_edit : System.Web.UI.Page
    {
        public string strTitle { get; set; }
        private Int64 project_id { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt16(Session["login_type"]) == 0)
                Response.Redirect("/index");

            if (String.IsNullOrEmpty(Request.QueryString["id"]))
                Response.Redirect("/index");
            else
            {
                project this_project = ProjectsDB.GetProject(Convert.ToInt32(Request.QueryString["id"]));

                if(this_project.id == 0)
                    Response.Redirect("/index");
            }
        }

        private void LoadProjectDetails()
        {

        }
    }
}