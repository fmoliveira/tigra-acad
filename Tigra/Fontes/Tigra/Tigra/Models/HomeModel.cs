using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tigra.Database;

namespace Tigra.Models
{
    public class HomeModel
    {

        public string CellName { get; set; }

        public string Description { get; set; }

        public HomeModel()
        {
            //
        }

        public HomeModel(Cell item)
        {
            this.CellName = item.CellName;
            this.Description = item.Description;
        }

    }
}