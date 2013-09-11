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
    public class StoriesViewModel
    {
        
        [Required]
        public int Id { get; set; }

        [DisplayName("Solicitante")]
        public string UserName { get; set; }

        [DisplayName("Data da solicitação")]
        public DateTime RequestDate { get; set; }

        [DisplayName("Título")]
        [DataType(DataType.Text)]
        [StringLength(100)]
        [Required]
        public string Summary { get; set; }

        [DisplayName("Descrição")]
        [DataType(DataType.Html)]
        [Required]
        public string Text { get; set; }

        public StoriesViewModel()
        {
            //
        }

        public StoriesViewModel(Story item)
        {
            this.Id = item.StoryID;
            this.UserName = item.UserAccount.GetDisplayName();
            this.RequestDate = item.RequestDate;
            this.Summary = item.Summary;
            this.Text = item.Text;
        }

    }
}