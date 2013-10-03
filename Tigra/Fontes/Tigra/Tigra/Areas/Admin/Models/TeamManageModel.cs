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
    public class TeamManageModel
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

        [DisplayName("Papel")]
        [UIHint("Choice_Roles")]
        public List<RoleModel> Roles { get; set; }

        public TeamManageModel()
        {
            //
        }

        public TeamManageModel(Cell item)
        {
            this.Id = item.CellID;
            this.CellName = item.CellName;
            this.Members = TeamMember.GetTeamMembers(item.CellID);
            this.Roles = RoleModel.GetRoles();
        }

        public static TeamManageModel GetTeam(int cellId)
        {
            using (var ctx = new Entities())
            {
                var item = ctx.Cells.FirstOrDefault(i => i.CellID == cellId);
                var model = new TeamManageModel(item);
                return model;
            }
        }

    }
}