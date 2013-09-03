using BootstrapSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tigra.Database;
using Tigra.Models;

namespace Tigra.Controllers
{
    public class CellsController : BootstrapBaseController
    {

        public ActionResult Index()
        {
            using (var ctx = new Entities())
            {
                var model = CellModel.GetCells();
                return View(model);
            }
        }

        public ActionResult Create()
        {
            var model = new CellModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CellModel model)
        {
            using (var ctx = new Entities())
            {
                Cell item = new Cell()
                {
                    CellName = model.CellName,
                    Tag = Utils.FormatLinkTag(model.CellName),
                    Description = model.Description
                };

                try
                {
                    ctx.Cells.Add(item);
                    ctx.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Error("Erro na criação da nova célula! " + ex.Message);
                    return View(model);
                }
            }
        }
    }
}
