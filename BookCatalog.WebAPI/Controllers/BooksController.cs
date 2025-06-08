using AutoMapper;
using BookCatalog.DataAccess.Models;
using BookCatalog.DataAccess.Services;
using BookCatalog.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.WebAPI.Controllers
{
    [ApiController]
    [Route("api/books/")]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _booksService;
        private readonly IMapper _mapper;

        public BooksController(BooksService booksService, IMapper mapper)
        {
            _booksService = booksService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _booksService.GetAllAsync();

            var bookDto = _mapper.Map<List<BookDto>>(books);

            return Ok(bookDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            Book? book = await _booksService.GetByIdAsync(id);
            if (book == null) { return NotFound(); };

            BookDto bookDto = _mapper.Map<BookDto>(book);

            return Ok(bookDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookRequestDto bookDto)
        {
            Book book = _mapper.Map<Book>(bookDto);

            await _booksService.AddAsync(book);

            return Created();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] BookRequestDto bookDto)
        {
            Book book = _mapper.Map<Book>(bookDto);

            await _booksService.UpdateAsync(id,book);

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            await _booksService.DeleteAsync(id);
            return NoContent();
        }
    }
}
