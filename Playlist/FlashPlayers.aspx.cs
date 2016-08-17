using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class FlashPlayers : System.Web.UI.Page
{
    string embedTemplate = "<embed allowScriptAccess=\"never\" allowNetworking=\"internal\" enableJSURL=\"false\" enableHREF=\"false\" saveEmbedTags=\"true\" src=\"http://www.kelleyjeanproductions.com/playlist/players/{0}&g_DemoMode=on\" width=\"320\" height=\"300\" name=\"mp3Player\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" />";
    string userEmbedTemplate = "<embed allowScriptAccess=\"never\" allowNetworking=\"internal\" enableJSURL=\"false\" enableHREF=\"false\" saveEmbedTags=\"true\" src=\"http://www.kelleyjeanproductions.com/playlist/players/{0}&userId={1}\" width=\"320\" height=\"300\" name=\"mp3Player\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" /><br><span style=\"font-size:xx-small; font-style:italic;\">The iZoom player brought to you by </span><a href=\"http://myplaylist.servehttp.com/iZoom\" target=\"new\">MyPlaylist.Servehttp.Com</a>";
    string jeroenEmbedTemplate = @"<center><embed src=""http://www.kelleyjeanproductions.com/playlist/players/Mp3Player.swf"" flashvars=""&showeq={0}&height=140&width=320&displaywidth=120&file=http://www.kelleyjeanproductions.com/playlistserver/virtual/playlist.xml?userid={1}"" menu=""false"" allowfullscreen=""true"" allowscriptaccess=""always"" width=""320"" height=""140"" name=""Jeroen.swf"" align=""middle"" /></center>";
    string jeroenUserEmbedTemplate = @"<center><embed src=""http://www.kelleyjeanproductions.com/playlist/players/Mp3Player.swf"" flashvars=""&showeq={0}&height=140&width=320&displaywidth=120&file=http://www.kelleyjeanproductions.com/playlistserver/virtual/playlist.xml?userid={1}"" menu=""false"" allowfullscreen=""true"" width=""320"" height=""140"" name=""Jeroen.swf"" align=""middle"" allowScriptAccess=""always"" type=""application/x-shockwave-flash"" pluginspage=""http://www.macromedia.com/go/getflashplayer"" /></center><br><span style=""font-size:xx-small; font-style:italic;"">This mp3 player and paylist brought to you by </span><a href=""http://myplaylist.servehttp.com/iZoom"" target=""new"">MyPlaylist.Servehttp.com</a>";

    protected void Page_Load(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser(HttpContext.Current.User.Identity.Name);
        string userEmbedText = string.Empty;

        DropDownList dd = LoginView1.FindControl("DropDownList1") as DropDownList;
        string selectedValue = string.Empty;
        if (dd != null) selectedValue = dd.SelectedValue;
        if (selectedValue == null) selectedValue = string.Empty;

        if (user != null)
        {
            userEmbedText = string.Format(userEmbedTemplate, selectedValue, user.ProviderUserKey.ToString().Replace("-", ""));
        }
        else
        {
            userEmbedText = "Please sign in to enjoy membership benefits.  It's free!";
        }


        //string userEmbedText = string.Format(userEmbedTemplate, selectedValue, user.ProviderUserKey.ToString().Replace("-", ""));

        HtmlTextArea embedCode = LoginView1.FindControl("textboxEmbedCode") as HtmlTextArea;
        if (embedCode != null)
        {
            embedCode.InnerText = userEmbedText;
        }

        bool showEq = false;
        bool showPlaylist = false;

        CheckBox eqShown = LoginView1.FindControl("showEQ") as CheckBox;
        if (eqShown != null)
        {
            showEq = eqShown.Checked;
        }

        CheckBox playlistShown = LoginView1.FindControl("showPlaylist") as CheckBox;
        if (playlistShown != null)
        {
            showPlaylist = playlistShown.Checked;
        }

        HtmlTextArea jeroenCode = LoginView1.FindControl("textboxJeroenEmbed") as HtmlTextArea;
        if (jeroenCode != null)
        {
            if (user != null)
            {
                jeroenCode.InnerText = string.Format(jeroenUserEmbedTemplate, showEq.ToString().ToLower(), (Guid)user.ProviderUserKey);
            }
            else
            {
                jeroenCode.InnerText = "Please sign in to enjoy member benefits.  It's free!";
            }
        }


        Literal jeroenLiteral = LoginView1.FindControl("literalJeroen") as Literal;
        if (jeroenLiteral != null && user != null)
        {
            jeroenLiteral.Text = string.Format(jeroenEmbedTemplate, showEq.ToString().ToLower(), (Guid)user.ProviderUserKey);
        }

    }

    protected void CheckBox_CheckedChanged(object sender, EventArgs e)
    {
        Literal jeroenLiteral = LoginView1.FindControl("literalJeroen") as Literal;
        if (jeroenLiteral != null)
        {
            bool showEq = false;
            bool showPlaylist = false;

            CheckBox eqShown = LoginView1.FindControl("showEQ") as CheckBox;
            if (eqShown != null)
            {
                showEq = eqShown.Checked;
            }

            CheckBox playlistShown = LoginView1.FindControl("showPlaylist") as CheckBox;
            if (playlistShown != null)
            {
                showPlaylist = playlistShown.Checked;
            }

            MembershipUser user = Membership.GetUser(HttpContext.Current.User.Identity.Name);
            if (user != null)
            {
                jeroenLiteral.Text = string.Format(jeroenEmbedTemplate, showEq.ToString().ToLower(), (Guid)user.ProviderUserKey);
            }
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList dd = LoginView1.FindControl("DropDownList1") as DropDownList;

        string embedText = string.Format(embedTemplate, dd.SelectedValue);

        Literal literal1 = LoginView1.FindControl("Literal1") as Literal;
        literal1.Text = embedText;
    }

}
