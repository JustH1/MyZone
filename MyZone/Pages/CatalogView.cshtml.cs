using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyZone.Structs;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyZone.Pages
{
    public class CatalogViewModel : PageModel
    {
        private MyZoneDbContext db;
        public string handler;
        public string requiredID;
        public List<product> products { get; set; }
        public int count { get; set; }

        public CatalogViewModel(MyZoneDbContext db)
        {
            this.db = db;
        }

		public IActionResult OnGetByCategory(string requiredID, int startID)
        {
            handler = "ByCategory";

            this.requiredID = requiredID;
            
            var products = db.product.Where(p => p.catalog_id.ToString() == requiredID).ToList();

            GetCurrentProductsList(products, startID);

            return Page();
        }
        /// <summary>
        /// A function for displaying products in the catalog in portions (10 items each).
        /// </summary>
        /// <param name="products"></param>
        /// <param name="startID"></param>
        private void GetCurrentProductsList(List<product> products, int startID)
        {
            if (products.Count == 0)
            {
                count = int.MinValue;
            }
            else if (startID + 10 > products.Count)
            {
                count = int.MaxValue;
                this.products = products.GetRange(startID, products.Count);
            }
            else
            {
                this.products = products.GetRange(startID, startID + 10);
                count = startID + 10;
            }
        }
    }
}
