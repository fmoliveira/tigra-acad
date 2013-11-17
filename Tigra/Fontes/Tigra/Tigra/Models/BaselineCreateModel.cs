using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Tigra.Database;

namespace Tigra.Models
{
    public class BaselineCreateModel
    {

        [Required]
        public int Id { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [DisplayName("Requisitos"), DataType("List_Requirements")]
        public List<BaselineRequirementsModel> Requisitos { get; set; }

        public BaselineCreateModel(List<BaselineRequirementsModel> reqs)
        {
            this.Requisitos = reqs;
        }

    }
}