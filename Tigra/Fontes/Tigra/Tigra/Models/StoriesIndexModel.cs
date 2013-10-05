using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tigra.Database;

namespace Tigra.Models
{
    public class StoriesIndexModel
    {

        [Required]
        public string Id { get; set; }

        [DisplayName("Autor")]
        public UserNameModel UserName { get; set; }

        [DisplayName("Data")]
        public DateTime Modified { get; set; }

        [DisplayName("Título")]
        public string Summary { get; set; }

        public StoriesIndexModel(GetRequirementsIndex_Result item)
        {
            this.Id = item.Tag;
            this.UserName = new UserNameModel(item.UserID);
            this.Modified = item.RevisionDate;
            this.Summary = item.Title;
        }

        public static List<StoriesIndexModel> GetModels(object cell)
        {
            List<StoriesIndexModel> ret = new List<StoriesIndexModel>();

            using (var ctx = new Entities())
            {
                int parent = ctx.GetCellID(cell);
                var list = ctx.GetRequirementsIndex(parent, RequirementTypes.Story, null).ToList();
                list.ForEach(i => ret.Add(new StoriesIndexModel(i)));
            }

            return ret;
        }

    }
}