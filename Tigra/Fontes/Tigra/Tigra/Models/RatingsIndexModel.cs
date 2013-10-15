using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tigra.Database;

namespace Tigra.Models
{
    public class RatingsIndexModel
    {

        [Required]
        public string Id { get; set; }

        [DisplayName("Autor")]
        public UserNameModel UserName { get; set; }

        [DisplayName("Data")]
        public DateTime Modified { get; set; }

        [DisplayName("Título")]
        public string Summary { get; set; }

        [HiddenInput]
        public decimal? UserFinalRating { get; set; }

        public RatingsIndexModel(GetRatingsIndex_Result item)
        {
            this.Id = item.Tag;
            this.UserName = new UserNameModel(item.UserID);
            this.Modified = item.RevisionDate;
            this.Summary = item.Title;
            this.UserFinalRating = item.UserFinalRating;
        }

        public static List<RatingsIndexModel> GetModels(object cell)
        {
            List<RatingsIndexModel> ret = new List<RatingsIndexModel>();

            using (var ctx = new Entities())
            {
                int parent = ctx.GetCellID(cell);
                var list = ctx.GetRatingsIndex(parent, Authentication.GetLoggedUser().UserID).ToList();
                list.ForEach(i => ret.Add(new RatingsIndexModel(i)));
            }

            return ret;
        }

    }
}