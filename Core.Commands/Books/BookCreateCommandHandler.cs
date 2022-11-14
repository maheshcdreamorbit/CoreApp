using Core.Entities.Books;
using Core.Managers.Books;
using MediatR;

namespace Core.Commands.Books
{
	public class BookCreateCommandHandler : IRequestHandler<BookCreateCommand, Book?>
	{
		private readonly IBookManager _bookManager;


		public BookCreateCommandHandler(IBookManager bookManager)
		{
			_bookManager = bookManager ?? throw new ArgumentNullException(nameof(bookManager));
		}

		public async Task<Book?> Handle(BookCreateCommand request, CancellationToken cancellationToken)
		{
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

			Book? book = new() { Name = request.Name, AuthorName = request.AuthorName };
			book = await _bookManager.InsertBook(book).ConfigureAwait(false);

			return book;
        }
	}
}
