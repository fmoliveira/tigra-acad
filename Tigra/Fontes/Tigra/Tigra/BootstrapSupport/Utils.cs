using System.Web.Mvc;

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
    }
}