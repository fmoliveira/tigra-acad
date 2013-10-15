﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Baseline> Baselines { get; set; }
        public DbSet<Cell> Cells { get; set; }
        public DbSet<RequirementRevision> RequirementRevisions { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<RequirementText> RequirementTexts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<RequirementRating> RequirementRatings { get; set; }
        public DbSet<UserRating> UserRatings { get; set; }
    
        public virtual ObjectResult<GetLatestRequirements_Result> GetLatestRequirements(Nullable<int> cellID, Nullable<System.DateTime> baselineDate, Nullable<byte> type)
        {
            var cellIDParameter = cellID.HasValue ?
                new ObjectParameter("CellID", cellID) :
                new ObjectParameter("CellID", typeof(int));
    
            var baselineDateParameter = baselineDate.HasValue ?
                new ObjectParameter("BaselineDate", baselineDate) :
                new ObjectParameter("BaselineDate", typeof(System.DateTime));
    
            var typeParameter = type.HasValue ?
                new ObjectParameter("Type", type) :
                new ObjectParameter("Type", typeof(byte));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetLatestRequirements_Result>("GetLatestRequirements", cellIDParameter, baselineDateParameter, typeParameter);
        }
    
        public virtual ObjectResult<GetRequirementHistory_Result> GetRequirementHistory(string tag, Nullable<System.DateTime> baselineDate)
        {
            var tagParameter = tag != null ?
                new ObjectParameter("Tag", tag) :
                new ObjectParameter("Tag", typeof(string));
    
            var baselineDateParameter = baselineDate.HasValue ?
                new ObjectParameter("BaselineDate", baselineDate) :
                new ObjectParameter("BaselineDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRequirementHistory_Result>("GetRequirementHistory", tagParameter, baselineDateParameter);
        }
    
        public virtual int SaveRequirement(Nullable<short> reqTypeID, Nullable<int> cellID, Nullable<long> revisionID, Nullable<int> userID, string message, string tag, string title, string text, Nullable<long> storyID)
        {
            var reqTypeIDParameter = reqTypeID.HasValue ?
                new ObjectParameter("ReqTypeID", reqTypeID) :
                new ObjectParameter("ReqTypeID", typeof(short));
    
            var cellIDParameter = cellID.HasValue ?
                new ObjectParameter("CellID", cellID) :
                new ObjectParameter("CellID", typeof(int));
    
            var revisionIDParameter = revisionID.HasValue ?
                new ObjectParameter("RevisionID", revisionID) :
                new ObjectParameter("RevisionID", typeof(long));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var messageParameter = message != null ?
                new ObjectParameter("Message", message) :
                new ObjectParameter("Message", typeof(string));
    
            var tagParameter = tag != null ?
                new ObjectParameter("Tag", tag) :
                new ObjectParameter("Tag", typeof(string));
    
            var titleParameter = title != null ?
                new ObjectParameter("Title", title) :
                new ObjectParameter("Title", typeof(string));
    
            var textParameter = text != null ?
                new ObjectParameter("Text", text) :
                new ObjectParameter("Text", typeof(string));
    
            var storyIDParameter = storyID.HasValue ?
                new ObjectParameter("StoryID", storyID) :
                new ObjectParameter("StoryID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SaveRequirement", reqTypeIDParameter, cellIDParameter, revisionIDParameter, userIDParameter, messageParameter, tagParameter, titleParameter, textParameter, storyIDParameter);
        }
    
        public virtual ObjectResult<GetRequirementsIndex_Result> GetRequirementsIndex(Nullable<int> cellID, Nullable<short> reqTypeID, Nullable<System.DateTime> baselineDate)
        {
            var cellIDParameter = cellID.HasValue ?
                new ObjectParameter("CellID", cellID) :
                new ObjectParameter("CellID", typeof(int));
    
            var reqTypeIDParameter = reqTypeID.HasValue ?
                new ObjectParameter("ReqTypeID", reqTypeID) :
                new ObjectParameter("ReqTypeID", typeof(short));
    
            var baselineDateParameter = baselineDate.HasValue ?
                new ObjectParameter("BaselineDate", baselineDate) :
                new ObjectParameter("BaselineDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRequirementsIndex_Result>("GetRequirementsIndex", cellIDParameter, reqTypeIDParameter, baselineDateParameter);
        }
    
        public virtual ObjectResult<GetRequirementDetails_Result> GetRequirementDetails(string tag, Nullable<System.DateTime> baselineDate)
        {
            var tagParameter = tag != null ?
                new ObjectParameter("Tag", tag) :
                new ObjectParameter("Tag", typeof(string));
    
            var baselineDateParameter = baselineDate.HasValue ?
                new ObjectParameter("BaselineDate", baselineDate) :
                new ObjectParameter("BaselineDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRequirementDetails_Result>("GetRequirementDetails", tagParameter, baselineDateParameter);
        }
    
        public virtual ObjectResult<GetRatingsIndex_Result> GetRatingsIndex(Nullable<int> cellID, Nullable<int> userID)
        {
            var cellIDParameter = cellID.HasValue ?
                new ObjectParameter("CellID", cellID) :
                new ObjectParameter("CellID", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetRatingsIndex_Result>("GetRatingsIndex", cellIDParameter, userIDParameter);
        }
    }
}
