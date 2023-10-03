namespace Rockaway.WebApp.Data.Entities;

public class TicketOrder {
	public Guid Id { get; set; }
	public Show Show { get; set; } = default!;
	public string CustomerName { get; set; } = String.Empty;
	public string CustomerEmail { get; set; } = String.Empty;
	public List<Ticket> Tickets { get; set; } = new();
	public OrderStatus Status { get; set; } = OrderStatus.InProgress;
}