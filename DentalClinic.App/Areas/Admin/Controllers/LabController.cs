using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.App.Areas.Admin.Controllers
{
	public class LabController : AdminBaseController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
