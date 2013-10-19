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

        [DisplayName("Autor")]
        public UserNameModel UserName { get; set; }

        [DisplayName("Número da revisão")]
        public int RevisionNumber { get; set; }

        [DisplayName("Data da revisão")]
        public DateTime RevisionDate { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Título")]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Required]
        public string Summary { get; set; }

        [DisplayName("Descrição")]
        [DataType(DataType.Html)]
        [Required]
        public string Text { get; set; }

        public bool Published = false;
        public bool MeRated = false;
        public bool TeamRated = false;
        public bool Approved = false;

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
            this.Published = item.Published;

            using (var ctx = new Entities())
            {
                int logged = Authentication.GetLoggedUser().UserID;
                this.MeRated = (item.UserID == logged) || (ctx.UserRatings.FirstOrDefault(i => i.RevisionID == item.RevisionID && i.UserID == logged) != null);
                this.TeamRated = (ctx.RequirementRatings.FirstOrDefault(i => i.RevisionID == item.RevisionID) != null);
                this.Approved = (ctx.RequirementRatings.FirstOrDefault(i => i.RevisionID == item.RevisionID && i.Approved == true) != null);
            }

            if (this.Published == false)
            {
                this.Status = "Em edição";
            }
            else if (this.MeRated == false)
            {
                this.Status = "Publicado, aguardando sua avaliação";
            }
            else if (this.TeamRated == false)
            {
                this.Status = "Publicado, aguardando avaliação da equipe";
            }
            else if (this.Approved == false)
            {
                this.Status = "Reprovado, por favor melhorar a qualidade do texto";
                this.Published = false;
            }
            else
            {
                this.Status = "Aprovado, aguardando documentação";
            }
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