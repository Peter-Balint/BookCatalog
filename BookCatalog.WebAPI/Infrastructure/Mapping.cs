using AutoMapper;
using BookCatalog.DataAccess.Models;
using BookCatalog.Shared.DTOs;

namespace BookCatalog.WebAPI.Infrastructure
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookRequestDto, Book>(MemberList.Source);

            CreateMap<Author,AuthorDto>();
            CreateMap<AuthorDto, Author>();
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();

            CreateMap<LoginDto, User>(MemberList.Source)
                .ForSourceMember(src => src.Password, opt => opt.DoNotValidate());
            //CreateMap<UserDto, User>();
            CreateMap<User,UserDto>(MemberList.Destination);
        }
    }
}
