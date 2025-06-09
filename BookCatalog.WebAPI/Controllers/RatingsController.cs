using AutoMapper;
using BookCatalog.DataAccess.Models;
using BookCatalog.DataAccess.Services;
using BookCatalog.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.WebAPI.Controllers
{
    [ApiController]
    [Route("api/ratings/")]
    [Authorize]
    public class RatingsController :  ControllerBase
    {
        private readonly RatingsService _ratingsService;
        private readonly IMapper _mapper;

        public RatingsController(RatingsService ratingsService, IMapper mapper)
        {
            _ratingsService = ratingsService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{bookId}")]
        public async Task<IActionResult> GetByBookId([FromRoute] int bookId)
        {
            var ratings = await _ratingsService.GetByBookIdAsync(bookId);

            var ratingDtos = _mapper.Map<List<RatingDto>>(ratings);

            return Ok(ratingDtos);
        }

        [HttpPost]
        public async Task<IActionResult> AddRating([FromBody] RatingRequestDto ratingDto)
        {
            Rating rating = _mapper.Map<Rating>(ratingDto);

            await _ratingsService.AddAsync(rating);

            return Created();
        }
    }
}
