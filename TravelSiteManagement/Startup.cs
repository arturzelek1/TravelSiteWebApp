using TravelSiteWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RepositoryUsingEFinMVC.Repository;
using TravelSiteWeb.Services;
using TravelSiteWeb.Models;
using Mapster;
using Elfie.Serialization;
using FluentValidation;

namespace TravelSiteWeb
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TravelContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IClientRepository, ClientRepository>(); // Register IClientRepository with its implementation ClientRepository
            //services.AddScoped<IClientOrderService, ClientOrderService>();
            services.AddScoped(typeof(IPaginatedListService), typeof(PaginatedListService));
            //services.AddScoped<IPaginatedList<Client>, PaginatedList<Client>>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            //services.AddScoped<IPaginatedList<Reservation>, PaginatedList<Reservation>>();
            services.AddControllersWithViews();
            services.AddControllersWithViews();
            //Mapping
            services.AddSingleton<MappingService>();
            var mappingService = services.BuildServiceProvider().GetRequiredService<MappingService>();
            mappingService.ConfigureMapping();
            //Injecting validators
            services.AddScoped<IValidator<Client>, ClientValidator>(); //Client validator
            services.AddScoped<IValidator<Reservation>, ReservationValidator>(); //Reservation validator
            services.AddScoped<IValidator<TravelDestination>, TravelDestinationValidator>(); //TravelDestination validator
        }
    }
}