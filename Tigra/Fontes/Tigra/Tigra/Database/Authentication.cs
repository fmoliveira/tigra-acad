using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

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
    }
}