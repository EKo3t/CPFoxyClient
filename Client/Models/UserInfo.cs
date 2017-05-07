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

        private string Email { get; set; }

        private string Password { get; set; }


    }
}