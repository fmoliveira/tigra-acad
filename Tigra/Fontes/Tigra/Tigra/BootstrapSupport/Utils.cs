using System;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Tigra.Database;
using Tigra;
using Tigra.BootstrapSupport;

namespace BootstrapSupport
{
    /// <summary>
    /// Util helper methods.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Creates a submit button.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static MvcHtmlString SubmitButton(this HtmlHelper helper, string text = "Salvar")
        {
            var btn = new TagBuilder("button");
            btn.MergeAttribute("type", "submit");
            btn.AddCssClass("btn");
            btn.AddCssClass("btn-lg");
            btn.AddCssClass("btn-primary");
            btn.SetInnerText(text);

            return MvcHtmlString.Create(btn.ToString());
        }

        /// <summary>
        /// Creates an action button.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static MvcHtmlString ActionButton(this HtmlHelper helper, string text, string context, string action, RouteValueDictionary routevalues = null)
        {
            UrlHelper h = new UrlHelper(helper.ViewContext.RequestContext);
            var btn = new TagBuilder("a");
            btn.MergeAttribute("href", h.Action(action, routevalues));
            btn.AddCssClass("btn");
            btn.AddCssClass("btn-lg");
            btn.AddCssClass("btn-" + context);
            btn.SetInnerText(text);

            return MvcHtmlString.Create(btn.ToString());
        }

        /// <summary>
        /// Creates a cancel button.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static MvcHtmlString CancelButton(this HtmlHelper helper, object model)
        {
            string action = "Index";

            if (model != null)
            {
                object v = model.GetId();
                if (v != null)
                {
                    int w;
                    if (int.TryParse(v.ToString(), out w))
                    {
                        if (w != 0)
                        {
                            action = "Details";
                        }
                    }
                }
            }

            return ActionButton(helper, "Cancelar", "default", action);
        }

        private static MenuArea _menuArea = null;

        public static MenuArea MenuAreaFor(this HtmlHelper helper, string areaName)
        {
            _menuArea = new MenuArea(areaName);
            return _menuArea;
        }

        /// <summary>
        /// Creates a menu link and automatically make it active if it's the current page.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="linkText"></param>
        /// <param name="glyphIcon"></param>
        /// <returns></returns>
        public static MvcHtmlString MenuLink(this HtmlHelper helper, string controllerName, string actionName, string linkText, string glyphIcon = null)
        {
            TagBuilder a, b, li;
            UrlHelper url = new UrlHelper(helper.ViewContext.RequestContext);

            a = new TagBuilder("a");

            if (glyphIcon != null)
            {
                b = new TagBuilder("b");
                b.AddCssClass("glyphicon");
                b.AddCssClass("glyphicon-" + glyphIcon);
                a.InnerHtml = b.ToString() + " &nbsp; " + linkText;
            }
            else
            {
                a.InnerHtml = linkText;
            }

            a.MergeAttribute("href", url.RouteUrl("Menu", new { @controller = controllerName }));

            li = new TagBuilder("li");
            li.InnerHtml = a.ToString();

            var ctrl = helper.ViewContext.RouteData.GetRequiredString("controller");
            var act = helper.ViewContext.RouteData.GetRequiredString("action");
            if (controllerName == ctrl)
            {
                if (actionName == "Index" || actionName == act)
                {
                    li.AddCssClass("active");
                }
            }

            return MvcHtmlString.Create(li.ToString());
        }

        /// <summary>
        /// Returns display name for currently selected cell.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="defaultStr"></param>
        /// <returns></returns>
        public static string CurrentCell(this HtmlHelper helper, string defaultStr = "")
        {
            string s = defaultStr;

            if (helper.ViewContext.RouteData.Values["cell"] != null)
            {
                Cell item = helper.ViewContext.RouteData.Values["cell"].GetCell();

                if (item != null)
                {
                    s = item.CellName;
                }
            }

            return s;
        }

        /// <summary>
        /// Creates a link to change to another cell.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="cellName"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static MvcHtmlString ChangeCellLink(this HtmlHelper helper, string cellName, string description)
        {
            bool active = false;

            if (helper.ViewContext.RouteData.Values["cell"] != null)
            {
                if (helper.ViewContext.RouteData.Values["cell"].ToString() == cellName)
                {
                    active = true;
                }
            }

            var ctrl = helper.ViewContext.RouteData.GetRequiredString("controller");
            var act = (helper.ViewContext.RouteData.Values.ContainsKey("id") && true == active ? "Details" : "Index");

            TagBuilder a, li;
            UrlHelper url = new UrlHelper(helper.ViewContext.RequestContext);

            a = new TagBuilder("a");
            //a.MergeAttribute("href", url.Action("Index", new { @cell = cellName, @id = "" }));
            a.MergeAttribute("href", url.RouteUrl(new { @controller = ctrl, @action = act, @cell = cellName }));
            a.SetInnerText(description);

            li = new TagBuilder("li");
            li.InnerHtml = a.ToString();

            if (true == active)
            {
                li.AddCssClass("active");
            }

            return MvcHtmlString.Create(li.ToString());
        }

        public static MvcHtmlString MakeBreadcrumb(this HtmlHelper helper)
        {
            UrlHelper url = new UrlHelper(helper.ViewContext.RequestContext);
            Breadcrumb bc = new Breadcrumb(url, helper.ViewContext.RouteData.DataTokens, helper.ViewContext.RouteData.Values);
            return MvcHtmlString.Create(bc.HtmlCode.ToString());
        }
        
        /// <summary>
        /// Format a tag to be used in canonical links.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Tagify(string value)
        {
            value = value.Replace(" ", "_");

            var normalizedString = value.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }

    /// <summary>
    /// Wrap inner links to a controller area.
    /// </summary>
    public class MenuArea : IDisposable
    {
        public string AreaName { get; private set; }

        public MenuArea(string areaName)
        {
            this.AreaName = areaName;
        }

        public void Dispose()
        {
            this.AreaName = null;
        }
    }
}