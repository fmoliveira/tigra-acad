using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tigra
{
    /// <summary>
    /// Possible status of any requirement revision.
    /// </summary>
    public enum RevisionStatus : byte
    {
        /// <summary>
        /// Requirement is elicited, awating for documentation.
        /// </summary>
        Elicited = 1,

        /// <summary>
        /// Requirement is documented, awaiting for approval.
        /// </summary>
        Documented = 2,

        /// <summary>
        /// Requirement is approved, process is finished.
        /// </summary>
        Approved = 3
    }
}