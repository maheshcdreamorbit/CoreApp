using Core.Entities.Books;
using Core.Managers.Books;
using MediatR;

namespace Core.Commands.Books.Queries
{
	public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<Book>>
	{
		private readonly IBookManager _bookManager;

		public GetBooksQueryHandler(IBookManager bookManager)
		{
			_bookManager = bookManager ?? throw new ArgumentNullException(nameof(bookManager));
		}

		public async Task<List<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			List<Book> book = await _bookManager.GetAllBooks().ConfigureAwait(false);

			return book;
		}
	}
}
