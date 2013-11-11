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
    [DisplayName("Requisitos"), Description("O documento de requisitos de software é a declaração oficial do que os desenvolvedores de sistema devem implementar. Deve incluir os requisitos de usuário de um sistema e uma especificação detalhada dos requisitos do sistema. (Sommerville)")]
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
                int ret = ctx.SaveRequirement(RequirementTypes.Requirement, cellID, model.RevisionId, userID, "Publicação de requisito", tag, model.Summary, model.Text, null);

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
        public ActionResult MarkAsDone(string tag)
        {
            using (var ctx = new Entities())
            {
                RequirementsDetailsModel model = new RequirementsDetailsModel(ctx.GetRequirementDetails(tag, null).FirstOrDefault());
                int cellID = RouteData.Values["cell"].GetCellID();

                int userID = Authentication.GetLoggedUser().UserID;
                int ret = ctx.SaveRequirement(RequirementTypes.MarkAsDone, cellID, model.RevisionId, userID, "Implementação de requisito", tag, model.Summary, model.Text, null);

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
