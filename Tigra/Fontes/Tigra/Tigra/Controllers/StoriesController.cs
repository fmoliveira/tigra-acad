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

        public ActionResult Index()
        {
            var cell = RouteData.Values["cell"];
            var model = StoriesIndexModel.GetModels(cell);
            return View(model);
        }

        public ActionResult Details(string tag)
        {
            using (var ctx = new Entities())
            {
                StoriesDetailsModel model = new StoriesDetailsModel(ctx.GetRequirementDetails(tag, null).FirstOrDefault());
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
                    int ret = ctx.SaveRequirement(Tigra.RequirementTypes.Story, cellID, null, userID, model.Message, Utils.Tagify(model.Summary), model.Summary, model.Text);

                    if (ret != 0)
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

        public ActionResult Edit(string tag)
        {
            using (var ctx = new Entities())
            {
                StoriesCreateModel model = new StoriesCreateModel(ctx.GetRequirementDetails(tag, null).FirstOrDefault());
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
                    int ret = ctx.SaveRequirement(Tigra.RequirementTypes.Story, cellID, model.Id, userID, model.Message, Utils.Tagify(model.Summary), model.Summary, model.Text);

                    if (ret != 0)
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
