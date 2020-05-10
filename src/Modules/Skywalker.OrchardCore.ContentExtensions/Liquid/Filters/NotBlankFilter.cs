using System.Threading.Tasks;
using Fluid;
using Fluid.Values;
using OrchardCore.Liquid;

namespace Skywalker.OrchardCore.ContentExtensions.Liquid.Filters
{
    public class NotBlankFilter : ILiquidFilter
    {
        public async ValueTask<FluidValue> ProcessAsync(FluidValue input, FilterArguments arguments, TemplateContext ctx)
        {
            var text = input.ToStringValue();
            return string.IsNullOrWhiteSpace(text) ? BooleanValue.False : BooleanValue.True;
        }
    }
}