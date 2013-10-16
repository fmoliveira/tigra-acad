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
    [DisplayName("Revisão"), Description("Todas as histórias e requisitos devem ser revisadas e aprovadas por todos os membros de sua equipe. Esta página exibe os itens que estão aguardando avaliação da equipe.")]
    public class RevisionController : BootstrapBaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            var cell = RouteData.Values["cell"];
            var model = RatingsIndexModel.GetModels(cell);
            return View(model);
        }

        [Authorize]
        public ActionResult Details(string tag)
        {
            using (var ctx = new Entities())
            {
                RequirementsDetailsModel req = new RequirementsDetailsModel(ctx.GetRequirementDetails(tag, null).FirstOrDefault());
                RouteData.Values["title"] = req.Summary;
                return View("Rate", new RateTopicModel(req));
            }
        }

        [Authorize, HttpPost]
        public ActionResult Details(UserRatingModel model)
        {
            using (var ctx = new Entities())
            {
                UserRating rating = new UserRating();
                rating.RevisionID = model.Id;
                rating.UserID = Authentication.GetLoggedUser().UserID;
                rating.RatingA = model.RatingA;
                rating.RatingB = model.RatingB;
                rating.RatingC = model.RatingC;
                rating.Comments = model.Comments;
                ctx.UserRatings.Add(rating);

                if (ctx.SaveChanges() != 0)
                {
                    Success("Obrigado por avaliar este tópico!");
                    return RedirectToRoute("Cells", new { @action = "Index" });
                }
                else
                {
                    Error("Erro ao enviar a sua avaliação!");
                    return View("Rate", model);
                }
            }
        }

    }
}
