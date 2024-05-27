using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Authy.Pages
{
    [Authorize(Roles = "Client")]
    public class ClientModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
