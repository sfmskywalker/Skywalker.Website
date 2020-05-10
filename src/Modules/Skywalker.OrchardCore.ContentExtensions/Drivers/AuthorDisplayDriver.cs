using System.Threading.Tasks;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Users.Models;
using Skywalker.OrchardCore.ContentExtensions.Models;

namespace Skywalker.OrchardCore.ContentExtensions.Drivers
{
    public class AuthorDisplayDriver : SectionDisplayDriver<User, Author>
    {
        public override IDisplayResult Edit(Author author, BuildEditorContext context)
        {
            return Initialize<Author>("Author_Edit", model =>
            {
                model.Name = author.Name;
                model.TagLine = author.TagLine;
                model.Bio = author.Bio;
                model.FacebookUrl = author.FacebookUrl;
                model.TwitterUrl = author.TwitterUrl;
                model.LinkedInUrl = author.LinkedInUrl;
            }).Location("Content:10");
        }

        public override async Task<IDisplayResult> UpdateAsync(Author author, BuildEditorContext context)
        {
            var model = new Author();

            if (await context.Updater.TryUpdateModelAsync(model, Prefix))
            {
                author.Name = model.Name?.Trim();
                author.TagLine = model.TagLine?.Trim();
                author.Bio = model.Bio?.Trim();
                author.FacebookUrl = model.FacebookUrl?.Trim();
                author.TwitterUrl = model.TwitterUrl?.Trim();
                author.LinkedInUrl = model.LinkedInUrl?.Trim();
            }

            return Edit(author, context);
        }
    }
}