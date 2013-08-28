using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tigra.Models
{
    public class AuthCookieModel
    {
        public int UserID { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public bool RememberMe { get; set; }
    }
}