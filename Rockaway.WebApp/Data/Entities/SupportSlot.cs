namespace Rockaway.WebApp.Data.Entities;

public class SupportSlot {
	public int Position { get; set; }
	public Artist Artist { get; set; } = default!;
	public Show Show { get; set; } = default!;
}