using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tigra.Database;

namespace Tigra.Areas.Admin.Models
{
    [DisplayName("Permissões")]
    public class RoleModel
    {

        [Required]
        public int Id { get; set; }

        [DisplayName("Nome do papel")]
        [StringLength(50)]
        [Required]
        public string RoleName { get; set; }

        public RoleModel()
        {
            //
        }

        public RoleModel(Role item)
        {
            this.Id = item.RoleID;
            this.RoleName = item.RoleName;
        }

        public static List<RoleModel> GetRoles()
        {
            using (var ctx = new Entities())
            {
                var list = (from i in ctx.Roles orderby i.RoleName ascending select i).ToList();
                var ret = new List<RoleModel>();
                list.ForEach(i => ret.Add(new RoleModel(i)));
                return ret;
            }
        }

    }
}