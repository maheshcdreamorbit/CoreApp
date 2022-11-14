using Core.Entities.Books;
using Microsoft.EntityFrameworkCore;

namespace Core.Data.Context
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
		{				
		}

		public DbSet<Book> Books { get; set; }
	}
}
