<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TracklistForm.ascx.cs" Inherits="controls_Tracklist" %>
    <!-- Pagetitle -->
    <asp:Panel ID="SearchPanel" runat="server">
    <div class="searchsubform" style="font-size:85%;">
        <fieldset>
            <p><asp:Label CssClass="top" ID="Label2" runat="server" AssociatedControlID="textTrackSearch">Search Tracks:</asp:Label>&nbsp;
            <asp:TextBox runat="server" id="textTrackSearch" CssClass="field" />&nbsp;
            <asp:Button CssClass="button" OnCommand="SearchButtons_Command" runat="server" ID="TrackSearchButton" Text="Search" ToolTip="Search For Tracks" CommandName="Search" />&nbsp;<asp:Button CssClass="button" OnCommand="SearchButtons_Command" runat="server" ID="buttonClearSearch" Text="Clear" ToolTip="Clear Current Search" CommandName="ClearSearch" /><br />
            </p>
         </fieldset>
    </div>
    </asp:Panel>
    <asp:GridView 
        BorderStyle="None"
        BorderColor="#000000"
        BackColor="#ffffff" 
        AllowSorting="False" 
        GridLines="None" 
        Width="100%" 
        ID="tracklistGrid" 
        PageSize="15" 
        runat="server" 
        AllowPaging="True" 
        AutoGenerateColumns="False"
        DataSourceID="TracklistDataSource" 
        OnRowDataBound="trackGrid_RowDataBound" 
        OnRowCreated="Grid_RowCreated"
        AlternatingRowStyle-CssClass="PlaylistGridAlternateRow" 
        RowStyle-CssClass="PlaylistGridRow" 
        OnDataBound="tracklistGrid_DataBound" 
        OnRowCommand="tracklistGrid_RowCommand"
        HeaderStyle-BackColor="#f6f6ff" 
        PagerStyle-BackColor="#f6f6ff"
        RowStyle-Wrap="true"
        AlternatingRowStyle-Wrap="true" 
        ShowFooter="true"
        FooterStyle-BackColor="#f6f6ff" OnPageIndexChanged="tracklistGrid_PageIndexChanged"
        >
        <Columns>
            <asp:TemplateField><ItemTemplate>&nbsp;</ItemTemplate></asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="12px"><ItemTemplate><asp:ImageButton ToolTip="Add Track" ID="buttonAddTrack" runat="server" ImageUrl="~/images/newadd.png" CommandName="AddTrack" /></ItemTemplate></asp:TemplateField>
            <asp:BoundField ItemStyle-Width="50%" DataField="Title" HeaderText="Title" SortExpression="Description" />
            <asp:BoundField DataField="Album" HeaderText="Album" SortExpression="Album" />
            <asp:BoundField DataField="Artist" HeaderText="Artist" SortExpression="Artist" />
            <asp:TemplateField ItemStyle-Width="12px"><ItemTemplate><asp:ImageButton ToolTip="Listen To Track" ID="buttonListenTrack" runat="server" ImageUrl="~/images/AudioHS.png" CommandName="ListenTrack" /></ItemTemplate> </asp:TemplateField>
        </Columns>
        <PagerTemplate>
        <table width="100%">                    
        <tr>                    
          <td style="width:30%; text-align:left;">
              <asp:LinkButton Font-Names="Webdings" ID="TracklistFirstPageButton" Text="7" BorderStyle="None" runat="server" CommandArgument="First" CommandName="Page" />
              <asp:LinkButton Font-Names="Webdings" ID="TracklistPreviousPageButton" Text="3" BorderStyle="None" runat="server" CommandArgument="Prev" CommandName="Page" />
              <asp:LinkButton Font-Names="Webdings" ID="TracklistNextPageButton" Text="4" BorderStyle="None" runat="server" CommandArgument="Next" CommandName="Page" />
              <asp:LinkButton Font-Names="Webdings" ID="TracklistLastPageButton" Text="8" BorderStyle="None" runat="server" CommandArgument="Last" CommandName="Page" />
                     
            &nbsp;&nbsp;<asp:label id="MessageLabel"
              forecolor="Blue"
              text="Jump To Page:" 
              runat="server"/>
            <asp:dropdownlist id="TracklistPageDropDownList"
              autopostback="true"
              onselectedindexchanged="TracklistPageDropDownList_SelectedIndexChanged" 
              runat="server" />
          </td>   
                  
          <td style="width:50%; text-align:right">
                  
            <asp:label id="TracklistCurrentPageLabel"
              forecolor="Blue"
              runat="server"/>
          </td>
                                        
        </tr>            
        </table>
        </PagerTemplate>
        <EmptyDataTemplate>
            There are no tracks available.
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:SqlDataSource ID="TracklistDataSource" OnSelected="TracklistDataSource_Selected" runat="server" ConnectionString="<%$ ConnectionStrings:soshimo %>">
    </asp:SqlDataSource>
    <asp:Literal runat="server" id="instructions">
    </asp:Literal>
        
