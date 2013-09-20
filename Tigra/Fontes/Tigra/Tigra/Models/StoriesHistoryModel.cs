using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tigra.Common;
using Tigra.Database;

namespace Tigra.Models
{
    public class StoriesHistoryModel
    {

        [Required]
        public string Id { get; set; }

        [DisplayName("Autor")]
        public UserNameModel UserName { get; set; }

        [DisplayName("Número da revisão")]
        public short RevisionNumber { get; set; }

        [DisplayName("Data da revisão")]
        public DateTime RevisionDate { get; set; }

        [DisplayName("Motivo da edição")]
        public string Message { get; set; }

        [DisplayName("Data")]
        public DateTime Modified { get; set; }

        [DisplayName("Título")]
        public string Summary { get; set; }

        [DisplayName("Descrição")]
        public string Text { get; set; }

        public StoriesHistoryModel(GetRequirementHistory_Result item, string diff)
        {
            this.Id = item.Tag;
            this.UserName = new UserNameModel(item.UserID);
            this.RevisionNumber = item.RevisionNumber;
            this.RevisionDate = item.RevisionDate;
            this.Message = item.Message;
            this.Modified = item.RevisionDate;
            this.Summary = item.Title;
            this.Text = diff;
        }

        public static List<StoriesHistoryModel> GetModels(string tag)
        {
            List<StoriesHistoryModel> ret = new List<StoriesHistoryModel>();

            using (var ctx = new Entities())
            {
                var list = ctx.GetRequirementHistory(tag, null).ToList();
                string previous = string.Empty, text;

                foreach (var i in list)
                {
                    text = String.Format("<p><strong>{0}</strong></p>{1}", i.Title, i.Text);
                    ret.Add(new StoriesHistoryModel(i, HtmlDiff.Execute(previous, text)));
                    previous = text;
                }
            }

            return (from i in ret orderby i.RevisionNumber descending select i).ToList();
        }

    }
}