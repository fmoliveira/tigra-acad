using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tigra.Database;

namespace Tigra.Models
{
    public class NewRequirementModel
    {
        public StoriesDetailsModel Story { get; set; }

        public RequirementCreateModel Requirement { get; set; }

        public NewRequirementModel()
        {
            //
        }

        public NewRequirementModel(GetRequirementDetails_Result item)
        {
            this.Story = new StoriesDetailsModel(item);
            this.Requirement = new RequirementCreateModel(item.RevisionID);
        }

        public NewRequirementModel(RequirementCreateModel item)
        {
            this.Story = new StoriesDetailsModel(item);
            this.Requirement = new RequirementCreateModel(item);
        }
    }
}