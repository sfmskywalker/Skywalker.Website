using System.Threading;
using System.Threading.Tasks;
using Fluid;
using Fluid.Values;
using Microsoft.AspNetCore.Identity;
using OrchardCore.Liquid;
using OrchardCore.Users;
using OrchardCore.Users.Models;

namespace Skywalker.OrchardCore.ContentExtensions.Liquid.Filters
{
    public class UserFilter : ILiquidFilter
    {
        private readonly IUserStore<IUser> _userStore;

        public UserFilter(IUserStore<IUser> userStore)
        {
            _userStore = userStore;
        }
        
        public async ValueTask<FluidValue> ProcessAsync(FluidValue input, FilterArguments arguments, TemplateContext ctx)
        {
            var userName = input.ToStringValue();
            var user = (User)await _userStore.FindByNameAsync(userName, CancellationToken.None);

            return user == null ? (FluidValue) NilValue.Instance : new ObjectValue(user);
        }
    }
}