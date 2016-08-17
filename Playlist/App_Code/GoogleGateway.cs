using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for GoogleGateway
/// </summary>
public class GoogleGateway
{
    private static string apiKey;
    static GoogleGateway()
    {
        //
        // TODO: Add constructor logic here
        //
        apiKey = ConfigurationManager.AppSettings["apiKey"];
    }

    public static string APIKey
    {
        get
        {
            return apiKey;
        }
    }

}
