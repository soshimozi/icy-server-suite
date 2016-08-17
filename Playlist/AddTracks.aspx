<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddTracks.aspx.cs" Inherits="AddTracks" Title="Add Tracks" %>
<%@ Register Assembly="iZoom.Web.Controls" Namespace="iZoom.Web.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/controls/TracklistForm.ascx" TagPrefix="iZoom" TagName="TrackList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <cc1:PageTitle ID="PageTitle1" runat="server" TitleText="Add Tracks" />
    <asp:LoginView runat="server">
        <AnonymousTemplate>
        <a href="LoginPage.aspx">Sign in</a> in for free stuff!
        </AnonymousTemplate>
        <LoggedInTemplate>
            <iZoom:TrackList ID="trackViewer" runat="server" />
        </LoggedInTemplate>
    </asp:LoginView>
</asp:Content>

