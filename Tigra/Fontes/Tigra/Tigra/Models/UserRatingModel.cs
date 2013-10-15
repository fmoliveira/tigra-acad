using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tigra.Models
{
    public class UserRatingModel
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Clareza"), DataType("StarRating")]
        public int RatingA { get; set; }

        [DisplayName("Coerência"), DataType("StarRating")]
        public int RatingB { get; set; }

        [DisplayName("Objetividade"), DataType("StarRating")]
        public int RatingC { get; set; }

        [DisplayName("Comentários"), DataType(DataType.MultilineText)]
        public string Comments { get; set; }

        public UserRatingModel()
        {
            this.RatingA = 1;
            this.RatingB = 2;
            this.RatingC = 3;
        }
    }
}