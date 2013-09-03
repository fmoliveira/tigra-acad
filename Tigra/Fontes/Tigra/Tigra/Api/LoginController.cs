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
    /// API class for user authentication.
    /// </summary>
    public class LoginController : ApiController
    {

        // POST api/login
        public HttpResponseMessage Post(AuthenticationModel value)
        {
            try
            {
                /* Test if credentials are accepted. */
                if (Authentication.Login(value))
                {
                    return new HttpResponseMessage(HttpStatusCode.Accepted);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

    }
}
