using Microsoft.EntityFrameworkCore;
using Rockaway.WebApp.Data;

var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

const string SQL_CONNECTION_STRING =
	"Server=localhost;Database=rockaway;User Id=rockaway_user;Password=3GX9i0F5YPmsa6;TrustServerCertificate=true;";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RockawayDbContext>(options => options
	.UseLoggerFactory(loggerFactory)
	.UseSqlServer(SQL_CONNECTION_STRING));
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.MapGet("/status", () => "Rockaway.WebApp is online.");
app.MapRazorPages();
app.MapDefaultControllerRoute();
app.Run();
