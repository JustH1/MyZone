using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using MyZone.Structs;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

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
            try
            {
                List<users>? users = db.users.FromSql($"select * from users where u_email={email}").ToList();
                if (!(users.Count > 1))
                {
                    users user = users.First();
                    if (HashPasswd(passwd) == user.u_passwd)
                    {
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.u_id.ToString()),
                        new Claim(ClaimTypes.Role, user.u_rights)
                    };
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                        return Redirect("/PersonalAccountPage");
                    }
                    else
                    {
                        ErrorMessage = "Incorrect data.";
                        return Unauthorized();
                    }
                }
                logger.LogCritical("The data in the database was not unambiguous.");
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Page();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Page();
            }
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
