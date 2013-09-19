using BootstrapSupport;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tigra.Common
{
    public class Choice
    {
        public List<string> Options { get; set; }

        public string Selected { get; set; }

        public List<SelectListItem> DropDownList
        {
            get
            {
                List<SelectListItem> opts = new List<SelectListItem>();
                this.Options.ForEach(i => opts.Add(new SelectListItem() { Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(i), Value = Utils.Tagify(i.ToLower()), Selected = (this.Selected != null && this.Selected.ToLower() == i.ToLower()) }));
                return opts;
            }
        }

        public Choice(List<string> options, string selected)
        {
            this.Options = options;
            this.Selected = selected;
        }
    }
}