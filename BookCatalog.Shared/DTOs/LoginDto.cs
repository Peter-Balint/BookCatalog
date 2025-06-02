

namespace BookCatalog.Shared.DTOs
{
    public record LoginDto
    {
        public required string UserName {  get; set; }
        public required string Password { get; set; }
    }
}
