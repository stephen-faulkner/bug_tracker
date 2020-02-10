<%@ Page Title="" Language="C#" MasterPageFile="~/masters/master_main.Master" AutoEventWireup="true" CodeBehind="view_all_users.aspx.cs" Inherits="bug_tracker.pages_dynamic.Users.view_all_users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>All Users</title>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid w-100 mt-15-px">
        <div class="title">
            <span><strong>All Users</strong></span>
        </div>
        <div class="col-12 p-15px back-white">
            <asp:Literal ID="ltlUsers" runat="server" ClientIDMode="Static" />
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblUsers').DataTable();
        });
    </script>
</asp:Content>
