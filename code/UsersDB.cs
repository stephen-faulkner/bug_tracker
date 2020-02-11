using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;
using bug_tracker.code;

namespace bug_tracker.code
{
    public class UsersDB
    {
        static BugTrackerDbmlDataContext bug_tracker = new BugTrackerDbmlDataContext();
        static WebFunctions wFunctions = new WebFunctions();
        static string connStr = wFunctions.GetConnectionString();

        public static MembershipUserCollection GetUsers()
        {
            return Membership.GetAllUsers();
        }

        public static string[] GetUsersByRole(string role)
        {
            return Roles.GetUsersInRole(role);
        }

        public static MembershipUserCollection GetUsersToAddToProject(Int64 project_id, string role)
        {
            MembershipUserCollection users = new MembershipUserCollection();

            using(SqlConnection sqlCon = new SqlConnection(connStr))
            {
                sqlCon.Open();
                using(SqlCommand sqlCmd = new SqlCommand("bug_tracker_projects_gets_non_users", sqlCon))
                {
                    sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@project_id", project_id);
                    sqlCmd.Parameters.AddWithValue("@role_name", role);

                    using(SqlDataReader read = sqlCmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            MembershipUser user = Membership.GetUser(read["Username"].ToString());
                            users.Add(user);
                        }
                    }
                }
                sqlCon.Close();
            }

            return users;
        }
    }
}