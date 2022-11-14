using AutoMapper;
using Core.Commands.Books;
using Core.Commands.Books.Queries;
using Core.Entities.Books;
using Core.Web.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Core.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : ControllerBase
	{

		private readonly IMediator _mediator;

		public BookController(IMediator mediator)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BookDto>))]
		public async Task<List<BookDto>?> Get([FromServices] IMapper mapper)
		{
			var query = new GetBooksQuery();
			List<Book>? books = await _mediator.Send(query);

			return books?.Select(a => mapper.Map<BookDto>(a)).ToList(); ;
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDto))]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BookDto))]
		public async Task<object> GetAsync(
			[FromServices] IMapper mapper,
			int id)
		{
			var query = new GetBookByIdQuery { BookId = id };
			Book? book = await _mediator.Send(query);

			if(book == null)
			{
				return NotFound("Book not found for the request.");
			}
			return mapper.Map<BookDto>(book);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDto))]
		public async Task<BookDto> Post([FromServices] IMapper mapper, [FromBody] BookDto bookdto, CancellationToken cancellationToken)
		{
			var query = new BookCreateCommand { AuthorName = bookdto.AuthorName, Name = bookdto.Name };
			Book? book = await _mediator.Send(query, cancellationToken);

			return mapper.Map<BookDto>(book);
		}

		////// PUT api/<BookController>
		////[HttpPut("{id}")]
		////public void Put(int id, [FromBody] string value)
		////{
		////}

		////// DELETE api/<BookController>/5
		////[HttpDelete("{id}")]
		////public void Delete(int id)
		////{
		////}
	}
}
