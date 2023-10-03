using Microsoft.Data.Sqlite;
using Rockaway.WebApp.Data;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(new StatusReporter());
var conn = new SqliteConnection("Data Source=rockaway;Mode=Memory;Cache=Shared");
await conn.OpenAsync();
builder.Services.AddDbContext<RockawayDbContext>(options => options.UseSqlite(conn));
var app = builder.Build();

if (!app.Environment.IsDevelopment())
	app.UseExceptionHandler("/Error");

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapGet("/status", (StatusReporter r) => r.Report());
app.MapGet("/exception", _ => throw new("ROCKAWAY_TEST_EXCEPTION"));
app.MapRazorPages();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
