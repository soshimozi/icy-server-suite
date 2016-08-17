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
using ObjectQuery;

public partial class controls_Tracklist : System.Web.UI.UserControl
{
    int trackColumnAdd = 1;
    int trackColumnTitle = 2;
    int trackColumnArtist = 4;
    int trackColumnAlbum = 3;
    int trackColumnListen = 5;
    int tracklistDataColumnSongId = 0;
    int totalTracklistRowCount = 0;
    string filter = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        filter = string.Empty;
            if (Session["trackfilter"] != null && !string.IsNullOrEmpty((string)Session["trackfilter"]))
            {
                filter = (string)Session["trackfilter"];

                if (!IsPostBack)
                {
                    textTrackSearch.Text = filter;
                }
            }


            InitTracklistDatasource();

            if (!Page.IsPostBack)
            {
                tracklistGrid.PageIndex = 0;

                if (Session["tracklistpage"] != null && !string.IsNullOrEmpty((string)Session["tracklistpage"]))
                {
                    int index;
                    if (int.TryParse((string)Session["tracklistpage"], out index))
                    {
                        // Set the PageIndex property to display that page selected by the user.
                        tracklistGrid.PageIndex = index;
                    }
                }
            }

        Button button = (Button)SearchPanel.FindControl("TrackSearchButton");
        if (button != null)
        {
            SearchPanel.DefaultButton = button.UniqueID.Remove(0, SearchPanel.Parent.UniqueID.Length + 1);
        }
    }

    protected void TracklistDataSource_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        totalTracklistRowCount = e.AffectedRows;
    }

    private void InitTracklistDatasource()
    {

        // remove any filter parameters that exist
        if (TracklistDataSource.SelectParameters.Count > 0)
        {
            TracklistDataSource.SelectParameters.Clear();
        }

        if (filter != "")
        {
            TracklistDataSource.SelectParameters.Add(new Parameter("filter", textTrackSearch.Text.GetTypeCode(), textTrackSearch.Text));
            TracklistDataSource.SelectCommand = "Playlist.uspSearchTracklist";
        }
        else
        {
            TracklistDataSource.SelectCommand = "Playlist.uspGetAllTracks";
        }

        TracklistDataSource.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
    }


    protected void SearchButtons_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "clearsearch")
        {
            textTrackSearch.Text = string.Empty;
            filter = string.Empty;

            Session["trackfilter"] = string.Empty;

            InitTracklistDatasource();
            tracklistGrid.DataBind();

            // reset page index
            Session["tracklistpage"] = string.Empty;
            tracklistGrid.PageIndex = 0;

        }
        else if (e.CommandName.ToLower() == "search")
        {
            // get the search text
            filter = textTrackSearch.Text;
            Session["trackfilter"] = filter;

            // reset page index
            Session["tracklistpage"] = string.Empty;

            InitTracklistDatasource();
            tracklistGrid.DataBind();

            tracklistGrid.PageIndex = 0;
        }
    }

    protected void tracklistGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //// kludge for now until we hookup playlist id into this page properly
        if (e.CommandName.ToLower() == "addtrack")
        {
            int songId;
            int.TryParse((string)e.CommandArgument, out songId);
            TrackInfo track = DataAccess.GetTrackInfo(songId);
            if (track != null)
            {
                UserInfo user = DataAccess.GetUserInfoByEmail(HttpContext.Current.User.Identity.Name);
                if (user != null)
                {

                    // only if we didnt' find song
                    DataAccess.AddPlaylistEntry(user.UserId, track.TrackId);
                }

                // finally rebind playlist grid to reflect added record
                tracklistGrid.DataBind();
            }
        } else if (e.CommandName.ToLower() == "listentrack")
        {
            int songId;
            int.TryParse((string)e.CommandArgument, out songId);

            // do a popup here
            Response.Redirect(string.Format("SongPreview.aspx?trackId={0}", songId));

            TrackInfo track = DataAccess.GetTrackInfo(songId);
            if( track != null )
            {
                // redirect user to song (should open media player hopefully)
                Response.ContentType = "audio/x-mpeg";
                Response.Redirect(track.Url.Replace("'", "%27"));
            }
        }

    }

    //private TrackInfo GetUserSong(int songId, List<TrackInfo> playlist)
    //{
    //    TrackInfo info = null;

    //    return info;
    //    //Song song = null;
    //    //// look for that song in playlist
    //    //foreach (Song searchSong in playlist.SongList)
    //    //{
    //    //    if (searchSong.Id == songId)
    //    //    {
    //    //        song = searchSong;
    //    //        break;
    //    //    }
    //    //}
    //    //return song;
    //}

    protected void Grid_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            AddSortGlyph((GridView)sender, e.Row);
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

    protected void trackGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        UserInfo user = DataAccess.GetUserInfoByEmail(HttpContext.Current.User.Identity.Name);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[trackColumnAlbum].ToolTip = e.Row.Cells[trackColumnAlbum].Text;
            if (e.Row.Cells[trackColumnAlbum].Text.Length > 20)
            {
                e.Row.Cells[trackColumnAlbum].Text = e.Row.Cells[trackColumnAlbum].Text.Substring(0, 20) + "...";
            }

            ImageButton listenButton = (ImageButton)e.Row.Cells[trackColumnListen].FindControl("buttonListenTrack");
            if (listenButton != null)
            {
                listenButton.CommandArgument = string.Format("{0}", ((DataRowView)e.Row.DataItem).Row.ItemArray[tracklistDataColumnSongId]);
            }

            ImageButton addButton = (ImageButton)e.Row.Cells[trackColumnAdd].FindControl("buttonAddTrack");
            if (addButton != null)
            {
                e.Row.Cells[trackColumnAdd].Controls.Remove(addButton);
            }

            if (user != null)
            {
                long songId = (long)((DataRowView)e.Row.DataItem).Row.ItemArray[tracklistDataColumnSongId];

                if (addButton != null)
                {
                    addButton.CommandArgument = string.Format("{0}", songId);
                    e.Row.Cells[trackColumnAdd].Controls.Add(addButton);
                }


                List<TrackInfo> trackList = DataAccess.GetTracklist(user.UserId);
                if (songInPlaylist(trackList, songId))
                {
                    Image img = new Image();
                    img.ImageUrl = "~/images/extra4.gif";
                    img.ToolTip = "This song is in your playlist";

                    if (addButton != null)
                    {
                        e.Row.Cells[trackColumnAdd].Controls.Remove(addButton);
                        e.Row.Cells[0].Controls.Add(img);
                    }
                }

            }

        }
        else if( e.Row.RowType == DataControlRowType.Footer )
        {
            if (user != null)
            {
                e.Row.Cells[0].ColumnSpan = 3;
                e.Row.Cells[0].VerticalAlign = VerticalAlign.Middle;

                Image imageControl = new Image();
                Label labelControl = new Label();

                imageControl.ImageUrl = "~/images/extra4.gif";
                imageControl.ImageAlign = ImageAlign.Middle;

                labelControl.Text = "Indicates song is in your playlist";

                // create a new image
                e.Row.Cells[0].Controls.Add(imageControl);
                e.Row.Cells[0].Controls.Add(labelControl);
            }
        }
    }

    private bool songInPlaylist(List<TrackInfo> trackList, long songId)
    {
        string sql = "FROM TrackInfo AS info WHERE info.TrackId = ?";
        IQuery<TrackInfo> query = QueryManager<TrackInfo>.GetQuery(sql, songId);
        return query.FindOne(trackList) != null;
    }

    protected void TracklistPageDropDownList_SelectedIndexChanged(Object sender, EventArgs e)
    {

        // Retrieve the pager row.
        GridViewRow pagerRow = tracklistGrid.BottomPagerRow;

        // Retrieve the PageDropDownList DropDownList from the bottom pager row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("TracklistPageDropDownList");

        Session["tracklistpage"] = pageList.SelectedIndex.ToString();

        // Set the PageIndex property to display that page selected by the user.
        tracklistGrid.PageIndex = pageList.SelectedIndex;
    }

    protected void tracklistGrid_DataBound(Object sender, EventArgs e)
    {

        // Retrieve the pager row.
        GridViewRow pagerRow = tracklistGrid.BottomPagerRow;

        if (pagerRow != null)
        {
            // Retrieve the DropDownList and Label controls from the row.
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("TracklistPageDropDownList");
            Label pageLabel = (Label)pagerRow.Cells[0].FindControl("TracklistCurrentPageLabel");

            if (pageList != null)
            {

                // Create the values for the DropDownList control based on 
                // the  total number of pages required to display the data
                // source.
                for (int i = 0; i < tracklistGrid.PageCount; i++)
                {

                    // Create a ListItem object to represent a page.
                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());

                    //// If the ListItem object matches the currently selected
                    //// page, flag the ListItem object as being selected. Because
                    //// the DropDownList control is recreated each time the pager
                    //// row gets created, this will persist the selected item in
                    //// the DropDownList control.   
                    if (i == tracklistGrid.PageIndex)
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
                int currentPage = tracklistGrid.PageIndex + 1;

                // Update the Label control with the current page information.
                pageLabel.Text = "Page " + currentPage.ToString() +
                  " of " + tracklistGrid.PageCount.ToString() + "  (" + totalTracklistRowCount + " tracks)";

            }
        }

    }



    protected void tracklistGrid_PageIndexChanged(object sender, EventArgs e)
    {
        Session["tracklistpage"] = tracklistGrid.PageIndex.ToString();
    }
}
