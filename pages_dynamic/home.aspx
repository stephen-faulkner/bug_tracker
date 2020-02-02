<%@ Page Title="" Language="C#" MasterPageFile="~/masters/master_main.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="bug_tracker.pages_dynamic.home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Home</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid w-100 mt-15-px">
        <div class="col-12">
            <div class="title">
                <span>Overall Ticket Info</span>
            </div>
            <div class='col-12'>
                <span>bar chart</span>
                <asp:Label ID="lblTicketsOverview" runat="server" ClientIDMode="Static" />                
                <div class="loading">
                    <img src="/images/loading_gif.gif" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
