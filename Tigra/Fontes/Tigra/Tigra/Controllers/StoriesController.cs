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
    [DisplayName("Histórias"), Description("As histórias descrevem a necessidade do cliente. A partir da história, serão definidos os requisitos que nós implementaremos para atender a necessidade descrita na história. Cada detalhe informado pelo cliente é relevante nestes tópicos.")]
    public class StoriesController : BootstrapBaseController
    {

        [Authorize]
        public ActionResult Index()
        {
            var cell = RouteData.Values["cell"];
            var model = StoriesIndexModel.GetModels(cell);
            return View(model);
        }

        [Authorize]
        public ActionResult Details(string tag)
        {
            using (var ctx = new Entities())
            {
                StoriesDetailsModel model = new StoriesDetailsModel(ctx.GetRequirementDetails(tag, null).FirstOrDefault());
                RouteData.Values["title"] = model.Summary;
                return View(model);
            }
        }

        [Authorize]
        public ActionResult NewRequirement(string tag)
        {
            using (var ctx = new Entities())
            {
                NewRequirementModel model = new NewRequirementModel(ctx.GetRequirementDetails(tag, null).FirstOrDefault());
                RouteData.Values["title"] = model.Story.Summary;
                return View(model);
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewRequirement(RequirementCreateModel model)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new Entities())
                {
                    model.Tag = Utils.Tagify(model.Summary);
                    int cellID = RouteData.Values["cell"].GetCellID();
                    int userID = Authentication.GetLoggedUser().UserID;
                    int ret = ctx.SaveRequirement(Tigra.RequirementTypes.Requirement, cellID, null, userID, model.Message, model.Tag, model.Summary, model.Text, model.StoryId);

                    if (ret != 0)
                    {
                        Success("Requisito inserido com sucesso!");
                        return RedirectToRoute("Details", new { @cell = RouteData.Values["cell"], @controller = RouteData.Values["controller"], @tag = model.Tag, @action = "Details" });
                    }
                    else
                    {
                        Error("Erro ao tentar inserir o novo requisito!");
                    }
                }
            }
            else
            {
                Warning("Preencha o formulário corretamente!");
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Create()
        {
            var model = new StoriesCreateModel();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(StoriesCreateModel model)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new Entities())
                {
                    model.Tag = Utils.Tagify(model.Summary);
                    int cellID = RouteData.Values["cell"].GetCellID();
                    int userID = Authentication.GetLoggedUser().UserID;
                    int ret = ctx.SaveRequirement(Tigra.RequirementTypes.Story, cellID, null, userID, model.Message, model.Tag, model.Summary, model.Text, null);

                    if (ret != 0)
                    {
                        Success("História inserida com sucesso!");
                        return RedirectToRoute("Details", new { @cell = RouteData.Values["cell"], @controller = RouteData.Values["controller"], @tag = model.Tag, @action = "Details" });
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

        [Authorize]
        public ActionResult Edit(string tag)
        {
            using (var ctx = new Entities())
            {
                StoriesCreateModel model = new StoriesCreateModel(ctx.GetRequirementDetails(tag, null).FirstOrDefault());
                RouteData.Values["title"] = model.Summary;
                return View("Create", model);
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(StoriesCreateModel model)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new Entities())
                {
                    model.Tag = Utils.Tagify(model.Summary);
                    int cellID = RouteData.Values["cell"].GetCellID();
                    int userID = Authentication.GetLoggedUser().UserID;
                    int ret = ctx.SaveRequirement(Tigra.RequirementTypes.Story, cellID, model.Id, userID, model.Message, model.Tag, model.Summary, model.Text, null);

                    if (ret != 0)
                    {
                        Success("História alterada com sucesso!");
                        return RedirectToRoute("Details", new { @cell = RouteData.Values["cell"], @controller = RouteData.Values["controller"], @tag = model.Tag, @action = "Details" });
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

        [Authorize]
        public ActionResult History(string tag)
        {
            var model = StoriesHistoryModel.GetModels(tag);
            RouteData.Values["title"] = model[0].Summary;
            return View(model);
        }

    }
}
