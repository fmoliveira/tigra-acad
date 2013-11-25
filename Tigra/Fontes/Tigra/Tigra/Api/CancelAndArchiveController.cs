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
    public class CancelAndArchiveController : ApiController
    {

        // POST api/login
        public HttpResponseMessage Post(RequirementNameModel model)
        {
            try
            {
                using (var ctx = new Entities())
                {
                    ctx.ArchiveRequirement(model.CellID, model.Tag);
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