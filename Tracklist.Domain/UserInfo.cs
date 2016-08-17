using System;
using System.Collections.Generic;
using System.Text;

namespace Tracklist.Domain
{
    public class UserInfo
    {
        private string _email;
        private Guid _userId;
        private string _password;
        private string _salt;
        private string _hint;
        private string _question;

        public string Question
        {
            get { return _question; }
            set { _question = value; }
        }

        public string Hint
        {
            get { return _hint; }
            set { _hint = value; }
        }

        public string Salt
        {
            get { return _salt; }
            set { _salt = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public Guid UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

    }
}
