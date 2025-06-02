

namespace BookCatalog.Shared.DTOs
{
    public record GenreDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
