using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.App.Areas.Admin.Controllers
{
	public class DoctorController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
