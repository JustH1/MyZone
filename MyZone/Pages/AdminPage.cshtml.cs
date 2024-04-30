using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyZone.Structs;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace MyZone.Pages
{
    public class AdminPageModel : PageModel
    {
        private MyZoneDbContext db;
        private ILogger<Program> logger;

        //Dictionary of data for statistics.
        public Dictionary<string, double> statistics { get; set; }

        /// <summary>
        /// Properties for working with data.(View and Edit)
        public users? user { get; set; } = null;
        public order? order { get; set; } = null;
        public reviews? reviews { get; set; } = null;
        public bool isFound { get; set; } = false;
        /// </summary>

        public AdminPageModel(MyZoneDbContext db, ILogger<Program> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            if (CheckAdmin(HttpContext.User.Identity))
            {
                GettingStatistics();
                return Page();
            }           
            return StatusCode(404);
        }

        /// <summary>
        /// Function for getting statistics data. The collection is carried out in the last 7 days.
        /// For User, Order and reviews entities.
        private async void GettingStatistics()
        {
            statistics = new Dictionary<string, double>
            {
                { "usersAll", db.users.Count() },
                { "usersNew", db.users.Where(p => p.u_date_account_creation > DateTime.UtcNow.AddDays(-7) ).Count()},
                { "shopAll", db.shop.Count() },
                { "shopNew", db.shop.Where(p => p.sh_date_creation > DateTime.UtcNow.AddDays(-7) ).Count() },
                { "orderAll", db.order.Count()},
                { "orderNew", db.order.Where(p => p.o_date_creation > DateTime.UtcNow.AddDays(-7) ).Count()}
            };

        }
        /// </summary>

        public async Task<IActionResult> OnPostSearch(int id, string choosingEntity)
        {
            try
            {
                if (CheckAdmin(HttpContext.User.Identity))
                {
                    switch (choosingEntity)
                    {
                        case "user":
                            user = db.users.FirstOrDefault(p => p.u_id == id);
                            if (user != null) { isFound = true; }
                            break;
                        case "order":
                            order = db.order.FirstOrDefault(p => p.o_id == id);
                            if (order != null) { isFound = true; }
                            break;
                        case "reviews":
                            reviews = db.reviews.FirstOrDefault(p => p.r_id == id);
                            if (reviews != null) { isFound = true; }
                            break;
                    }
                    return Page();
                }
                else
                {
                    logger.LogError("Someone tried to make a post request to the admin panel without authorization.");
                    return StatusCode(404);
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex.Message);
                return StatusCode(500);
            }
        }


        //For change User.
        public async Task<IActionResult> OnPostChangeUser(int id, string newName, string newEmail, string newRights, string confirmationPassword)
        {
            try
            {
                if (CheckAdmin(HttpContext.User.Identity, ref confirmationPassword))
                {
                    users users = db.users.FirstOrDefault(p => p.u_id == id);
                    users.u_name = newName;
                    users.u_email = newEmail;
                    users.u_rights = newRights;

                    db.SaveChanges();
                    return Page();
                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex.Message);
                return StatusCode(500);
            }
        }
        //For change Order
        public async Task<IActionResult> OnPostChangeOrder(int id, string newStatus, string confirmationPassword)
        {
            try
            {
                if (CheckAdmin(HttpContext.User.Identity, ref confirmationPassword))
                {
                    order order = db.order.FirstOrDefault(p => p.o_id == id);
                    order.o_status = newStatus;

                    db.SaveChanges();

                    return Page();
                }
                else
                {
                    return StatusCode(404);
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex.Message);
                return StatusCode(500);
            }
        }
        //For change Reviews

        public async Task<IActionResult> OnPostChangeReviews(int id, string newAdvantages, string newDisadvantages, string newComments, string confirmationPassword)
        {
            try
            {
                if (CheckAdmin(HttpContext.User.Identity, ref confirmationPassword))
                {
                    reviews reviews = db.reviews.FirstOrDefault(p => p.r_id == id);
                    reviews.r_advantages = newAdvantages;
                    reviews.r_disadvantages = newDisadvantages;
                    reviews.r_comments = newComments;

                    db.SaveChanges();
                    return Page();
                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex.Message);
                return StatusCode(500);
            }
        }
        private bool CheckAdmin(IIdentity userIdentity, ref string passwd)
        {
            if (userIdentity != null && userIdentity.IsAuthenticated)
            {
                users? _user = db.users.FirstOrDefault(p => p.u_id.ToString() == userIdentity.Name);

                if (_user is not null && _user.u_rights == "admin" && HashPasswd(passwd) == _user.u_passwd)
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckAdmin(IIdentity userIdentity)
        {
            if (userIdentity != null && userIdentity.IsAuthenticated)
            {
                users? _user = db.users.FirstOrDefault(p => p.u_id.ToString() == userIdentity.Name);

                if (_user is not null && _user.u_rights == "admin")
                {
                    return true;
                }
            }
            return false;
        }
        private string HashPasswd(string passwd)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwd));
                string hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hashedPassword;
            }
        }
    }
}
