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
    public class StoriesDetailsModel
    {
        
        [Required]
        public string Id { get; set; }

        public long RevisionId = 0;
        private RequirementCreateModel item;

        [DisplayName("Autor")]
        public UserNameModel UserName { get; set; }

        [DisplayName("Número da revisão")]
        public int RevisionNumber { get; set; }

        [DisplayName("Data da revisão")]
        public DateTime RevisionDate { get; set; }

        [DisplayName("Título")]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Required]
        public string Summary { get; set; }

        [DisplayName("Descrição")]
        [DataType(DataType.Html)]
        [Required]
        public string Text { get; set; }

        public StoriesDetailsModel()
        {
            //
        }

        public StoriesDetailsModel(GetRequirementDetails_Result item)
        {
            this.Id = item.Tag;
            this.RevisionId = item.RevisionID;
            this.UserName = new UserNameModel(item.UserID);
            this.RevisionNumber = item.RevisionNumber;
            this.RevisionDate = item.RevisionDate;
            this.Summary = item.Title;
            this.Text = item.Text;
        }

        public StoriesDetailsModel(RequirementCreateModel req)
        {
            using (var ctx = new Entities())
            {
                var item = ctx.RequirementRevisions.FirstOrDefault(i => i.RevisionID == req.StoryId);
                this.RevisionId = item.RevisionID;
                this.UserName = new UserNameModel(item.UserID);
                this.RevisionNumber = item.RevisionNumber;
                this.RevisionDate = item.RevisionDate;
                this.Summary = item.Title;
                this.Text = ctx.RequirementTexts.FirstOrDefault(i => i.RevisionID == req.StoryId).Text;
            }
        }

    }
}