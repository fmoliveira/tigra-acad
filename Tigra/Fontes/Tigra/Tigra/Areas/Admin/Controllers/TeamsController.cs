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
using Tigra.Common;

namespace Tigra.Areas.Admin.Controllers
{
    public class TeamsController : BootstrapBaseController
    {

        public ActionResult Index()
        {
            var model = TeamIndexModel.GetTeams();
            return View(model);
        }

        public ActionResult Manage(int id)
        {
            using (var ctx = new Entities())
            {
                var model = TeamManageModel.GetTeam(id);

                var mb = (from m in ctx.UserAccounts orderby m.UserID select m).ToList();
                Dictionary<int, string> members = new Dictionary<int, string>();
                mb.ForEach(i => members.Add(i.UserID, i.GetDisplayName()));
                ViewBag.MembersList = new Choice(members).GetSelectList();

                var rl = (from r in ctx.Roles orderby r.RoleName select r).ToList();
                Dictionary<int, string> roles = new Dictionary<int, string>();
                rl.ForEach(i => roles.Add(i.RoleID, i.RoleName));
                ViewBag.RolesList = new Choice(roles).GetSelectList();

                return View(model);
            }
        }

    }
}
