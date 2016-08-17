using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Tracklist.Domain;

/// <summary>
/// Summary description for TracklistMembershipProvider
/// </summary>
public class TracklistMembershipProvider : MembershipProvider
{
    public TracklistMembershipProvider()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public override bool ChangePassword(string username, string oldPassword, string newPassword)
    {
        bool success = false;
        if (Security.VerifyPassword(username, oldPassword))
        {
            UserInfo info = DataAccess.GetUserInfoByEmail(username);
            info.Password = Security.CreatePasswordHash(newPassword, info.Salt);
            DataAccess.UpdateUserInfo(info.UserId, info);

            success = true;
        }

        return success;
    }

    public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
    {
        bool success = false;
        if (Security.VerifyPassword(username, password))
        {
            UserInfo info = DataAccess.GetUserInfoByEmail(username);
            info.Question = newPasswordQuestion;
            info.Hint = newPasswordAnswer;

            DataAccess.UpdateUserInfo(info.UserId, info);

            success = true;
        }

        return success;
    }

    public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
    {
        MembershipUser user = null;
        status = MembershipCreateStatus.ProviderError;

        if (DataAccess.GetUserInfoByEmail(username) != null)
        {
            status = MembershipCreateStatus.DuplicateUserName;
        }
        else
        {
            string salt = Security.CreateSalt(10);

            Guid userid = Guid.NewGuid();
            DataAccess.RegisterUser(userid, username, salt, Security.CreatePasswordHash(password, salt), passwordAnswer, passwordQuestion);

            user = new MembershipUser("TracklistMembershipProvider", username, userid, username, passwordQuestion, "", true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.MinValue, DateTime.MinValue);

            status = MembershipCreateStatus.Success;
        }

        return user;
    }

    public override bool DeleteUser(string username, bool deleteAllRelatedData)
    {
        return false;
    }

    public override bool EnablePasswordReset
    {
        get { return true; }
    }

    public override bool EnablePasswordRetrieval
    {
        get { return true; }
    }

    public override string GetPassword(string username, string answer)
    {
        string password = null;

        UserInfo info = DataAccess.GetUserInfoByEmail(username);
        if (info != null && info.Hint.Equals(answer, StringComparison.OrdinalIgnoreCase))
        {
            password = info.Password;    
        }

        return password;
    }

    public override MembershipUser GetUser(string username, bool userIsOnline)
    {
        UserInfo info = DataAccess.GetUserInfoByEmail(username);
        if (info != null)
        {
            return new MembershipUser("TracklistMembershipProvider", info.Email, info.UserId, info.Email, info.Question, "", true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
        }
        else
        {
            return null;
        }
    }

    public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    {
        UserInfo info = DataAccess.GetUserInfoByUserId((Guid)providerUserKey);
        return new MembershipUser("TracklistMembershipProvider", info.Email, info.UserId, info.Email, info.Question, "", true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
    }

    public override string GetUserNameByEmail(string email)
    {
        return email;
    }

    public override int MaxInvalidPasswordAttempts
    {
        get { return 3; }
    }

    public override int MinRequiredNonAlphanumericCharacters
    {
        get { return 0; }
    }

    public override int MinRequiredPasswordLength
    {
        get { return 6; }
    }

    public override int PasswordAttemptWindow
    {
        get { return 600; }
    }

    public override MembershipPasswordFormat PasswordFormat
    {
        get { return MembershipPasswordFormat.Hashed; }
    }

    public override bool RequiresQuestionAndAnswer
    {
        get { return true; }
    }

    public override bool RequiresUniqueEmail
    {
        get { return false; }
    }

    public override bool ValidateUser(string username, string password)
    {
        return Security.VerifyPassword(username, password);
    }

    public override string ApplicationName
    {
        get
        {
            throw new Exception("The method or operation is not implemented.");
        }
        set
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    public override int GetNumberOfUsersOnline()
    {
        throw new Exception("The method or operation is not implemented.");
    }

    public override string PasswordStrengthRegularExpression
    {
        get { throw new Exception("The method or operation is not implemented."); }
    }

    public override string ResetPassword(string username, string answer)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    public override bool UnlockUser(string userName)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    public override void UpdateUser(MembershipUser user)
    {
        throw new Exception("The method or operation is not implemented.");
    }
}
