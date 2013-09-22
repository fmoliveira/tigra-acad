using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tigra.Models
{
    public class RequirementCreateModel
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

        public RequirementCreateModel()
        {
            //
        }
    }
}