﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tigra.Database;

namespace Tigra.Models
{
    public class RequirementsDetailsModel
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
        public DateTime? LatestBaseline = null;
        public bool Archived = false;

        public RequirementsDetailsModel()
        {
            //
        }

        public RequirementsDetailsModel(GetRequirementDetails_Result item)
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
                this.Archived = (ctx.RequirementRevisions.FirstOrDefault(i => i.RevisionID == item.RevisionID && i.Archived == true) != null);

                if (this.Rated)
                {
                    this.ComentarioRevisao = (ctx.UserRatings.FirstOrDefault(i => i.RevisionID == item.RevisionID).Comments);
                }

                var bl = (from i in ctx.Baselines orderby i.SetDate descending select i.SetDate).Take(1);

                if (bl != null && bl.Count() == 1)
                {
                    this.LatestBaseline = bl.ToArray()[0];
                }
            }

            if (item.BaselineDate.HasValue)
            {
                this.Published = this.Rated = this.Implemented = true;

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
                this.Status = "Publicado, aguardando avaliação";
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
            else if (this.Archived)
            {
                this.Status = "Cancelado";
                this.Implemented = true;
            }
            else
            {
                this.Status = "Aprovado, aguardando implementação";
            }
        }

        public RequirementsDetailsModel(RequirementCreateModel req)
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