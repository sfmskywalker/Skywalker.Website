using Microsoft.Extensions.DependencyInjection;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Liquid;
using OrchardCore.Modules;
using OrchardCore.Users.Models;
using Skywalker.OrchardCore.ContentExtensions.Drivers;
using Skywalker.OrchardCore.ContentExtensions.Liquid;
using Skywalker.OrchardCore.ContentExtensions.Liquid.Handlers;

namespace Skywalker.OrchardCore.ContentExtensions.Features
{
    [Feature("Skywalker.OrchardCore.ContentExtensions.Author")]
    public class AuthorStartup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddScoped<IDisplayDriver<User>, AuthorDisplayDriver>()
                .AddScoped<ILiquidTemplateEventHandler, AuthorLiquidHandler>();
        }
    }
}
