using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Tigra.Database;

namespace Tigra.Models
{
    public class ExistingRequirementsModel
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

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [DisplayName("Tag")]
        public string Tag { get; set; }

        public ExistingRequirementsModel()
        {
            //
        }

        public ExistingRequirementsModel(GetExistingRequirements_Result res)
        {
            this.Id = res.RevisionID;
            this.Title = res.Title;
            this.Autor = new UserNameModel(res.UserID);
            this.Revisao = res.RevisionNumber;
            this.Modificacao = res.RevisionDate;
            this.Descricao = res.Text;
            this.Tag = res.Tag;

            string[] words = this.Descricao.Split(new char[] { ' ' });
            this.Descricao = String.Empty;

            for (int i = 0; i < 15 && i < words.Length; i++)
            {
                this.Descricao += (this.Descricao.Length != 0 ? " " : "") + words[i];
            }

            int dot = this.Descricao.LastIndexOf('.');

            if (words.Length >= 15)
            {
                if (dot == -1 || (this.Descricao.Length - dot) > 3)
                {
                    this.Descricao += "...";
                }
            }
        }
    }
}