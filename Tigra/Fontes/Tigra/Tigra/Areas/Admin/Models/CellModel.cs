using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tigra.Database;

namespace Tigra.Areas.Admin.Models
{
    [DisplayName("Células")]
    public class CellModel
    {

        [Required]
        public int Id { get; set; }

        [DisplayName("Nome da célula")]
        [StringLength(50)]
        [Required]
        public string CellName { get; set; }

        [DisplayName("Descrição")]
        [DataType(DataType.MultilineText)]
        [StringLength(250)]
        [Required]
        public string Description { get; set; }

        public CellModel()
        {
            //
        }

        public CellModel(Cell item)
        {
            this.Id = item.CellID;
            this.CellName = item.CellName;
            this.Description = item.Description;
        }

        public static List<Cell> GetCellTags()
        {
            using (var ctx = new Entities())
            {
                var list = (from i in ctx.Cells orderby i.CellName ascending select i).ToList();
                return list;
            }
        }

        public static List<CellModel> GetCells()
        {
            using (var ctx = new Entities())
            {
                var list = (from i in ctx.Cells orderby i.CellName ascending select i).ToList();
                var ret = new List<CellModel>();
                list.ForEach(i => ret.Add(new CellModel(i)));
                return ret;
            }
        }

    }
}