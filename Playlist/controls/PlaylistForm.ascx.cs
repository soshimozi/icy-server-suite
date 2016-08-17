using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Tracklist.Domain;

public partial class controls_PlaylistDetails : System.Web.UI.UserControl
{
    string popupScript = @"
    <script>
        function popup(message) {
            alert(message);
        }
    </script>";

    string doPopupFormat = @"
    <script>
        popup('{0}');
    </script>";

    // grid column indexes
    //int playlistTitleColumn = 1;
    int playlistCommandColumn = 2;

    // data column indexes
    //int playlistDataColumnPlaylistId = 9;
    int playlistDataColumnSongId = 0;
    //int playlistDataColumnPlaylistIndex = 10;

    string filter = "";
    int totalPlaylistRowCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if( !Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), "popup") )
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", popupScript);
        }

        UserInfo info = DataAccess.GetUserInfoByEmail(HttpContext.Current.User.Identity.Name);
        if (info != null)
        {
            InitPlaylistDataSource(info);
        }
    }

    protected void PlaylistDataSource_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        totalPlaylistRowCount = e.AffectedRows;
        totalPlaylistCountHiddenField.Value = e.AffectedRows.ToString();
    }
    private void InitPlaylistDataSource(UserInfo info)
    {
        string selectCommand = "Playlist.uspGetPlaylist";
        PlaylistDataSource.SelectCommand = selectCommand;
        PlaylistDataSource.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;

        PlaylistDataSource.SelectParameters[0].DefaultValue = info.UserId.ToString();
    }

    protected void playlistGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        UserInfo user = DataAccess.GetUserInfoByEmail(HttpContext.Current.User.Identity.Name);
        if (e.CommandName.ToLower() == "removeselected")
        {
            if (user != null)
            {
                List<TrackInfo> trackList = DataAccess.GetTracklist(user.UserId);

                List<long> removedIds = new List<long>();
                for (int i = 0; i < playlistGrid.Rows.Count; i++)
                {
                    GridViewRow row = playlistGrid.Rows[i];

                    CheckBox checkbox = (CheckBox)row.Cells[3].FindControl("deleteSong");
                    if (checkbox != null)
                    {
                        if (checkbox.Checked)
                        {
                            checkbox.Checked = false;
                            removedIds.Add(trackList[i].TrackId);
                        }

                        DataAccess.RemoveUserTrack(trackList[i].TrackId, user.UserId);
                    }
                }

                int currentIndex = 0;
                for (int i = 0; i < trackList.Count; i++)
                {
                    if (!removedIds.Contains(trackList[i].TrackId))
                    {
                        DataAccess.AddUserTrack(trackList[i].TrackId, user.UserId);
                        currentIndex++;
                    }
                }
            }
            

            playlistGrid.DataBind();

        }
        else if (e.CommandName.ToLower() == "removetrack")
        {
            string[] tokens = ((string)e.CommandArgument).Split("&".ToCharArray());
            //int playlistId;
            int songId;
            //int playlistIndex;
            int playlistCount;

            int.TryParse(tokens[0], out songId);
            //int.TryParse(tokens[1], out songId);
            //int.TryParse(tokens[2], out playlistIndex);
            int.TryParse(totalPlaylistCountHiddenField.Value, out playlistCount);

            if (user != null)
            {
                //AccountInfo info = DataAccess.GetAccountInfo((Guid)user.ProviderUserKey);
                //if (info != null)
                //{
                // fucking kludge man - but delete each song after and re-add with new index
                //for (int i = playlistIndex; i < playlistCount; i++)
                //{
                //    //DataAccess.RemovePlayistEntry(playlistId, info.Playlist.SongList[i].Id);
                //}

                //for (int i = playlistIndex + 1; i < playlistCount; i++)
                //{
                //    //DataAccess.AddPlaylistEntry(playlistId, info.Playlist.SongList[i].Id, i - 1);
                //}
                //}
            }

            playlistGrid.DataBind();

        }
        else if (e.CommandName.ToLower() == "moveup")
        {
            string[] tokens = ((string)e.CommandArgument).Split("&".ToCharArray());
            //int playlistId;
            int songId;
            //int playlistIndex;

            //int.TryParse(tokens[0], out playlistId);
            int.TryParse(tokens[0], out songId);
            //int.TryParse(tokens[2], out playlistIndex);

            //if (playlistIndex > 0)
            //{
            //    //DataAccess.UpdatePlaylistEntry(playlistId, songId, playlistIndex, playlistIndex - 1);
            //    //playlistGrid.DataBind();
            //}

        }
        else if (e.CommandName.ToLower() == "movedown")
        {
            string[] tokens = ((string)e.CommandArgument).Split("&".ToCharArray());
            //int playlistId;
            int songId;
            //int playlistIndex;
            int playlistCount;

            //int.TryParse(tokens[0], out playlistId);
            int.TryParse(tokens[0], out songId);
            //int.TryParse(tokens[2], out playlistIndex);
            int.TryParse(totalPlaylistCountHiddenField.Value, out playlistCount);


            //if (playlistIndex < playlistCount - 1)
            //{
            //    //DataAccess.UpdatePlaylistEntry(playlistId, songId, playlistIndex, playlistIndex + 1);
            //    //playlistGrid.DataBind();
            //}

        }
        else if (e.CommandName.ToLower() == "listentracklist")
        {
            Response.Redirect(string.Format("SongPreview.aspx?userId={0}", (string)e.CommandArgument));
        }
        else if (e.CommandName.ToLower() == "movetrack")
        {
            string dragTarget = Request.Form["__DRAGSOURCE"];
            TableRow row = FindGridRow(dragTarget.Replace("_", "$"));
            if (row != null)
            {
                ImageButton button = (ImageButton)row.Cells[playlistCommandColumn].FindControl("buttonDragDrop");
                if (button != null)
                {
                    string argument = (string)button.CommandArgument;
                    string[] tokens = argument.Split("&".ToCharArray());

                    int songId;
                    int sourceIndex;
                    //int playlistId;
                    int playlistCount;
                    //int.TryParse(tokens[0], out playlistId);
                    int.TryParse(tokens[0], out songId);
                    //int.TryParse(tokens[2], out sourceIndex);
                    int.TryParse(totalPlaylistCountHiddenField.Value, out playlistCount);


                    tokens = ((string)e.CommandArgument).Split("&".ToCharArray());
                    int destinationIndex;
                    //int.TryParse(tokens[2], out destinationIndex);

                    //DataAccess.UpdatePlaylistEntry(playlistId, songId, sourceIndex, destinationIndex);
                    //playlistGrid.DataBind();
                }
            }
        }
    }

    private TableRow FindGridRow(string dragTarget)
    {
        TableRow foundRow = null;
        foreach (TableRow row in playlistGrid.Controls[0].Controls)
        {
            if (row.UniqueID == dragTarget)
            {
                foundRow = row;
                break;
            }
        }

        return foundRow;
    }

    private void PopupOnReturn(string message)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "startup", string.Format(doPopupFormat, message));
    }

    protected void Grid_RowCreated(object sender, GridViewRowEventArgs e)
    {
        UserInfo user = DataAccess.GetUserInfoByEmail(HttpContext.Current.User.Identity.Name);
        if (e.Row.RowType == DataControlRowType.Header)
        {
            AddSortGlyph((GridView)sender, e.Row);
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            string userId = string.Empty;
            if (user != null)
            {
                //AccountInfo info = DataAccess.GetAccountInfo((Guid)user.ProviderUserKey);
                userId = user.UserId.ToString().Replace("-", "");

                //// span the entire row
                e.Row.Cells[1].ColumnSpan = 2;

                string urlString = string.Format("http://zoom.servemp3.com/playlist?userid={0}", userId);

                Label label = new Label();
                label.Text = "WimAmp / ShoutCAST Url: ";
                label.Font.Bold = true;
                e.Row.Cells[1].Controls.Add(label);

                TextBox textBox = new TextBox();
                textBox.ReadOnly = true;
                textBox.Width = Unit.Pixel(350);
                textBox.ToolTip = urlString;
                textBox.Attributes["onmouseup"] = "this.focus(); this.select();";
                textBox.Text = urlString;
                e.Row.Cells[1].Controls.Add(textBox);

                ImageButton listenButton = new ImageButton();
                listenButton.ToolTip = "Listen To Tracklist";
                listenButton.ID = "buttonListenTrack";
                listenButton.ImageUrl = "~/images/AudioHS.png";
                listenButton.CommandName = "ListenTracklist";
                listenButton.CommandArgument = user.UserId.ToString().Replace("-", "");

                Literal literal = new Literal();
                literal.Text = "&nbsp;";
                e.Row.Cells[1].Controls.Add(literal);
                e.Row.Cells[1].Controls.Add(listenButton);

                ImageButton deleteSelected = new ImageButton();
                HtmlGenericControl div = new HtmlGenericControl("div");
                div.ID = "deleteDiv";
                div.Attributes["style"] = "text-align:center;";
                deleteSelected.ToolTip = "Remove Selected";
                deleteSelected.ID = "deleteSelected";
                deleteSelected.ImageUrl = "~/images/DeleteHS.png";
                deleteSelected.CommandName = "RemoveSelected";
                deleteSelected.OnClientClick = "javascript:return confirm(\"Are you sure you want to remove these songs?\");";
                div.Controls.Add(deleteSelected);
                e.Row.Cells[2].Controls.Add(div);

                Literal scriptPanel = new Literal();
                Page.ClientScript.RegisterClientScriptInclude("scriptaculousPrototype", "jscript/lib/prototype.js");
                Page.ClientScript.RegisterClientScriptInclude("dragDropObjInc", "jscript/scriptaculous.js");
                Page.ClientScript.RegisterHiddenField("__DRAGSOURCE", "");
                System.Text.StringBuilder scriptBuilder = new System.Text.StringBuilder();
                scriptBuilder.Append("<script type='text/jscript'>");

                for (int i = 0; i < playlistGrid.Rows.Count; i++)
                {
                    if (playlistGrid.Rows[i].RowType == DataControlRowType.DataRow)
                    {
                        ImageButton button = (ImageButton)playlistGrid.Rows[i].Cells[playlistCommandColumn].FindControl("buttonDragDrop");

                        playlistGrid.Rows[i].Attributes["ID"] = playlistGrid.Rows[i].ClientID;

                        scriptBuilder.AppendFormat("new Draggable('{0}',\n\r" +
                                                    "{{\n\r" +
                                                    "   starteffect:function(){{\n\r" +
                                                    "       new Effect.Highlight('{0}',{{queue:'end'}});\n\r" +
                                                    "   }},\n\r" +
                                                    "   revert:true, scroll:window\n\r" +
                                                    "}} );\n\r",
                                                    playlistGrid.Rows[i].ClientID);

                        string script = string.Format("theForm.__DRAGSOURCE.value = element.id;\n\r{0};\n\r", Page.ClientScript.GetPostBackClientHyperlink(button, "").Replace("javascript:", ""));

                        scriptBuilder.AppendFormat("Droppables.add('{0}', {{onDrop:function(element, dropon, event) {{{1}}}}});\n\r", playlistGrid.Rows[i].ClientID, script);
                    }
                }

                scriptBuilder.Append("</script>");

                scriptPanel.Text = scriptBuilder.ToString();
                e.Row.Cells[2].Controls.Add(scriptPanel);
            }
        }

    }

    #region Helper Functions
    private void AddSortGlyph(GridView grid, GridViewRow item)
    {
        Label glyph = new Label();
        glyph.EnableTheming = false;
        glyph.Font.Name = "webdings";
        glyph.Font.Size = FontUnit.XSmall;
        glyph.Text = (grid.SortDirection == SortDirection.Ascending ? "5" : " 6");

        // Find the column you sorted by
        for (int i = 0; i < grid.Columns.Count; i++)
        {

            string colExpr = grid.Columns[i].SortExpression;
            if (colExpr != "" && colExpr == grid.SortExpression)
            {
                item.Cells[i].Controls.Add(glyph);
            }
        }
    }
    #endregion

    protected void playlistGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton button;
            HtmlGenericControl div = (HtmlGenericControl)e.Row.Cells[playlistCommandColumn].FindControl("deleteDiv");
            if (div != null)
            {
                button = (ImageButton)div.FindControl("deleteSelected");
                if (button != null)
                {

                    button.CommandArgument = string.Format("{0}",
                                ((DataRowView)e.Row.DataItem).Row.ItemArray[playlistDataColumnSongId]);

                    button.OnClientClick = "javascript:return confirm(\"Are you sure you want to remove these songs?\");";
                }
            }

            button = (ImageButton)e.Row.Cells[playlistCommandColumn].FindControl("buttonMoveUp");
            if (button != null)
            {
                button.CommandArgument = string.Format("{0}",
                            ((DataRowView)e.Row.DataItem).Row.ItemArray[playlistDataColumnSongId]);
            }

            button = (ImageButton)e.Row.Cells[playlistCommandColumn].FindControl("buttonMoveDown");
            if (button != null)
            {
                button.CommandArgument = string.Format("{0}",
                            ((DataRowView)e.Row.DataItem).Row.ItemArray[playlistDataColumnSongId]);
            }

            button = (ImageButton)e.Row.Cells[playlistCommandColumn].FindControl("buttonDragDrop");
            if (button != null)
            {
                button.CommandArgument = string.Format("{0}",
                           ((DataRowView)e.Row.DataItem).Row.ItemArray[playlistDataColumnSongId]);
            }

            e.Row.Cells[1].ToolTip = e.Row.Cells[1].Text;
            if (e.Row.Cells[1].Text.Length > 65)
            {
                e.Row.Cells[1].Text = e.Row.Cells[1].Text.Substring(0, 65) + "...";
            }
        }
    }

    protected void PlaylistPageDropDownList_SelectedIndexChanged(Object sender, EventArgs e)
    {

        // Retrieve the pager row.
        GridViewRow pagerRow = playlistGrid.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PlaylistPageDropDownList");

        // Set the PageIndex property to display that page selected by the user.
        playlistGrid.PageIndex = pageList.SelectedIndex;
    }

    protected void PlaylistGrid_DataBound(Object sender, EventArgs e)
    {

        // Retrieve the pager row.
        GridViewRow pagerRow = playlistGrid.BottomPagerRow;

        // pagerRow can be null if result set is empty
        if (pagerRow != null)
        {
            // Retrieve the DropDownList and Label controls from the row.
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PlaylistPageDropDownList");
            Label pageLabel = (Label)pagerRow.Cells[0].FindControl("PlaylistCurrentPageLabel");

            if (pageList != null)
            {

                // Create the values for the DropDownList control based on 
                // the  total number of pages required to display the data
                // source.
                for (int i = 0; i < playlistGrid.PageCount; i++)
                {

                    // Create a ListItem object to represent a page.
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());

                    // If the ListItem object matches the currently selected
                    // page, flag the ListItem object as being selected. Because
                    // the DropDownList control is recreated each time the pager
                    // row gets created, this will persist the selected item in
                    // the DropDownList control.   
                    if (i == playlistGrid.PageIndex)
                    {
                        item.Selected = true;
                    }

                    // Add the ListItem object to the Items collection of the 
                    // DropDownList.
                    pageList.Items.Add(item);

                }

            }

            if (pageLabel != null)
            {

                // Calculate the current page number.
                int currentPage = playlistGrid.PageIndex + 1;

                // Update the Label control with the current page information.
                pageLabel.Text = "Page " + currentPage.ToString() +
                  " of " + playlistGrid.PageCount.ToString() + "  (" + totalPlaylistRowCount + " tracks)";
            }
        }

        Literal literal = (Literal)FindControl("instructions");
        if (literal != null)
        {

            if (totalPlaylistRowCount > 0)
            {
                literal.Text = "<br><div id='instructionDiv' style='#404060;margin-left:10px;margin-right:10px;'>\n\r" +
                    "<p>\n\r" +
                        "Drag and drop songs to reposition them in your playlist or use the arrow buttons to move the songs up or down.\n\r" +
                        "Click the delete button to remove songs from your playlist.\n\r" +
                    "</p></div>";
            }
            else
            {
                literal.Text = "";
            }
        }

    }
}
