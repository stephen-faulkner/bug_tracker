using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Globalization;
using System.Web.Security;

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

        public static List<projects_tickets_detail> GetAllProjectsDetails()
        {
            List<projects_tickets_detail> all_Details = (from projects in bug_tracker.projects_tickets_details
                                                     orderby projects.name
                                                     select projects).Distinct().ToList();

            return all_Details;
        }

        public static List<projects_tickets_detail> GetAllProjectsDetails(Guid user_id)
        {
            List<projects_tickets_detail> all_details = (from projects in bug_tracker.projects_tickets_details
                                                     join projects_user in bug_tracker.projects_users on projects.project_id equals projects_user.project_id
                                                     where projects_user.user_id == user_id
                                                     select projects).Distinct().ToList();

            return all_details;
        }

        public static List<projects_tickets_detail> GetActiveProjectsDetails()
        {
            List<projects_tickets_detail> all_Details = (from projects in bug_tracker.projects_tickets_details
                                                     where projects.active == true
                                                     orderby projects.name
                                                     select projects).Distinct().ToList();

            return all_Details;
        }

        public static List<projects_tickets_detail> GetActiveProjectsDetails(Guid user_id)
        {
            List<projects_tickets_detail> all_details = (from projects in bug_tracker.projects_tickets_details
                                                     join projects_user in bug_tracker.projects_users on projects.project_id equals projects_user.project_id
                                                     where projects_user.user_id == user_id && projects.active == true
                                                     select projects).Distinct().ToList();

            return all_details;
        }

        public static List<MembershipUser> GetProjectUsers(project this_project)
        {
            List<MembershipUser> users = new List<MembershipUser>();
            List<projects_user> project_users = (from project_user in bug_tracker.projects_users
                                                 join user in bug_tracker.aspnet_Users on project_user.user_id equals user.UserId
                                                 join user_role in bug_tracker.aspnet_UsersInRoles on user.UserId equals user_role.UserId
                                                 join roles in bug_tracker.aspnet_Roles on user_role.RoleId equals roles.RoleId
                                                 where project_user.project_id == this_project.id && roles.RoleName == "User"
                                                 select project_user).Distinct().ToList();

            foreach (projects_user user in project_users)
                users.Add(Membership.GetUser(user.user_id));

            return users;
        }

        public static List<MembershipUser> GetProjectDevs(project this_project)
        {
            List<MembershipUser> devs = new List<MembershipUser>();
            List<projects_user> project_devs = (from project_user in bug_tracker.projects_users
                                                 join user in bug_tracker.aspnet_Users on project_user.user_id equals user.UserId
                                                 join user_role in bug_tracker.aspnet_UsersInRoles on user.UserId equals user_role.UserId
                                                 join roles in bug_tracker.aspnet_Roles on user_role.RoleId equals roles.RoleId
                                                 where project_user.project_id == this_project.id && roles.RoleName == "Developer"
                                                select project_user).Distinct().ToList();

            foreach (projects_user dev in project_devs)
                devs.Add(Membership.GetUser(dev.user_id));

            return devs;
        }



        public static List<MembershipUser> GetProjectManagers(project this_project)
        {
            List<MembershipUser> managers = new List<MembershipUser>();
            List<projects_user> project_managers = (from project_user in bug_tracker.projects_users
                                                join user in bug_tracker.aspnet_Users on project_user.user_id equals user.UserId
                                                join user_role in bug_tracker.aspnet_UsersInRoles on user.UserId equals user_role.UserId
                                                join roles in bug_tracker.aspnet_Roles on user_role.RoleId equals roles.RoleId
                                                where project_user.project_id == this_project.id && roles.RoleName == "Project Manager"
                                                select project_user).Distinct().ToList();

            foreach (projects_user manager in project_managers)
                managers.Add(Membership.GetUser(manager.user_id));

            return managers;
        }

        public static List<project_detail> GetProjectTickets(project this_project)
        {
            List<project_detail> project_tickets = (from pt in bug_tracker.project_details
                                                             where pt.project_id == this_project.id
                                                             select pt).Distinct().ToList();

            return project_tickets;
        }
    }
}