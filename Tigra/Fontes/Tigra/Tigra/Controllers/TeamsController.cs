using BootstrapSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tigra.Database;
using Tigra.Models;

namespace Tigra.Controllers
{
    public class TeamsController : BootstrapBaseController
    {

        public ActionResult Index()
        {
            using (var ctx = new Entities())
            {
                var model = TeamsModel.GetTeams();
                return View(model);
            }
        }

    }
}
