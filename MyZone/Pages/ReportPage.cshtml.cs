using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyZone.Structs;

namespace MyZone.Pages
{
    public class ReportPageModel : PageModel
    {
        public List<reportDayOrder> reportDayOrders = null;

        private readonly MyZoneDbContext db;
        private readonly ILogger<Program> logger;
        public ReportPageModel(MyZoneDbContext db, ILogger<Program> logger)
        {
            this.db = db;
            this.logger = logger;
        }
        public void OnGet() 
        {

            var Report = db.reportDayOrders.FromSqlRaw("select dow, count from get_report_day_order();").ToList();

            if (Report.Count == 7)
            {
                reportDayOrders = Report;
            }
            else
            {
                reportDayOrders = new List<reportDayOrder>(6);
                for (int i = 0; i < 7; i++) 
                {
                    bool check = false;
                    foreach (var item in Report)
                    {
                        if (item.dow == i && check == false)
                        {
                            reportDayOrders.Add(item);
                            check = true;
                        }
                    }
                    if (check == true) { check = false; }
                    else { reportDayOrders.Add(new reportDayOrder { dow = i, count = 0 }); }
                }
            }
        }
    }
}
