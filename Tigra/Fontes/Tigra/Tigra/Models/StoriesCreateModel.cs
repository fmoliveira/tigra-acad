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
    public class StoriesCreateModel
    {
        
        [Required]
        public long Id { get; set; }

        public string Tag = null;

        [DisplayName("Título"), DataType(DataType.Text), StringLength(100)]
        [Required]
        public string Summary { get; set; }

        [DisplayName("Descrição"), DataType(DataType.Html)]
        [Required]
        public string Text { get; set; }

        [DisplayName("Motivo da edição"), DataType(DataType.Text), StringLength(250)]
        [Required]
        public string Message { get; set; }

        public StoriesCreateModel()
        {
            //
        }

        public StoriesCreateModel(GetRequirementDetails_Result item)
        {
            this.Id = item.RevisionID;
            this.Tag = item.Tag;
            this.Summary = item.Title;
            this.Text = item.Text;
        }

    }
}