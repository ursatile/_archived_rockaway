using System;
using Rockaway.WebApp.Data.Sample;

namespace Rockaway.WebApp.Data;

public class RockawayDbContext : DbContext, IDatabaseServerInfo {

	public RockawayDbContext(DbContextOptions<RockawayDbContext> options) : base(options) { }
	public DbSet<Artist> Artists { get; set; } = null!;
	public DbSet<Venue> Venues { get; set; } = null!;
	public DbSet<Show> Shows { get; set; } = null!;
	public DbSet<TicketOrder> TicketOrders { get; set; } = null!;
	public DbSet<Ticket> Tickets { get; set; } = null!;
	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);
		// Override EF Core's default table naming (which pluralizes entity names)
		// and use the same names as the C# classes instead
		modelBuilder.Model.GetEntityTypes().ToList().ForEach(e => e.SetTableName(e.DisplayName()));

		modelBuilder.Entity<TicketOrder>(entity => {
			// Store enums in the database as their string name
			// (InProgress, Cancelled) rather than numeric value (1,2,3)
			entity.Property(order => order.Status).HasConversion<string>();
		});

		modelBuilder.Entity<TicketType>(entity => {
			entity.Property(tt => tt.Price).HasColumnType("money");
		});

		modelBuilder.Entity<Show>(entity => {
			entity.HasMany(show => show.SupportSlots).WithOne(slot => slot.Show).OnDelete(DeleteBehavior.Cascade);
			entity.HasMany(show => show.TicketTypes).WithOne(ticketType => ticketType.Show).OnDelete(DeleteBehavior.Cascade);
			entity.HasKey(
				nameof(Show.Venue) + nameof(Show.Venue.Id),
				nameof(Show.Date)
			);
		});

		modelBuilder.Entity<Artist>(entity => {
			entity.HasMany(artist => artist.HeadlineShows).WithOne(show => show.HeadlineArtist).OnDelete(DeleteBehavior.Restrict);
			entity.HasMany(artist => artist.SupportSlots).WithOne(slot => slot.Artist).OnDelete(DeleteBehavior.Restrict);
		});

		modelBuilder.Entity<Ticket>(entity => {
			entity.HasOne(item => item.Order).WithMany(order => order.Tickets).OnDelete(DeleteBehavior.Cascade);
			entity.HasOne(item => item.TicketType).WithMany().OnDelete(DeleteBehavior.Restrict);
		});

		modelBuilder.Entity<SupportSlot>(entity => {
			entity.HasOne(slot => slot.Artist).WithMany(artist => artist.SupportSlots);
			entity.HasKey(
				nameof(SupportSlot.Position),
				nameof(SupportSlot.Show) + nameof(SupportSlot.Show.Date),
				nameof(SupportSlot.Show) + nameof(SupportSlot.Show.Venue) + nameof(SupportSlot.Show.Venue.Id));
		});

		SampleData.Populate(modelBuilder);

	}

	private string DbVersionExpression {
		get {
			if (Database.IsSqlServer()) return "@@VERSION";
			if (Database.IsSqlite()) return ("'SQLite ' || sqlite_version()");
			throw new("Unsupported database provider");
		}
	}

	// A helper property which we'll use to test the connection and return the server version.
	public string ServerVersion
		=> Database.SqlQueryRaw<string>($"SELECT {DbVersionExpression} as Value").Single();

}