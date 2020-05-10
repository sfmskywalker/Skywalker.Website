using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Liquid;
using OrchardCore.Modules;
using Skywalker.OrchardCore.ContentExtensions.Liquid;
using Skywalker.OrchardCore.ContentExtensions.Liquid.Filters;
using Skywalker.OrchardCore.ContentExtensions.Liquid.Handlers;

namespace Skywalker.OrchardCore.ContentExtensions.Features
{
    [RequireFeatures("Skywalker.OrchardCore.ContentExtensions.Author", "OrchardCore.Liquid")]
    public class AuthorLiquidStartup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddScoped<ILiquidTemplateEventHandler, AuthorLiquidHandler>()
                .AddLiquidFilter<AuthorFilter>("author").
                AddLiquidFilter<UserAuthorNameFilter>("user_author_name");
        }
    }
}
