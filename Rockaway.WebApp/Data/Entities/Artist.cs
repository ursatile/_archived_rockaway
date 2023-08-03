using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace Rockaway.WebApp.Data.Entities;

public class Artist {
	public Artist() { }

	public Artist(string name, string description) {
		this.Name = name;
		this.Description = description;
	}

	public Guid Id { get; set; }

	[MaxLength(100)]
	public string Name { get; set; } = String.Empty;

	[MaxLength(1000)]
	public string Description { get; set; } = String.Empty;

	public List<Slot> Slots { get; set; } = new();
}

public class Venue {
	public Guid Id { get; set; }

	[MaxLength(100)]
	public string Name { get; set; } = String.Empty;

	[MaxLength(500)]
	public string Address { get; set; } = String.Empty;

	[MaxLength(50)]
	public string City { get; set; } = String.Empty;

	[MaxLength(50)]
	[Unicode(false)]
	public string CultureCode { get; set; } = String.Empty;

	[MaxLength(50)]
	[Unicode(false)]
	public string Phone { get; set; } = String.Empty;

	[MaxLength(50)]
	[Unicode(false)]
	public string PostalCode { get; set; } = String.Empty;

	[MaxLength(50)]
	[Unicode(false)]
	public string Website { get; set; } = String.Empty;
	public List<Show> Shows { get; set; } = new();
}

public class Slot {
	public Guid Id { get; set; }
	public Show Show { get; set; } = null!;
	public Artist Artist { get; set; } = null!;
	public Slot() { }

	public Slot(Artist artist, Show show) {
		this.Artist = artist;
		this.Show = show;
	}
}

public class Show {
	public Guid Id { get; set; }
	public List<Slot> Slots { get; set; } = new();
	public List<TicketType> TicketTypes { get; set; } = new();
	public List<Ticket> Tickets { get; set; } = new();
	public Venue Venue { get; set; } = null!;
	public LocalDate Date { get; set; }
	public LocalTime DoorsOpen { get; set; }
	public LocalTime StartTime { get; set; }
	public LocalTime FinishTime { get; set; }

	public void AddArtists(params Artist[] artists)
		=> Slots.AddRange(artists.Select(a => new Slot(a, this)));

	public TicketType AddTicketType(string name, decimal price, int? salesLimit = null) {
		var tt = new TicketType { Name = name, Price = price, Show = this, SalesLimit = salesLimit };
		this.TicketTypes.Add(tt);
		return tt;
	}
}

public class TicketType {
	public Guid Id { get; set; }
	public Show Show { get; set; } = null!;
	public string Name { get; set; } = String.Empty;

	// The world's least valuable currency is the Iranian rial
	// 100,000 GBP - a reasonable upper limit for a concert ticket?
	// equals 5 billion Iranian rials. (5_000_000_000)
	// In many Arabic countries - Jordan, Bahrain, Iraq, Libya -
	// the local currency is the dinar, which is divided into 1000,
	// not into 100, so requires 4 decimal places to store
	// 1 digit more than we need.
	// So we need to be able to store 5_000_000_000 units (10 digits) and 4 decimals places
	// which translates to a precision of 14 (10 + 4), and a scale of 4
	[Precision(14, 4)]
	public decimal Price { get; set; }

	public int? SalesLimit { get; set; } = null;
}

public class Ticket {
	public Purchase Purchase { get; set; } = null!;
	public Guid Id { get; set; }
	public TicketType TicketType { get; set; } = null!;
}

public class Purchase {
	public Guid Id { get; set; }
	[MaxLength(100)]
	public string CustomerName { get; set; } = null!;
	[MaxLength(100)]
	public string CustomerEmail { get; set; } = null!;
	[MaxLength(100)]
	[Unicode(false)]
	public string CustomerPhone { get; set; } = null!;
	public List<Ticket> Tickets { get; set; } = new();
}