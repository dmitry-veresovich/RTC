using System;
using System.Web.Mvc;

namespace Rtc.Mvc.HtmlHelpers
{
    public static class ImageHelper
    {
        public static string JpgImageSrc(this HtmlHelper html, byte[] image)
        {
            return "data:image/jpeg;base64," + Convert.ToBase64String(image);
        }

        public static MvcHtmlString Image(this HtmlHelper html, string src, string alternativeText, int size)
        {
            return html.Image(src, alternativeText, size, size);
        }

        public static MvcHtmlString Image(this HtmlHelper html, string src, string alternativeText, int width, int height)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("alt", alternativeText);
            builder.MergeAttribute("width", width.ToString());
            builder.MergeAttribute("height", height.ToString());
            builder.MergeAttribute("src", src);
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }


    }
}