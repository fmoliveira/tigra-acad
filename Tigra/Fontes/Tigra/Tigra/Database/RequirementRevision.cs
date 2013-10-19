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
    
    public partial class RequirementRevision
    {
        public RequirementRevision()
        {
            this.RequirementRevisions1 = new HashSet<RequirementRevision>();
            this.RequirementRevisions = new HashSet<RequirementRevision>();
            this.UserRatings = new HashSet<UserRating>();
        }
    
        public long RevisionID { get; set; }
        public int RequirementID { get; set; }
        public short RevisionNumber { get; set; }
        public System.DateTime RevisionDate { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public bool Published { get; set; }
    
        public virtual Requirement Requirement { get; set; }
        public virtual UserAccount UserAccount { get; set; }
        public virtual RequirementText RequirementText { get; set; }
        public virtual ICollection<RequirementRevision> RequirementRevisions1 { get; set; }
        public virtual ICollection<RequirementRevision> RequirementRevisions { get; set; }
        public virtual ICollection<UserRating> UserRatings { get; set; }
        public virtual RequirementRating RequirementRating { get; set; }
    }
}
