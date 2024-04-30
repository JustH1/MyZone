using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyZone.Structs;
using System.Security.Principal;

namespace MyZone.Pages
{
    public class ShopPageModel : PageModel
    {
        public bool check { get; set; }
        public shop? shop { get; set; } = null;
        public List<product> products { get; set; } = null;
        public List<order> orders { get; set; } = null;

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
            shop = db.shop.FirstOrDefault(p => p.sh_id.ToString() == shopID);

            if (shop != null)
            {
                if (ÑheckingSeller(userIdentity, ref shopID, shop))
                {
                    products = db.product.Where(p => p.pr_shop_id.ToString() == shopID).ToList();
                    orders = db.order.Where(p => p..ToString() == );
                    return Page();

                }
                else
                {
                    return StatusCode(404);
                }
            }
            else
            {
                return StatusCode(404);
            }
        }
        private bool ÑheckingSeller(IIdentity userIdentity, ref string shopID, shop shop)
        {
            if (userIdentity != null && userIdentity.IsAuthenticated && shop.u_id.ToString() == userIdentity.Name)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
