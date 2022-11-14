using Core.Data.Context;
using Core.Entities.Books;

namespace Core.Managers.Books
{
	public class BookManager : IBookManager
	{
		private readonly DatabaseContext _databaseContext;

		public BookManager(DatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}
		public Task<Book> InsertBook(Book book)
		{
			_databaseContext.Books.Add(book);
			_databaseContext.SaveChanges();

			return Task.FromResult(book);
		}

		public Task<Book> GetBookById(int bookId)
		{
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
			return Task.FromResult(_databaseContext.Books.FirstOrDefault(book => book.Id == bookId));
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
		}

		public Task<List<Book>> GetAllBooks()
		{
			return Task.FromResult(_databaseContext.Books.ToList());
		}
	}
}
