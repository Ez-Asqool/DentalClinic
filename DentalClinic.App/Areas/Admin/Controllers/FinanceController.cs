using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.App.Areas.Admin.Controllers
{
    public class FinanceController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
