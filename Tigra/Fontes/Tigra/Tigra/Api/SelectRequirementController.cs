using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Tigra.Database;
using Tigra.Models;

namespace Tigra.Api
{
    public class SelectRequirementController : ApiController
    {
        // POST api/profile
        public HttpResponseMessage Post(UseExistingRequirementModel value)
        {
            try
            {
                using (var ctx = new Entities())
                {
                    int userID = Authentication.GetLoggedUser().UserID;
                    ctx.SelectRequirement(value.Cell, value.LeftTag, value.RightTag, userID, "Alteração de requisito para atender uma nova história");
                    ctx.SaveChanges();
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}