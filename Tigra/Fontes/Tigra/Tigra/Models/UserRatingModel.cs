﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tigra.Models
{
    public class UserRatingModel
    {
        [Required, HiddenInput]
        public long Id { get; set; }

        [DisplayName("Aprovar"), DataType("BooleanRadio"), Required]
        public bool Approved { get; set; }

        [DisplayName("Comentários"), DataType(DataType.MultilineText), Required, MaxLength(250)]
        public string Comments { get; set; }

        public UserRatingModel()
        {
            //
        }

        public UserRatingModel(long revisionId)
        {
            this.Id = revisionId;
            this.Approved = false;
        }
    }
}