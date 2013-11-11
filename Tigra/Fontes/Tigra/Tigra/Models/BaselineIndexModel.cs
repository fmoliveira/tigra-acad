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
    public class BaselineIndexModel
    {

        [Required]
        public int Id { get; set; }

        [DisplayName("Data")]
        public DateTime Modified { get; set; }

        [DisplayName("Responsável")]
        public UserNameModel UserName { get; set; }

        [DisplayName("Título")]
        public string Message { get; set; }

        public BaselineIndexModel(GetBaselineList_Result item)
        {
            this.Id = item.BaselineID;
            this.UserName = new UserNameModel(item.UserID);
            this.Modified = item.SetDate;
            this.Message = item.Message;
        }

        public static List<BaselineIndexModel> GetModels(object cell)
        {
            List<BaselineIndexModel> ret = new List<BaselineIndexModel>();

            using (var ctx = new Entities())
            {
                int parent = ctx.GetCellID(cell);
                var list = ctx.GetBaselineList(parent).ToList();
                list.ForEach(i => ret.Add(new BaselineIndexModel(i)));
            }

            return ret;
        }

    }
}