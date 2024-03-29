using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyZone.Structs;

namespace MyZone.Pages
{
    public class MainPageModel : PageModel
    {
        public List<Catalog> Catalog { get; set; }
        public void OnGet() { }
    }
}
