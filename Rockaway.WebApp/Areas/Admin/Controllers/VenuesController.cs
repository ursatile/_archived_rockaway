using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Areas.Admin.Controllers {
	[Area("Admin")]
	public class VenuesController : Controller {
		private readonly RockawayDbContext context;

		public VenuesController(RockawayDbContext context) {
			this.context = context;
		}

		// GET: Admin/Venuess
		public async Task<IActionResult> Index() {
			return context.Venues != null ?
						View(await context.Venues.ToListAsync()) :
						Problem("Entity set 'RockawayDbContext.Venues'  is null.");
		}

		// GET: Admin/Venuess/Details/5
		public async Task<IActionResult> Details(Guid? id) {
			if (id == null || context.Venues == null) {
				return NotFound();
			}

			var venue = await context.Venues
				.FirstOrDefaultAsync(m => m.Id == id);
			if (venue == null) {
				return NotFound();
			}

			return View(venue);
		}

		// GET: Admin/Venuess/Create
		public IActionResult Create() {
			return View();
		}

		// POST: Admin/Venuess/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Address,City,CountryCode,PostalCode,Telephone,WebsiteUrl")] Venue venue) {
			if (ModelState.IsValid) {
				venue.Id = Guid.NewGuid();
				context.Add(venue);
				await context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(venue);
		}

		// GET: Admin/Venuess/Edit/5
		public async Task<IActionResult> Edit(Guid? id) {
			if (id == null || context.Venues == null) {
				return NotFound();
			}

			var venue = await context.Venues.FindAsync(id);
			if (venue == null) {
				return NotFound();
			}
			return View(venue);
		}

		// POST: Admin/Venuess/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Address,City,CountryCode,PostalCode,Telephone,WebsiteUrl")] Venue venue) {
			if (id != venue.Id) {
				return NotFound();
			}

			if (ModelState.IsValid) {
				try {
					context.Update(venue);
					await context.SaveChangesAsync();
				} catch (DbUpdateConcurrencyException) {
					if (!VenueExists(venue.Id)) {
						return NotFound();
					} else {
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(venue);
		}

		// GET: Admin/Venuess/Delete/5
		public async Task<IActionResult> Delete(Guid? id) {
			if (id == null || context.Venues == null) {
				return NotFound();
			}

			var venue = await context.Venues
				.FirstOrDefaultAsync(m => m.Id == id);
			if (venue == null) {
				return NotFound();
			}

			return View(venue);
		}

		// POST: Admin/Venuess/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id) {
			if (context.Venues == null) {
				return Problem("Entity set 'RockawayDbContext.Venues'  is null.");
			}
			var venue = await context.Venues.FindAsync(id);
			if (venue != null) {
				context.Venues.Remove(venue);
			}

			await context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool VenueExists(Guid id) {
			return (context.Venues?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
