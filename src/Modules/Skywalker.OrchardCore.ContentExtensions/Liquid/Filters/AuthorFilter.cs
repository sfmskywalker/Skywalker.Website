using System.Threading.Tasks;
using Fluid;
using Fluid.Values;
using OrchardCore.Entities;
using OrchardCore.Liquid;
using OrchardCore.Users.Models;
using Skywalker.OrchardCore.ContentExtensions.Models;

namespace Skywalker.OrchardCore.ContentExtensions.Liquid.Filters
{
    public class AuthorFilter : ILiquidFilter
    {
        public ValueTask<FluidValue> ProcessAsync(FluidValue input, FilterArguments arguments, TemplateContext ctx)
        {
            var user = (User)input.ToObjectValue();
            var author = user == null ? (FluidValue) NilValue.Instance : new ObjectValue(user.As<Author>());
            
            return new ValueTask<FluidValue>(author);
        }
    }
}