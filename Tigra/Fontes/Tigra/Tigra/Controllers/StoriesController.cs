﻿using BootstrapSupport;
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
    [DisplayName("Histórias"), Description("As histórias descrevem a necessidade do cliente. A partir da história, serão definidos os requisitos que deverão ser implementados pelos desenvolvedores de sistema para satisfazer as necessidades declaradas na história. Cada detalhe informado pelo cliente é relevante nestes tópicos.")]
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

                    if (ctx.TagExists(RequirementTypes.Requirement, cellID, null, model.Tag))
                    {
                        Warning("Já existe um requisito com este nome!");
                    }
                    else
                    {
                        int userID = Authentication.GetLoggedUser().UserID;
                        int ret = ctx.SaveRequirement(RequirementTypes.Requirement, cellID, null, userID, model.Message, model.Tag, model.Summary, model.Text, model.StoryId);

                        if (ret != 0)
                        {
                            Success("Requisito inserido com sucesso!");
                            return RedirectToRoute("Details", new { @cell = RouteData.Values["cell"], @controller = "Requirements", @tag = model.Tag, @action = "Details" });
                        }
                        else
                        {
                            Error("Erro ao tentar inserir o novo requisito!");
                        }
                    }
                }
            }
            else
            {
                Warning("Preencha o formulário corretamente!");
            }

            NewRequirementModel edit = new NewRequirementModel(model);
            RouteData.Values["title"] = edit.Story.Summary;
            return View(edit);
        }

        [Authorize]
        public ActionResult ExistingRequirement()
        {
            List<ExistingRequirementsModel> reqs = new List<ExistingRequirementsModel>();
            using (var ctx = new Entities())
            {
                int cellID = RouteData.Values["cell"].GetCellID();
                List<GetExistingRequirements_Result> list = ctx.GetExistingRequirements(cellID).ToList();
                list.ForEach(i => reqs.Add(new ExistingRequirementsModel(i)));
            }
            return View(reqs);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ExistingRequirement(UseExistingRequirementModel model)
        {
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

                    if (ctx.TagExists(RequirementTypes.Story, cellID, null, model.Tag))
                    {
                        Warning("Já existe uma história com este nome!");
                    }
                    else
                    {
                        int userID = Authentication.GetLoggedUser().UserID;
                        int ret = ctx.SaveRequirement(RequirementTypes.Story, cellID, null, userID, model.Message, model.Tag, model.Summary, model.Text, null);

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

                    if (ctx.TagExists(RequirementTypes.Story, cellID, model.Id, model.Tag))
                    {
                        Warning("Já existe outro tópico com este nome!");
                    }
                    else
                    {
                        int userID = Authentication.GetLoggedUser().UserID;
                        int ret = ctx.SaveRequirement(RequirementTypes.Story, cellID, model.Id, userID, model.Message, model.Tag, model.Summary, model.Text, null);

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
            }
            else
            {
                Warning("Preencha o formulário corretamente!");
            }
            return View("Create", model);
        }

        [Authorize]
        public ActionResult Publish(string tag)
        {
            using (var ctx = new Entities())
            {
                StoriesDetailsModel model = new StoriesDetailsModel(ctx.GetRequirementDetails(tag, null).FirstOrDefault());
                int cellID = RouteData.Values["cell"].GetCellID();

                int userID = Authentication.GetLoggedUser().UserID;
                int ret = ctx.SaveRequirement(RequirementTypes.Publish, cellID, model.RevisionId, userID, "Publicação de história", tag, model.Summary, model.Text, null);

                if (ret != 0)
                {
                    model = new StoriesDetailsModel(ctx.GetRequirementDetails(tag, null).FirstOrDefault());
                    RequirementRevision rev = ctx.RequirementRevisions.FirstOrDefault(i => i.RevisionID == model.RevisionId);
                    rev.Published = true;

                    if (ctx.SaveChanges() != 0)
                    {
                        Success("História publicada com sucesso!");
                        return RedirectToAction("Details", new { @tag = tag });
                    }
                }

                Error("Erro ao tentar publicar a história!");
                return RedirectToAction("Details", new { @tag = tag });
            }
        }

        [Authorize]
        public ActionResult History(string tag)
        {
            var model = StoriesHistoryModel.GetModels(tag);
            RouteData.Values["title"] = model[0].Summary;
            return View(model);
        }

        [Authorize]
        public ActionResult MarkAsDone(string tag)
        {
            using (var ctx = new Entities())
            {
                RequirementsDetailsModel model = new RequirementsDetailsModel(ctx.GetRequirementDetails(tag, null).FirstOrDefault());
                int cellID = RouteData.Values["cell"].GetCellID();

                int userID = Authentication.GetLoggedUser().UserID;
                int ret = ctx.SaveRequirement(RequirementTypes.MarkAsDone, cellID, model.RevisionId, userID, "História atendida", tag, model.Summary, model.Text, null);

                if (ret != 0)
                {
                    model = new RequirementsDetailsModel(ctx.GetRequirementDetails(tag, null).FirstOrDefault());
                    RequirementRevision rev = ctx.RequirementRevisions.FirstOrDefault(i => i.RevisionID == model.RevisionId);
                    rev.Published = true;

                    if (ctx.SaveChanges() != 0)
                    {
                        Success("História finalizada com sucesso!");
                        return RedirectToAction("Details", new { @tag = tag });
                    }
                }

                Error("Erro ao tentar finalizar a história!");
                return RedirectToAction("Details", new { @tag = tag });
            }
        }

    }
}
