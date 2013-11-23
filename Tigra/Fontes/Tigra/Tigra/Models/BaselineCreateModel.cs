using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Tigra.Database;

namespace Tigra.Models
{
    public class BaselineCreateModel
    {

        [Required]
        public int Id { get; set; }

        [DisplayName("Título"), MaxLength(20)]
        public string Descricao { get; set; }

        [DisplayName("Data do Baseline"), DataType("DateTime_Display")]
        public string SetDate { get; set; }

        [DisplayName("Requisitos"), DataType("List_Requirements")]
        public List<BaselineRequirementsModel> Requisitos { get; set; }

        public BaselineCreateModel()
        {
            //
        }

        public BaselineCreateModel(List<BaselineRequirementsModel> reqs)
        {
            this.Requisitos = reqs;
            this.SetDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        }

    }
}