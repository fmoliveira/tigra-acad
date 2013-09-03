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
    /// <summary>
    /// API class for user registral.
    /// </summary>
    public class RegisterController : ApiController
    {

        // POST api/register
        public HttpResponseMessage Post(AuthenticationModel value)
        {
            try
            {
                using (var ctx = new Entities())
                {
                    UserAccount ua = ctx.UserAccounts.Where(i => i.Email == value.Email).FirstOrDefault();

                    /* Check if user isn't registered yet. */
                    if (ua == null)
                    {
                        /* Create the new user and salt the password. */
                        ua = new UserAccount() { Email = value.Email, RegisterDate = DateTime.Now };
                        ua.Password = Authentication.MakePassword(ua, value.Password);
                        ctx.UserAccounts.Add(ua);

                        /* Save changes. */
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

    }
}
