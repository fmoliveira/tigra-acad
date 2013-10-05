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
    public class RequirementCreateModel
    {
        
        [Required]
        public long Id { get; set; }

        [HiddenInput]
        public long StoryId { get; set; }

        public string Tag = null;
        private RequirementCreateModel item;

        [DisplayName("Título"), DataType(DataType.Text), StringLength(100)]
        [Required]
        public string Summary { get; set; }

        [DisplayName("Descrição"), DataType(DataType.Html)]
        [Required]
        public string Text { get; set; }

        [DisplayName("Motivo da edição"), DataType(DataType.Text), StringLength(250)]
        [Required]
        public string Message { get; set; }

        public RequirementCreateModel()
        {
            //
        }

        public RequirementCreateModel(long storyId)
        {
            this.StoryId = storyId;
        }

        public RequirementCreateModel(RequirementCreateModel item)
        {
            using(var ctx=new Entities())
            {
                this.StoryId = item.StoryId;
                this.Summary = item.Summary;
                this.Text = item.Text;
                this.Message = item.Message;
            }
        }
    }
}