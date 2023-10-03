namespace Rockaway.WebApp.Data.Sample;

public static class SeedDataHelpers {
	public static object ToSeedData(this Show show) => new {
		VenueId = show.Venue.Id,
		show.Date,
		HeadlineArtistId = show.HeadlineArtist.Id,
	};

	public static object ToSeedData(this SupportSlot slot) => new {
		ShowVenueId = slot.Show.Venue.Id,
		ShowDate = slot.Show.Date,
		slot.Position,
		ArtistId = slot.Artist.Id
	};

	public static object ToSeedData(this TicketType tt) => new {
		tt.Id,
		ShowVenueId = tt.Show.Venue.Id,
		ShowDate = tt.Show.Date,
		tt.Price,
		tt.QuantityAvailable,
		tt.Name
	};

	public static object ToSeedData(this Venue venue) => new {
		venue.Id,
		venue.Name,
		venue.Address,
		venue.City,
		venue.PostalCode,
		venue.CountryCode,
		venue.Telephone,
		venue.WebsiteUrl
	};

	public static object ToSeedData(this Artist artist) => new {
		artist.Id,
		artist.Name,
		artist.Description,
		artist.Slug
	};
}
