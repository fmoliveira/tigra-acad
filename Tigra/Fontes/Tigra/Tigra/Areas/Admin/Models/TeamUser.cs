using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tigra.Database;

namespace Tigra.Areas.Admin.Models
{
    public class TeamUser : IComparable
    {

        public int Id { get; set; }

        public string DisplayName { get; set; }

        public TeamUser(UserAccount item)
        {
            this.Id = item.UserID;
            this.DisplayName = item.GetDisplayName();
        }

        public static List<TeamUser> GetTeamUsers()
        {
            List<TeamUser> ret = new List<TeamUser>();

            using (var ctx = new Entities())
            {
                var list = ctx.UserAccounts.ToList();
                list.ForEach(i => ret.Add(new TeamUser(i)));
            }

            ret.Sort();
            return ret;
        }


        public int CompareTo(object obj)
        {
            TeamUser b = (TeamUser)obj;
            return this.DisplayName.CompareTo(b.DisplayName);
        }
    }
}