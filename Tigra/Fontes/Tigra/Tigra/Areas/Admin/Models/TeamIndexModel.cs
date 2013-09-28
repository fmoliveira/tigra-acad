using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using Tigra.Database;

namespace Tigra.Areas.Admin.Models
{
    [DisplayName("Equipes")]
    public class TeamIndexModel
    {

        [Required]
        public int Id { get; set; }

        [DisplayName("Nome da célula")]
        [StringLength(50)]
        [Required]
        public string CellName { get; set; }

        [DisplayName("Membros")]
        [UIHint("List_TeamMember")]
        public List<TeamMember> Members { get; set; }

        public TeamIndexModel()
        {
            //
        }

        public TeamIndexModel(Cell item)
        {
            this.Id = item.CellID;
            this.CellName = item.CellName;
            this.Members = TeamMember.GetTeamMembers(item.CellID);
        }

        public static List<TeamIndexModel> GetTeams()
        {
            using (var ctx = new Entities())
            {
                var list = (from i in ctx.Cells orderby i.CellName ascending select i).ToList();
                var model = new List<TeamIndexModel>();
                list.ForEach(i => model.Add(new TeamIndexModel(i)));
                return model;
            }
        }

    }
}