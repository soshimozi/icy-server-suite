<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="search.aspx.cs" Inherits="search" Title="Search" %>

<%@ Register Assembly="iZoom.Web.Controls" Namespace="iZoom.Web.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/controls/SearchForm.ascx" TagPrefix="iZoom" TagName="TrackSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <cc1:PageTitle ID="PageTitle1" runat="server" TitleText="Search" />
    <iZoom:TrackSearch ID="trackSearch" runat="server" />
</asp:Content>

