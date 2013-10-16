using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tigra.Models
{
    public class RateTopicModel
    {
        public RequirementsDetailsModel Topic { get; set; }

        public UserRatingModel Rating { get; set; }

        public RateTopicModel()
        {
            //
        }

        public RateTopicModel(RequirementsDetailsModel req)
        {
            this.Topic = req;
            this.Rating = new UserRatingModel(req.RevisionId);
        }
    }
}