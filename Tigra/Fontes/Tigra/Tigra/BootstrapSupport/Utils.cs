using System.Globalization;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace BootstrapSupport
{
    public static class Utils
    {
        public static MvcHtmlString SubmitButton(this HtmlHelper helper, string text)
        {
            var btn = new TagBuilder("button");
            btn.MergeAttribute("type", "submit");
            btn.AddCssClass("btn");
            btn.AddCssClass("btn-primary");
            btn.SetInnerText(text);

            return MvcHtmlString.Create(btn.ToString());
        }

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

            a.MergeAttribute("href", url.Action(actionName, controllerName));

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
}