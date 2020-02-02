<%@ Page Title="" Language="C#" MasterPageFile="~/masters/master_plain.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="bug_tracker.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Sign In | Bug Tracker</title>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtUsername').select();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="w-100 d-flex">
        <div class="col-sm-11 col-lg-6 curved-borders m-auto back-white p-25px text-center">
            <div class="title">
                <span>Sign In</span>
            </div>
            <div class="ml-auto mr-auto border-bottom-dashed login-div">
                <div class="d-block text-center">
                    <label for="txtUsername"><strong>Username</strong></label>
                    <asp:TextBox ID="txtUsername" runat="server" ClientIDMode="Static" CssClass="form-control ml-auto mr-auto" Text="" AutoCompleteType="None" />
                </div>
                <div class="d-block text-center pt-15px">
                    <label for="txtPword"><strong>Password</strong></label>
                    <asp:TextBox ID="txtPWord" runat="server" ClientIDMode="Static" CssClass="form-control ml-auto mr-auto" Text="" AutoCompleteType="None" TextMode="Password" />
                </div>
                <div class="d-block text-center pt-15px">             
                    <input type="checkbox" class="checkbox-inline" id="chkRememberMe" runat="server" clientidmode="static" />       
                    <label for="chkRememberMe">Remember Me</label>
                </div>
                <div class="d-block pt-15px w-100">
                    <asp:Button ID="btnSignIn" runat="server" ClientIDMode="Static" Text="Sign In" CssClass="btn btn-info w-100" OnClick="btnSignIn_Click" />
                </div>
                <div class="d-block pt-15px w-100">
                    <asp:Label ID="lblMessage" runat="server" Text="" />
                </div>
            </div>
            <div class="text-center d-flex mt-15-px">
                <div class="ml-auto click-link" data-toggle="modal" data-target="#mdlForgotPword">
                    <span>Forgot Password</span>
                </div>
                <div class="click-link ml-15-px mr-15-px border-left-solid border-right-solid pl-15-px pr-15-px">
                    <a href="/pages_static/new_user.aspx">Create User</a>
                </div>
                <div class="mr-auto click-link" data-toggle="modal" data-target="#mdlDemoSignIn">
                    <span>Sign In As Demo User</span>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" tabindex="-1" role="dialog" id="mdlForgotPword">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Demo Accounts</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="col-sm-12 col-md-6">
                        <div class="ml-auto mr-auto">
                            <asp:LinkButton ID="btnAdminLogin" runat="server" ClientIDMode="Static">
                                <i class="material-icons">account_box</i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-6">
                        <div class="ml-auto mr-auto">
                            <asp:LinkButton ID="btnProjectLogin" runat="server" ClientIDMode="Static">
                                <i class="material-icons">assignment_ind</i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-6">
                        <div class="ml-auto mr-auto">
                            <asp:LinkButton ID="btnDevLogin" runat="server" ClientIDMode="Static">
                                <i class="material-icons">record_voice_over</i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-6">
                        <div class="ml-auto mr-auto">
                            <asp:LinkButton ID="btnUserAdmin" runat="server" ClientIDMode="Static">
                                <i class="material-icons">face</i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" tabindex="-1" role="dialog" id="mdlDemoSignIn">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Forgot Password</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>Email</p>
                    <input type="text" autocomplete="off" class="form-control w-100" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary">Send Password Reset Email</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
