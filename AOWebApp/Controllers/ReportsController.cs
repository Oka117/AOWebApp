using AOWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AOWebApp.Controllers
{
    public class ReportsController : Controller
    {
        private readonly AmazonOrders2025Context _context;
        public ReportsController(AmazonOrders2025Context context)
        {
            _context = context;
        
        }
        public IActionResult Index()
        {
            var yearList = _context.CustomerOrders
                .Select(co => co.OrderDate.Year)
                .Distinct()
                .OrderByDescending(co => co)
                .ToList();

            return View("AnnualSalesReport", new SelectList(yearList));
        }

        [Produces("application/json")]
        public IActionResult AnnualSalesReportData(int Year)
        {
            //Year = System.DateTime.Now.Year;
            if (Year > 0)
            {
                var orderSummary = _context.ItemsInOrders
                    .Where(iio => iio.OrderNumberNavigation.OrderDate.Year == Year)
                    .GroupBy(iio => new
                    {
                        iio.OrderNumberNavigation.OrderDate.Year,
                        iio.OrderNumberNavigation.OrderDate.Month
                    })
                    .Select(group => new
                    {
                        year = group.Key.Year,
                        monthNo = group.Key.Month,

                        // monthName FROM SQL projection (in the same LINQ-to-Entities query)
                        monthName = System.Globalization.CultureInfo
                                        .CurrentCulture
                                        .DateTimeFormat
                                        .GetMonthName(group.Key.Month),

                        totalItems = group.Sum(iio => iio.NumberOf),
                        totalSales = group.Sum(iio => iio.TotalItemCost)
                    })
                    .OrderBy(data => data.monthNo);

                // OR: compute monthName AFTER SQL (on the in-memory result)
                // var summary = orderSummary.Select(os => new
                // {
                //     os.year,
                //     os.monthNo,
                //     monthName = System.Globalization.CultureInfo
                //                     .CurrentCulture
                //                     .DateTimeFormat
                //                     .GetMonthName(os.monthNo),
                //     os.totalItems,
                //     os.totalSales
                // });

                return Json(orderSummary);
            }
            else
            {
                return BadRequest();
            }
        }


    }
}
