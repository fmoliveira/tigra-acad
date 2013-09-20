using BootstrapSupport;
using Microsoft.Web.Mvc.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tigra.Common;
using Tigra.Database;

namespace Tigra.Models
{
    public class MyProfileModel
    {
        [DisplayName("Nome")]
        [Description("Digite seu nome e sobrenome")]
        [StringLength(50)]
        [AutoFocus]
        public string FullName { get; set; }

        [DisplayName("Data de nascimento")]
        [Description("Informe sua data de nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        public DateTime? BirthDate { get; set; }

        [DisplayName("Localização")]
        [Description("Em que cidade você mora?")]
        [StringLength(50)]
        public string Location { get; set; }

        [DisplayName("Tema")]
        [Description("Deixe o Tigra com a sua cara!")]
        [DataType("Choice_UserThemes")]
        public string UserTheme { get; set; }

        [DisplayName("Auto biografia")]
        [Description("Escreva um pouco sobre você...")]
        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        public string Biography { get; set; }

        public MyProfileModel()
        {
            //
        }

        public MyProfileModel(UserProfile up)
        {
            this.FullName = up.FullName;
            this.BirthDate = up.BirthDate;
            this.Location = up.Location;
            this.UserTheme = up.UserTheme;
            this.Biography = up.Biography;
        }
    }
}