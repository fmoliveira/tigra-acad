using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tigra.Database;

namespace Tigra.Models
{
    public class MyProfileModel
    {
        public string FullName { get; set; }

        public MyProfileModel()
        {
            //
        }

        public MyProfileModel(UserProfile up)
        {
            this.FullName = up.FullName;
        }
    }
}