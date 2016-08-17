<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="_Default" Title="Home" %>
<%@ Register Assembly="iZoom.Web.Controls" Namespace="iZoom.Web.Controls" TagPrefix="cc1" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <cc1:PageTitle ID="PageTitle1" runat="server" TitleText="Home" />
    <asp:LoginView runat="server" ID="loginView1">
        <AnonymousTemplate>
            <a href="LoginPage.aspx">Sign in</a> in for free stuff!
        </AnonymousTemplate>
        <LoggedInTemplate>
            <p>
            Welcome to the iZoom website.  You can find quality MP3 streaming solutions for your
            website or MySpace profile.  Unlimited playlist length and customizable skins for
            the players are some of the many features you get for free!
            </p>
            <p>If you have any questions or would like to suggest any improvements please feel 
            free to <a href="#contactform">contact us</a>.
            </p>
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>
