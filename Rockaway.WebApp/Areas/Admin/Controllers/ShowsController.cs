using Microsoft.AspNetCore.Mvc;
using Rockaway.WebApp.Data;

namespace Rockaway.WebApp.Areas.Admin.Controllers {
	[Area("Admin")]
	public class ShowsController : Controller {
		private readonly RockawayDbContext context;

		public ShowsController(RockawayDbContext context) {
			this.context = context;
		}

		// GET: Admin/Shows
		public async Task<IActionResult> Index() {
			return context.Shows != null ?
						View(await context.Shows.ToListAsync()) :
						Problem("Entity set 'RockawayDbContext.Shows'  is null.");
		}

		// GET: Admin/Shows/Details/5
		public async Task<IActionResult> Details(Guid? venueId, DateTime date) {
			if (venueId == null || context.Shows == null) {
				return NotFound();
			}

			var show = await context.Shows
				.FirstOrDefaultAsync(m => m.Venue.Id == venueId && m.Date == date);
			if (show == null) {
				return NotFound();
			}

			return View(show);
		}

		// GET: Admin/Shows/Create
		public IActionResult Create() {
			return View();
		}

		// POST: Admin/Shows/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Show show) {
			if (!ModelState.IsValid) return View(show);
			context.Add(show);
			await context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		[Route("{area}/venues/{venueId}/shows/{date}")]
		public async Task<IActionResult> Edit(Guid? venueId, DateTime date) {
			var show = await context.Shows.FindAsync(venueId, date);
			if (show == default) return NotFound();

			return View(show);
		}

		//// POST: Admin/Shows/Edit/5
		//// To protect from overposting attacks, enable the specific properties you want to bind to.
		//// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Edit(Guid? id, [Bind("Date")] Show show) {
		//	if (id != show.VenueId) {
		//		return NotFound();
		//	}

		//	if (ModelState.IsValid) {
		//		try {
		//			context.Update(show);
		//			await context.SaveChangesAsync();
		//		} catch (DbUpdateConcurrencyException) {
		//			if (!ShowExists(show.VenueId)) {
		//				return NotFound();
		//			} else {
		//				throw;
		//			}
		//		}
		//		return RedirectToAction(nameof(Index));
		//	}
		//	return View(show);
		//}

		//// GET: Admin/Shows/Delete/5
		//public async Task<IActionResult> Delete(Guid? id) {
		//	if (id == null || context.Shows == null) {
		//		return NotFound();
		//	}

		//	var show = await context.Shows
		//		.FirstOrDefaultAsync(m => m.VenueId == id);
		//	if (show == null) {
		//		return NotFound();
		//	}

		//	return View(show);
		//}

		//// POST: Admin/Shows/Delete/5
		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> DeleteConfirmed(Guid? id) {
		//	if (context.Shows == null) {
		//		return Problem("Entity set 'RockawayDbContext.Shows'  is null.");
		//	}
		//	var show = await context.Shows.FindAsync(id);
		//	if (show != null) {
		//		context.Shows.Remove(show);
		//	}

		//	await context.SaveChangesAsync();
		//	return RedirectToAction(nameof(Index));
		//}

		//private bool ShowExists(Guid? id) {
		//	return (context.Shows?.Any(e => e.VenueId == id)).GetValueOrDefault();
		//}
	}
}
