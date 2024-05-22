using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyZone.Structs;

namespace MyZone.Pages
{
    public class ProductPageModel : PageModel
    {
        public product? product { get; set; }

        private MyZoneDbContext db;
        private ILogger<Program> logger;
        public ProductPageModel(MyZoneDbContext db)
        {
            this.db = db;
        }
        public IActionResult OnGet(int productId)
        {
            try
            {
                product = db.product.FirstOrDefault(p => p.pr_id == productId);

                if (product != null)
                {
                    return Page();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
