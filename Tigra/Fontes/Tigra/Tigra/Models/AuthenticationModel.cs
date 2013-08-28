using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tigra.Models
{
    public class AuthenticationModel
    {

        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}