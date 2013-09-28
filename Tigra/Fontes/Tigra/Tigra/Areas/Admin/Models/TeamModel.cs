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
    public class TeamModel
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

        public TeamModel()
        {
            //
        }

        public TeamModel(Cell item)
        {
            this.Id = item.CellID;
            this.CellName = item.CellName;
            this.Members = TeamMember.GetTeamMembers(item.CellID);
        }

        public static TeamModel GetTeam(int cellId)
        {
            using (var ctx = new Entities())
            {
                var item = ctx.Cells.FirstOrDefault(i => i.CellID == cellId);
                var model = new TeamModel(item);
                return model;
            }
        }

        public static List<TeamModel> GetTeams()
        {
            using (var ctx = new Entities())
            {
                var list = (from i in ctx.Cells orderby i.CellName ascending select i).ToList();
                var model = new List<TeamModel>();
                list.ForEach(i => model.Add(new TeamModel(i)));
                return model;
            }
        }

    }
}