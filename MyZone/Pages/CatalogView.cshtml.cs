using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyZone.Structs;

namespace MyZone.Pages
{
    public class CatalogViewModel : PageModel
    {
        public List<Product> products { get; set; }
        public IActionResult OnGet(string category, int StartID)
        {
            if (true)
            {
                //запрос и заполнение в бд
            }
            else
            {
                return StatusCode(404);
            }
        }
    }
}
