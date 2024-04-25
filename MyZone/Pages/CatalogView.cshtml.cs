using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyZone.Structs;

namespace MyZone.Pages
{
    public class CatalogViewModel : PageModel
    {
        private MyZoneDbContext db;
        public string category_id;
        public List<product> products { get; set; }
        public int count { get; set; }

        public CatalogViewModel(MyZoneDbContext db)
        {
            this.db = db;
        }
		public void OnGet(string category_id, int startID)
        {
            this.category_id = category_id;
            var products = db.product.Where(p => p.catalog_id == Convert.ToInt32(category_id)).ToList();
            if (products.Count == 0)
            {
                count = int.MinValue;
			}
            else if (startID + 30 > products.Count)
            {
				count = int.MaxValue;
				this.products = products.GetRange(startID, products.Count);
			}
            else
            {
				this.products = products.GetRange(startID, startID + 30);
				count = startID + 30;
			}
        }
    }
}
