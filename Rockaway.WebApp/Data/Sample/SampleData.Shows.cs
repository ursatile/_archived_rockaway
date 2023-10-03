using System.Net.NetworkInformation;

// ReSharper disable InconsistentNaming

namespace Rockaway.WebApp.Data.Sample;

public partial class SampleData {

	public static class Shows {

		private static int ticketTypeSeed = 1;
		private static Guid NextId => TestGuid(ticketTypeSeed++, 'd');

		public static Show DevLeppard_Astoria_20231201 = Artists.DevLeppard
			.BookShow(Venues.Electric, new(2023, 12, 1))
			.WithSupportArtists(Artists.IronMedian, Artists.JavasCrypt)
			.WithTicketType(NextId, "Standing", 25m)
			.WithTicketType(NextId, "Seated", 20m);

		public static Show DevLeppard_Barracuda_20231202 = Artists.DevLeppard
			.BookShow(Venues.Barracuda, new(2023, 12, 2))
			.WithSupportArtists(Artists.IronMedian, Artists.JavasCrypt)
			.WithTicketType(NextId, "Standing", 25m, 100)
			.WithTicketType(NextId, "Seated", 20m, 150);

		public static Show DevLeppard_Stengade_20231203 = Artists.DevLeppard
			.BookShow(Venues.Stengade, new(2023, 12, 3))
			.WithSupportArtists(Artists.IronMedian, Artists.JavasCrypt)
			.WithTicketType(NextId, "Standing", 350m, 100)
			.WithTicketType(NextId, "Seated", 250m, 150);

		public static Show DevLeppard_JohnDee_20231204 = Artists.DevLeppard
			.BookShow(Venues.JohnDee, new(2023, 12, 4))
			.WithSupportArtists(Artists.IronMedian, Artists.JavasCrypt)
			.WithTicketType(NextId, "Standing", 400m, 100)
			.WithTicketType(NextId, "Seated", 280m, 100);

		public static Show DevLeppard_PubAnchor_20231205 = Artists.DevLeppard
			.BookShow(Venues.PubAnchor, new(2023, 12, 5))
			.WithSupportArtists(Artists.IronMedian, Artists.JavasCrypt)
			.WithTicketType(NextId, "Standing", 270m, 100)
			.WithTicketType(NextId, "Seated", 220m, 150);

		public static Show[] AllShows = {
			DevLeppard_Astoria_20231201,
			DevLeppard_Barracuda_20231202,
			DevLeppard_Stengade_20231203,
			DevLeppard_JohnDee_20231204,
			DevLeppard_PubAnchor_20231205
		};

		public static IEnumerable<object> SeedData = AllShows.Select(show => show.ToSeedData());

	}

	public static class TicketTypes {
		public static IEnumerable<TicketType> AllTicketTypes => Shows.AllShows.SelectMany(show => show.TicketTypes);
		public static IEnumerable<object> SeedData => AllTicketTypes.Select(tt => tt.ToSeedData());
	}

	public static class SupportSlots {
		public static IEnumerable<SupportSlot> AllSupportSlots => Shows.AllShows.SelectMany(show => show.SupportSlots);
		public static IEnumerable<object> SeedData => AllSupportSlots.Select(ss => ss.ToSeedData());
	}
}