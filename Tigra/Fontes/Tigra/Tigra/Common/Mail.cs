using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Tigra.Database;

namespace Tigra.Common
{
    public static class Mail
    {
        public static string FullUrl(string relative)
        {
            UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            return String.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, url.Content(relative));
        }

        public static string MakeToken(UserAccount user, string action, DateTime? expires = null)
        {
            if (expires == null)
            {
                expires = DateTime.UtcNow.AddDays(1);
            }

            string data = String.Format("UserId:{0},Email:{1},Action:{2},Expires:{3:yyyyMMddHHmmss}"
                , user.UserID, user.Email, action, expires);

            string token = Crypt.EncryptString(data);
            byte[] buf = Encoding.UTF8.GetBytes(token);
            token = String.Empty;
            buf.ToList().ForEach(i => token += i.ToString("X2"));

            return token;
        }

        public static bool ValidateToken(string token)
        {
            Regex r = new Regex("^UserId:(?<UserId>[0-9]+),Email:(?<Email>.*),Action:(?<Action>[A-Z]+),Expires:(?<Year>[0-9]{4})(?<Month>[0-9]{2})(?<Day>[0-9]{2})(?<Hour>[0-9]{2})(?<Minute>[0-9]{2})(?<Second>[0-9]{2})$");

            byte[] buf = new byte[token.Length / 2];
            for (int i = 0; i < buf.Length; i++)
            {
                buf[i] = byte.Parse(token.Substring(i * 2, 2), NumberStyles.AllowHexSpecifier, null);
            }
            token = Encoding.UTF8.GetString(buf);

            string data = Crypt.DecryptString(token);
            Match m = r.Match(data);

            if (m.Success)
            {
                int uid, day, mon, year, hour, min, sec;
                uid = int.Parse(m.Groups["UserId"].Value);
                day = int.Parse(m.Groups["Day"].Value);
                mon = int.Parse(m.Groups["Month"].Value);
                year = int.Parse(m.Groups["Year"].Value);
                hour = int.Parse(m.Groups["Hour"].Value);
                min = int.Parse(m.Groups["Minute"].Value);
                sec = int.Parse(m.Groups["Second"].Value);

                DateTime expires = new DateTime(year, mon, day, hour, min, sec);

                if (DateTime.UtcNow < expires)
                {
                    using (var ctx = new Entities())
                    {
                        string email = m.Groups["Email"].Value;
                        UserAccount user = ctx.UserAccounts.FirstOrDefault(i => i.UserID == uid && i.Email == email);

                        if (user != null)
                        {
                            switch (m.Groups["Action"].Value)
                            {
                                case "REGISTER":
                                    user.Enabled = true;
                                    if (ctx.SaveChanges() != 0)
                                    {
                                        return true;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static void SendRegisterTokenMail(UserAccount user)
        {
            string token = Mail.MakeToken(user, "REGISTER");
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("SetupLink", Mail.FullUrl("~/"));
            values.Add("LogoUrl", Mail.FullUrl("~/Content/logo_email.png"));
            values.Add("ConfirmAccountCreationLink", String.Format("{0}{1}", Mail.FullUrl("~/Account/Confirm?token="), token));
            Mail.SendMail(user.Email, "Mail.ConfirmAccountCreation", values);
        }

        public static bool SendMail(string recipient, string template, Dictionary<string, string> values)
        {
            string culture = "pt-BR";
            string fn = String.Format("~/Templates/{0}/{1}.html", culture, template);
            string path = HttpContext.Current.Server.MapPath(fn);

            if (File.Exists(path))
            {
                using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        string subject = reader.ReadLine();
                        string body = reader.ReadToEnd();
                        reader.Close();

                        foreach (var i in values)
                        {
                            body = body.Replace("{" + i.Key + "}", i.Value);
                        }

                        using (var mail = new MailMessage())
                        {
                            mail.From = new MailAddress("tigra@fmoliveira.com.br", "Tigra");
                            mail.To.Add(new MailAddress(recipient));
                            mail.Subject = subject;
                            mail.Body = body;
                            mail.IsBodyHtml = true;

                            using (var smtp = new SmtpClient())
                            {
                                smtp.Send(mail);
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}