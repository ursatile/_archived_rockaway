using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Data.Entities;

const string SQL_CONNECTION_STRING =
	"Server=localhost;Database=rockaway-sandbox;User Id=sa;Password=p@ssw0rd;TrustServerCertificate=True;MultipleActiveResultSets=true";
const bool DELETE_AND_RECREATE_DATABASE = true;
var host = Host
	.CreateDefaultBuilder(args)
	.ConfigureServices(services => {
		services.AddDbContext<RockawayDbContext>(options => options.UseSqlServer(SQL_CONNECTION_STRING), ServiceLifetime.Transient);
	}).Build();

var dbContext = host.Services.GetRequiredService<RockawayDbContext>();
if (DELETE_AND_RECREATE_DATABASE) {
	dbContext.Database.EnsureDeleted();
	dbContext.Database.EnsureCreated();

	var beatles = new Artist { Name = "The Beatles" };
	dbContext.Artists.Add(beatles);
	dbContext.SaveChanges();

	var astoria = new Venue { Name = "London Astoria" };
	dbContext.Venues.Add(astoria);
	dbContext.SaveChanges();

	var show = new Show {
		HeadlineArtist = beatles,
		Venue = astoria,
		Date = new(1964, 4, 30)
	};
	show.SupportSlots.Add(new() {
		Artist = new() { Name = "The Kinks" },
		Show = show,
		Position = 1
	});
	show.SupportSlots.Add(new() {
		Artist = new() { Name = "The Ronettes" },
		Show = show,
		Position = 2
	});
	show.SupportSlots.Add(new() {
		Artist = new() { Name = "The Rolling Stones" },
		Show = show,
		Position = 3
	});

	show.TicketTypes.Add(new() {
		Name = "Standing",
		Price = 25.50m,
		QuantityAvailable = 100,
		Show = show
	});
	show.TicketTypes.Add(new() {
		Name = "Balcony Seated",
		Price = 22.50m,
		QuantityAvailable = 150,
		Show = show
	});

	dbContext.Shows.Add(show);
	dbContext.SaveChanges();

	var order = new TicketOrder {
		Show = show,
		CustomerName = "Susan Calvin",
		CustomerEmail = "susan@example.com"
	};

	order.Tickets.Add(new() {
		Order = order,
		TicketType = show.TicketTypes[0],
	});
	order.Tickets.Add(new() {
		Order = order,
		TicketType = show.TicketTypes[1],
	});

	dbContext.TicketOrders.Add(order);
	dbContext.SaveChanges();
}

var db2 = host.Services.GetRequiredService<RockawayDbContext>();
var o2 = db2.TicketOrders
	.Include(o => o.Tickets).ThenInclude(t => t.TicketType).ThenInclude(tt => tt.Show).ThenInclude(s => s.HeadlineArtist)
	.Include(o => o.Tickets).ThenInclude(t => t.TicketType).ThenInclude(tt => tt.Show).ThenInclude(s => s.Venue)
	.Include(o => o.Tickets).ThenInclude(t => t.TicketType).ThenInclude(tt => tt.Show).ThenInclude(s => s.SupportSlots).ThenInclude(slot => slot.Artist)
	.First();
Console.WriteLine($"Order {o2.Id} for {o2.CustomerName} ({o2.CustomerEmail})");
Console.WriteLine(o2.Tickets[0].TicketType.Show.HeadlineArtist.Name);
Console.WriteLine(o2.Tickets[0].TicketType.Show.Venue.Name);
Console.WriteLine(o2.Tickets[0].TicketType.Name);
foreach (var support in o2.Tickets[0].TicketType.Show.SupportSlots.Select(s => s.Artist.Name)) {
	Console.WriteLine(support);
}