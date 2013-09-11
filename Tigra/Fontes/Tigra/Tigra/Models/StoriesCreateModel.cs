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
    [DisplayName("Histórias")]
    public class StoriesCreateModel
    {
        
        [Required]
        public int Id { get; set; }

        [DisplayName("Título")]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Required]
        public string Summary { get; set; }

        [DisplayName("Descrição")]
        [DataType(DataType.Html)]
        [Required]
        public string Text { get; set; }

        public StoriesCreateModel()
        {
            //
        }

        public StoriesCreateModel(Story item)
        {
            this.Id = item.StoryID;
            this.Summary = item.Summary;
            this.Text = item.Text;
        }

    }
}