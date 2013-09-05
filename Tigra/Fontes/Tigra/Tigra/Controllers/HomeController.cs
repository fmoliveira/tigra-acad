using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tigra.Database;
using Tigra.Models;

namespace Tigra.Controllers
{
	public class HomeController : BootstrapBaseController
	{

		public ActionResult Index()
		{
			HomeModel model = null;

			try
			{
				model = new HomeModel(RouteData.Values["cell"].GetCell());
			}
			catch (NullReferenceException)
			{
				model = new HomeModel
				(
					new Cell()
					{
						CellID = 0,
						CellName = "Teste",
						Description = "Nenhuma célula selecionada."
					}
				);
			}
			catch (Exception)
			{
				return RedirectToAction("Index", "Error");
			}
			return View(model);
		}

	}
}
