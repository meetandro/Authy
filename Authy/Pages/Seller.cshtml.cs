using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Authy.Pages
{
    [Authorize(Roles = "Administrator,Seller")]
    public class SellerModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
