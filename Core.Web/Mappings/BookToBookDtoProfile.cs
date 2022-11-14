using AutoMapper;
using Core.Entities.Books;
using Core.Web.DTOs;

namespace Core.Web.Mappings
{
	public class BookToBookDtoProfile : Profile
	{
		public BookToBookDtoProfile()
		{
			CreateMap<Book, BookDto>()
				.ForMember(d => d.Id, m => m.MapFrom(s => s.Id))
				.ForMember(d => d.Name, m => m.MapFrom(s => s.Name))
				.ForMember(d => d.AuthorName, m => m.MapFrom(s => s.AuthorName));
		}
	}
}
