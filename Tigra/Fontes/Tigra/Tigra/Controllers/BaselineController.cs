using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tigra.Database;
using Tigra.Models;

namespace Tigra.Controllers
{
    [DisplayName("Baseline"), Description("O baseline é um controle histórico do status de todos os requisitos em cada versão de software gerada.")]
    public class BaselineController : BootstrapBaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            var cell = RouteData.Values["cell"];
            var model = BaselineIndexModel.GetModels(cell);
            return View(model);
        }

    }
}
