using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tigra.Database;

namespace Tigra.Models
{
    public class BaselineDetailsModel
    {

        [Required]
        public int Id { get; set; }

        [DisplayName("Título"), MaxLength(20)]
        public string Descricao { get; set; }

        [DisplayName("Data do Baseline"), DataType("DateTime_Display")]
        public DateTime SetDate { get; set; }

        [DisplayName("Requisitos"), DataType("List_Requirements")]
        public List<BaselineRequirementsModel> Requisitos { get; set; }

        public BaselineDetailsModel(int id)
        {
            using (var ctx = new Entities())
            {
                var bl = ctx.Baselines.FirstOrDefault(i => i.BaselineID == id);

                if (bl != null)
                {
                    this.Id = bl.BaselineID;
                    this.Descricao = bl.Message;
                    this.SetDate = bl.SetDate;

                    var list = ctx.GetBaselineRequirements(bl.BaselineID).ToList();
                    this.Requisitos = new List<BaselineRequirementsModel>();
                    list.ForEach(i => this.Requisitos.Add(new BaselineRequirementsModel(i)));
                }
            }
        }

    }
}