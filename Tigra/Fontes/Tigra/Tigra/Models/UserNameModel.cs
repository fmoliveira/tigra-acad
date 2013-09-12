using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tigra.Database;

namespace Tigra.Models
{
    public class UserNameModel
    {

        private List<UserNameModel> _AllUsers = null;

        private List<UserNameModel> AllUsers
        {
            get
            {
                if (_AllUsers == null)
                {
                    _AllUsers = new List<UserNameModel>();

                    using (var ctx = new Entities())
                    {
                        ctx.UserAccounts.ToList().ForEach(i => _AllUsers.Add(new UserNameModel(i)));
                    }
                }

                return _AllUsers;
            }
        }

        public int UserID { get; set; }

        public string DisplayName { get; set; }

        public UserNameModel(int uid)
        {
            this.UserID = uid;
            this.DisplayName = AllUsers.FirstOrDefault(i => i.UserID == UserID).DisplayName;
        }

        private UserNameModel(UserAccount user)
        {
            this.UserID = user.UserID;
            this.DisplayName = user.GetDisplayName();
        }

    }
}