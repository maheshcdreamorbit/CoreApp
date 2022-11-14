using Core.Entities.Books;
using MediatR;

namespace Core.Commands.Books
{
	public class BookCreateCommand : IRequest<Book?>
	{
		public string Name { get; set; }
		public string AuthorName { get; set; }
	}
}
