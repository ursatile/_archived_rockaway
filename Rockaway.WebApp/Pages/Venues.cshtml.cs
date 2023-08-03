using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Pages;

public class VenuesPageModel : PageModel {
	private readonly RockawayDbContext dbc;

	public VenuesPageModel(RockawayDbContext dbc) {
		this.dbc = dbc;
	}

	public IList<Venue> Venues { get; set; } = new List<Venue>();

	public async Task OnGetAsync() {
		Venues = await dbc.Venues
			.Include(v => v.Shows)
			.ThenInclude(show => show.Slots)
			.ThenInclude(slot => slot.Artist)
			.ToListAsync();
	}
}