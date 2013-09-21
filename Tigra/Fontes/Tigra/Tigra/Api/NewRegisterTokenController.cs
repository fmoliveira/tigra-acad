using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tigra.Common;
using Tigra.Database;
using Tigra.Models;

namespace Tigra.Api
{
    /// <summary>
    /// API class for user authentication.
    /// </summary>
    public class NewRegisterTokenController : ApiController
    {

        // POST api/login
        public HttpResponseMessage Post(EmailModel value)
        {
            try
            {
                using (var ctx = new Entities())
                {
                    UserAccount user = ctx.UserAccounts.FirstOrDefault(i => i.Email == value.Email);

                    if (user != null)
                    {
                        if (false == user.Enabled)
                        {
                            Mail.SendRegisterTokenMail(user);
                            return new HttpResponseMessage(HttpStatusCode.Accepted);
                        }
                        else
                        {
                            return new HttpResponseMessage(HttpStatusCode.NotModified);
                        }
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound);
                    }
                }
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

    }
}
