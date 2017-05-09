using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.ViewModels
{
    public class UserDetails
    {        
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}