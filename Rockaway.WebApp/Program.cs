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

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetService<RockawayDbContext>()!;
lock (db) db.Database.EnsureCreated();

if (!app.Environment.IsDevelopment())
	app.UseExceptionHandler("/Error");

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapGet("/status", (StatusReporter r) => r.Report());
app.MapGet("/exception", _ => throw new("ROCKAWAY_TEST_EXCEPTION"));
app.MapRazorPages();
app.MapControllerRoute(name: "admin", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();