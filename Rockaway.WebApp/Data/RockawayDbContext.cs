using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Rockaway.WebApp.Data.Entities;
using System.Collections.Generic;

namespace Rockaway.WebApp.Data;

public class RockawayDbContext : DbContext {
	// We must declare a constructor that takes a DbContextOptions<RockawayDbContext>
	// if we want to use Asp.NET to configure our database connection and provider.
	public RockawayDbContext(DbContextOptions<RockawayDbContext> options) : base(options) { }

	public virtual DbSet<Artist> Artists => Set<Artist>();
	public virtual DbSet<Venue> Venues => Set<Venue>();
	public virtual DbSet<Ticket> Tickets => Set<Ticket>();
	public virtual DbSet<TicketType> TicketTypes => Set<TicketType>();
	public virtual DbSet<Purchase> Purchase => Set<Purchase>();
	public virtual DbSet<Show> Shows => Set<Show>();

	protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {
		configurationBuilder.Properties<Instant>().HaveConversion<InstantToDateTimeOffsetConverter>();
		configurationBuilder.Properties<LocalDate>().HaveConversion<LocalDateConverter>();
		configurationBuilder.Properties<LocalTime>().HaveConversion<LocalTimeConverter>();
		configurationBuilder.Properties<LocalDateTime>().HaveConversion<LocalDateTimeConverter>();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder) {

		modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");

		modelBuilder.Entity<Artist>(artist => {
			artist.HasMany(a => a.Slots).WithOne(slot => slot.Artist);
		});

		modelBuilder.Entity<Venue>(venue => {
			venue.HasMany(v => v.Shows).WithOne(s => s.Venue);
		});
	}
}

public class LocalDateConverter : ValueConverter<LocalDate, DateTime> {
	public LocalDateConverter() :
		base(ld => ld.ToDateTimeUnspecified(), dt => LocalDate.FromDateTime(dt)) { }
}

public class LocalTimeConverter : ValueConverter<LocalTime, DateTime> {
	private static readonly LocalDate epoch = new(1, 1, 1);
	public LocalTimeConverter() :
		base(ld => ld.On(epoch).ToDateTimeUnspecified(),
			dt => LocalTime.FromTimeOnly(TimeOnly.FromDateTime(dt))) { }
}

public class LocalDateTimeConverter : ValueConverter<LocalDateTime, DateTime> {
	public LocalDateTimeConverter() :
		base(ld => ld.ToDateTimeUnspecified(), dt => LocalDateTime.FromDateTime(dt)) { }
}

public class InstantToDateTimeOffsetConverter : ValueConverter<Instant, DateTimeOffset> {
	public InstantToDateTimeOffsetConverter() :
		base(i => i.ToDateTimeOffset(), dto => Instant.FromDateTimeOffset(dto)) { }
}