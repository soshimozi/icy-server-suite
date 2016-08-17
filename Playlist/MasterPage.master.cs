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

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Template_OnCommand(object sender, CommandEventArgs e)
    {
//        if (e.CommandName == "SendEmail")
//        {
////            TextBox ContactEmail = (TextBox)loginView1.FindControl("ContactEmail");
////            TextBox ContactName = (TextBox)loginView1.FindControl("ContactName");
////            TextBox ContactMessage = (TextBox)loginView1.FindControl("ContactMessage");

//            string script = string.Format("<script type='text/javascript'>window.open('mailto:?to=solanis.realms@gmail.com&from={0}&subject=iZoom Information From {1}&body={2}');</script>", ContactEmailLabel.Text, ContactNameLabel.Text, ContactMessageLabel.Text);
//            Page.ClientScript.RegisterStartupScript(this.GetType(), "sendmail", script);
//        }
    }
}
