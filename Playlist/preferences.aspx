<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="preferences.aspx.cs" Inherits="preferences" Title="Preferences" %>
<%@ Register Assembly="iZoom.Web.Controls" Namespace="iZoom.Web.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <cc1:PageTitle ID="PageTitle1" runat="server" TitleText="Edit Preferences" />
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
        <a href="LoginPage.aspx">Sign in</a> in for free stuff!
        </AnonymousTemplate>
        <LoggedInTemplate>
        <a href="resetpassword.aspx">Change Password</a>
        </LoggedInTemplate>
    </asp:LoginView>    
</asp:Content>

