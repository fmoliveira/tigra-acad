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
    [DisplayName("Baseline"), Description("O baseline é um controle histórico do status de todos os requisitos em cada versão lançada.")]
    public class BaselineController : BootstrapBaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            var cell = RouteData.Values["cell"];
            var model = BaselineIndexModel.GetModels(cell);
            return View(model);
        }

        [Authorize]
        public ActionResult Create()
        {
            List<BaselineRequirementsModel> reqs = new List<BaselineRequirementsModel>();
            using (var ctx = new Entities())
            {
                int cellID = RouteData.Values["cell"].GetCellID();
                List<GetRequirementsForBaseline_Result> list = ctx.GetRequirementsForBaseline(cellID).ToList();
                list.ForEach(i => reqs.Add(new BaselineRequirementsModel(i)));
            }

            var model = new BaselineCreateModel(reqs);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(BaselineCreateModel model)
        {
            if (model.Descricao == null || model.Descricao.Trim().Length == 0)
            {
                Error("Digite a descrição do baseline!");
            }
            else
            {
                using (var ctx = new Entities())
                {
                    Baseline bl = new Baseline();
                    bl.CellID = RouteData.Values["cell"].GetCellID();
                    bl.UserID = Authentication.GetLoggedUser().UserID;
                    bl.SetDate = DateTime.Parse(model.SetDate);
                    bl.Message = model.Descricao;
                    ctx.Baselines.Add(bl);

                    if (ctx.SaveChanges() != 0)
                    {
                        Success("Baseline registrado com sucesso!");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Error("Erro ao tentar registrar o baseline!");
                    }
                }
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Details(int tag)
        {
            return View();
        }

    }
}
