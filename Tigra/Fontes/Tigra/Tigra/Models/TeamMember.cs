using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tigra.Database;

namespace Tigra.Models
{
    public class TeamMember : IComparable
    {

        public int Id { get; set; }

        public string DisplayName { get; set; }

        public string RoleName { get; set; }

        public TeamMember(Team item)
        {
            this.Id = item.UserID;

            if (item.UserAccount.UserProfile != null && item.UserAccount.UserProfile.FullName.Trim().Length != 0)
            {
                this.DisplayName = item.UserAccount.UserProfile.FullName;
            }
            else
            {
                this.DisplayName = item.UserAccount.Email;
            }

            this.RoleName = item.Role.RoleName;
        }

        public static List<TeamMember>GetTeamMembers(int cellId)
        {
            List<TeamMember> ret = new List<TeamMember>();

            using (var ctx = new Entities())
            {
                var list = ctx.Teams.Where(i => i.CellID == cellId);

                foreach (var item in list)
                {
                    ret.Add(new TeamMember(item));
                }
            }

            ret.Sort();
            return ret;
        }


        public int CompareTo(object obj)
        {
            TeamMember b = (TeamMember)obj;
            return this.DisplayName.CompareTo(b.DisplayName);
        }
    }
}