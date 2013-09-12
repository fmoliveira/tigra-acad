using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tigra.Database;
using Tigra.Models;

namespace Tigra.Controllers
{
    public class StoriesController : BootstrapBaseController
    {

        public ActionResult Index()
        {
            var cell = RouteData.Values["cell"];
            var model = StoriesIndexModel.GetModels(cell);
            return View(model);
        }

        public ActionResult View(int id)
        {
            using (var ctx = new Entities())
            {
                StoriesViewModel model = new StoriesViewModel(ctx.GetRequirementDetails(id, null).FirstOrDefault());
                RouteData.Values["title"] = model.Summary;
                return View(model);
            }
        }

        public ActionResult Create()
        {
            var model = new StoriesCreateModel();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(StoriesCreateModel model)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new Entities())
                {
                    int cellID = RouteData.Values["cell"].GetCellID();
                    int userID = Authentication.GetLoggedUser().UserID;
                    ctx.SaveRequirement(Tigra.RequirementTypes.Story, cellID, null, userID, model.Message, model.Summary, model.Text);

                    if (SaveChanges(ctx) != 0)
                    {
                        Success("História inserida com sucesso!");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Error("Erro ao tentar inserir a nova história!");
                    }
                }
            }
            else
            {
                Warning("Preencha o formulário corretamente!");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            using (var ctx = new Entities())
            {
                StoriesCreateModel model = new StoriesCreateModel(ctx.GetRequirementDetails(id, null).FirstOrDefault());
                RouteData.Values["title"] = model.Summary;
                return View("Create", model);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(StoriesCreateModel model)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new Entities())
                {
                    int cellID = RouteData.Values["cell"].GetCellID();
                    int userID = Authentication.GetLoggedUser().UserID;
                    ctx.SaveRequirement(Tigra.RequirementTypes.Story, cellID, model.Id, userID, model.Message, model.Summary, model.Text);

                    if (SaveChanges(ctx) != 0)
                    {
                        Success("História alterada com sucesso!");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Error("Erro ao tentar alterar a história!");
                    }
                }
            }
            else
            {
                Warning("Preencha o formulário corretamente!");
            }
            return View("Create", model);
        }

    }
}
