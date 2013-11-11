using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
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
        public List<string> Requisitos { get; set; }

        public BaselineCreateModel()
        {
            this.Requisitos = new List<string>();
            this.Requisitos.Add("Controle de Licenciamento");
            this.Requisitos.Add("Envio de anexos");
            this.Requisitos.Add("Módulo de análise de impacto");
        }

    }
}