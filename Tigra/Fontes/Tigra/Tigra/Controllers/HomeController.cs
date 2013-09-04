using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tigra.Models;

namespace Tigra.Controllers
{
	public class HomeController : BootstrapBaseController
	{

		public ActionResult Index()
		{
			try
			{
				HomeModel model = new HomeModel(RouteData.Values["cell"].GetCell());
				return View(model);
			}
			catch (NullReferenceException)
			{
				HomeModel m = new HomeModel() { CellName = "Teste", Description = "Nenhuma célula selecionada." };
				return View(m);
			}
		}

	}
}
