using BootstrapSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tigra.Database;
using Tigra.Models;
using Tigra.Controllers;
using Tigra.Areas.Admin.Models;

namespace Tigra.Areas.Admin.Controllers
{
    public class TeamsController : BootstrapBaseController
    {

        public ActionResult Index()
        {
            var model = TeamModel.GetTeams();
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            using(var ctx= new Entities())
            {
                var model = TeamModel.GetTeam(id);
                return View(model);
            }
        }

    }
}
