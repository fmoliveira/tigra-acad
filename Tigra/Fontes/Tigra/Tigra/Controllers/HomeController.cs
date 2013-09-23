using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Tigra.Database;
using Tigra.Models;

namespace Tigra.Controllers
{
	public class HomeController : BootstrapBaseController
	{

        public ActionResult Index()
        {
            if (RouteData.Values.ContainsKey("cell"))
            {
                if (User.Identity.IsAuthenticated)
                {
                    try
                    {
                        HomeModel model = new HomeModel(RouteData.Values["cell"].GetCell());
                        return View(model);
                    }
                    catch (NullReferenceException)
                    {
                        // nothing to do, just skips to home page
                    }
                }
                else
                {
                    return Redirect(FormsAuthentication.LoginUrl);
                }
            }

            return View("HomePage");
        }

	}
}
