using Core.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Core.IntegrationTest.Fixtures
{
    public class Fixture
    {
        static Fixture()
        {
            Configuration = GetConfiguration();
            CreateDatabase();
        }

        private static IConfiguration GetConfiguration()
            => new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        private static void CreateDatabase()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseSqlServer(Configuration["DbExample"])
                .EnableSensitiveDataLogging()
                //.UseInMemoryDatabase(databaseName: "SimpleToDoList")
                .Options;

            new DatabaseContext(options).Database.Migrate();
        }

        protected static IConfiguration Configuration { get; }
    }
}