using Authy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Authy.Pages
{
    [Authorize]
    public class UserModel(UserManager<ApplicationUser> userManager) : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;

        public void OnGet()
        {
            var task = _userManager.GetUserAsync(User);
            task.Wait();
            ApplicationUser = task.Result;
        }

        public ApplicationUser? ApplicationUser { get; set; }
    }
}
