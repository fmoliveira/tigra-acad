using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
            FormsAuthentication.SignOut();
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }

    }
}
