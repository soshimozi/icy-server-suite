<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PlaylistViewer.aspx.cs" Inherits="PlaylistViewer" Title="My Playlist" %>
<%@ Register Assembly="iZoom.Web.Controls" Namespace="iZoom.Web.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/controls/PlaylistForm.ascx" TagPrefix="iZoom" TagName="Playlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <cc1:PageTitle ID="PageTitle1" runat="server" TitleText="Manage Playlist" />
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
        <a href="LoginPage.aspx">Sign in</a> in for free stuff!
        </AnonymousTemplate>
        <LoggedInTemplate>
            <iZoom:Playlist ID="playlistViewer" runat="server" />
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>

