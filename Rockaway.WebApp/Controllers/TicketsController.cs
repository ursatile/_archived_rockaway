using Microsoft.AspNetCore.Mvc;

namespace Rockaway.WebApp.Controllers {
	public class TicketsController : Controller {
		public IActionResult Index() {
			return View();
		}
	}
}
