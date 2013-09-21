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
                Authentication.AuthResponse resp =  Authentication.Login(value) ;
                switch (resp)
                {
                    case Authentication.AuthResponse.Ok:
                        return new HttpResponseMessage(HttpStatusCode.Accepted);

                    case Authentication.AuthResponse.AccountNotEnabled:
                        return new HttpResponseMessage(HttpStatusCode.NotModified);

                    case Authentication.AuthResponse.InvalidCredentials:
                        return new HttpResponseMessage(HttpStatusCode.Unauthorized);

                    default:
                        return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

    }
}
