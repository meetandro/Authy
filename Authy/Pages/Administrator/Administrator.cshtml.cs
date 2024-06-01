using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Authy.Pages.Administrator
{
    [Authorize(Roles = "Administrator")]
    public class AdministratorModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
