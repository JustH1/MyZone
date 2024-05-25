using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyZone.Structs;

namespace MyZone.Pages
{
    public class ReportPageModel : PageModel
    {
        public List<reportDayOrder> reportDayOrders = null;

        private MyZoneDbContext db;
        private ILogger<Program> logger;
        public ReportPageModel(MyZoneDbContext db, ILogger<Program> logger)
        {
            this.db = db;
            this.logger = logger;
        }
        public void OnGet()
        {
            reportDayOrder a = db.reportDayOrders.FromSql($"select season, count from first_task_one(first_task_two('users'), 'users')").FirstOrDefault();
        }
    }
}
