using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tigra.Common
{
    public class AcronymAttribute : Attribute
    {
        public string Acronym { get; private set; }

        public AcronymAttribute(string acronym)
        {
            this.Acronym = acronym;
        }
    }
}