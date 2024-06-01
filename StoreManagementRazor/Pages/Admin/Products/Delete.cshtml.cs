using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreManagementRazor.Services;

namespace StoreManagementRazor.Pages.Admin.Products
{
    public class DeleteModel(IProductsService productsService) : PageModel
    {
        private readonly IProductsService _productsService = productsService;

        public void OnGet(int? id)
        {
            if (id is null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            _productsService.DeleteProduct((int)id);

            Response.Redirect("/Admin/Products/Index");
        }
    }
}
