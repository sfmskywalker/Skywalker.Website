using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Liquid;
using OrchardCore.Modules;
using Skywalker.OrchardCore.Gravatar.Filters;

namespace Skywalker.OrchardCore.Gravatar.Features
{
    [RequireFeatures("Skywalker.OrchardCore.Gravatar", "OrchardCore.Liquid")]
    public class LiquidStartup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddLiquidFilter<GravatarFilter>("gravatar");
        }
    }
}
