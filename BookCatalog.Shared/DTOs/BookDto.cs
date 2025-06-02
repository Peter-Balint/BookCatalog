

namespace BookCatalog.Shared.DTOs
{
    public record BookDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Synopsis {  get; set; }
        public DateTime PublishedAt { get; set; }
        public required GenreDto Genre { get; set; } 
        public required AuthorDto Author { get; set; }
    }
}
