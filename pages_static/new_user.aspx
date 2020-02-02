<%@ Page Title="" Language="C#" MasterPageFile="~/masters/master_plain.Master" AutoEventWireup="true" CodeBehind="new_user.aspx.cs" Inherits="bug_tracker.pages_static.new_user" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Create User | Bug Tracker</title>
    <script type="text/javascript">
        $(document).ready(function () {
            SetFocus("txtFirstName");
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="w-100 d-flex">
        <div class="col-sm-11 col-lg-2 curved-borders m-auto back-white p-25px">
            <div class="title text-center">
                <span>Create New User</span>
            </div>             
            <div class=" col-sm-12 d-block mt-15-px">
                <label for="txtFirstName">First Name*</label>
                <asp:TextBox ID="txtFirstName" runat="server" ClientIDMode="Static" Text="" AutoCompleteType="None" CssClass="form-control Next-Control" data-next-control ="txtLastName" />
                <asp:RequiredFieldValidator ID="rqfFirstName" runat="server" ControlToValidate="txtFirstName" Display="Dynamic" ErrorMessage="Enter First Name" ForeColor="Red"/>
            </div>
            <div class="col-sm-12 d-block mt-15-px">
                <label for="txtLastName">Last Name*</label>
                <asp:TextBox ID="txtLastName" runat="server" ClientIDMode="Static" Text="" AutoCompleteType="None" CssClass="form-control Next-Control" data-next-control ="txtNewUserEmail" />
                <asp:RequiredFieldValidator ID="rqfLastName" runat="server" ControlToValidate="txtLastName" Display="Dynamic" ErrorMessage="Enter Last Name" ForeColor="Red"/>
            </div>
            <div class="col-sm-12 d-block mt-15-px">
                <label for="txtNewUserEmail">Email Address*</label>
                <asp:TextBox ID="txtNewUserEmail" runat="server" ClientIDMode="Static" Text="" AutoCompleteType="None" CssClass="form-control Next-Control" TextMode="Email" data-next-control ="ddlUserType" />
                <asp:RequiredFieldValidator ID="rqfNewUserEmail" runat="server" ControlToValidate="txtNewUserEmail" Display="Dynamic" ErrorMessage="Enter Email" ForeColor="Red"/>
            </div>
            <div class="d-block col-sm-12 mt-15-px">
                <label for="ddlUserType">User Type*</label>
                <asp:DropDownList ID="ddlUserType" runat="server" ClientIDMode="Static" CssClass="w-100 form-control">
                    <asp:ListItem Text="--Select User Type--" Value="0" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvUserType" runat="server" ControlToValidate="ddlUserType" InitialValue="0" ErrorMessage="Select User Type" ForeColor="Red" />
            </div>
            <div class="d-block col-sm-12 mt-15-px">
                <strong><span>*Password will be generated and emailed to the user upon completion.</span></strong>
            </div>
            <div class="d-block col-sm-12 mt-15-px">
                <asp:Button ID="btnCreateUser" runat="server" ClientIDMode="Static" CssClass="btn btn-info w-100" Text="Create User" OnClick="btnCreateUser_Click" />
            </div>
            <div class="d-block col-sm-12 mt-15-px">
                <asp:Label ID="lblMessage" runat="server" ClientIDMode="Static" Text="" />
            </div>
        </div>
    </div>
</asp:Content>
