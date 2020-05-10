using System;
using System.Threading;
using System.Threading.Tasks;
using Fluid;
using Fluid.Values;
using GravatarHelper.NetStandard;
using Microsoft.AspNetCore.Identity;
using OrchardCore.Liquid;
using OrchardCore.Users;
using OrchardCore.Users.Models;

namespace Skywalker.OrchardCore.Gravatar.Filters
{
    public class GravatarFilter : ILiquidFilter
    {
        private readonly IUserStore<IUser> _userStore;

        public GravatarFilter(IUserStore<IUser> userStore)
        {
            _userStore = userStore;
        }

        public async ValueTask<FluidValue> ProcessAsync(
            FluidValue input,
            FilterArguments arguments,
            TemplateContext ctx)
        {
            var email = await GetEmailAsync(input);

            if (email == null)
                return NilValue.Instance;

            var size = arguments["size"];
            var imageType = arguments["type"];
            var rating = arguments["rating"];

            var url = GravatarHelper.NetStandard.Gravatar.GetGravatarImageUrl(
                email,
                size.IsNil() ? 0 : (int) size.ToNumberValue(),
                gravatarDefaultImageType: imageType.IsNil()
                    ? default(GravatarDefaultImageType?)
                    : Enum.Parse<GravatarDefaultImageType>(imageType.ToStringValue()),
                rating: rating.IsNil() ? default(GravatarRating?) : Enum.Parse<GravatarRating>(rating.ToStringValue()));

            return new StringValue(url);
        }

        private async Task<string?> GetEmailAsync(FluidValue input)
        {
            var obj = input.ToObjectValue();

            if (obj is string text)
            {
                if (text.Contains("@"))
                    return text;

                var user = (User) await _userStore.FindByNameAsync(text, CancellationToken.None);

                return user?.Email;
            }
            else
            {
                if (obj is User user)
                    return user.Email;
            }

            return null;
        }
    }
}