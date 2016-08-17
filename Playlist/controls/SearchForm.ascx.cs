using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class controls_SearchForm : System.Web.UI.UserControl
{
    const string loadScriptNoSearch = @"
        <script language=""Javascript"" type=""text/javascript"">    
            function OnLoad() {      
                // Create a search control      
                var searchControl = new GSearchControl();      

                // Add searchers
                var options = new GsearcherOptions();
                options.setExpandMode(GSearchControl.EXPAND_MODE_CLOSED);
                searchControl.addSearcher(new GvideoSearch(), options);

                var ws = new GwebSearch();
                options = new GsearcherOptions();
                options.setExpandMode(GSearchControl.EXPAND_MODE_OPEN);

                searchControl.addSearcher(ws, options);
                ws.setResultSetSize(GSearch.LARGE_RESULTSET);
               
                // Tell the searcher to draw itself and tell it where to attach      
                searchControl.draw(document.getElementById(""searchcontrol""));      
                }

            OnLoad();
        </script>";

    const string loadScriptSearch = @"
        <script language=""Javascript"" type=""text/javascript"">    
            function OnLoad() {{      
                // Create a search control      
                var searchControl = new GSearchControl();      

                // Add searchers
                var options = new GsearcherOptions();
                options.setExpandMode(GSearchControl.EXPAND_MODE_CLOSED);
                searchControl.addSearcher(new GvideoSearch(), options);

                // web search, open
                var ws = new GwebSearch();
                options = new GsearcherOptions();
                options.setExpandMode(GSearchControl.EXPAND_MODE_OPEN);

                searchControl.addSearcher(ws, options);
                ws.setResultSetSize(GSearch.LARGE_RESULTSET);

                // Tell the searcher to draw itself and tell it where to attach      
                searchControl.draw(document.getElementById(""searchcontrol""));      
                searchControl.execute(""{0}"");
                }}

            OnLoad();
        </script>";

    string searchText;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.ClientScript.IsStartupScriptRegistered(this.GetType(), "onload"))
        {
            if (SearchText != null && SearchText != "")
            {
                string text = string.Format(loadScriptSearch, Request.QueryString["search"]);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", text);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", loadScriptNoSearch);
            }
        }
    }

    [Bindable(true), Category("Display")]
    public string SearchText
    {
        get { return searchText; }
        set { searchText = value; }
    }
}
