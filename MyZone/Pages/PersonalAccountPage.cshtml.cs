using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyZone.Structs;

namespace MyZone.Pages
{
    public class PersonalAccountPageModel : PageModel
    {
        public bool check { get; set; }
        public users users { get; set; }

        public PersonalAccountPageModel(MyZoneDbContext db)
        {
            var user = HttpContext.User.Identity;
            if (user != null && user.IsAuthenticated)
            {
                check = true;
                users = db.users.FromSql($"SELECT * FROM users WHERE u_email={user.Name}").First();
            }
            else
            {
                check = false;
            }
        }
    }
}
