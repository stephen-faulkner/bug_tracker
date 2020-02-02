<%@ Page Title="" Language="C#" MasterPageFile="~/masters/master_main.Master" AutoEventWireup="true" CodeBehind="change_password.aspx.cs" Inherits="bug_tracker.pages_static.change_password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Change Password | Bug Tracker</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="w-100 d-flex">
        <div class="col-sm-11 col-md-5 col-lg-6 curved-borders m-auto back-white p-25px text-center">
            <div class="title">
                <span>Change Password</span>
            </div>
            <div class="col-12 row mt-15-px">
                <div class="col-sm-6 col-sm-12 col-md-6">
                    <label for="txtCurrentPword"><strong>Current Password</strong></label>
                </div>
                <div class="col-sm-6 col-sm-12 col-md-6">
                    <asp:TextBox ID="txtCurrentPword" runat="server" ClientIDMode="Static" Text="" CssClass="form-control" MaxLength="250" AutoCompleteType="None" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="rqfCurrentPword" runat="server" ControlToValidate="txtCurrentPword" Display="Dynamic" ErrorMessage="Enter Current Password" ForeColor="Red"/>
                </div>
            </div>
            <div class="col-12 text-center mt-15-px">
                <strong><i>*Password must be at least 5 characters long.</i></strong>
            </div>
            <div class="col-12 row mt-15-px">
                <div class="col-sm-6 col-sm-12 col-md-6">
                    <label for="txtCurrentPword"><strong>New Password</strong></label>
                </div>
                <div class="col-sm-6 col-sm-12 col-md-6">
                    <asp:TextBox ID="txtNewPword" runat="server" ClientIDMode="Static" Text="" CssClass="form-control" MaxLength="250" AutoCompleteType="None" TextMode="Password" />
                    <asp:RequiredFieldValidator ID="rqfNewPword" runat="server" ControlToValidate="txtNewPword" Display="Dynamic" ErrorMessage="Enter Current Password" ForeColor="Red"/>
                </div>
            </div>
            <div class="col-12 mt-15-px">
                <asp:Button ID="btnUpdatePassword" runat="server" ClientIDMode="Static" CssClass="btn btn-info w-100" Text="Update Password" OnClick="btnUpdatePassword_Click" />
            </div>
            <div class="col-12 mt-15-px">
                <asp:Literal ID="ltlMessage" runat="server" ClientIDMode="Static" Text="" />
            </div>
        </div>
    </div>
</asp:Content>
