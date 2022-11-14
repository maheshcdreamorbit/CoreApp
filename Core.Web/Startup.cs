using Core.Commands.Books;
using Core.Data.Context;
using Core.Managers.Books;
using Core.Managers.Extensions;
using Core.Web.Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Core.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DbExample")));
            services.AddManagers();
            services.AddMediatR(typeof(BookCreateCommandHandler).GetTypeInfo().Assembly);
			services.AddAutoMapper(typeof(BookToBookDtoProfile));

            services.AddScoped(typeof(BookCreateCommand), typeof(BookCreateCommandHandler));
            services.AddScoped<IBookManager, BookManager>();

            // Register swagger.
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Book API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapGet(
                        string.Empty,
                        httpContext =>
                        {
                            httpContext.Response.Redirect("/swagger", false);
                            return Task.CompletedTask;
                        });
                });
        }
    }
}
