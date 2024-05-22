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

        public Dictionary<order, List<order_product>> dicOrders = null;

        private MyZoneDbContext db;
        private ILogger<Program> logger;
        public ShopPageModel(MyZoneDbContext db, ILogger<Program> logger)
        {
            this.db = db;
            this.logger = logger;
        }
        //Pulls up the store's orders and products from the database
        public async Task<IActionResult> OnGet()
        {
            IIdentity? userIdentity = HttpContext.User.Identity;

            if (String.IsNullOrWhiteSpace(RouteData.Values["shopID"].ToString())) { return StatusCode(404); }

            shop = db.shop.FirstOrDefault(p => p.sh_id.ToString() == RouteData.Values["shopID"]);

            if (shop != null)
            {
                if (ÑheckingSeller(userIdentity, shop))
                {
                    GetProductsAndOrders();
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
        //Request to change the order status
        public async Task<IActionResult> OnPostChangeStatus(string newStatus, string orderID)
        {
            try
            {
                IIdentity? userIdentity = HttpContext.User.Identity;

                shop = db.shop.FirstOrDefault(p => p.sh_id.ToString() == RouteData.Values["shopID"]);

                if (shop != null)
                {
                    if (ÑheckingSeller(userIdentity, shop))
                    {
                        GetProductsAndOrders();
                        order? order = db.order.FirstOrDefault(p => p.o_id.ToString() == orderID);

                        if (order != null)
                        {
                            switch (newStatus)
                            {
                                case "sentSeller":
                                    order.o_status = "Îòïðàâëåíî ïðîäàâöó";
                                    break;
                                case "inAssembly":
                                    order.o_status = "Â ñáîðêå";
                                    break;
                                case "onWay":
                                    order.o_status = "Â ïóòè";
                                    break;
                                case "atPickUpPoint":
                                    order.o_status = "Îæèäàåò â ïóíêòå âûäà÷è";
                                    break;
                                case "":
                                    break;
                            }
                            await db.SaveChangesAsync();
                        }
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
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
        //I just took the code out of OnGet
        private void GetProductsAndOrders()
        {
            products = db.product.Where(p => p.pr_shop_id.ToString() == RouteData.Values["shopID"]).ToList();
            List<order>? orders = db.order.Where(p => p.o_sh_id.ToString() == RouteData.Values["shopID"]).ToList();
            if (orders.Count != 0 && orders != null)
            {
                dicOrders = new Dictionary<order, List<order_product>>();
                orders.ForEach(order =>
                {
                    dicOrders.Add(order, db.order_product.Where(p => p.o_id == order.o_id).ToList());
                });
            }
        }
        //Checking on the seller
        private bool ÑheckingSeller(IIdentity? userIdentity, shop shop)
        {
            if (userIdentity != null && userIdentity.IsAuthenticated && shop.sh_u_id.ToString() == userIdentity.Name)
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
