using BootstrapSupport;
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
    [DisplayName("Requisitos"), Description("O documento de requisitos de software é a declaração oficial do que os desenvolvedores de sistema devem implementar. Deve incluir os requisitos de usuário de um sistema e uma especificação detalhada dos requisitos do sistema. (Sommerville)")]
    public class RequirementsController : BootstrapBaseController
    {

        [Authorize]
        public ActionResult Index()
        {
            var cell = RouteData.Values["cell"];
            var model = RequirementsIndexModel.GetModels(cell);
            return View(model);
        }

    }
}
