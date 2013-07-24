using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamisa.Data.Social;

namespace Tamisa.Data
{
    public class UserProfile
    {
        #region Properties

        public string FullName { get; set; }

        public string JobTitle { get; set; }

        public string Company { get; set; }

        public string Location { get; set; }

        public string Biography { get; set; }

        public List<ISocial> Social { get; set; }

        #endregion
    }
}
