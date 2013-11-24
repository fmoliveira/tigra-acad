using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tigra.Database;

namespace Tigra.Models
{
    public class BaselineRequirementDetailsModel
    {
        [Required]
        public long Id { get; set; }

        [DisplayName("Autor")]
        public UserNameModel Autor { get; set; }

        [DisplayName("Título")]
        public string Title { get; set; }

        [DisplayName("Revisão")]
        public short Revisao { get; set; }

        [DisplayName("Modificação")]
        public DateTime Modificacao { get; set; }

        [DisplayName("Texto")]
        public string Texto { get; set; }

        public BaselineRequirementDetailsModel()
        {
            //
        }

        public BaselineRequirementDetailsModel(GetBaselineRequirementDetails_Result item)
        {
            this.Id = item.RevisionID;
            this.Autor = new UserNameModel(item.UserID);
            this.Title = item.Title;
            this.Revisao = item.RevisionNumber;
            this.Modificacao = item.RevisionDate;
            this.Texto = item.Text;
        }
    }
}