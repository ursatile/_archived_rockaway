namespace Rockaway.WebApp.Data.Entities;

public class TicketType {
	public TicketType() { }

	public TicketType(Show show, Guid id, string name, decimal price, int? quantityAvailable = null) {
		Show = show;
		Id = id;
		Name = name;
		Price = price;
		QuantityAvailable = quantityAvailable;
	}

	public Guid Id { get; set; }
	public Show Show { get; set; } = default!;
	public string Name { get; set; } = String.Empty;
	public decimal Price { get; set; }
	public int? QuantityAvailable { get; set; }

}