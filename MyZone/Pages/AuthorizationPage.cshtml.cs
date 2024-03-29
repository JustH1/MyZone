using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyZone.Pages
{
    public class AuthorizationPageModel : PageModel
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public void OnGet()
        {
        }
        public IActionResult OnPost(string login, string passwd) 
        {
            if (true)
            {
                return Redirect("");
            }
            else
            {
                ErrorMessage = "Ошибка";
                return Page();
            }
        }
        public record class NewUser(string name, string phone, string email, string login, string passwd);
        public IActionResult OnPost()
        {
            return Redirect("");
        }
        
    }
}
