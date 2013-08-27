using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tigra.Models
{
    public class LoginModel
    {

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(80)]
        public string Email { get; set; }

        [DisplayName("Senha")]
        [DataType(DataType.Password)]
        [StringLength(50)]
        public string Password { get; set; }

    }
}