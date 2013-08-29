using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using Tigra.Models;

namespace Tigra.Database
{
    public class Authentication
    {
        private const string PasswordFormat = "Tigra.UserAccount { Email = '{Email}', Password='{Password}' };";

        public static byte[] MakePassword(UserAccount user, string password)
        {
            string s = PasswordFormat;
            s = s.Replace("{Email}", user.Email);
            s = s.Replace("{Password}", password);

            using (SHA256 sha = new SHA256Managed())
            {
                byte[] buf = Encoding.UTF8.GetBytes(s);
                return sha.ComputeHash(buf);
            }
        }

        public static bool ValidatePassword(UserAccount user, string password)
        {
            byte[] input = MakePassword(user, password);
            return input.CompareTo(user.Password);
        }

        private static string MakeAuthCookie(UserAccount user, bool remember)
        {
            AuthCookieModel data = new AuthCookieModel()
            {
                UserID = user.UserID,
                Email = user.Email,
                RememberMe = remember
            };

            data.FullName =
                (user.UserProfile != null && user.UserProfile.FullName.Trim().Length != 0)
                ? user.UserProfile.FullName
                : user.Email.Substring(0, user.Email.IndexOf('@'));

            var json = new JavaScriptSerializer().Serialize(data);

            FormsAuthenticationTicket ticket =
                new FormsAuthenticationTicket(1, data.FullName, DateTime.Now, DateTime.Now.AddMinutes(240)
                    , data.RememberMe, json, FormsAuthentication.FormsCookiePath);

            return FormsAuthentication.Encrypt(ticket);
        }

        public static AuthCookieModel GetLoggedUser()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var id = (FormsIdentity)HttpContext.Current.User.Identity;
                var data = id.Ticket.UserData;
                AuthCookieModel user = (AuthCookieModel)new JavaScriptSerializer().Deserialize(data, typeof(AuthCookieModel));
                return user;
            }
            return null;
        }

        public static void RefreshCookie()
        {
            AuthCookieModel user = GetLoggedUser();

            if (user != null)
            {
                using (var ctx = new Entities())
                {
                    UserAccount ua = ctx.UserAccounts.FirstOrDefault(i => i.UserID == user.UserID);

                    if (ua != null)
                    {
                        var ticket = MakeAuthCookie(ua, user.RememberMe);
                        HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, ticket);
                        HttpContext.Current.Response.Cookies.Set(ck);
                    }
                }
            }
        }

        public static bool Login(AuthenticationModel model)
        {
            using (var ctx = new Entities())
            {
                UserAccount user = ctx.UserAccounts.Where(i => i.Email == model.Email).FirstOrDefault();

                if (user != null)
                {
                    if (ValidatePassword(user, model.Password))
                    {
                        var ticket = MakeAuthCookie(user, model.RememberMe);
                        HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, ticket);
                        HttpContext.Current.Response.Cookies.Set(ck);

                        return true;
                    }
                }
            }
            return false;
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}