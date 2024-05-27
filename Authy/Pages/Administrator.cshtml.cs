using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Authy.Pages
{
    [Authorize(Roles = "Administrator")]
    public class AdministratorModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
