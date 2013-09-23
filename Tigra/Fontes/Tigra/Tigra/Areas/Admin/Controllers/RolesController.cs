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
    public class RolesController : BootstrapBaseController
    {

        [Authorize]
        public ActionResult Index()
        {
            var model = RoleModel.GetRoles();
            return View(model);
        }

        [Authorize]
        public ActionResult Create()
        {
            var model = new RoleModel();
            return View(model);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            using (var ctx = new Entities())
            {
                var item = ctx.Roles.FirstOrDefault(i => i.RoleID == id);

                if (item != null)
                {
                    var model = new RoleModel(item);
                    RouteData.Values["title"] = model.RoleName;
                    return View("Create", model);
                }
                else
                {
                    Error("Papel inválido!");
                    return RedirectToAction("Index");
                }
            }
        }

        private bool PostModel(RoleModel model)
        {
            using (var ctx = new Entities())
            {
                Role item;

                if (model.Id != 0)
                {
                    item = ctx.Roles.FirstOrDefault(i => i.RoleID == model.Id);

                    if (item == null)
                    {
                        return false;
                    }
                }
                else
                {
                    item = new Role();
                    item.Authorisations = new byte[0];
                }

                item.RoleName = model.RoleName;

                if (model.Id == 0)
                {
                    ctx.Roles.Add(item);
                }

                if (SaveChanges(ctx) != 0)
                {
                    return true;
                }
            }

            return false;
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(RoleModel model)
        {
            try
            {
                if (PostModel(model))
                {
                    Success("Novo papel criado com sucesso!");
                    return RedirectToAction("Index");
                }
                else
                {
                    Warning("Não foi possível criar o novo papel!");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                Error("Erro na criação do novo papel! " + ex.Message);
                return View(model);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(RoleModel model)
        {
            try
            {
                if (PostModel(model))
                {
                    Success("Papel alterado com sucesso!");
                    return RedirectToAction("Index");
                }
                else
                {
                    Warning("Não foi possível alterar o papel!");
                    return View("Create", model);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.GetInnerstException().Message;

                if (msg.Contains("UNIQUE KEY"))
                {
                    Error("Já existe um papel com este nome! Por favor escolha outro nome.");
                }
                else
                {
                    Error("Erro na alteração do papel! " + msg);
                }

                return View("Create", model);
            }
        }
    }
}
