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

        [DisplayName("Estado")]
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
        public bool Rated = false;
        public bool Approved = false;
        public bool Implemented = false;
        public string ComentarioRevisao = string.Empty;

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
                this.Rated = (ctx.RequirementRatings.FirstOrDefault(i => i.RevisionID == item.RevisionID) != null);
                this.Approved = (ctx.RequirementRatings.FirstOrDefault(i => i.RevisionID == item.RevisionID && i.Approved == true) != null);

                if (this.Rated)
                {
                    this.ComentarioRevisao = (ctx.UserRatings.FirstOrDefault(i => i.RevisionID == item.RevisionID).Comments);
                }
            }

            if(item.BaselineDate.HasValue)
            {
                this.Published = this.Rated = this.Implemented = true;
                this.Status = "Atendida";
            }
            else if (this.Published == false)
            {
                this.Status = "Em edição";
            }
            else if (this.Rated == false)
            {
                this.Status = "Publicada, aguardando avaliação";
            }
            else if (this.Approved == false)
            {
                this.Status = "Reprovado";

                if (this.ComentarioRevisao != null && this.ComentarioRevisao.Length != 0)
                {
                    this.Status += " - Comentários: " + this.ComentarioRevisao;
                }

                this.Published = false;
            }
            else
            {
                this.Status = "Aprovada, aguardando documentação";
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