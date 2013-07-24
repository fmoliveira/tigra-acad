    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tigra.Common
{
    public class BaseController : Controller
    {
        protected DatabaseContext Database { get; set; }

        public BaseController()
        {
            this.Database = new DatabaseContext();
        }

        protected override void Dispose(bool disposing)
        {
            this.Database.Dispose();
            base.Dispose(disposing);
        }
    }
}
