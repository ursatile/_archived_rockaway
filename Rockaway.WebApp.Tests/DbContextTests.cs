using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rockaway.WebApp.Data;
using Rockaway.WebApp.Data.Entities;
using Rockaway.WebApp.Data.Sample;
using Shouldly;

namespace Rockaway.WebApp.Tests {
	public class DbContextTests : IDisposable {
		const string SQLITE_CONNECTION_STRING = $"Data Source=:memory:";
		private readonly SqliteConnection sqliteConnection = new(SQLITE_CONNECTION_STRING);

		private async Task<RockawayDbContext> Connect() {
			await sqliteConnection.OpenAsync();
			var options = new DbContextOptionsBuilder<RockawayDbContext>()
				.UseSqlite(sqliteConnection)
				.Options;
			var db = new RockawayDbContext(options);
			await db.Database.EnsureCreatedAsync();
			return db;
		}

		[Fact]
		public async void DbContext_Finds_Shows() {
			var db = await Connect();
			var dl = SampleData.Artists.DevLeppard;
			var artist = db.Artists.Include(a => a.HeadlineShows).Single(a => a.Id == dl.Id);
			artist.HeadlineShows.Count.ShouldBe(dl.HeadlineShows.Count);
		}

		[Fact]
		public async void Show_Always_Includes_Venue_And_Headliner() {
			var db = await Connect();
			var show = db.Shows.First();
			show.Venue.ShouldNotBe(null);
			show.HeadlineArtist.ShouldNotBe(null);
		}

		[Fact]
		public async void DbContext_Finds_SupportSlots() {
			var db = await Connect();
			var dl = SampleData.Artists.DevLeppard;
			var artist = db.Artists.Include(a => a.HeadlineShows)
				.ThenInclude(show => show.SupportSlots)
				.ThenInclude(slot => slot.Artist)
				.Single(a => a.Id == dl.Id);
			foreach (var show in artist.HeadlineShows) show.SupportArtists.ShouldNotBeEmpty();
		}

		public void Dispose() {
			sqliteConnection.Close();
			sqliteConnection.Dispose();
		}
	}
}
