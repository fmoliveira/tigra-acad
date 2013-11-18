using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tigra.Database;

namespace Tigra.Models
{
    public class RequirementsIndexModel
    {

        [Required]
        public string Id { get; set; }

        [DisplayName("Autor")]
        public UserNameModel UserName { get; set; }

        [DisplayName("Data")]
        public DateTime Modified { get; set; }

        [DisplayName("Estado")]
        public string Status { get; set; }

        [DisplayName("Título")]
        public string Summary { get; set; }

        public bool Published = false;
        public bool Rated = false;
        public bool Approved = false;
        public DateTime? LatestBaseline = null;

        public RequirementsIndexModel(GetRequirementsIndex_Result item)
        {
            this.Id = item.Tag;
            this.UserName = new UserNameModel(item.UserID);
            this.Modified = item.RevisionDate;
            this.Summary = item.Title;

            using (var ctx = new Entities())
            {
                int logged = Authentication.GetLoggedUser().UserID;
                this.Published = item.Published;
                this.Rated = (ctx.RequirementRatings.FirstOrDefault(i => i.RevisionID == item.RevisionID) != null);
                this.Approved = (ctx.RequirementRatings.FirstOrDefault(i => i.RevisionID == item.RevisionID && i.Approved == true) != null);

                var bl = (from i in ctx.Baselines orderby i.SetDate descending select i.SetDate).Take(1);

                if (bl != null && bl.Count() == 1)
                {
                    this.LatestBaseline = bl.ToArray()[0];
                }
            }

            if (item.BaselineDate.HasValue)
            {
                this.Status = "Implementado";

                if (this.LatestBaseline.HasValue && this.LatestBaseline.Value >= item.BaselineDate.Value)
                {
                    this.Status = "Baseline";
                }
                else
                {
                    this.Status = "Implementado";
                }
            }
            else if (this.Published == false)
            {
                this.Status = "Em edição";
            }
            else if (this.Rated == false)
            {
                this.Status = "Ag. avaliação";
            }
            else if (this.Approved == false)
            {
                this.Status = "Reprovado";
                this.Published = false;
            }
            else
            {
                this.Status = "Aprovado";
            }
        }

        public static List<RequirementsIndexModel> GetModels(object cell)
        {
            List<RequirementsIndexModel> ret = new List<RequirementsIndexModel>();

            using (var ctx = new Entities())
            {
                int parent = ctx.GetCellID(cell);
                var list = ctx.GetRequirementsIndex(parent, RequirementTypes.Requirement, null).ToList();
                list.ForEach(i => ret.Add(new RequirementsIndexModel(i)));
            }

            return ret;
        }

    }
}