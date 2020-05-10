using System.Threading.Tasks;
using Fluid;
using Fluid.Values;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Models;
using OrchardCore.Liquid;
using Skywalker.OrchardCore.ContentExtensions.Extensions;
using Skywalker.OrchardCore.ContentExtensions.Services.Contracts;

namespace Skywalker.OrchardCore.ContentExtensions.Liquid.Filters
{
    public sealed class ReadTimeFilter : ILiquidFilter
    {
        private readonly IContentManager _contentManager;
        private readonly IReadTimeCalculator _readTimeCalculator;

        public ReadTimeFilter(IContentManager contentManager, IReadTimeCalculator readTimeCalculator)
        {
            _contentManager = contentManager;
            _readTimeCalculator = readTimeCalculator;
        }
        
        public async ValueTask<FluidValue> ProcessAsync(FluidValue input, FilterArguments arguments, TemplateContext ctx)
        {
            var text = await GetTextAsync(input);
            var readTime = _readTimeCalculator.CalculateReadTime(text);
            
            return NumberValue.Create(readTime);
        }

        private async Task<string> GetTextAsync(FluidValue input)
        {
            var obj = input.ToObjectValue();

            if (obj is ContentItem contentItem)
            {
                var bodyAspect = await _contentManager.PopulateAspectAsync<BodyAspect>(contentItem);
                return bodyAspect.Body.StripHtml();
            }

            return ((string) obj).StripHtml();
        }
    }
}
