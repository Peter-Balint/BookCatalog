

using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Shared.DTOs
{
    public record AuthorDto
    {
        public int Id { get; set; }

        [MaxLength(255)]
        [MinLength(1)]
        public string Name { get; set; } = string.Empty;
    }
}
