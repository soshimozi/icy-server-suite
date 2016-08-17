using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using Tracklist.Domain;

/// <summary>
/// Summary description for Security
/// </summary>
static public class Security
{
    public static string CreateSalt(int size)
    {
        // Generate a cyrpotgraphic random number using the crypto api
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] buff = new byte[size];
        rng.GetBytes(buff);

        // return a base64 string representation of the random number
        return Convert.ToBase64String(buff);
    }

    public static string CreatePasswordHash(string plainText, string salt)
    {
        string saltAndPwd = string.Concat(plainText, salt);
        string hashedPassword =
            FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "SHA1");

        return hashedPassword;
    }

    public static bool VerifyPassword(string email, string password)
    {
        bool passwordMatch = false;

        // Get the salt and pwd from the database based on the user name.
        UserInfo userInfo = DataAccess.GetUserInfoByEmail(email);
        if (userInfo != null)
        {
            string passwordAndSalt = string.Concat(password, userInfo.Salt);

            // now hash them
            string hashedPassword =
                FormsAuthentication.HashPasswordForStoringInConfigFile(passwordAndSalt, "SHA1");

            passwordMatch = hashedPassword.Equals(userInfo.Password);
        }

        return passwordMatch;
    }
}
