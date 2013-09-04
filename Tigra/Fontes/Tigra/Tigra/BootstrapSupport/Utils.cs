using System;
using System.Globalization;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Tigra.Database;
using Tigra;

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
        public static MvcHtmlString ActionButton(this HtmlHelper helper, string text, string context, string action, string controller = null)
        {
            if (controller == null)
            {
                controller = helper.ViewContext.RouteData.GetRequiredString("controller");
            }

            UrlHelper h = new UrlHelper(helper.ViewContext.RequestContext);
            var btn = new TagBuilder("a");
            btn.MergeAttribute("href", h.Action(action, controller));
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
        public static MvcHtmlString CancelButton(this HtmlHelper helper, string text = "Cancelar", string action = null)
        {
            if (action == null)
            {
                action = "Index";
            }

            return ActionButton(helper, text, "default", action);
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

            object route = null;

            if (_menuArea != null)
            {
                route = new { @area = _menuArea.AreaName };
            }
            else
            {
                if (helper.ViewContext.RouteData.Values.ContainsKey("cell"))
                {
                    route = new { @cell = helper.ViewContext.RouteData.Values["cell"].ToString() };
                }
            }

            string href = url.Action(actionName, controllerName, route).Replace("//", "/");
            a.MergeAttribute("href", href);

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
        public static MvcHtmlString CurrentCell(this HtmlHelper helper, string defaultStr = "")
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

            return MvcHtmlString.Create(s);
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
            var ctrl = helper.ViewContext.RouteData.GetRequiredString("controller");
            var act = helper.ViewContext.RouteData.GetRequiredString("action");

            TagBuilder a, li;
            UrlHelper url = new UrlHelper(helper.ViewContext.RequestContext);

            a = new TagBuilder("a");
            a.MergeAttribute("href", url.Action(act, ctrl, new { @cell = cellName }));
            a.SetInnerText(description);

            li = new TagBuilder("li");
            li.InnerHtml = a.ToString();

            if (helper.ViewContext.RouteData.Values["cell"] != null)
            {
                if (helper.ViewContext.RouteData.Values["cell"].ToString() == cellName)
                {
                    li.AddCssClass("active");
                }
            }

            return MvcHtmlString.Create(li.ToString());
        }
        
        /// <summary>
        /// Format a tag to be used in links.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatLinkTag(string value)
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