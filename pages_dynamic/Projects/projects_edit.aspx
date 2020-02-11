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
            <div id="dProjectDescr" class="d-flex w-100">
                <asp:Label ID="lblProjectDescr" runat="server" ClientIDMode="Static" Text="" />
                <asp:Button ID="btnEditProjectDescr" runat="server" ClientIDMode="Static" Text="Edit" OnClientClick="return false;" Visible="false" />
            </div>
            <asp:Panel ID="dEditDescr" runat="server" ClientIDMode="Static" CssClass="d-none w-100" Visible="false">
                <asp:TextBox ID="txtProjectDescr" runat="server" ClientIDMode="Static" Text="" CssClass="form-control" />
                <asp:Button ID="btnSaveProjectDescr" runat="server" ClientIDMode="Static" Text="Save" OnClientClick="return false;" />
            </asp:Panel>
        </div>
        <asp:Panel ID="pnlDevs" runat="server" ClientIDMode="Static" CssClass="col-sm-12 col-md-6 p-15px back-white">
            <span class="table-header" id="sDevCount">Developers:
                <asp:Literal ID="ltlDevCount" runat="server" Text="" /></span>
            <button id="btnShowAddProjectDev" class="btn btn-info float-right mb-15-px" data-toggle="modal" data-target="#mdlAddProjectDev" onclick="return false">Add Dev</button>
            <asp:Literal ID="ltlDevs" runat="server" Text="" />
        </asp:Panel>
        <asp:Panel ID="pnlUsers" runat="server" ClientIDMode="Static" CssClass="col-sm-12 col-md-6 p-15px back-white">
            <span class="table-header">Users:
                <asp:Literal ID="ltlUserCount" runat="server" Text="" /></span>
            <button id="btnShowAddProjectUser" class="btn btn-info float-right mb-15-px" data-toggle="modal" data-target="#mdlAddProjectUser" onclick="return false">Add User</button>
            <asp:Literal ID="ltlUsers" runat="server" Text="" />
        </asp:Panel>
        <hr />
        <asp:Panel ID="pnlTickets" runat="server" ClientIDMode="Static" CssClass="col-sm-12 p-15px back-white mt-15-px">
            <span class="table-header">Tickets:
                <asp:Literal ID="ltlTicketCount" runat="server" Text="" /></span>
            <asp:Literal ID="ltlTicketsOpen" runat="server" Text="" />
        </asp:Panel>
        <div class="modal" tabindex="-1" role="dialog" id="mdlAddProjectDev">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Add Developer</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:DropDownList ID="ddlAddProjectDeveloper" runat="server" ClientIDMode="Static" CssClass="dropdown" style="min-width:50%" />
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="return false;" id="btnAddProjectDev">Add</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal" tabindex="-1" role="dialog" id="mdlAddProjectUser">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Add User</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:DropDownList ID="ddlAddProjectUser" runat="server" ClientIDMode="Static" CssClass="drop" style="min-width:50%" />
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="return false;" id="btnAddProjectUser">Add</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.project-users').DataTable({
                columnDefs: [{
                    targets: [ 1, 2],
                    className: 'dt-body-center'
                }]
            });
            $('#tblTickets').DataTable();
        });
    </script>
</asp:Content>
