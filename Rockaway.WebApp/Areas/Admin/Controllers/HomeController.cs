using Microsoft.AspNetCore.Mvc;

namespace Rockaway.WebApp.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller {
	public IActionResult Index() {
		return View();
	}
}
