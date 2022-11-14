using Core.Entities.Books;
using MediatR;

namespace Core.Commands.Books.Queries
{
	public class GetBookByIdQuery : IRequest<Book>
	{
		public int BookId { get; set; }
	}
}
