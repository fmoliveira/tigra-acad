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
			return View();
		}

	}
}
