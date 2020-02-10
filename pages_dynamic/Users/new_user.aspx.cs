using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Security;
using System.Security.Cryptography;
using bug_tracker.code;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;

namespace bug_tracker.pages_static
{
    public partial class new_user : System.Web.UI.Page
    {
        WebFunctions wf = new WebFunctions();
        private string connStr = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            connStr = wf.GetConnectionString();

            if (!IsPostBack)
            {
                LoadRoles();
                ClearInputs();
            }
        }

        private void LoadRoles()
        {
            ListItem item;

            using (SqlConnection sqlCon = new SqlConnection(connStr))
            {
                sqlCon.Open();
                using (SqlCommand sqlCmd = new SqlCommand("SELECT RoleId, RoleName from dbo.aspnet_Roles order by RoleName", sqlCon))
                {
                    using (SqlDataReader read = sqlCmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            item = new ListItem()
                            {
                                Text = read["RoleName"].ToString().Trim(),
                                Value = read["RoleName"].ToString().Trim()
                            };

                            ddlUserType.Items.Add(item);
                        }
                    }
                }
                sqlCon.Close();
            }
        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            MembershipCreateStatus createStatus;
            string tempPword = Membership.GeneratePassword(8, 2);
            MembershipUser user = Membership.CreateUser(String.Format("{0} {1}", txtFirstName.Text, txtLastName.Text), tempPword, txtNewUserEmail.Text.ToLower(), null, null, true, out createStatus);

            switch (createStatus)
            {
                case MembershipCreateStatus.Success:
                    Roles.AddUserToRole(String.Format("{0} {1}", txtFirstName.Text, txtLastName.Text), ddlUserType.SelectedValue);
                    wf.SendNewUserEmail(String.Format("{0} {1}", txtFirstName.Text, txtLastName.Text), tempPword, txtNewUserEmail.Text);
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = "The user account has been created. An email will be sent to the user shortly.";
                    ClearInputs();
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Username already exists.";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "User with this email already exists.";
                    txtNewUserEmail.Focus();
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Email address is not valid.";
                    txtNewUserEmail.Focus();
                    break;
                default:
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "An unexpected error occurred.";
                    break;
            }
        }

        private void ClearInputs()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtNewUserEmail.Text = string.Empty;
            ddlUserType.SelectedIndex = -1;
        }
    }
}