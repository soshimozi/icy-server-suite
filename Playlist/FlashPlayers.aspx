<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="FlashPlayers.aspx.cs" Inherits="FlashPlayers" Title="Quality Flash MP3 Players" %>
<%@ Register Assembly="iZoom.Web.Controls" Namespace="iZoom.Web.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <cc1:PageTitle ID="PageTitle1" runat="server" TitleText="Flash MP3 Players" />
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
        <a href="LoginPage.aspx">Sign in</a> in for free stuff!
        </AnonymousTemplate>
        <LoggedInTemplate>
        <cc1:SectionHeader runat="server" Text="Modified Jeroen Mp3 Player" />
        <asp:CheckBox AutoPostBack="true" Checked="false" Text="Start With Expanded EQ" runat="server" ID="showEQ"  /><br />
        <asp:CheckBox AutoPostBack="true" Checked="false" Text="Start With Expanded Playlist" runat="server" ID="showPlaylist" />
        <table cellpadding="10" cellspacing="10">
            <tr>
                <td rowspan="2" width="318px">
                    <asp:Literal ID="literalJeroen" runat="server" Text='<center><embed src="http://www.kelleyjeanproductions.com/playlist/players/Mp3Player.swf" menu="false" quality="best" scale="noscale" bgcolor="#ffffff" wmode="transparent" width="318" height="320" name="iZoom" align="middle" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" /></center>' />
                </td>                             
                <td valign="top">
                    <asp:Label runat="server" ID="label5" Text="Click textbox below and copy text.  Then paste into your MySpace profile." /><br />
                    <textarea runat="server" rows="6" cols="25" readonly="readonly" id="textboxJeroenEmbed" onmouseup="javascript:this.select();" />
                </td>
            </tr>
        </table>
        <cc1:SectionHeader ID="SectionHeader1" runat="server" Text="Zoom&trade; Mp3 Player" />
        <asp:Label ID="Label1" runat="server" Text="Please choose a skin:"></asp:Label><br />
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" >
            <asp:ListItem Value="zoomplayer.swf?p_SkinHalo=haloBlue&p_SkinTextColor=0xcccccc&p_SkinColor=0x99ccff">Diamond Skin</asp:ListItem>
            <asp:ListItem Value="zoomplayer.swf?p_SkinHalo=haloGreen&p_SkinTextColor=0x006633&p_SkinColor=0x00ff66&p_SkinEQColor=0x000000">Emerald Skin</asp:ListItem>
            <asp:ListItem Value="zoomplayer.swf?p_SkinHalo=haloOrange&p_SkinTextColor=0x990000&p_SkinColor=0x990000&p_SkinEQColor=0x330000">Ruby Skin</asp:ListItem>
            <asp:ListItem Value="zoomplayer.swf?p_SkinHalo=0x333333&p_SkinTextColor=0x001100&p_SkinColor=0xD0D0D0&p_SkinEQColor=0x000000">Pearl Skin</asp:ListItem>
        </asp:DropDownList><br /><br />
        <table cellpadding="10" cellspacing="10">
            <tr>
                <td rowspan="2" width="318px">
                    <asp:Literal ID="Literal1" runat="server" Text='<embed allowScriptAccess="never" allowNetworking="internal" enableJSURL="false" enableHREF="false" saveEmbedTags="true" src="http://www.kelleyjeanproductions.com/playlist/players/zoomPlayer.swf?p_SkinHalo=haloBlue&p_SkinTextColor=0xcccccc&p_SkinColor=0x99ccff&g_DemoMode=on" FlashVars="playlistId=DVD" quality="high" width="318" height="300" name="mp3Player" style="filter: alpha(opacity=30);" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />'></asp:Literal>
                </td>
                <td valign="top">
                    <asp:Label runat="server" ID="label3" Text="Click textbox below and copy text.  Then paste into your MySpace profile." /><br />
                    <textarea runat="server" rows="6" cols="25" readonly="readonly" id="textboxEmbedCode" onmouseup="javascript:this.select();" />
                </td>
            </tr>
        </table>
        </LoggedInTemplate>
    </asp:LoginView>
    
</asp:Content>

