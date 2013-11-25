using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tigra.Database;

namespace Tigra.Models
{
    public class TraceMatrixItemModel
    {
        public long Id { get; set; }

        public int Revisao { get; set; }

        public DateTime Modificacao { get; set; }

        public UserNameModel Autor { get; set; }

        public string Tag { get; set; }

        public string Title { get; set; }

        public string Descricao { get; set; }

        public TraceMatrixItemModel()
        {
            //
        }

        public TraceMatrixItemModel(GetTraceabilityMatrix_Result item)
        {
            this.Id = item.RevisionID;
            this.Revisao = item.RevisionNumber;
            this.Modificacao = item.RevisionDate;
            this.Autor = new UserNameModel(item.UserID);
            this.Tag = item.Tag;
            this.Title = item.Title;
            this.Descricao = item.Text.Replace("<p>", "").Replace("</p>", "");

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