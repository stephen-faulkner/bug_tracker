<%@ Page Title="" Language="C#" MasterPageFile="~/masters/master_main.Master" AutoEventWireup="true" CodeBehind="projects_edit.aspx.cs" Inherits="bug_tracker.pages_dynamic.Projects.projects_edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title><%=strTitle %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid row">
        <div class="col-sm-12 col-md-6">
            <asp:Literal ID="ltlDevs" runat="server" Text="" />
        </div>
        <div class="col-sm-12 col-md-6">
            <asp:Literal ID="ltlUsers" runat="server" Text="" />
        </div>
        <div class="col-sm-12">
            <asp:Literal ID="ltlTicketsOpen" runat="server" Text="" />
        </div>
    </div>
</asp:Content>
