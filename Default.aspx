<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForms_Owin_TestApp._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <p>This is home page.</p>
    
    <asp:LoginView runat="server">
        <AnonymousTemplate>
            <p><em>You are not logged in.</em></p>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <p><em>You are logged in.</em></p>
        </LoggedInTemplate>
    </asp:LoginView>

</asp:Content>
