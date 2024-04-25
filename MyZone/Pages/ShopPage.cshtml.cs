using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyZone.Structs;
using System.Security.Principal;

namespace MyZone.Pages
{
    public class ShopPageModel : PageModel
    {
        public bool check { get; set; }
        public shop shop { get; set; }
        public List<product> products { get; set; }

        private MyZoneDbContext db;
        private ILogger<Program> logger;
        public ShopPageModel(MyZoneDbContext db, ILogger<Program> logger)
        {
            this.db = db;
            this.logger = logger;
        }
        public async Task<IActionResult> OnGet(string shopID)
        {
            IIdentity? userIdentity = HttpContext.User.Identity;

            if (userIdentity != null && userIdentity.IsAuthenticated &&
                db.shop.Where(p => p.u_id.ToString() == userIdentity.Name).First() != null)
            {
                shop = db.shop.FirstOrDefault(p => p.sh_id.ToString() == shopID);

                if (shop == null)
                {
                    return StatusCode(404);
                }

                products = db.product.Where(p => p.pr_shop_id.ToString() == shopID).ToList();

                return Page();
                
            }
            else
            {
                return StatusCode(404);
            }
        }
        
    }
}
