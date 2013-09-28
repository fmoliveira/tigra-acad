using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tigra.Database;

namespace Tigra.Areas.Admin.Models
{
    public class TeamMember : IComparable
    {

        public int Id { get; set; }

        public string DisplayName { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public TeamMember(Team item)
        {
            this.Id = item.UserID;
            this.DisplayName = item.UserAccount.GetDisplayName();
            this.RoleId = item.RoleID;
            this.RoleName = item.Role.RoleName;
        }

        public static List<TeamMember>GetTeamMembers(int cellId)
        {
            List<TeamMember> ret = new List<TeamMember>();

            using (var ctx = new Entities())
            {
                var list = ctx.Teams.Where(i => i.CellID == cellId).ToList();
                list.ForEach(i => ret.Add(new TeamMember(i)));
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