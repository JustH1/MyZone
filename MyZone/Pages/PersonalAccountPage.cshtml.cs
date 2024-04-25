using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyZone.Structs;
using System.Security.Principal;

namespace MyZone.Pages
{
    public class PersonalAccountPageModel : PageModel
    {
        private MyZoneDbContext db;

        public bool check { get; set; }
        public users? user { get; set; }
        public List<order> orders { get; set; } 
        
        public PersonalAccountPageModel(MyZoneDbContext db)
        {
            this.db = db;
        }
        public void OnGet()
        {
            IIdentity? userIdentity = HttpContext.User.Identity;
            if (userIdentity != null && userIdentity.IsAuthenticated)
            {
                check = true;
                GetData(Convert.ToInt32(userIdentity.Name), db);
            }
            else
            {
                check = false;
            }
        }
        private void GetData(int userId, MyZoneDbContext db)
        {
            user = db.users.FromSql($"SELECT * FROM users WHERE u_id={userId}").First();
            orders = db.order.FromSql($"SELECT * FROM \"order\" WHERE u_id={userId}").ToList();
        }
    }
}
