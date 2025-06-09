

using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Shared.DTOs
{
    public record RatingDto
    {
        public BookDto Book { get; set; } = null!;
        public UserDto User { get; set; } = null!;

        [Range(1,10)]
        public int Value { get; set; }
    }
}
