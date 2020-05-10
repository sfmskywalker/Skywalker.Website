using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Modules;
using OrchardCore.Users;
using OrchardCore.Users.Models;
using Skywalker.OrchardCore.ContentExtensions.ViewModels;

namespace Skywalker.OrchardCore.ContentExtensions.Controllers
{
    [RequireFeatures("Skywalker.OrchardCore.ContentExtensions.Author")]
    [Route("author")]
    public class AuthorController : Controller
    {
        private readonly IUserStore<IUser> _userStore;

        public AuthorController(IUserStore<IUser> userStore)
        {
            _userStore = userStore;
        }
        
        [HttpGet("{userName}")]
        public async Task<IActionResult> Index(string userName, CancellationToken cancellationToken)
        {
            var user = (User)await _userStore.FindByNameAsync(userName, cancellationToken);

            if (user == null)
                return NotFound();

            var authorViewModel = new AuthorViewModel(user);
            return View(authorViewModel);
        }
    }
}