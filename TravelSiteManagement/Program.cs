using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TravelSiteWeb.Data;
using TravelSiteWeb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TravelSiteWeb.Services;
using TravelSiteWeb.Models;
using RepositoryUsingEFinMVC.Repository;
using FluentValidation;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

var host = CreateHostBuilder(args).Build();

static void CreateDbIfNotExists(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<TravelContext>();
            DBInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}

static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

//Services

//Mapping
builder.Services.AddSingleton<MappingService>();
var mappingService = builder.Services.BuildServiceProvider().GetRequiredService<MappingService>();
mappingService.ConfigureMapping();
builder.Services.AddScoped<IClientRepository, ClientRepository>();


builder.Services.AddScoped<IClientRepository, ClientRepository>(); // Register IClientRepository with its implementation ClientRepository
                                                           //services.AddScoped<IClientOrderService, ClientOrderService>();
builder.Services.AddScoped(typeof(IPaginatedListService), typeof(PaginatedListService));
//services.AddScoped<IPaginatedList<Client>, PaginatedList<Client>>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
//services.AddScoped<IPaginatedList<Reservation>, PaginatedList<Reservation>>();
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews();

//Injecting validators
builder.Services.AddScoped<IValidator<Client>, ClientValidator>(); //Client validator
builder.Services.AddScoped<IValidator<Reservation>, ReservationValidator>(); //Reservation validator
builder.Services.AddScoped<IValidator<TravelDestination>, TravelDestinationValidator>(); //TravelDestination validator
/******************************************************************************/
using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<TravelContext>();
        DBInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}
CreateDbIfNotExists(host);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<TravelContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<TravelContext>();
builder.Services.AddControllersWithViews();
//Do zrobienia
//builder.Services.Configure<ApplicationDbContext>(options =>){
//};
//Do zrobienia
//builder.Services.AddDbContext<TravelConte>

//Settings for Authentication
builder.Services.Configure<IdentityOptions>(options =>
{
    //For password
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;

    //Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    //User settings
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFHGIJKLMNOPQRSTUVWXYZ0123456789_@.";
    options.User.RequireUniqueEmail = true;

});

//Settings for cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});
var app = builder.Build();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//ControllerWithViews
//builder.Services.AddControllersWithViews();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//Authorization and authentication
app.UseAuthorization();
//app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
