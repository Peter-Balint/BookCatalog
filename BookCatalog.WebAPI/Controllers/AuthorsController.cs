using AutoMapper;
using BookCatalog.DataAccess.Models;
using BookCatalog.DataAccess.Services;
using BookCatalog.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.WebAPI.Controllers
{
    [ApiController]
    [Route("api/authors/")]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorsService _authorsService;
        private readonly IMapper _mapper;

        public AuthorsController(AuthorsService authorsService, IMapper mapper)
        {
            _authorsService = authorsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _authorsService.GetAllAsync();

            var authorDtos = _mapper.Map<List<AuthorDto>>(authors);

            return Ok(authorDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(AuthorDto authorDto)
        {
            Author author = _mapper.Map<Author>(authorDto);

            await _authorsService.AddAsync(author);

            return Created();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAuthor([FromRoute] int id, [FromBody] AuthorDto authorDto)
        {
            Author author = _mapper.Map<Author>(authorDto);

            await _authorsService.UpdateAsync(id, author);

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
        {
            await _authorsService.DeleteAsync(id);
            return NoContent();
        }
    }
}
