using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreManagementRazor.Services;

namespace StoreManagementRazor.Pages.Admin.Products
{
    public class DeleteModel(IWebHostEnvironment environment, ApplicationDbContext context) : PageModel
    {
        private readonly IWebHostEnvironment _environment = environment;
        private readonly ApplicationDbContext _context = context;

        public void OnGet(int? id)
        {
            if (id is null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product is null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            string imageFullPath = environment.WebRootPath + "/products/" + product.ImageFileName;
            System.IO.File.Delete(imageFullPath);

            context.Products.Remove(product);
            context.SaveChanges();

            Response.Redirect("/Admin/Products/Index");
        }
    }
}
