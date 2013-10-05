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
    public class TeamManageController : ApiController
    {
        // POST api/teammanage
        public HttpResponseMessage Post(TeamMemberModel value)
        {
            using (var ctx = new Entities())
            {
                Team t = ctx.Teams.FirstOrDefault(i => i.CellID == value.CellId && i.UserID == value.UserId);
                bool createdNew = false;

                if (t != null)
                {
                    t.RoleID = value.RoleId;
                }
                else
                {
                    t = new Team()
                    {
                        CellID = value.CellId,
                        UserID = value.UserId,
                        RoleID = value.RoleId
                    };
                    ctx.Teams.Add(t);
                    createdNew = true;
                }

                if (ctx.SaveChanges() != 0)
                {
                    if(createdNew)
                    {
                        return new HttpResponseMessage(HttpStatusCode.Created);
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.Gone);
                }
            }

            return new HttpResponseMessage(HttpStatusCode.InternalServerError);
        }

        // DELETE api/teammanage/5
        public void Delete(int id)
        {
        }
    }
}
