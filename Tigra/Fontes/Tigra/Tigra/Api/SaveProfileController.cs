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
    public class SaveProfileController : ApiController
    {
        // GET api/profile
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/profile/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/profile
        public HttpResponseMessage Post(MyProfileModel value)
        {
            try
            {
                using (var ctx = new Entities())
                {
                    int userid = Authentication.GetLoggedUser().UserID;
                    UserProfile profile = ctx.UserProfiles.FirstOrDefault(i => i.UserID == userid);

                    if (profile == null)
                    {
                        profile = ctx.UserProfiles.Add(new UserProfile() { UserID = userid });
                    }

                    profile.FullName = value.FullName;
                    ctx.SaveChanges();
                    Authentication.RefreshCookie();

                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        // PUT api/profile/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/profile/5
        public void Delete(int id)
        {
        }
    }
}
