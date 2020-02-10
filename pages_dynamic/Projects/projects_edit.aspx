<%@ Page Title="" Language="C#" MasterPageFile="~/masters/master_main.Master" AutoEventWireup="true" CodeBehind="projects_edit.aspx.cs" Inherits="bug_tracker.pages_dynamic.Projects.projects_edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title><%=strTitle %></title>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid w-100 row">
        <div class="title">
            <asp:Literal ID="ltlTitle" runat="server" />
        </div>
        <div class="col-sm-12">
            <u><strong>Project Description</strong></u>
            <br />
            <asp:Literal ID="ltlDescription" runat="server" />
        </div>
        <asp:Panel ID="pnlDevs" runat="server" ClientIdMode="Static" CssClass="col-sm-12 col-md-6 p-15px back-white">
            <span class="table-header">Developers: <asp:Literal ID="ltlDevCount" runat="server" Text="" /></span>
            <asp:Literal ID="ltlDevs" runat="server" Text="" />
        </asp:Panel>
        <asp:Panel ID="pnlUsers" runat="server" ClientIdMode="Static" CssClass="col-sm-12 col-md-6 p-15px back-white">
            <span class="table-header">Users: <asp:Literal ID="ltlUserCount" runat="server" Text="" /></span>
            <asp:Literal ID="ltlUsers" runat="server" Text="" />
        </asp:Panel>
        <hr />
        <asp:Panel ID="pnlTickets" runat="server" ClientIdMode="Static" CssClass="col-sm-12 p-15px back-white mt-15-px">
            <span class="table-header">Tickets: <asp:Literal ID="ltlTicketCount" runat="server" Text="" /></span>
            <asp:Literal ID="ltlTicketsOpen" runat="server" Text="" />
        </asp:Panel>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblDevelopers').DataTable();
            $('#tblUsers').DataTable();
            $('#tblTickets').DataTable();
        });
    </script>
</asp:Content>
