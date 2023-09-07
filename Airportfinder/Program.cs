using Airportfinder;
using Airportfinder.Models;
using Microsoft.EntityFrameworkCore;
using Airportfinder.Services;
using Airportfinder.RepositoryPattern;
using Airportfinder.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var cons = builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
builder.Services.AddDbContext<DatabaseContext>(x => x.UseSqlServer(cons.ToString()));


builder.Services.AddTransient<IRepository<AirportInfo>, Repository<AirportInfo>>();
builder.Services.AddTransient<IRepository<CityInfo>, Repository<CityInfo>>();
builder.Services.AddTransient<IRepository<StateImg>, Repository<StateImg>>();

builder.Services.AddScoped<IAirportInfo, AirportInfoService>();
builder.Services.AddScoped<ICityInfo, CityInfoService>();
builder.Services.AddScoped<IStateImg, StateImgService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Airport}/{action=Index}/{id?}");

app.Run();
