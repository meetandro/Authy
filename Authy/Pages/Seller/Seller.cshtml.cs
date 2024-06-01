using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Authy.Pages.Seller
{
    [Authorize(Roles = "Administrator,Seller")]
    public class SellerModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
