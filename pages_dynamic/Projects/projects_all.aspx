<%@ Page Title="" Language="C#" MasterPageFile="~/masters/master_main.Master" AutoEventWireup="true" CodeBehind="projects_all.aspx.cs" Inherits="bug_tracker.pages_dynamic.Projects.projects_all" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>All Projects | Bug Tracker</title>


    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.css">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid w-100">
        <div class="title mt-15-px">
            <span>All Projects</span>
        </div>
        <div class="col-12 p-15px back-white">
            <asp:Literal ID="ltlProjects" runat="server" ClientIDMode="Static" Text="" />
        </div>
    </div>
    
    
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblProjects_Open').DataTable();
        });
    </script>
</asp:Content>
