﻿using BootstrapSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tigra.Database;
using Tigra.Models;
using Tigra.Controllers;
using Tigra.Areas.Admin.Models;

namespace Tigra.Areas.Admin.Controllers
{
    public class CellsController : BootstrapBaseController
    {

        [Authorize]
        public ActionResult Index()
        {
            var model = CellModel.GetCells();
            return View(model);
        }

        [Authorize]
        public ActionResult Create()
        {
            var model = new CellModel();
            return View(model);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            using (var ctx = new Entities())
            {
                var item = ctx.Cells.FirstOrDefault(i => i.CellID == id);

                if (item != null)
                {
                    var model = new CellModel(item);
                    RouteData.Values["title"] = model.CellName;
                    return View("Create", model);
                }
                else
                {
                    Error("Célula inválida!");
                    return RedirectToAction("Index");
                }
            }
        }

        private bool PostModel(CellModel model)
        {
            using (var ctx = new Entities())
            {
                Cell item;

                if (model.Id != 0)
                {
                    item = ctx.Cells.FirstOrDefault(i => i.CellID == model.Id);

                    if (item == null)
                    {
                        return false;
                    }
                }
                else
                {
                    item = new Cell();
                }

                item.CellName = model.CellName;
                item.Tag = Utils.Tagify(model.CellName);
                item.Description = model.Description;

                if (model.Id == 0)
                {
                    ctx.Cells.Add(item);
                }

                if (SaveChanges(ctx) != 0)
                {
                    return true;
                }
            }

            return false;
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CellModel model)
        {
            try
            {
                if (PostModel(model))
                {
                    Success("Nova célula criada com sucesso!");
                    return RedirectToAction("Index");
                }
                else
                {
                    Warning("Não foi possível criar a nova célula!");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                Error("Erro na criação da nova célula! " + ex.Message);
                return View(model);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(CellModel model)
        {
            try
            {
                if (PostModel(model))
                {
                    Success("Célula alterada com sucesso!");
                    return RedirectToAction("Index");
                }
                else
                {
                    Warning("Não foi possível alterar a célula!");
                    return View("Create", model);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.GetInnerstException().Message;

                if (msg.Contains("UNIQUE KEY"))
                {
                    Error("Já existe uma célula com este nome! Por favor escolha outro nome.");
                }
                else
                {
                    Error("Erro na alteração da célula! " + msg);
                }

                return View("Create", model);
            }
        }
    }
}
