<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="resetpassword.aspx.cs" Inherits="resetpassword" Title="Change Password" %>
<%@ Register Assembly="iZoom.Web.Controls" Namespace="iZoom.Web.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<cc1:PageTitle ID="PageTitle1" runat="server" TitleText="Change Password" />
<table cellpadding="0" cellspacing="4">
    <tr><td align="right" valign="top">Email Address:</td><td align="left"><input type="text" id="emailAddress" runat="server" /></td></tr>
    <tr><td align="right" valign="top">Old Password:</td><td align="left"><input type="Password" id="oldPassword" runat="server" /></td></tr>
    <tr><td align="right" valign="top">New Password:</td><td align="left"><input type="password" id="newPassword" runat="server" /></td></tr>
    <tr><td align="right" valign="top">Verify Password:</td><td align="left"><input type="password" id="verifyPassword" runat="server" /></td></tr>
    <tr><td align="right" valign="top">Password Hint:</td><td align="left"><textarea cols="24" rows="4" id="hint" runat="server" /></td></tr>
    <tr><td>&nbsp;</td><td align="left"><input value="Submit" type="submit"/></td></tr>
    <tr><td colspan="2"><div id="passwordErrorDiv" runat="server" style="visibility:hidden; color:Red;"></div></td></tr>
</table>
</asp:Content>

