using System.Threading.Tasks;
using Fluid;
using Fluid.Values;
using OrchardCore.Entities;
using OrchardCore.Liquid;
using OrchardCore.Users.Models;
using Skywalker.OrchardCore.ContentExtensions.Models;

namespace Skywalker.OrchardCore.ContentExtensions.Liquid.Filters
{
    public class UserAuthorNameFilter : ILiquidFilter
    {
        public ValueTask<FluidValue> ProcessAsync(FluidValue input, FilterArguments arguments, TemplateContext ctx)
        {
            var user = (User)input.ToObjectValue();
            var author = user?.As<Author>();
            var name = string.IsNullOrWhiteSpace(author?.Name) ? user?.UserName : author.Name;
            
            return new ValueTask<FluidValue>(new StringValue(name));
        }
    }
}