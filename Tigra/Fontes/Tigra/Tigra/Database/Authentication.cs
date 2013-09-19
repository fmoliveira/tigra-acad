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
        /// <summary>
        /// Format used to salt the passwords.
        /// </summary>
        private const string Salt = "Tigra.UserAccount { Email = '{Email}', Password='{Password}' };";

        /// <summary>
        /// Make a salted password for the given user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static byte[] MakePassword(UserAccount user, string password)
        {
            string s = Salt;
            s = s.Replace("{Email}", user.Email);
            s = s.Replace("{Password}", password);

            using (SHA256 sha = new SHA256Managed())
            {
                byte[] buf = Encoding.UTF8.GetBytes(s);
                return sha.ComputeHash(buf);
            }
        }

        /// <summary>
        /// Validates the existing password of a given user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool ValidatePassword(UserAccount user, string password)
        {
            byte[] input = MakePassword(user, password);
            return input.CompareTo(user.Password);
        }

        /// <summary>
        /// Creates a new ticket for a forms authentication cookie.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="remember"></param>
        /// <returns></returns>
        private static string MakeAuthCookie(UserAccount user, bool remember)
        {
            AuthCookieModel data = new AuthCookieModel()
            {
                UserID = user.UserID,
                Email = user.Email,
                UserTheme = user.UserProfile.UserTheme,
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

        /// <summary>
        /// Gets logged user from the authentication ticket.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Refresh authentication cookie with updated details of the logged user.
        /// </summary>
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

        /// <summary>
        /// Validates user login.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Login(AuthenticationModel model)
        {
            using (var ctx = new Entities())
            {
                /* Find user by email address. */
                UserAccount user = ctx.UserAccounts.Where(i => i.Email == model.Email).FirstOrDefault();

                /* Validate user password if it exists. */
                if (user != null)
                {
                    if (ValidatePassword(user, model.Password))
                    {
                        /* Sets authentication ticket. */
                        var ticket = MakeAuthCookie(user, model.RememberMe);
                        HttpCookie ck = new HttpCookie(FormsAuthentication.FormsCookieName, ticket);
                        HttpContext.Current.Response.Cookies.Set(ck);

                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Logs user out.
        /// </summary>
        public static void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}