using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using PRG.EVA.BlackJack.Models;

//var builder = WebApplication.CreateBuilder(args);

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

//// Voeg services toe aan de container
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// Configureer de HTTP-request pipeline
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//// Voeg specifieke routes toe voor de BlackJack controller
//app.MapControllerRoute(
//    name: "InitGame",
//    pattern: "init/{bet:decimal}",
//    defaults: new { controller = "BlackJack", action = "InitGame" });

//app.MapControllerRoute(
//    name: "PlayGame",
//    pattern: "play/{option}",
//    defaults: new { controller = "BlackJack", action = "Play" });

//app.Run();


var builder = WebApplication.CreateBuilder(args);

// Voeg de HttpClient service toe
builder.Services.AddHttpClient();

// Voeg de databasecontext toe
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

// Voeg services toe aan de container
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configureer de HTTP-request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "InitGame",
    pattern: "init/{bet:decimal}",
    defaults: new { controller = "BlackJack", action = "InitGame" });

app.MapControllerRoute(
    name: "PlayGame",
    pattern: "play/{option}",
    defaults: new { controller = "BlackJack", action = "Play" });

app.MapControllerRoute(
    name: "GameLogs",
    pattern: "/GameLogs",
    defaults: new {controller = "GameLogs", action = "GameLogExists" });

app.MapControllerRoute(
    name: "Card",
    pattern: "/Card",
    defaults: new { controller = "Cards", action = "Index" }); 

app.MapControllerRoute(
    name: "Deck",
    pattern: "/Deck",
    defaults: new { controller = "Decks", action = "Index" });

app.Run();

