using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tigra.Database;
using Tigra.Models;

namespace Tigra.Controllers
{
    public class AccountController : BootstrapBaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            AuthenticationModel model = new AuthenticationModel();
            return View(model);
        }

        public ActionResult Logout()
        {
            Authentication.Logout();
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        public ActionResult MyProfile()
        {
            return View();
        }

    }
}
