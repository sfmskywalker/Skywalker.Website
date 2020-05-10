using OrchardCore.DisplayManagement.Views;
using OrchardCore.Entities;
using OrchardCore.Users.Models;
using Skywalker.OrchardCore.ContentExtensions.Models;

namespace Skywalker.OrchardCore.ContentExtensions.ViewModels
{
    public class AuthorViewModel : ShapeViewModel
    {
        public AuthorViewModel(User user)
        {
            Metadata.Type = "Author";
            User = user;
        }

        public User User { get; }
        public Author Author => User.As<Author>();
    }
}