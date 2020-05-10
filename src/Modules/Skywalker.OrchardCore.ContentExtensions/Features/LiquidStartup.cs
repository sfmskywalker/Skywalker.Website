using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Liquid;
using OrchardCore.Modules;
using Skywalker.OrchardCore.ContentExtensions.Liquid.Filters;
using Skywalker.OrchardCore.ContentExtensions.Liquid.Handlers;
using Skywalker.OrchardCore.ContentExtensions.Services;
using Skywalker.OrchardCore.ContentExtensions.Services.Contracts;

namespace Skywalker.OrchardCore.ContentExtensions.Features
{
    [RequireFeatures("OrchardCore.Liquid", "Skywalker.OrchardCore.ContentExtensions")]
    public class LiquidStartup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IWordCounter, WordCounter>()
                .AddSingleton<IReadTimeCalculator, ReadTimeCalculator>()
                .AddScoped<ILiquidTemplateEventHandler, RouteLiquidHandler>()
                .AddLiquidFilter<UserFilter>("user")
                .AddLiquidFilter<BodyAspectFilter>("body_aspect")
                .AddLiquidFilter<ReadTimeFilter>("read_time")
                .AddLiquidFilter<BlankFilter>("blank")
                .AddLiquidFilter<NotBlankFilter>("not_blank");
        }
    }
}