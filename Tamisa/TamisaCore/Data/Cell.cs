using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamisa.Data
{
    public class Cell
    {
        #region Properties

        public int CellId { get; private set; }

        public string CellName { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        #endregion
    }
}
