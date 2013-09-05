using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tigra.Database;
using Tigra.Models;

namespace Tigra.Controllers
{
    public class ElicitationController : BootstrapBaseController
    {

        public ActionResult Index()
        {
            var cell = RouteData.Values["cell"];
            var model = ElicitationIndexModel.GetModels(cell);
            return View(model);
        }

        public ActionResult View(int id)
        {
            using (var ctx = new Entities())
            {
                var model = new ElicitationViewModel(ctx.Elicitations.FirstOrDefault(i => i.ElicitationID == id));
                RouteData.Values["title"] = model.Summary;
                return View(model);
            }
        }

        public ActionResult Create()
        {
            var model = new ElicitationCreateModel();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ElicitationCreateModel model)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new Entities())
                {
                    Elicitation item = new Elicitation();
                    item.CellID = RouteData.Values["cell"].GetCellID();
                    item.UserID = Authentication.GetLoggedUser().UserID;
                    item.RequestDate = DateTime.Now;
                    item.Summary = model.Summary;
                    item.Text = model.Text;
                    ctx.Elicitations.Add(item);

                    if (SaveChanges(ctx) != 0)
                    {
                        Success("Elicitação inserida com sucesso!");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Error("Erro ao tentar inserir a nova elicitação!");
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
                var model = new ElicitationCreateModel(ctx.Elicitations.FirstOrDefault(i => i.ElicitationID == id));
                RouteData.Values["title"] = model.Summary;
                return View("Create", model);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ElicitationCreateModel model)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new Entities())
                {
                    Elicitation item = ctx.Elicitations.FirstOrDefault(i => i.ElicitationID == model.Id);

                    if (item != null)
                    {
                        item.CellID = RouteData.Values["cell"].GetCellID();
                        item.UserID = Authentication.GetLoggedUser().UserID;
                        item.RequestDate = DateTime.Now;
                        item.Summary = model.Summary;
                        item.Text = model.Text;

                        if (SaveChanges(ctx) != 0)
                        {
                            Success("Elicitação alterada com sucesso!");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Error("Erro ao tentar alterar a elicitação!");
                        }
                    }
                    else
                    {
                        Error("Elicitação inválida!");
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
