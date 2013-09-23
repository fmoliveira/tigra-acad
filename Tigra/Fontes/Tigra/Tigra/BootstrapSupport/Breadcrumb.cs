using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Tigra.BootstrapSupport
{
    public class Breadcrumb
    {
        public string HtmlCode { get; private set; }

        private Dictionary<string, string> mLevelsDicionary = null;

        private Dictionary<string, string> LevelsDicionary
        {
            get
            {
                if (this.mLevelsDicionary == null)
                {
                    this.mLevelsDicionary = new Dictionary<string, string>();
                    this.mLevelsDicionary.Add("Admin", "Administração");
                    this.mLevelsDicionary.Add("Home", "Início");
                    this.mLevelsDicionary.Add("Stories", "Histórias");
                    this.mLevelsDicionary.Add("Requirements", "Requisitos");
                    this.mLevelsDicionary.Add("Revision", "Revisão");
                    this.mLevelsDicionary.Add("Cells", "Células");
                    this.mLevelsDicionary.Add("Teams", "Equipes");
                    this.mLevelsDicionary.Add("Roles", "Permissões");
                    this.mLevelsDicionary.Add("Create", "Criar novo");
                    this.mLevelsDicionary.Add("NewRequirement", "Criar novo requisito");
                    this.mLevelsDicionary.Add("Edit", "Editar");
                    this.mLevelsDicionary.Add("History", "Histórico");
                    this.mLevelsDicionary.Add("Delete", "Excluir");
                    this.mLevelsDicionary.Add("Error", "Erro");
                }

                return this.mLevelsDicionary;
            }
        }

        private string Route(UrlHelper url, RouteValueDictionary routevalues)
        {
            string href = string.Empty;

            foreach (KeyValuePair<string, object> item in routevalues)
            {
                if (item.Value != null && item.Value.ToString().Length != 0)
                {
                    href += (href.Length == 0 ? "~/" : "/") + item.Value;
                }
            }

            return url.Content(href);
        }

        public Breadcrumb(UrlHelper url, params RouteValueDictionary[] routevalues)
        {
            /* Merge all route values into one list. */
            var list = new RouteValueDictionary();
            routevalues.ToList().ForEach(i => i.ToList().ForEach(j => list.Add(j.Key, j.Value)));

            /* Those are the levels in the desired order of display. */
            var levels = new string[] { "area", "cell", "controller", "tag", "action" };

            /* Creates breadcrumb. */
            StringBuilder sb = new StringBuilder();

            /* Inserts top level. */
            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", url.Content("~/"));
            a.SetInnerText("Tigra");
            TagBuilder li = new TagBuilder("li") { InnerHtml = a.ToString() };
            sb.Append(li.ToString());

            /* Add all empty values to the route dictionary. */
            var atb = new RouteValueDictionary();
            levels.ToList().ForEach(i => atb.Add(i, null));

            /* Reset item tags to properly handle current active level. */
            TagBuilder oli, oa;
            oli = oa = li = a = null;

            /* Iterate through all bradcrumb levels. */
            foreach (var k in levels)
            {
                oli = li;
                oa = a;

                /* Check for current level. */
                object v;
                if (true == list.TryGetValue(k, out v) && v != null && v.ToString().Length != 0)
                {
                    a = new TagBuilder("a");
                    atb[k] = v.ToString();
                    a.MergeAttribute("href", this.Route(url, atb));

                    string title = v.ToString();

                    /* Get cell name. */
                    if (k == "cell")
                    {
                        title = v.GetCell().CellName;
                    }
                    /* Get topic title. */
                    else if (k == "tag")
                    {
                        if (list.TryGetValue("title", out v))
                        {
                            title = v.ToString();
                        }
                    }
                    /* Get area, controller or action description. */
                    else
                    {
                        LevelsDicionary.TryGetValue(title, out title);
                    }

                    /* Skip if title not found. */
                    if (title == null)
                    {
                        continue;
                    }

                    a.SetInnerText(title.ToString());
                    li = new TagBuilder("li") { InnerHtml = a.ToString() };

                    /* Add last level in memory. */
                    if (oli != null && oa != null)
                    {
                        sb.Append(oli.ToString());
                        oli = li;
                        oa = a;
                    }
                }
            }

            /* Remove link of current active level. */
            oli.InnerHtml = oa.InnerHtml;
            oli.AddCssClass("active");
            sb.Append(oli.ToString());

            /* Append all to breadcrumb. */
            var ol = new TagBuilder("ol");
            ol.AddCssClass("breadcrumb");
            ol.InnerHtml = sb.ToString();

            /* Sets HTML code. */
            this.HtmlCode = ol.ToString();
        }
    }
}