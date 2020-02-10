using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Globalization;

namespace bug_tracker.code
{
    public class ProjectsDB
    {
        static BugTrackerDbmlDataContext bug_tracker = new BugTrackerDbmlDataContext();

        public static project GetProject(Int64 projectID)
        {
            project project = null;

            project = (from _project in bug_tracker.projects
                       where _project.id == projectID
                       select _project).FirstOrDefault();

            return project;
        }

        public static project GetProject(string project_name)
        {
            project project = null;

            project = (from _project in bug_tracker.projects
                       where _project.name.ToLower().Trim() == project_name.ToLower(CultureInfo.CurrentCulture).Trim()
                       select _project).FirstOrDefault();

            return project;
        }

        public static List<project> GetAllProjects()
        {
            List<project> projects = (from _project in bug_tracker.projects
                                      orderby _project.name
                                      select _project).Distinct().ToList();

            return projects;
        }

        public static project AddEditProject(project project)
        {
            project _project = null;

            try
            {
                if(project.id != 0)
                {
                    //update existing project
                    _project = (from projects in bug_tracker.projects
                                where projects.id == project.id
                                select projects).FirstOrDefault();

                    _project.description = project.description;
                    _project.status = project.status;
                }
                else
                {
                    //create new project
                    _project = new project();
                    _project.name = project.name;
                    _project.description = project.description;
                    _project.status = project.status;
                    _project.created_date = DateTime.Now;
                    _project.created_by = new Guid(HttpContext.Current.Session["user_id"].ToString());

                    bug_tracker.projects.InsertOnSubmit(_project);
                }

                bug_tracker.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
            catch(ChangeConflictException ex)
            {
                foreach (ObjectChangeConflict objConflict in bug_tracker.ChangeConflicts)
                    foreach (MemberChangeConflict memberChange in objConflict.MemberConflicts)
                        memberChange.Resolve(RefreshMode.KeepCurrentValues);

                bug_tracker.SubmitChanges(ConflictMode.ContinueOnConflict);
            }
            catch(Exception ex)
            {
                _project = null;
            }

            return _project;
        }

        public static List<projects_all_detail> GetAllProjectsDetails()
        {
            List<projects_all_detail> all_Details = (from projects in bug_tracker.projects_all_details
                                                     orderby projects.name
                                                     select projects).Distinct().ToList();

            return all_Details;
        }

        public static List<projects_all_detail> GetAllProjectsDetails(Guid user_id)
        {
            List<projects_all_detail> all_details = (from projects in bug_tracker.projects_all_details
                                                     join projects_user in bug_tracker.projects_users on projects.project_id equals projects_user.project_id
                                                     where projects_user.user_id == user_id
                                                     select projects).Distinct().ToList();

            return all_details;
        }

        public static List<projects_all_detail> GetActiveProjectsDetails()
        {
            List<projects_all_detail> all_Details = (from projects in bug_tracker.projects_all_details
                                                     where projects.active == true
                                                     orderby projects.name
                                                     select projects).Distinct().ToList();

            return all_Details;
        }

        public static List<projects_all_detail> GetActiveProjectsDetails(Guid user_id)
        {
            List<projects_all_detail> all_details = (from projects in bug_tracker.projects_all_details
                                                     join projects_user in bug_tracker.projects_users on projects.project_id equals projects_user.project_id
                                                     where projects_user.user_id == user_id && projects.active == true
                                                     select projects).Distinct().ToList();

            return all_details;
        }
    }
}