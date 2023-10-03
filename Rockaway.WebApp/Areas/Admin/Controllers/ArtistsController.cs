using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Areas.Admin.Controllers {
	[Area("Admin")]
	public class ArtistsController : Controller {
		private readonly RockawayDbContext context;

		public ArtistsController(RockawayDbContext context) {
			this.context = context;
		}

		// GET: Admin/Artists
		public async Task<IActionResult> Index() {
			return context.Artists != null ?
						View(await context.Artists.ToListAsync()) :
						Problem("Entity set 'RockawayDbContext.Artists'  is null.");
		}

		// GET: Admin/Artists/Details/5
		public async Task<IActionResult> Details(Guid? id) {
			if (id == null || context.Artists == null) {
				return NotFound();
			}

			var artist = await context.Artists
				.FirstOrDefaultAsync(m => m.Id == id);
			if (artist == null) {
				return NotFound();
			}

			return View(artist);
		}

		// GET: Admin/Artists/Create
		public IActionResult Create() {
			return View();
		}

		// POST: Admin/Artists/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Description,Slug")] Artist artist) {
			if (ModelState.IsValid) {
				artist.Id = Guid.NewGuid();
				context.Add(artist);
				await context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(artist);
		}

		// GET: Admin/Artists/Edit/5
		public async Task<IActionResult> Edit(Guid? id) {
			if (id == null || context.Artists == null) {
				return NotFound();
			}

			var artist = await context.Artists.FindAsync(id);
			if (artist == null) {
				return NotFound();
			}
			return View(artist);
		}

		// POST: Admin/Artists/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Slug")] Artist artist) {
			if (id != artist.Id) {
				return NotFound();
			}

			if (ModelState.IsValid) {
				try {
					context.Update(artist);
					await context.SaveChangesAsync();
				} catch (DbUpdateConcurrencyException) {
					if (!ArtistExists(artist.Id)) {
						return NotFound();
					} else {
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(artist);
		}

		// GET: Admin/Artists/Delete/5
		public async Task<IActionResult> Delete(Guid? id) {
			if (id == null || context.Artists == null) {
				return NotFound();
			}

			var artist = await context.Artists
				.FirstOrDefaultAsync(m => m.Id == id);
			if (artist == null) {
				return NotFound();
			}

			return View(artist);
		}

		// POST: Admin/Artists/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id) {
			if (context.Artists == null) {
				return Problem("Entity set 'RockawayDbContext.Artists'  is null.");
			}
			var artist = await context.Artists.FindAsync(id);
			if (artist != null) {
				context.Artists.Remove(artist);
			}

			await context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ArtistExists(Guid id) {
			return (context.Artists?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
