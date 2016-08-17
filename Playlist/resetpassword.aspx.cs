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
using Tracklist.Domain;

public partial class resetpassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            UserInfo info = DataAccess.GetUserInfoByEmail(emailAddress.Value);
            if (info == null)
            {
                passwordErrorDiv.Style["visibility"] = "visible";
                passwordErrorDiv.InnerText = "Invalid email address.";
            }
            else if (!Security.VerifyPassword(emailAddress.Value, oldPassword.Value))
            {
                passwordErrorDiv.Style["visibility"] = "visible";
                passwordErrorDiv.InnerText = "Invalid password.";
            }
            else if (!newPassword.Value.Equals(verifyPassword.Value))
            {
                passwordErrorDiv.Style["visibility"] = "visible";
                passwordErrorDiv.InnerText = "Passwords do not match.";
            }
            else if (newPassword.Value.Length < 6)
            {
                passwordErrorDiv.Style["visibility"] = "visible";
                passwordErrorDiv.InnerText = "Please enter at least 6 characters.";
            }
            else
            {
                info.Password = Security.CreatePasswordHash(newPassword.Value, info.Salt);
                info.Hint = hint.Value;

                DataAccess.UpdateUserInfo(info.UserId, info);

                passwordErrorDiv.Style["visibility"] = "visible";
                passwordErrorDiv.Style["color"] = "Black";
                passwordErrorDiv.InnerText = "Password updated.";
            }
        }
    }
}
