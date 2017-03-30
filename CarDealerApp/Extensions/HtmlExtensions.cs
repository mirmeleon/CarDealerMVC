using System.Web.Mvc;

namespace CarDealerApp.Extensions
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString ImageHelper(this HtmlHelper helper, string imageUrl,
            string alt = null, string width = "150px", string height = "150px")
        {
            TagBuilder builder = new TagBuilder("img");
            builder.AddCssClass("img-responsive");
            builder.MergeAttribute("src", imageUrl);

            if (alt != null)
            {
                builder.MergeAttribute("alt", alt);
            }

            builder.MergeAttribute("width", width);
            builder.MergeAttribute("height", height);

            return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString YouTubeVideo(this HtmlHelper helper, string videoId, string width = "600px",
            string height = "600px")
        {
            TagBuilder builder = new TagBuilder("iframe");
            builder.MergeAttribute("width", width);
            builder.MergeAttribute("height", height);
            builder.MergeAttribute("src", $"https://www.youtube.com/embed/{videoId}");
            builder.MergeAttribute("frameborder", "0");
            builder.MergeAttribute("allowfullscreen", "allowfullscreen");

            return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
        }
    }
}