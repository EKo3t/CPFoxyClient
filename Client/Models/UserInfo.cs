using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.Models.UserSettings
{
    public class UserInfo
    {
        public UserInfo(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }

        public string Password { get; set; }


    }
}