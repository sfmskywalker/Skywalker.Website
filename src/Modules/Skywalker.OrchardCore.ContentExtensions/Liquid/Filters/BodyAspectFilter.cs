using System.Threading.Tasks;
using Fluid;
using Fluid.Values;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Models;
using OrchardCore.Liquid;

namespace Skywalker.OrchardCore.ContentExtensions.Liquid.Filters
{
    public sealed class BodyAspectFilter : ILiquidFilter
    {
        private readonly IContentManager _contentManager;

        public BodyAspectFilter(IContentManager contentManager)
        {
            _contentManager = contentManager;
        }
        
        public async ValueTask<FluidValue> ProcessAsync(FluidValue input, FilterArguments arguments, TemplateContext ctx)
        {
            var contentItem = (ContentItem)input.ToObjectValue();
            var bodyAspect = await _contentManager.PopulateAspectAsync<BodyAspect>(contentItem);
            
            return new StringValue(bodyAspect.Body.ToString(), false);
        }
    }
}
