using System.Threading.Tasks;
using Fluid;
using Fluid.Values;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using OrchardCore.Liquid;

namespace Skywalker.OrchardCore.ContentExtensions.Liquid.Handlers
{
    public class RouteLiquidHandler : ILiquidTemplateEventHandler
    {
        static RouteLiquidHandler()
        {
            TemplateContext.GlobalMemberAccessStrategy.Register<HttpRequest, FluidValue>((request, name) =>
            {
                switch (name)
                {
                    case "Route": return new ObjectValue(new RouteValueDictionaryWrapper(request.RouteValues));
                    default: return null!;
                }
            });

            TemplateContext.GlobalMemberAccessStrategy.Register<RouteValueDictionaryWrapper, object>((headers, name) => headers.RouteValueDictionary[name]);            
        }

        public Task RenderingAsync(TemplateContext context) => Task.CompletedTask;

        private class RouteValueDictionaryWrapper
        {
            public readonly RouteValueDictionary RouteValueDictionary;

            public RouteValueDictionaryWrapper(RouteValueDictionary routeValueDictionary)
            {
                RouteValueDictionary = routeValueDictionary;
            }
        }
    }
}
