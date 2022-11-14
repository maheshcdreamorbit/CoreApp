using Core.Commands.Books;
using Core.Data.Context;
using Core.Managers.Books;
using Core.Managers.Extensions;
using Core.Web.Mappings;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.IntegrationTest.Fixtures
{
    public class StartupStub
    {
        static StartupStub()
        {
            Configuration = GetConfiguration();
        }

        protected static IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<DatabaseContext>(
                options => options.UseSqlServer(Configuration["DbExample"]),
                ServiceLifetime.Singleton,
                ServiceLifetime.Singleton);

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DbExample")));
            services.AddManagers();
            services.AddMediatR(typeof(BookCreateCommandHandler).GetTypeInfo().Assembly);
            ////services.AddMediatR(typeof(BookCreateCommandHandler), typeof(GetBookByIdQueryHandler));
            services.AddAutoMapper(typeof(BookToBookDtoProfile));

            services.AddScoped(typeof(BookCreateCommand), typeof(BookCreateCommandHandler));
            services.AddScoped<IBookManager, BookManager>();

            ////services.AddTransient<IToDoListService, ToDoListService>();
            ////services.AddTransient<ITaskService, TaskService>();

            ////services.AddTransient<IToDoListRepository, ToDoListRepository>();
            ////services.AddTransient<ITaskRepository, TaskRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, DatabaseContext dbContext)
        {
            dbContext.Database.Migrate();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Book}");
            });
        }

        private static IConfiguration GetConfiguration()
            => new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    }

}