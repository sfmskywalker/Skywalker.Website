using System.Threading.Tasks;
using Fluid;
using OrchardCore.Liquid;
using Skywalker.OrchardCore.ContentExtensions.Models;

namespace Skywalker.OrchardCore.ContentExtensions.Liquid.Handlers
{
    public class AuthorLiquidHandler : ILiquidTemplateEventHandler
    {
        static AuthorLiquidHandler()
        {
            TemplateContext.GlobalMemberAccessStrategy.Register<Author>();
        }
        
        public Task RenderingAsync(TemplateContext context) => Task.CompletedTask;
    }
}
