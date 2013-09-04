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
    [DisplayName("Elicitação")]
    public class ElicitationCreateModel
    {
        
        [Required]
        public int Id { get; set; }

        [DisplayName("Descrição")]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Required]
        public string Summary { get; set; }

        [DisplayName("Texto")]
        [DataType(DataType.Html)]
        [Required]
        public string Text { get; set; }

        public ElicitationCreateModel()
        {
            //
        }

        public ElicitationCreateModel(Elicitation item)
        {
            this.Id = item.ElicitationID;
            this.Summary = item.Summary;
            this.Text = item.Text;
        }

    }
}