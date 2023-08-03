using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data;

var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

const string SQL_CONNECTION_STRING =
	"Server=localhost;Database=rockaway;User Id=rockaway_user;Password=3GX9i0F5YPmsa6;TrustServerCertificate=true;";

var builder = WebApplication.CreateBuilder(args);

var SQLITE = "Data Source=rockaway;Mode=Memory;Cache=Shared";
var conn = new SqliteConnection(SQLITE);
await conn.OpenAsync();

builder.Services.AddDbContext<RockawayDbContext>(options => options
	.UseLoggerFactory(loggerFactory)
	.UseSqlite(SQLITE));
//.UseSqlServer(SQL_CONNECTION_STRING));

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
var app = builder.Build();
using (var scope = app.Services.CreateScope()) {
	var db = scope.ServiceProvider.GetService<RockawayDbContext>();
	lock (db) {
		var created = db.Database.EnsureCreated();
		if (created) {
			var populator = new SampleDataPopulator(db);
			populator.PopulateSampleDataAsync().Wait();
		}
	}
}

app.MapGet("/status", () => "Rockaway.WebApp is online.");
app.MapRazorPages();
app.MapDefaultControllerRoute();
app.Run();
await conn.CloseAsync();