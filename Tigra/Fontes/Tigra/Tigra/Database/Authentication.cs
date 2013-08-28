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
            dynamic data = new ExpandoObject();
            data.UserID = user.UserID;
            data.Email = user.Email;

            data.FullName = 
                (user.UserProfile != null) 
                ? user.UserProfile.FullName 
                : user.Email.Substring(0, user.Email.IndexOf('@'));

            var json = new JavaScriptSerializer().Serialize(data);

            FormsAuthenticationTicket ticket =
                new FormsAuthenticationTicket(1, data.FullName, DateTime.Now, DateTime.Now.AddMinutes(240)
                    , remember, json, FormsAuthentication.FormsCookiePath);

            return FormsAuthentication.Encrypt(ticket);
        }

        public static UserAccount GetLoggedUser()
        {
            HttpCookie ck = HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName];

            if (ck != null)
            {
                var ticket = FormsAuthentication.Decrypt(ck.Value);

                try
                {
                    UserAccount user = (UserAccount)(new JavaScriptSerializer().Deserialize(ticket.UserData, typeof(UserAccount)));
                    return user;
                }
                catch { }
            }
            return null;
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
                        HttpContext.Current.Response.Cookies.Add(ck);

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