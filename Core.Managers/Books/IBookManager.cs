using Core.Entities.Books;

namespace Core.Managers.Books
{
	public interface IBookManager
	{
		Task<Book> InsertBook(Book book);
		Task<Book> GetBookById(int bookId);
		Task<List<Book>> GetAllBooks();
	}
}
