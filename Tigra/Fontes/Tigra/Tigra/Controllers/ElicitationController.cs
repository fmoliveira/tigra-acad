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
                return View("Create", model);
            }
        }

    }
}
