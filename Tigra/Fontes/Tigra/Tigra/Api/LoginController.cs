﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using Tigra.Database;
using Tigra.Models;

namespace Tigra.Api
{
    public class LoginController : ApiController
    {
        // GET api/login
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/login/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/login
        public HttpResponseMessage Post(AuthenticationModel value)
        {
            try
            {
                using (var ctx = new Entities())
                {
                    UserAccount ua = ctx.UserAccounts.Where(i => i.Email == value.Email).FirstOrDefault();
                    if (ua != null)
                    {
                        if (Authentication.ValidatePassword(ua, value.Password))
                        {
                            FormsAuthentication.SetAuthCookie(ua.Email, value.RememberMe);
                            return new HttpResponseMessage(HttpStatusCode.Accepted);
                        }
                    }
                    return new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        // PUT api/login/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/login/5
        public void Delete(int id)
        {
        }
    }
}
