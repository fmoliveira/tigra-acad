//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tigra.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Requirement
    {
        public Requirement()
        {
            this.RequirementRevisions = new HashSet<RequirementRevision>();
        }
    
        public int RequirementID { get; set; }
        public byte ReqType { get; set; }
        public int CellID { get; set; }
    
        public virtual Cell Cell { get; set; }
        public virtual ICollection<RequirementRevision> RequirementRevisions { get; set; }
    }
}