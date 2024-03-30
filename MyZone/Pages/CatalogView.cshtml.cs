using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyZone.Structs;

namespace MyZone.Pages
{
    public class CatalogViewModel : PageModel
    {
        public List<product> products { get; set; }
        public void OnGet(string category, int StartID)
        {
            if (category != null)
            {
                //запрос и заполнение в бд
            }
            else
            {
            }
        }
    }
}
