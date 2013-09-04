using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tigra.Models;

namespace Tigra.Controllers
{
    public class ElicitationController : BootstrapBaseController
    {

        public ActionResult Index()
        {
            var cell = RouteData.Values["cell"];
            var model = ElicitationIndexModel.GetModels(cell);
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new ElicitationCreateModel();
            return View(model);
        }

    }
}
