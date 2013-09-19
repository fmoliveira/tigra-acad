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
    /// <summary>
    /// API class for saving user profile changes.
    /// </summary>
    public class SaveProfileController : ApiController
    {

        // POST api/profile
        public HttpResponseMessage Post(MyProfileModel value)
        {
            try
            {
                using (var ctx = new Entities())
                {
                    /* Get logged user ID. That won't be fetched from the form to avoid XSS atacks. */
                    int userid = Authentication.GetLoggedUser().UserID;
                    UserProfile profile = ctx.UserProfiles.FirstOrDefault(i => i.UserID == userid);

                    /* If there isn't a profile for this user yet, create it. */
                    if (profile == null)
                    {
                        profile = ctx.UserProfiles.Add(new UserProfile() { UserID = userid });
                    }

                    /* Set user profile info. */
                    profile.FullName = value.FullName;
                    profile.BirthDate = value.BirthDate;
                    profile.UserTheme = value.UserTheme;
                    profile.Location = value.Location;
                    profile.Biography = value.Biography;

                    /* Save changes and refresh user's full name in the cookie. */
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

    }
}
