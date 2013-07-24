using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamisa.Data
{
    public class User // abstract or virtual?
    {
        #region Properties

        public int UserId { get; private set; }

        public string UserName { get; private set; }

        public string EmailAddress { get; set; }

        public DateTime RegisterDate { get; set; }

        public UserProfile Profile { get; set; }

        #endregion
    }
}
