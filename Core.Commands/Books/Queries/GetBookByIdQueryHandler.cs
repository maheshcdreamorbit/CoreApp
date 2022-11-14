using Core.Entities.Books;
using Core.Managers.Books;
using MediatR;

namespace Core.Commands.Books.Queries
{
	public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
	{
		private readonly IBookManager _bookManager;

		public GetBookByIdQueryHandler(IBookManager bookManager)
		{
			_bookManager = bookManager ?? throw new ArgumentNullException(nameof(bookManager));
		}

		public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			Book book = await _bookManager.GetBookById(request.BookId).ConfigureAwait(false);

			return book;
		}
	}
}
