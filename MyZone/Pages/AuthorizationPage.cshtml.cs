using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Security.Claims;

namespace MyZone.Pages
{

    public class AuthorizationPageModel : PageModel
    {
        public string ErrorMessage { get; set; } = string.Empty;

        private MyZoneDbContext db;
        private ILogger<Program> logger;
        public AuthorizationPageModel(MyZoneDbContext db, ILogger<Program> logger)
        {
            this.db = db;
            this.logger = logger;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPost(string email, string passwd) 
        {
            var user = db.users.FromSql($"select u_passwd from users where u_email={email}").ToList();
            if (!(user.Count > 1))
            {
                if (passwd == user.First().u_passwd)
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, email) };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("PersonalAccount");
                }
            }
            logger.LogCritical("The data in the database was not unambiguous.");
            return Unauthorized();
        }
    }
}
