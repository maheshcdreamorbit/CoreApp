using Core.Managers.Books;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Managers.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddScoped<IBookManager, BookManager>();

            return services;
        }
    }
}
