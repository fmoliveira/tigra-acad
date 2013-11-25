using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tigra.Models
{
    public class RequirementNameModel
    {
        public int CellID { get; set; }

        public string Tag { get; set; }

        public RequirementNameModel()
        {
            //
        }

        public RequirementNameModel(int cellID, string tag)
        {
            this.CellID = cellID;
            this.Tag = tag;
        }
    }
}