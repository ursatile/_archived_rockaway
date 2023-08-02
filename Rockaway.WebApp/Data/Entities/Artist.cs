using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace Rockaway.WebApp.Data.Entities;

public class Artist {
	public Guid Id { get; set; }

	[MaxLength(100)]
	public string Name { get; set; } = String.Empty;

	public string Description { get; set; } = String.Empty;
}

public class Venue {
	public Guid Id { get; set; }

	[MaxLength(100)]
	public string Name { get; set; } = String.Empty;

	public string Address { get; set; } = String.Empty;
	public string City { get; set; } = String.Empty;
	public string CultureCode { get; set; } = String.Empty;
	public string CurrencyCode { get; set; } = String.Empty;
	public string Phone { get; set; } = String.Empty;
	public string PostalCode { get; set; } = String.Empty;
	public string Website { get; set; } = String.Empty;
}

public class Show {
	public Guid Id { get; set; }
	public List<Artist> Artists { get; set; } = new();
	public Venue Venue { get; set; } = null!;
	public LocalDate Date { get; set; }
	public LocalTime DoorsOpen { get; set; }
	public LocalTime StartTime { get; set; }
	public LocalTime FinishTime { get; set; }
}

public class TicketType {
	public Guid Id { get; set; }
	public Show Show { get; set; } = null!;
	public string Name { get; set; } = String.Empty;
	public decimal Price { get; set; }
}

public class Ticket {
	public Purchase Purchase { get; set; } = null!;
	public Guid Id { get; set; }
	public TicketType TicketType { get; set; } = null!;
}

public class Purchase {
	public Guid Id { get; set; }
	public string CustomerName { get; set; } = null!;
	public string CustomerEmail { get; set; } = null!;
	public string CustomerPhone { get; set; } = null!;
	public List<Ticket> Tickets { get; set; } = new();
}

