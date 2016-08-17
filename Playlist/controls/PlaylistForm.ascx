<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PlaylistForm.ascx.cs" Inherits="controls_PlaylistDetails" %>
        <asp:GridView CssClass="PlaylistGrid"
            BorderStyle="None"
            BorderColor="#000000"
            BackColor="#ffffff" 
            AlternatingRowStyle-Wrap="true"
            RowStyle-Wrap="true"
            AllowSorting="false" 
            GridLines="None" 
            Width="100%" 
            ID="playlistGrid" 
            PageSize="15" 
            runat="server" 
            AllowPaging="False" 
            AutoGenerateColumns="False"
            DataSourceID="PlaylistDataSource" 
            OnRowDataBound="playlistGrid_RowDataBound" 
            OnRowCreated="Grid_RowCreated" 
            AlternatingRowStyle-CssClass="PlaylistGridAlternateRow" 
            RowStyle-CssClass="PlaylistGridRow" 
            OnDataBound="PlaylistGrid_DataBound"
            OnRowCommand="playlistGrid_RowCommand"
            HeaderStyle-BackColor="#f6f6ff" 
            PagerStyle-BackColor="#f6f6ff"  
            FooterStyle-BackColor="#f6f6ff"
            ShowFooter="true"
        >
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate></ItemTemplate>
                    <ItemStyle Width="2px" />
                </asp:TemplateField>
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" >
                <ItemStyle Width="100%" Wrap="false" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>&nbsp;</ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remove Entry">
                    <ItemTemplate><div style="text-align:center;"><asp:CheckBox TextAlign="Right" ID="deleteSong" AutoPostBack="false" runat="server" Checked="false" Text=" " /><asp:ImageButton ID="buttonDragDrop" runat="server" ImageUrl="~/images/onepixel.png" CommandName="MoveTrack" Width="1" Height="1"></asp:ImageButton></div></ItemTemplate>
                </asp:TemplateField>
            </Columns>
            
            <PagerTemplate>
            <table width="100%">                    
            <tr>                    
              <td style="width:70%; text-align:left;">
                  <asp:LinkButton Font-Names="Webdings" ID="PlaylistFirstPageButton" Text="7" BorderStyle="None" runat="server" CommandArgument="First" CommandName="Page" />
                  <asp:LinkButton Font-Names="Webdings" ID="PlaylistPreviousPageButton" Text="3" BorderStyle="None" runat="server" CommandArgument="Prev" CommandName="Page" />
                  <asp:LinkButton Font-Names="Webdings" ID="PlaylistNextPageButton" Text="4" BorderStyle="None" runat="server" CommandArgument="Next" CommandName="Page" />
                  <asp:LinkButton Font-Names="Webdings" ID="PlaylistLastPageButton" Text="8" BorderStyle="None" runat="server" CommandArgument="Last" CommandName="Page" />
                &nbsp;&nbsp;
                <asp:label id="MessageLabel"
                  forecolor="Blue"
                  text="Jump To Page:" 
                  runat="server"/>
                <asp:dropdownlist id="PlaylistPageDropDownList"
                  autopostback="true"
                  onselectedindexchanged="PlaylistPageDropDownList_SelectedIndexChanged" 
                  runat="server"/>
              </td>   
                      
              <td style="width:30%; text-align:right">
                      
                <asp:label id="PlaylistCurrentPageLabel"
                  forecolor="Blue"
                  runat="server"/>
              </td>

            </tr>                    
            </table>
            </PagerTemplate>
            <EmptyDataTemplate>
                Your playlist is empty.
            </EmptyDataTemplate>
            <RowStyle CssClass="PlaylistGridRow" />
            <PagerStyle BackColor="#f6f6ff" />
            <HeaderStyle BackColor="#f6f6ff" />
            <AlternatingRowStyle CssClass="PlaylistGridAlternateRow" />
        </asp:GridView>
        <asp:SqlDataSource ID="PlaylistDataSource" OnSelected="PlaylistDataSource_Selected" runat="server" ConnectionString="<%$ ConnectionStrings:soshimo %>">
            <SelectParameters>
                <asp:Parameter Name="userid" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:HiddenField ID="totalPlaylistCountHiddenField" runat="server" />
        <asp:Literal runat="server" id="instructions">
        </asp:Literal>