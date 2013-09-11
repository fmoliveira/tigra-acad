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
                var model = new StoriesViewModel(ctx.Stories.FirstOrDefault(i => i.StoryID == id));
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
                    Story item = new Story();
                    item.CellID = RouteData.Values["cell"].GetCellID();
                    item.UserID = Authentication.GetLoggedUser().UserID;
                    item.RequestDate = DateTime.Now;
                    item.Summary = model.Summary;
                    item.Text = model.Text;
                    ctx.Stories.Add(item);

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
                var model = new StoriesCreateModel(ctx.Stories.FirstOrDefault(i => i.StoryID == id));
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
                    Story item = ctx.Stories.FirstOrDefault(i => i.StoryID == model.Id);

                    if (item != null)
                    {
                        item.CellID = RouteData.Values["cell"].GetCellID();
                        item.UserID = Authentication.GetLoggedUser().UserID;
                        item.RequestDate = DateTime.Now;
                        item.Summary = model.Summary;
                        item.Text = model.Text;

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
                    else
                    {
                        Error("História inválida!");
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
