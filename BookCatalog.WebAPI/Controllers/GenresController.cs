using AutoMapper;
using BookCatalog.DataAccess.Models;
using BookCatalog.DataAccess.Services;
using BookCatalog.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.WebAPI.Controllers
{
    [ApiController]
    [Route("api/genres/")]
    public class GenresController : ControllerBase
    {
        private readonly GenresService _genresService;
        private readonly IMapper _mapper;

        public GenresController(GenresService genresService, IMapper mapper)
        {
            _genresService = genresService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _genresService.GetAllAsync();

            var genreDtos = _mapper.Map<List<Genre>>(genres);

            return Ok(genreDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre(GenreDto genreDto)
        {
            Genre genre = _mapper.Map<Genre>(genreDto);

            await _genresService.AddAsync(genre);

            return Created();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateGenre([FromRoute] int id, [FromBody] GenreDto genreDto)
        {
            Genre genre = _mapper.Map<Genre>(genreDto);

            await _genresService.UpdateAsync(id, genre);

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteGenre([FromRoute] int id)
        {
            await _genresService.DeleteAsync(id);
            return NoContent();
        }
    }
}
