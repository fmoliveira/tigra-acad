using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tigra.Database;

namespace Tigra.Models
{
    [DisplayName("Elicitação")]
    public class ElicitationIndexModel
    {

        [Required]
        public int Id { get; set; }

        [DisplayName("Solicitante")]
        public string UserName { get; set; }

        [DisplayName("Data da solicitação")]
        public DateTime RequestDate { get; set; }

        [DisplayName("Descrição")]
        public string Summary { get; set; }

        public ElicitationIndexModel(Elicitation item)
        {
            this.Id = item.ElicitationID;
            this.UserName = item.UserAccount.GetDisplayName();
            this.RequestDate = item.RequestDate;
            this.Summary = item.Summary;
        }

        public static List<ElicitationIndexModel> GetModels(object cell)
        {
            List<ElicitationIndexModel> ret = new List<ElicitationIndexModel>();

            using (var ctx = new Entities())
            {
                int id = ctx.GetCellID(cell);
                var list = (from i in ctx.Elicitations where i.CellID == id select i).ToList();
                list.ForEach(i => ret.Add(new ElicitationIndexModel(i)));
            }

            return ret;
        }

    }
}