<%@ Page Title="" Language="C#" MasterPageFile="~/masters/master_main.Master" AutoEventWireup="true" CodeBehind="tickets_create.aspx.cs" Inherits="bug_tracker.pages_dynamic.Tickets.tickets_create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Create Ticket</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="w-100 d-flex">
        <div class="col-sm-11 col-lg-6 curved-borders m-auto back-white p-25px text-center">
            <div class="title">
                <span>Create Ticket</span>
            </div>
            <div class="col-12 row">
                <div class="d-flex col-sm-12 mt-15-px">
                    <div class="col-sm-12 col-md-6">
                        <label for="ddlProjects">Project*</label>
                    </div>
                    <div class="col-sm-12 col-md-6">
                        <asp:DropDownList ID="ddlProjects" runat="server" ClientIDMode="Static" CssClass="w-100 form-control">
                            <asp:ListItem Text="--Select Project--" Value="0" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvUserType" runat="server" ControlToValidate="ddlProjects" InitialValue="0" ErrorMessage="Select Project" ForeColor="Red" />
                    </div>
                </div>
                <div class="d-flex col-sm-12 mt-15-px">
                    <div class="col-sm-12 col-md-6">
                        <label for="txtTicketTitle">Title*</label>
                    </div>
                    <div class="col-sm-12 col-md-6">
                        <asp:TextBox ID="txtTicketTitle" runat="server" ClientIDMode="Static" Text="" AutoCompleteType="None" CssClass="form-control Next-Control" data-next-control="txtTicketDescr" />
                        <asp:RequiredFieldValidator ID="trqfTicketTitle" runat="server" ControlToValidate="txtTicketTitle" Display="Dynamic" ErrorMessage="Enter Title" ForeColor="Red" />
                    </div>
                </div>
                <div class="d-flex col-sm-12 mt-15-px">
                    <div class="col-sm-12 col-md-6">
                        <label for="txtTicketDescr">Description*</label>
                    </div>
                    <div class="col-sm-12 col-md-6">
                        <asp:TextBox ID="txtTicketDescr" runat="server" ClientIDMode="Static" Text="" AutoCompleteType="None" CssClass="form-control Next-Control" data-next-control="txtTicketDescription" TextMode="MultiLine" Rows="5" />
                        <asp:RequiredFieldValidator ID="rqfTicketDescr" runat="server" ControlToValidate="txtTicketDescr" Display="Dynamic" ErrorMessage="Enter Description" ForeColor="Red" />
                    </div>
                </div>
                <div class="d-flex col-sm-12 mt-15-px">
                    <div class="col-sm-12 col-md-6">
                        <label for="ddlTicketPriority">Priority*</label>
                    </div>
                    <div class="col-sm-12 col-md-6">
                        <asp:DropDownList ID="ddlTicketPriority" runat="server" ClientIDMode="Static" CssClass="w-100 form-control">
                            <asp:ListItem Text="--Select Priority--" Value="0" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rqfTicketPriority" runat="server" ControlToValidate="ddlTicketPriority" InitialValue="0" ErrorMessage="Select Priority" ForeColor="Red" />
                    </div>
                </div>
                <div class="d-flex col-sm-12 col-lg-12 mt-15-px">
                    <asp:Button ID="btnCreateTicket" runat="server" ClientIDMode="Static" CssClass="btn btn-info w-100" Text="Create Ticket" OnClick="btnCreateTicket_Click" />
                </div>
                <div class="d-flex col-sm-12 mt-15-px">
                    <asp:Label ID="lblMessage" runat="server" ClientIDMode="Static" Text="" />
                    <asp:Literal ID="ltlMessage" runat="server" ClientIDMode="Static" Text="" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
