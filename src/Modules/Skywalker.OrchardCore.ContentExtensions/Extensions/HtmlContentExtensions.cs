using Microsoft.AspNetCore.Html;

namespace Skywalker.OrchardCore.ContentExtensions.Extensions
{
    public static class HtmlContentExtensions
    {
        public static string StripHtml(this IHtmlContent content) => content.ToString()!.StripHtml();
    }
}