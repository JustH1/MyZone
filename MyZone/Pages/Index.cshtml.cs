using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyZone.Structs;

namespace MyZone.Pages
{
    /// <summary>
    /// Страница для отображения каталога
    /// </summary>
    public class IndexPage : PageModel
    {
        private MyZoneDbContext db;
        public List<catalogs> catalog;

        public List<catalogs> Catalog { get; set; }
        public IndexPage(MyZoneDbContext db)
        {
            this.db = db;
        }
        public void OnGet() 
        {
            catalog = db.catalog.FromSql($"SELECT * FROM catalog").ToList();
        }
    }
}
