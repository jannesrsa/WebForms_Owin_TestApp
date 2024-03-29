﻿<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebForms_Owin_TestApp.Account.Login" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <h2><%: Title %>.</h2>
    <div class="row">
        <div class="col-md-8">
            <section id="loginForm">
                <div class="form-horizontal">

                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>

                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="ddlClaimsIssuer" CssClass="col-md-2 control-label">Claims Issuer</asp:Label>
                        <div class="col-md-10">
                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlClaimsIssuer"></asp:DropDownList>
                            <span class="text-danger" style="visibility: hidden;">The claims issuer is required.</span>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button ID="btnLogIn" runat="server" Text="Log in" CssClass="btn btn-default" OnClick="LogIn" />
                        </div>
                    </div>

                </div>
            </section>
        </div>
    </div>
</asp:Content>
