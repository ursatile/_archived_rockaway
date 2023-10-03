using Microsoft.AspNetCore.Mvc.Rendering;

namespace Rockaway.WebApp.Areas.Admin.Models {
	public class ShowEditModel {
		public Guid VenueId { get; set; }
		public string VenueName { get; set; } = String.Empty;

		public Guid HeadlineArtistId { get; set; }

		public List<SelectListItem> Artists {
			get;
			set;
		} = new();

		public string Date { get; set; } = String.Empty;

		public ShowEditModel() { }

		public ShowEditModel(Show show) {
			this.VenueId = show.Venue.Id;
			this.VenueName = show.Venue.Name;

		}
	}
}
