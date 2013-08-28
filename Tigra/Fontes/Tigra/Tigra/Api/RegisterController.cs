using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tigra.Database;
using Tigra.Models;

namespace Tigra.Api
{
    public class RegisterController : ApiController
    {
        // GET api/register
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/register/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/register
        public HttpResponseMessage Post(AuthenticationModel value)
        {
            try
            {
                using (var ctx = new Entities())
                {
                    UserAccount ua = ctx.UserAccounts.Where(i => i.Email == value.Email).FirstOrDefault();
                    if (ua == null)
                    {
                        ua = new UserAccount() { Email = value.Email, RegisterDate = DateTime.Now };
                        ua.Password = Authentication.MakePassword(ua, value.Password);
                        ctx.UserAccounts.Add(ua);

                        if (ctx.SaveChanges() != 0)
                        {
                            return new HttpResponseMessage(HttpStatusCode.Created);
                        }
                        else
                        {
                            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                        }
                    }
                    return new HttpResponseMessage(HttpStatusCode.Conflict);
                }
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        // PUT api/register/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/register/5
        public void Delete(int id)
        {
        }
    }
}
