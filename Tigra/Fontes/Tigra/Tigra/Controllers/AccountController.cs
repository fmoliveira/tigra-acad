using BootstrapSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tigra.Common;
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

        public ActionResult Profile(int id)
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult MyProfile()
        {
            using (var ctx = new Entities())
            {
                int userid = Authentication.GetLoggedUser().UserID;
                UserProfile up = ctx.UserProfiles.FirstOrDefault(i => i.UserID == userid);

                if (up == null)
                {
                    up = new UserProfile();
                }

                var profile = new MyProfileModel(up);
                ViewBag.UserThemeList = new Choice(Utils.GetThemes(), true, up.UserTheme).GetSelectList();
                return View(profile);
            }
        }

    }
}
