

namespace BookCatalog.Shared.DTOs
{
    public record AuthorDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
