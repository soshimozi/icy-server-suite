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
using iZoom.Web.Controls;
using Tracklist.Domain;

public partial class LoginPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string action = Request.QueryString["action"];

        if (action == null)
        {
            ShowLogin();
        }
        else if (action.ToLower() == "login")
        {
            ShowLogin();
        }
        else if (action.ToLower() == "recover")
        {
            RecoverPanel.Visible = true;
        }
        else if (action.ToLower() == "register")
        {
            RegisterPanel.Visible = true;
        }
        else
        {
            ShowLogin();
        }

    }

    private void ShowLogin()
    {
        LoginPanel.Visible = true;

        Login login = (Login)LoginPanel.FindControl("LoginControl");
        if (login != null)
        {
            CheckBox rm = (CheckBox)login.FindControl("RememberMe");

            HttpCookie myCookie = Request.Cookies["loginCookie"];
            if (myCookie != null)
            {
                if (myCookie["username"] != null)
                {
                    login.UserName = myCookie["username"];
                    login.RememberMeSet = true;
                }
            }
        }

        Button button = (Button)LoginControl.FindControl("LoginButton");
        if (button != null)
        {
            LoginPanel.DefaultButton = button.UniqueID.Remove(0, LoginPanel.Parent.UniqueID.Length + 1);
        }

        Table table = (Table)LoginControl.FindControl("loginTable");
        if (table != null)
        {
            TextBox userId = (TextBox)table.FindControl("UserName");
            if (userId != null)
            {
                this.Page.ClientScript.RegisterStartupScript(typeof(LoginPage), "focusme", string.Format("document.getElementById('{0}').focus();\r\n ", userId.ClientID), true);
                //table.Attributes["onload"] = "javascript:alert('fuck me');"; // string.Format("javascript:document.getElementById('{0}').focus();", userId.ClientID);
            }
        }
    }

    protected void CancelButton_OnCommand(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "CancelRecover")
        {
            Response.Redirect("loginpage.aspx?action=");
        }
    }


    protected void CaptchaValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // find the captcha control
        CaptchaControl captcha = (CaptchaControl)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Captcha1");
        if (captcha != null)
        {
            args.IsValid = captcha.IsValid;
        }
    }

    protected void PasswordRecover_OnVerifyingAnswer(object sender, EventArgs e)
    {
    }

    protected void PasswordRecover_OnSendingMail(object sender, EventArgs e)
    {
    }

    protected void CreateUserWizard1_CreatingUser(object sender, LoginCancelEventArgs e)
    {
        //if (DataAccess.GetUserInfoByEmail(CreateUserWizard1.UserName) != null)
        //{
        //    e.Cancel = true;
        //    SetError(MembershipCreateStatus.DuplicateUserName);
        //}
        //else
        //{
        //    Guid userId = Guid.NewGuid();

        //    string salt = Security.CreateSalt(10);
        //    string encryptedPassword = Security.CreatePasswordHash(CreateUserWizard1.Password, salt);

        //    DataAccess.RegisterUser(userId, CreateUserWizard1.UserName, salt, encryptedPassword, CreateUserWizard1.Answer, CreateUserWizard1.Question);
        //}
    
    }

    protected void CreateUserWizard1_CreateUserError(object sender, CreateUserErrorEventArgs e)
    {
        SetError(e.CreateUserError);
    }

    private void SetError(MembershipCreateStatus status)
    {
        Label createUserErrorLabel = (Label)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("createUserErrorLabel");

        switch (status)
        {
            case MembershipCreateStatus.DuplicateEmail:
                createUserErrorLabel.Text = "That email is in use, please try another.";
                break;

            case MembershipCreateStatus.DuplicateUserName:
                createUserErrorLabel.Text = "That username is in use, please try another.";
                break;

            case MembershipCreateStatus.InvalidEmail:
                createUserErrorLabel.Text = "Please enter a valid email address";
                break;

            case MembershipCreateStatus.InvalidPassword:
                createUserErrorLabel.Text = "Please enter a valid password.  Valid passwords contain at least 6 characters with at least one non-alphanumeric character.";
                break;

            default:
                createUserErrorLabel.Text = "An error occured creating your account: " + status.ToString() + ".  Please contact an administrator.";
                break;
        }
    }

    protected void LoginControl_LoginError(object sender, EventArgs e)
    {
        Login login = (Login)LoginPanel.FindControl("LoginControl");

        //See if this user exists in the database
        MembershipUser userInfo = Membership.GetUser(login.UserName);

        if (userInfo != null)
        {
            //See if the user is locked out or not approved
            if (!userInfo.IsApproved)
            {
                login.FailureText = "<span class='errorText'>Your account has not yet been approved by the site's administrators. Please try again later...</span><br />";
            }
            else if (userInfo.IsLockedOut)
            {
                login.FailureText = "<span class='errorText'>Your account has been locked out because of a maximum number of incorrect login attempts. You will NOT be able to login until you contact a site administrator and have your account unlocked.</span><br />";
            }
            else
            {
                login.FailureText = "<span class='errorText'>" + login.FailureText + "</span><br />";
            }
        }
        else
        {
            login.FailureText = "<span class='errorText'>" + login.FailureText + "</span><br />";
        }

    }

    protected void LoginControl_LoggedIn(object sender, EventArgs e)
    {
        Login login = (Login)LoginPanel.FindControl("LoginControl");
        MembershipUser userInfo = Membership.GetUser(login.UserName);

        CheckBox rm = (CheckBox)login.FindControl("RememberMe");
        if (rm.Checked)
        {
            Response.Cookies.Remove("loginCookie");
            HttpCookie myCookie = new HttpCookie("loginCookie");
            myCookie.Values.Add("username", login.UserName);
            Response.Cookies.Add(myCookie);
        }
        else
        {
            HttpCookie myCookie = new HttpCookie("loginCookie");
            myCookie.Expires = DateTime.Now.AddDays(-1);
            myCookie.Values.Add("username", login.UserName);
            Response.Cookies.Add(myCookie);
        }

        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
            LoginControl.UserName, rm.Checked, 30);

        string encryptedTicket = FormsAuthentication.Encrypt(ticket);

        // create a cokie and add the encrypted ticket to the
        // cookie as data
        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName,
            encryptedTicket);

        // add cookie to response
        Response.Cookies.Add(authCookie);

        Response.Redirect(FormsAuthentication.GetRedirectUrl(
            LoginControl.UserName, rm.Checked));
    }

}
