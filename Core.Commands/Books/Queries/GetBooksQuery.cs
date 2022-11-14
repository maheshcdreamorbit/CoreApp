using Core.Entities.Books;
using MediatR;

namespace Core.Commands.Books.Queries
{
	public class GetBooksQuery : IRequest<List<Book>>
	{
	}
}
