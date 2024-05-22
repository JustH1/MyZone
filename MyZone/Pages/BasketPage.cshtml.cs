using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyZone.Structs;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace MyZone.Pages
{
    public class BasketPageModel : PageModel
    {
        public List<basket> basketList = null;
        public bool checkAuthorization = false;

        private MyZoneDbContext db;
        private ILogger<Program> logger;
        public BasketPageModel(MyZoneDbContext db, ILogger<Program> logger)
        {
            this.db = db;
            this.logger = logger;
        }
        public IActionResult OnGet()
        {
            try
            {
                IIdentity? userIdentity = HttpContext.User.Identity;
                if (userIdentity != null && userIdentity.IsAuthenticated)
                {
                    checkAuthorization = true;
                    basketList = db.basket.Where(p => p.b_u_id.ToString() == userIdentity.Name).ToList();                   
                }
                else
                {
                    checkAuthorization = false;
                }
                return Page();

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        //Creates a new order
        public IActionResult OnGetCreateOrder() 
        {
            try
            {
                IIdentity? userIdentity = HttpContext.User.Identity;
                if (userIdentity != null && userIdentity.IsAuthenticated)
                {
                    List<basket> userBasketForOrder = db.basket.Where(p => p.b_u_id.ToString() == userIdentity.Name).ToList();

                    if (userBasketForOrder.Count != 0 & userBasketForOrder != null)
                    {
                        //Переработать алгоритм
                        var orderData = userBasketForOrder.GroupBy(p => p.b_sh_id);

                        foreach (var keys in orderData)
                        {
                            Guid newGuid = Guid.NewGuid();
                            ulong numericId = BitConverter.ToUInt64(newGuid.ToByteArray(), 0);

                            order newOrder = new order()
                            {
                                o_id = numericId,
                                o_u_id = Convert.ToInt32(userIdentity.Name),
                                o_status = "Отправлено продавцу",
                                o_date_creation = DateTime.UtcNow,
                                o_deli_date = null,
                                o_sh_id = keys.Key
                            };
                            foreach (var value in keys)
                            {
                                newOrder.o_price += value.b_cost_product;
                                db.order_product.Add(new order_product()
                                {
                                    o_id = numericId,
                                    pr_id = value.b_pr_id,
                                    quantity = 1
                                });
                            }
                            db.order.Add(newOrder);
                        }
                        db.basket.Where(p => p.b_u_id.ToString() == userIdentity.Name).ExecuteDelete();
                        db.SaveChanges();
                        return RedirectToPage("PersonalAccountPage");
                    }
                    else
                    {
                        return StatusCode(400);
                    }
                }
                else
                {
                    return StatusCode(401);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
        //Deletes an item from the basket
        public IActionResult OnGetDeletePoint(int pointId)
        {
            try
            {
                IIdentity? userIdentity = HttpContext.User.Identity;
                if (userIdentity != null && userIdentity.IsAuthenticated)
                {
                    db.basket.Where(p => p.id == pointId).ExecuteDelete();
                    return OnGet();
                }
                else
                {
                    return StatusCode(401);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
        //Adds a new item to the cart.
        public IActionResult OnGetAddPoint(int productId, int shopId) 
        {
            try
            {
                IIdentity? userIdentity = HttpContext.User.Identity;
                if (userIdentity != null && userIdentity.IsAuthenticated)
                {
                    product? product = db.product.FirstOrDefault(p => p.pr_id == productId);

                    if (product != null)
                    {
                        basket basket = new basket()
                        {
                            b_u_id = Convert.ToInt32(userIdentity.Name),
                            b_pr_id = productId,
                            b_cost_product = product.pr_price,
                            b_pr_name = product.pr_name,
                            b_sh_id = shopId
                        };
                        db.basket.Add(basket);
                        db.SaveChanges();
                        return OnGet();
                    }
                    else
                    {
                        return NotFound(); 
                    }
                }
                else
                {
                    return StatusCode(401);
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
