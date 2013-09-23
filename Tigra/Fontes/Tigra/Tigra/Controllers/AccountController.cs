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

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToRoute("Default", new { @controller = "Home", @action = "Index" });
            }
            else
            {
                return RedirectToRoute("Error", new { @action = "Login" });
            }
        }

        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToRoute("Default", new { @controller = "Home", @action = "Index" });
            }
            else
            {
                return RedirectToRoute("Error", new { @action = "Register" });
            }
        }

        public ActionResult Confirm(string token)
        {
            if (Mail.ValidateToken(token))
            {
                Success("Sua conta foi ativada com sucesso! Você já pode entrar!");
            }
            else
            {
                Error("Token inválido! Provavelmente o link foi corrompido ou o token foi expirado. Por favor solicite um novo.");
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Authentication.Logout();
            return Redirect("~/");
        }

        [Authorize]
        public ActionResult Profile(int id)
        {
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
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
