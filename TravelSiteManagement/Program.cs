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

namespace ProgramSettings
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
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

            /******************************************************************************/
            //Repositories
            builder.Services.AddScoped<IClientRepository, ClientRepository>(); // Register IClientRepository with its implementation ClientRepository
            builder.Services.AddScoped(typeof(IPaginatedListService), typeof(PaginatedListService));
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
            builder.Services.AddScoped<ITravelDestinationRepository, TravelDestinationRepository>();
            builder.Services.AddControllersWithViews();

            /******************************************************************************/

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
            //Later state that to true after good verification email sender
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>() //Add roles
                .AddEntityFrameworkStores<TravelContext>();
            builder.Services.AddControllersWithViews();

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

            // Roles
            using (var scope = app.Services.CreateScope())
            {
                var rolesService = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                
                var roles = new[] { "Admin", "Manager", "User" };

                foreach (var role in roles)
                {
                    if (!await rolesService.RoleExistsAsync(role))
                    {
                        await rolesService.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                string email = "admin@admin.com";
                string password = "Admin1234!";

                if(await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new IdentityUser { UserName = email, Email = email };
                    
                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Admin");
                }
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

        }
    }
}