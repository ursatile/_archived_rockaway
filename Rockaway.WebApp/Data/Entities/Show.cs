namespace Rockaway.WebApp.Data.Entities;

public class Show {

	public object RouteValues => new { VenueId = Venue.Id, Date = Date.ToString("O") };
	public Venue Venue { get; set; } = default!;
	public DateTime Date { get; set; }
	public Artist HeadlineArtist { get; set; } = default!;
	public List<SupportSlot> SupportSlots { get; set; } = new();
	public List<TicketType> TicketTypes { get; set; } = new();

	public IEnumerable<Artist> SupportArtists
		=> SupportSlots.OrderBy(slot => slot.Position).Select(slot => slot.Artist);

	public Show WithSupportArtists(params Artist[] artists) {
		var slots = artists.Select((artist, index) => new SupportSlot { Artist = artist, Position = index, Show = this });
		this.SupportSlots.AddRange(slots);
		return this;
	}

	public Show WithTicketType(Guid id, string name, decimal price, int? quantityAvailable = null) {
		var tt = new TicketType(this, id, name, price, quantityAvailable);
		this.TicketTypes.Add(tt);
		return this;
	}
}