using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Tigra.Database;

namespace Tigra.Models
{
    public class BaselineRequirementsModel
    {
        public long Id { get; set; }

        [DisplayName("Autor")]
        public UserNameModel Autor { get; set; }

        [DisplayName("Título")]
        public string Title { get; set; }

        [DisplayName("Revisão")]
        public short Revisao { get; set; }

        [DisplayName("Modificação")]
        public DateTime Modificacao { get; set; }

        public BaselineRequirementsModel()
        {
            //
        }

        public BaselineRequirementsModel(GetRequirementsForBaseline_Result res)
        {
            this.Id = res.RevisionID;
            this.Title = res.Title;
            this.Autor = new UserNameModel(res.UserID);
            this.Revisao = res.RevisionNumber;
            this.Modificacao = res.RevisionDate;
        }
    }
}