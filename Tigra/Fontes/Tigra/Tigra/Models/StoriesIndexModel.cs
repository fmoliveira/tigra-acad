using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tigra.Database;

namespace Tigra.Models
{
    [DisplayName("Histórias")]
    public class StoriesIndexModel
    {

        [Required]
        public int Id { get; set; }

        [DisplayName("Solicitante")]
        public string UserName { get; set; }

        [DisplayName("Data da solicitação")]
        public DateTime RequestDate { get; set; }

        [DisplayName("Título")]
        public string Summary { get; set; }

        public StoriesIndexModel(Story item)
        {
            this.Id = item.StoryID;
            this.UserName = item.UserAccount.GetDisplayName();
            this.RequestDate = item.RequestDate;
            this.Summary = item.Summary;
        }

        public static List<StoriesIndexModel> GetModels(object cell)
        {
            List<StoriesIndexModel> ret = new List<StoriesIndexModel>();

            using (var ctx = new Entities())
            {
                int id = ctx.GetCellID(cell);
                var list = (from i in ctx.Stories where i.CellID == id select i).ToList();
                list.ForEach(i => ret.Add(new StoriesIndexModel(i)));
            }

            return ret;
        }

    }
}