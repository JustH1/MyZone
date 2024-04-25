using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using MyZone.Structs;
using System.Security.Cryptography;
using System.Text;

namespace MyZone.Pages
{
    public class RegistrationPageModel : PageModel
    {
        public string ErrorMessage { get; set; } = string.Empty;

        private MyZoneDbContext db;
        private ILogger<Program> logger;
        public RegistrationPageModel(MyZoneDbContext db, ILogger<Program> logger)
        {
            this.db = db;
            this.logger = logger;
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPost(string name, string email, string passwd, string repeat_passwd)
        {
            try
            {
                List<users>? users = db.users.FromSql($"select * from users where u_email={email}").ToList();
                if (users.Count == 0)
                {
                    if (passwd == repeat_passwd)
                    {
                        users user = new users()
                        {
                            u_name = name,
                            u_email = email,
                            u_date_account_creation = DateTime.Now.ToString(),
                            u_passwd = HashPasswd(passwd),
                            u_rights = "user"
                        };
                        db.users.Add(user);
                        db.SaveChanges();
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
                        HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        return Content("Passwords are not the same.");
                    }
                }
                else
                {
                    HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return Page();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);                 
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
