

namespace BookCatalog.Shared.DTOs
{
    public record RatingDto
    {
        public required BookDto Book { get; set; }
        public required string UserName { get; set; }
        public int Value { get; set; }
    }
}
