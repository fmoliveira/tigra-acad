//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tigra.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Baseline
    {
        public Baseline()
        {
            this.RequirementRevisions = new HashSet<RequirementRevision>();
        }
    
        public int BaselineId { get; set; }
        public int ProjectId { get; set; }
        public Nullable<int> ModuleId { get; set; }
        public string Version { get; set; }
        public System.DateTime Built { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
    
        public virtual Module Module { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<RequirementRevision> RequirementRevisions { get; set; }
    }
}
