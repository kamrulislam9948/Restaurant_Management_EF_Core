using Microsoft.EntityFrameworkCore;
using Restaurant_Management.HostedServices;
using Restaurant_Management.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RestaurantDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("db")));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ApplyMigrationService>();
builder.Services.AddHostedService<MigrationHostedService>();
var app = builder.Build();

app.UseStaticFiles();
app.MapDefaultControllerRoute();
app.Run();