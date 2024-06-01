using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Authy.Pages.Client
{
    [Authorize(Roles = "Client")]
    public class ClientModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
