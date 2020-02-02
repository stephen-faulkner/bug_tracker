<%@ Page Title="" Language="C#" MasterPageFile="~/masters/master_main.Master" AutoEventWireup="true" CodeBehind="projects_add.aspx.cs" Inherits="bug_tracker.pages_dynamic.Projects.projects_add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>New Project | Bug Tracker</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="w-100 d-flex">
        <div class="col-sm-11 col-lg-6 curved-borders m-auto back-white p-25px text-center">
            <div class="title">
                <span>Add Project</span>
            </div>
            <div class="col-12 row">
                <div class="col-sm-6 col-sm-12 col-md-6">
                    <label for="txtProjectName"><strong>Name</strong></label>
                </div>
                <div class="col-sm-6 col-sm-12 col-md-6">
                    <asp:TextBox ID="txtProjectName" runat="server" ClientIDMode="Static" Text="" CssClass="form-control" MaxLength="250" AutoCompleteType="None" />
                    <asp:RequiredFieldValidator ID="rqfProjectName" runat="server" ControlToValidate="txtProjectName" Display="Dynamic" ErrorMessage="Enter Project Name" ForeColor="Red"/>
                </div>
            </div>
            <div class="col-12 row">
                <div class="col-sm-6 col-sm-12 col-md-6">
                    <label for="txtProjectDescription"><strong>Description</strong></label>
                </div>
                <div class="col-sm-6 col-sm-12 col-md-6">
                    <asp:TextBox ID="txtProjectDescription" runat="server" ClientIDMode="Static" Text="" CssClass="form-control" AutoCompleteType="None" />
                    <asp:RequiredFieldValidator ID="rqfProjectDescr" runat="server" ControlToValidate="txtProjectDescription" Display="Dynamic" ErrorMessage="Enter Project Description" ForeColor="Red"/>
                </div>
            </div>
            <div class="col-12">
                <asp:Button ID="btnCreateProject" runat="server" ClientIDMode="Static" CssClass="btn btn-info w-100" Text="Create Project" OnClick="btnCreateProject_Click" />
            </div>
            <div class="col-12 mt-15-px">
                <asp:Literal ID="ltlMessage" runat="server" ClientIDMode="Static" Text="" />
            </div>
        </div>
    </div>
</asp:Content>
