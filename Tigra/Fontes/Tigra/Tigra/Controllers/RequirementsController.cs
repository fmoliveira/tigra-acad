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
    [DisplayName("Requisitos"), Description("O documento de requisitos de software é a declaração oficial do que os desenvolvedores de sistema devem implementar. Deve incluir os requisitos de usuário de um sistema e uma especificação detalhada dos requisitos do sistema.")]
    public class RequirementsController : BootstrapBaseController
    {

        [Authorize]
        public ActionResult Index()
        {
            var cell = RouteData.Values["cell"];
            var model = RequirementsIndexModel.GetModels(cell);
            return View(model);
        }

        [Authorize]
        public ActionResult Details(string tag)
        {
            using (var ctx = new Entities())
            {
                RequirementsDetailsModel model = new RequirementsDetailsModel(ctx.GetRequirementDetails(tag, null).FirstOrDefault());
                RouteData.Values["title"] = model.Summary;
                return View(model);
            }
        }

        [Authorize]
        public ActionResult Publish(string tag)
        {
            using (var ctx = new Entities())
            {
                RequirementsDetailsModel model = new RequirementsDetailsModel(ctx.GetRequirementDetails(tag, null).FirstOrDefault());
                int cellID = RouteData.Values["cell"].GetCellID();

                int userID = Authentication.GetLoggedUser().UserID;
                int ret = ctx.SaveRequirement(RequirementTypes.Publish, cellID, model.RevisionId, userID, "Publicação de requisito", tag, model.Summary, model.Text, null);

                if (ret != 0)
                {
                    model = new RequirementsDetailsModel(ctx.GetRequirementDetails(tag, null).FirstOrDefault());
                    RequirementRevision rev = ctx.RequirementRevisions.FirstOrDefault(i => i.RevisionID == model.RevisionId);
                    rev.Published = true;

                    if (ctx.SaveChanges() != 0)
                    {
                        Success("Requisito publicado com sucesso!");
                        return RedirectToAction("Details", new { @tag = tag });
                    }
                }

                Error("Erro ao tentar publicar o requisito!");
                return RedirectToAction("Details", new { @tag = tag });
            }
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

                    if (ctx.TagExists(RequirementTypes.Requirement, cellID, model.Id, model.Tag))
                    {
                        Warning("Já existe outro tópico com este nome!");
                    }
                    else
                    {
                        int userID = Authentication.GetLoggedUser().UserID;
                        int ret = ctx.SaveRequirement(RequirementTypes.Requirement, cellID, model.Id, userID, model.Message, model.Tag, model.Summary, model.Text, null);

                        if (ret != 0)
                        {
                            Success("Requisito alterado com sucesso!");
                            return RedirectToRoute("Details", new { @cell = RouteData.Values["cell"], @controller = RouteData.Values["controller"], @tag = model.Tag, @action = "Details" });
                        }
                        else
                        {
                            Error("Erro ao tentar alterar o requisito!");
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
                int ret = ctx.SaveRequirement(RequirementTypes.MarkAsDone, cellID, model.RevisionId, userID, "Requisito implementado", tag, model.Summary, model.Text, null);

                if (ret != 0)
                {
                    model = new RequirementsDetailsModel(ctx.GetRequirementDetails(tag, null).FirstOrDefault());
                    RequirementRevision rev = ctx.RequirementRevisions.FirstOrDefault(i => i.RevisionID == model.RevisionId);
                    rev.Published = true;

                    if (ctx.SaveChanges() != 0)
                    {
                        Success("Requisito finalizado com sucesso!");
                        return RedirectToAction("Details", new { @tag = tag });
                    }
                }

                Error("Erro ao tentar finalizar o requisito!");
                return RedirectToAction("Details", new { @tag = tag });
            }
        }

    }
}
