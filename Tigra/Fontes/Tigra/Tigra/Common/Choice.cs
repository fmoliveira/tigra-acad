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
        public Dictionary<string,string> Options { get; set; }

        public string Selected { get; set; }

        public Choice(List<string> options, bool createDefault, string selected)
        {
            this.Options= new Dictionary<string,string>();
            
            if(true == createDefault)
            {
                this.Options.Add("default", "(Padrão)");
            }

            options.ForEach(i => this.Options.Add(i, CultureInfo.CurrentCulture.TextInfo.ToTitleCase(i)));
            this.Selected = selected;
        }

        public Choice(Dictionary<int, string> options)
        {
            this.Options = new Dictionary<string, string>();
            options.ToList().ForEach(i => this.Options.Add(i.Key.ToString(), i.Value));
        }

        public SelectList GetSelectList()
        {
            return new SelectList(this.Options, "Key", "Value", this.Selected);
        }
    }
}