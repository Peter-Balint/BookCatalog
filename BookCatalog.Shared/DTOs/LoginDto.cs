

using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Shared.DTOs
{
    public record LoginDto
    {
        [Required]
        public string UserName { get; set; } = null!;

        [MinLength(6)]
        [Required]
        public string Password { get; set; } = null!;
    }
}
